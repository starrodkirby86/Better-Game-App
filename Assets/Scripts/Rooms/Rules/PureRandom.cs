/**
 * PureRandom.cs
 * Generation rule for a pure randomness algorithm.
 * Derived from BaseRuleset
 */

// TODO:
// REFACTOR TO USE COORD TYPE???
// MAY MAKE OOB COUNTING EASIER

using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

[System.Serializable]
public class PureRandom : BaseRuleset {

	public PureRandom() {
		row = 8;
		col = 8;
		map = new Tile[row,col];
		mapValidFuncs = new MapValidationFunctions();
	}

	public PureRandom(int r, int c) {
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
	
	public override void generateMap() {
		// This is where the pure randomness algorithm begins.
		// Step 1: Fill the map randomly based on MAX walls
		
		Debug.Log ("Step 1");
		
		for(int i = 0; i < row; i++)
			for(int j = 0; j < col; j++) 
				map[i,j].property = ( Random.Range(0, 100) < 50 ) ? TileType.OuterWall1 : TileType.Floor1;
		
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

		// Warp.
		mapValidFuncs.warpPlayer(map);

		// Print the map.
		for(int i = 0; i < row; i++)
			for(int j = 0; j < col; j++)
				Debug.Log( (map[i,j].property == TileType.Floor1 ) ? "." : "x" );
	}

	public override void initializeMap(){
		for(int i = 0; i < row; i++)
			for(int j = 0; j < col; j++) 
				map[i,j] = new Tile();
	}
}
