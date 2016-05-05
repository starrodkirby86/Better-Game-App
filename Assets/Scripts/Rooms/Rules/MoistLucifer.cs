/**
 * MoistLucifer.cs
 * Test case.
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
        // I
		map[10,8].property = TileType.OuterWall1;
		map[9,8].property = TileType.OuterWall1;
		map[10,9].property = TileType.OuterWall1;
		map[10,7].property = TileType.OuterWall1;
        map[11, 7].property = TileType.OuterWall1;
        map[11, 8].property = TileType.OuterWall1;
        map[11, 10].property = TileType.OuterWall1;
        map[11, 11].property = TileType.OuterWall1;
        map[10, 10].property = TileType.OuterWall1;
        map[10, 11].property = TileType.OuterWall1;
        map[9, 11].property = TileType.OuterWall1;
        map[9, 7].property = TileType.OuterWall1;
        map[9, 10].property = TileType.OuterWall1;

        // H
        map[4, 7].property = TileType.OuterWall1;
        map[6, 9].property = TileType.OuterWall1;
        map[6, 7].property = TileType.OuterWall1;
        map[6, 8].property = TileType.OuterWall1;
        map[6, 10].property = TileType.OuterWall1;
        map[4, 11].property = TileType.OuterWall1;
        map[4, 10].property = TileType.OuterWall1;
        map[4, 9].property = TileType.OuterWall1;
        map[4, 8].property = TileType.OuterWall1;
        map[5, 9].property = TileType.OuterWall1;
        map[6, 11].property = TileType.OuterWall1;
        map[5, 8].property = TileType.OuterWall1;
        map[6, 12].property = TileType.OuterWall1;
        map[7, 12].property = TileType.OuterWall1;
        map[7, 11].property = TileType.OuterWall1;

        map[9, 3].property = TileType.OuterWall1;
        map[9, 4].property = TileType.OuterWall1;
        //map[9, 2].property = TileType.OuterWall1;
        map[11, 3].property = TileType.OuterWall1;
        map[11, 4].property = TileType.OuterWall1;
        map[11, 2].property = TileType.OuterWall1;
        map[10, 2].property = TileType.OuterWall1;
        map[10, 4].property = TileType.OuterWall1;
        map[9, 5].property = TileType.OuterWall1;
        map[10, 5].property = TileType.OuterWall1;
        //map[11, 5].property = TileType.OuterWall1;


        for (int i = 6; i < 6; i++)
			for(int j = 3; j < 4; j++)
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
