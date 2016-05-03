﻿/**
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
public class MoistLucifer : BaseRuleset {

	public MoistLucifer() {
		row = 8;
		col = 8;
		map = new Tile[row,col];
		mapValidFuncs = new MapValidationFunctions();
	}

	public MoistLucifer(int r, int c) {
		row = 16;
		col = 16;
		map = new Tile[row,col];
		mapValidFuncs = new MapValidationFunctions();
	}

	public override void setRowCol(int r, int c) {
		row = 16;
		col = 16;
		map = new Tile[row,col];
	}

	public override void generateMap() {
		// Hard code.

		// Step 1: Fill the map randomly based on MAX walls

		Debug.Log ("Step 1");

		map[10,8].property = TileType.OuterWall1;
		map[9,8].property = TileType.OuterWall1;
		map[10,9].property = TileType.OuterWall1;
		map[10,7].property = TileType.OuterWall1;

		for(int i = 6; i < 9; i++)
			for(int j = 3; j < 8; j++)
				map[i,j].property = TileType.OuterWall1;

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

		// New purpose! WARP THE PLAYER to a proposed spot and see if we can accomplish floodfill.
		mapValidFuncs.warpPlayer (map);

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
