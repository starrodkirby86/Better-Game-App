/**
 * CeullarAutomata.cs
 * Generation rule for a cellular automata algorithm.
 * Derived from BaseRuleset
 */

// TODO:
// REFACTOR TO USE COORD TYPE???
// MAY MAKE OOB COUNTING EASIER

using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

[System.Serializable]
public class CellularAutomata : BaseRuleset {

	public CellularAutomata() {
		row = 8;
		col = 8;
		map = new Tile[row,col];
		mapValidFuncs = new MapValidationFunctions();
	}

	public CellularAutomata(int r, int c) {
		row = r;
		col = c;
		map = new Tile[row,col];
		mapValidFuncs = new MapValidationFunctions();
	}

	public override void setRowCol(int r, int c) {
		row = r;
		col = c;
		map = new Tile[row,col];
	}

	public int countWalls(int x, int y, Tile[,] map, int r, int c) {
		// Returns the amount of walls that surround (x,y).
		// It must be legal space. (No segmentation faults!)
		int counter = 0;
		for(int i = -1; i < 2; ++i)
			for(int j = -1; j < 2; ++j) {
			// Check if it's not OOB
			if( (x+i) > 0 && (x+i) < r && (y+j) > 0 && (y+j) < c) {
				if(!(i == 0 && j == 0))
					counter += ( map[x+i,y+j].property == TileType.OuterWall1 ) ? 1 : 0;
			}
			else
			{
				// OOB
				counter += 1;
			}
		}

		//Debug.Log (counter);
		return counter;
	}

	public override void generateMap() {
		// This is where the cellular automata algorithm begins.

		// Step 1: Fill the map randomly based on MAX walls

		Debug.Log ("Step 1");

		for(int i = 0; i < row; i++)
			for(int j = 0; j < col; j++) 
				map[i,j].property = ( Random.Range(0, 100) < 40 ) ? TileType.OuterWall1 : TileType.Floor1;

		// Also fill the corner wall tiles as walls
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

		// Step 2: (looping step) until iterations complete
		// if (space+adjspaces) > 4 then space -> wall
		// else space -> floor

		Debug.Log ("Step 2");

		for(int x = 0; x < 4; x++)
		{
			Tile[,] newMap = new Tile[row,col];

			// Initialize tiles
			for(int i = 0; i < row; i++)
				for(int j = 0; j < col; j++) 
					newMap[i,j] = new Tile();

			for(int i = 0; i < row; i++)
				for(int j = 0; j < col; j++){
				int wallCounter = countWalls(i,j,map,row,col);

				// What should the cell do based on the adjacent cells?
				// Use the 4-5 rule
				if( wallCounter > 4 )
					newMap[i,j].property = TileType.OuterWall1;
				else if( wallCounter < 4 )
					newMap[i,j].property = TileType.Floor1;
				else
					newMap[i,j].property = map[i,j].property;

				}
			
			map = newMap;
		}
		
		Debug.Log ("Step 3");

		// New purpose! WARP THE PLAYER to a proposed spot and see if we can accomplish floodfill.
		//mapValidFuncs.warpPlayer (map);

		/*
		// Print the map.
		for(int i = 0; i < row; i++)
			for(int j = 0; j < col; j++)
				Debug.Log( (map[i,j] == Tile.Floor1 ) ? "." : "x" );
		*/
	}

	public override void initializeMap(){
		for(int i = 0; i < row; i++)
			for(int j = 0; j < col; j++) 
				map[i,j] = new Tile();
	}
}
