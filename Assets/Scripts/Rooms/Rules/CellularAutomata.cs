/**
 * CeullarAutomata.cs
 * Generation rule for a cellular automata algorithm.
 * Derived from BaseRuleset
 */

using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

[System.Serializable]
public class CellularAutomata : BaseRuleset {

	public CellularAutomata(int r, int c) {
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
			if( (x+i) > 0 && (x+i) < r && (y+j) > 0 && (y+j) < c )
				counter += ( map[x+i,y+j] == Tile.OuterWall1 ) ? 1 : 0;
		}

		return counter;
	}

	public override void generateMap() {
		// This is where the cellular automata algorithm begins.
		// Step 1: Fill the map randomly based on MAX walls

		Debug.Log ("Step 1");

		for(int i = 0; i < row; i++)
			for(int j = 0; j < col; j++) 
				map[i,j] = ( Random.Range(0, 100) < 45 ) ? Tile.OuterWall1 : Tile.Floor1;

		// Step 2: (looping step) until iterations complete
		// if (space+adjspaces) > 4 then space -> wall
		// else space -> floor

		Debug.Log ("Step 2");

		for(int x = 0; x < 5; x++)
		{
			Tile[,] newMap = new Tile[row,col];
			for(int i = 0; i < row; i++)
				for(int j = 0; j < col; j++){
				int wallCounter = countWalls(i,j,map,row,col);
				if(map[i,j] == Tile.OuterWall1) {
					if( wallCounter >= 4 )
						newMap[i,j] = Tile.OuterWall1;
					if( wallCounter < 2 )
						newMap[i,j] = Tile.Floor1;
				}
				else
				{
					if( wallCounter >= 5 )
						newMap[i,j] = Tile.OuterWall1;
					else
						newMap[i,j] = Tile.Floor1;
				}
			
			map = newMap;
			}
		}
		
		Debug.Log ("Step 3");

		// Print the map.
		for(int i = 0; i < row; i++)
			for(int j = 0; j < col; j++)
				Debug.Log( (map[i,j] == Tile.Floor1 ) ? "." : "x" );
	}
}
