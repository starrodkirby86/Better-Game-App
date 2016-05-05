using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

[System.Serializable]
public class BossStage : BaseRuleset {

	public BossStage() {
		row = 8;
		col = 8;
		map = new Tile[row,col];
		mapValidFuncs = new MapValidationFunctions();
	}

	public BossStage(int r, int c) {
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
        // GAZE UPON THE FLYING SPAGHETTI CODE
        // This goes against everything I've been taught, I am sorry
		// Step 1: Fill the map randomly based on MAX walls

		Debug.Log ("Step 1");
        // I
		map[14,1].property = TileType.OuterWall1;
		map[13,1].property = TileType.OuterWall1;
		map[12,1].property = TileType.OuterWall1;
		map[11,1].property = TileType.OuterWall1;
        map[10,1].property = TileType.OuterWall1;
        map[9,1].property = TileType.OuterWall1;
        map[6,1].property = TileType.OuterWall1;
        map[5,1].property = TileType.OuterWall1;
        map[4,1].property = TileType.OuterWall1;
        map[3,1].property = TileType.OuterWall1;
        map[2,1].property = TileType.OuterWall1;
        map[1,1].property = TileType.OuterWall1;

        map[14, 2].property = TileType.OuterWall1;
        map[13, 2].property = TileType.OuterWall1;
        map[12, 2].property = TileType.OuterWall1;
        map[11, 2].property = TileType.OuterWall1;
        map[10, 2].property = TileType.OuterWall1;
        map[9, 2].property = TileType.OuterWall1;
        map[6, 2].property = TileType.OuterWall1;
        map[5, 2].property = TileType.OuterWall1;
        map[4, 2].property = TileType.OuterWall1;
        map[3, 2].property = TileType.OuterWall1;
        map[2, 2].property = TileType.OuterWall1;
        map[1, 2].property = TileType.OuterWall1;

        map[14, 3].property = TileType.OuterWall1;
        map[13, 3].property = TileType.OuterWall1;
        map[12, 3].property = TileType.OuterWall1;
        map[11, 3].property = TileType.OuterWall1;
        map[10, 3].property = TileType.OuterWall1;
        map[9, 3].property = TileType.OuterWall1;
        map[6, 3].property = TileType.OuterWall1;
        map[5, 3].property = TileType.OuterWall1;
        map[4, 3].property = TileType.OuterWall1;
        map[3, 3].property = TileType.OuterWall1;
        map[2, 3].property = TileType.OuterWall1;
        map[1, 3].property = TileType.OuterWall1;

        map[14, 4].property = TileType.OuterWall1;
        map[13, 4].property = TileType.OuterWall1;
        map[12, 4].property = TileType.OuterWall1;
        map[11, 4].property = TileType.OuterWall1;
        map[10, 4].property = TileType.OuterWall1;
        map[9, 4].property = TileType.OuterWall1;
        map[6, 4].property = TileType.OuterWall1;
        map[5, 4].property = TileType.OuterWall1;
        map[4, 4].property = TileType.OuterWall1;
        map[3, 4].property = TileType.OuterWall1;
        map[2, 4].property = TileType.OuterWall1;
        map[1, 4].property = TileType.OuterWall1;

        map[14, 5].property = TileType.OuterWall1;
        map[13, 5].property = TileType.OuterWall1;
        map[12, 5].property = TileType.OuterWall1;
        map[11, 5].property = TileType.OuterWall1;
        map[4, 5].property = TileType.OuterWall1;
        map[3, 5].property = TileType.OuterWall1;
        map[2, 5].property = TileType.OuterWall1;
        map[1, 5].property = TileType.OuterWall1;

        map[14, 6].property = TileType.OuterWall1;
        map[13, 6].property = TileType.OuterWall1;
        map[12, 6].property = TileType.OuterWall1;
        map[11, 6].property = TileType.OuterWall1;
        map[4, 6].property = TileType.OuterWall1;
        map[3, 6].property = TileType.OuterWall1;
        map[2, 6].property = TileType.OuterWall1;
        map[1, 6].property = TileType.OuterWall1;

        map[14, 7].property = TileType.OuterWall1;
        map[13, 7].property = TileType.OuterWall1;
        map[2, 7].property = TileType.OuterWall1;
        map[1, 7].property = TileType.OuterWall1;
        // H

        map[14, 14].property = TileType.OuterWall1;
        map[13, 14].property = TileType.OuterWall1;
        map[12, 14].property = TileType.OuterWall1;
        map[11, 14].property = TileType.OuterWall1;
        map[10, 14].property = TileType.OuterWall1;
        map[9, 14].property = TileType.OuterWall1;
        map[6, 14].property = TileType.OuterWall1;
        map[5, 14].property = TileType.OuterWall1;
        map[4, 14].property = TileType.OuterWall1;
        map[3, 14].property = TileType.OuterWall1;
        map[2, 14].property = TileType.OuterWall1;
        map[1, 14].property = TileType.OuterWall1;

        map[14, 13].property = TileType.OuterWall1;
        map[13, 13].property = TileType.OuterWall1;
        map[2, 13].property = TileType.OuterWall1;
        map[1, 13].property = TileType.OuterWall1;

        map[1, 6].property = TileType.OuterWall1;
        map[1, 7].property = TileType.OuterWall1;
        map[1, 8].property = TileType.OuterWall1;
        map[1, 9].property = TileType.OuterWall1;
        map[1, 10].property = TileType.OuterWall1;
        map[1, 11].property = TileType.OuterWall1;
        map[1, 12].property = TileType.OuterWall1;

        map[14, 6].property = TileType.OuterWall1;
        map[14, 7].property = TileType.OuterWall1;
        map[14, 8].property = TileType.OuterWall1;
        map[14, 9].property = TileType.OuterWall1;
        map[14, 10].property = TileType.OuterWall1;
        map[14, 11].property = TileType.OuterWall1;
        map[14, 12].property = TileType.OuterWall1;

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
