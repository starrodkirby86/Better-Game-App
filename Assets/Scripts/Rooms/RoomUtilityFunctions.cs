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



/**
 * Generic functions that help make adding certain
 * tiles when generating algorithms.
 * -> Explode tile
 */
public class TileFunctions {

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

		return ( map[target.x, target.y].property != TileType.Floor1 );
	}

	// TODO:
	// The floodfill check should not be a part of a ruleset, but
	// actually its own unique phase inside board Setup or some other aspect.
	//
	// If we have the time, we should separate these. But for now, prioritize
	// having flood fill work in general first.
	public void warpPlayer(Tile[,] map) {

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
				Rigidbody2D rb2D = playerChar.GetComponent<Rigidbody2D>() as Rigidbody2D;
				rb2D.MovePosition(moveMe);
				return;
			}
			
		}
	}

	public void FloodFillCheck(Tile[,] map, int x, int y, Coord start, Coord stop) {
		// The recursive algorithm. Starting at x and y, traverse down adjacent tiles and mark them if travelled, find the exit from the entrance
			int mapWidth = map.GetLength(0);
			int mapHeight = map.GetLength(1);

			if (map[x,y].mark != 0)
				// Base case. If the current tile is marked, then do nothing.
				return;

				// Change the current tile as marked
				map[x,y].mark = 1;

			if ( stop.isEqual (new Coord(x,y)) ) {
				clearable = true;
				return;
			}
					// Recursive calls. Make a recursive call as long as we are not on the
					// boundary (which would cause an Index Error.)
			if (x > 0) // left
				FloodFillCheck(map, x-1, y, start, stop);

			if (y > 0) // up
				FloodFillCheck(map, x, y-1, start, stop);

			if (x < mapWidth-1) // right
				FloodFillCheck(map, x+1, y, start, stop);

			if (y < mapHeight-1) // down
				FloodFillCheck(map, x, y+1, start, stop);

	}
}