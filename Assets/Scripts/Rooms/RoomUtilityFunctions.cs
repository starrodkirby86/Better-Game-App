/**
 * RoomUtilityFunctions.cs
 * 
 * Sometimes you need a set of generic functions to help you get along your way.
 * This is what this script does. It'll host plenty of generic room generation
 * functions that can be helpful in providing the coder useful information
 * or to make generating something easier.
 */

using System;
using UnityEngine;
using System.Collections.Generic;
using Random = UnityEngine.Random;		// Unity Random
using System.Linq;
using System.Text;



/**
 * Generic functions that help make adding certain
 * tiles when generating algorithms.
 */
public class TileFunctions {


	// For linspace
	public static IEnumerable<double> Arrange(double start, int count)
	{
		return Enumerable.Range((int)start, count).Select(v => (double)v);
	}

	public static IEnumerable<double> LinSpace(double start, double stop, int num, bool endpoint = true)
	{
		var result = new List<double>();
		if (num <= 0)
		{
			return result;
		}
		
		if (endpoint)
		{
			if (num == 1) 
			{
				return new List<double>() { start };
			}
			
			var step = (stop - start)/ ((double)num - 1.0d);
			result = Arrange(0, num).Select(v => (v * step) + start).ToList();
		}
		else 
		{
			var step = (stop - start) / (double)num;
			result = Arrange(0, num).Select(v => (v * step) + start).ToList();
		}
		
		return result;
	}

	// Generate a line from one coordinate to another
	// You can ask for it to be totaly walls or totally floor, based on the bool
	// False: Walls
	// True: Floor
	public static void makeLine(Tile[,] map, Coord begin, Coord end, bool applyFloor) {

		// Assert in range
		if( begin.isOOB (map.GetLength(0), map.GetLength (1), Direction.Stop) ||
		    end.isOOB (map.GetLength(0), map.GetLength (1), Direction.Stop) )
			return;
		
		int startX = begin.x;
		int endX = end.x;
		int startY = begin.y;
		int endY = end.y;


		// Find the linear spacing appropriate from point
		// including the endpoint
		int lengthX = Math.Abs( endX - startX );

		var linspace = new List<Double>();
			linspace = LinSpace (startY, endY, lengthX, true).ToList ();

		// Now it's time to actually put our money where our mouth is
		for(int i = startX; i < endX; i++) {
			int j = (int) linspace[i] ;
			map[i,j].property = applyFloor ? TileType.Floor1 : TileType.OuterWall1;
		}

		// Phew! Thought this one was so easy, didn't cha!?

		return;
	}

	// Generate a rectangle starting from the topleft point to the bottomright point
	// You can ask for it to be totally walls or totally floor, based on the bool
	// False: Walls
	// True: Floor
	public static void makeRectangle(Tile[,] map, Coord topLeft, Coord bottomRight, bool applyFloor) {

		// Assert that topLeft and bottomRight are in range
		if( topLeft.isOOB (map.GetLength(0), map.GetLength (1), Direction.Stop) ||
			bottomRight.isOOB (map.GetLength(0), map.GetLength (1), Direction.Stop) )
			return;

		int startX = topLeft.x;
		int endX = bottomRight.x;
		int startY = bottomRight.y;
		int endY = topLeft.y;

		for(int i = startX; i < endX; i++) {
			for(int j = startY; j < endY; j++) {
				map[i,j].property = applyFloor ? TileType.Floor1 : TileType.OuterWall1;
			}
		}

		return;

	}

	public static void clearMap(Tile[,] map, bool applyFloor) {
		for(int i = 0; i < map.GetLength (0); i++) {
			for(int j = 0; j < map.GetLength (1); j++) {
				map[i,j].property = applyFloor ? TileType.Floor1 : TileType.OuterWall1;
			}
		}
	}

	public static void fillCorners(Tile[,] map) {
		// Also fill the corner wall tiles as walls
		var row = map.GetLength (0);
		var col = map.GetLength (1);

		for(int i = 0; i < row; i++)
		{
			map[i,0].property = TileType.OuterWall1;
			map[i,col-1].property = TileType.OuterWall1;
		}
		
		for(int j = 0; j < col; j++)
		{
			map[0,j].property = TileType.OuterWall1;
			map[row-1,j].property = TileType.OuterWall1;
		}
	}
}

/**
 * Functions that help verify aspects of a map to 
 * help ensure that the map made is a "good" one.
 * LIST OF FUNCS:
 * -> isSolid
 * -> warpPlayer
 * -> FloodfillCheck
 */
public class MapValidationFunctions {
	public Boolean clearable = false;


	/**
	 *  isSolid is a boolean function that checks
	 *  if a tile is on top of a "wall" or is OOB.
	 */
	public bool isSolid(Tile[,] map, Coord target) {
		int rows = map.GetLength (0);
		int columns = map.GetLength (1);

		if(target.isOOB (rows,columns,Direction.Stop)) return true;

		//Debug.Log (map[target.x, target.y].property);


		return ( map[target.x, target.y].property == TileType.OuterWall1 );
	}

	// TODO:
	// The floodfill check should not be a part of a ruleset, but
	// actually its own unique phase inside board Setup or some other aspect.
	//
	// If we have the time, we should separate these. But for now, prioritize
	// having flood fill work in general first.

	// Manhattan Distance.
	// Nice meme.
	public static int manhattanDistance(Coord a, Coord b) {
		return Math.Abs(b.x - a.x) + Math.Abs (b.y - a.y);
	}

	public static Coord warpPlayer(Tile[,] map) {

		int rows = map.GetLength (0);
		int columns = map.GetLength (1);

		while(true){
			int candidX = Random.Range(0, rows);
			int candidY = Random.Range(0, columns); 
			Tile[,] candidMap = map;
			if((candidMap[candidX,candidY]).property == TileType.Floor1) {
				// We need to move the player to this position.
				Vector3 moveMe = new Vector3(candidX, candidY);
				GameObject playerChar = GameObject.FindGameObjectWithTag("Player");
				playerChar.transform.position = moveMe;
				Debug.Log ( "Spawning player at (" + candidX + "," + candidY + ")" ); 
				return( new Coord(candidX, candidY) );
			}
			
		}
	}

	public void FloodFillCheck(Tile[,] map, Coord current, Coord stop) {
		// The recursive algorithm. Starting at x and y, traverse down adjacent tiles and mark them if travelled, find the exit from the entrance
			int mapWidth = map.GetLength(0);
			int mapHeight = map.GetLength(1);

			int x = current.x;
			int y = current.y;

			//Debug.Log (x.ToString() + " and " + y.ToString () );

			if (map[x,y].mark != 0)
				// Base case. If the current tile is marked, then do nothing.
				return;

				// Change the current tile as marked
				map[x,y].mark = 1;

			if ( stop.isEqual (current) ) {
				Debug.Log ("Hit at " + current.x.ToString() + " " + current.y.ToString ());
				clearable = true;
				return;
			}
					// Recursive calls. Make a recursive call as long as we are not on the
					// boundary (which would cause an Index Error.)
			if (x > 0 && !isSolid (map, current.nextCoord (Direction.West)) ) // left
				FloodFillCheck(map, current.nextCoord (Direction.West), stop);

			if (y > 0 && !isSolid (map, current.nextCoord (Direction.South)) ) // up
				FloodFillCheck(map, current.nextCoord (Direction.South), stop);

			if (x < mapWidth-1 && !isSolid (map, current.nextCoord (Direction.East)) ) // right
				FloodFillCheck(map, current.nextCoord (Direction.East), stop);

			if (y < mapHeight-1 && !isSolid (map, current.nextCoord (Direction.North)) ) // down
				FloodFillCheck(map, current.nextCoord (Direction.North), stop);

	}
}