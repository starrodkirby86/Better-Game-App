/**
 * SemiRandom.cs
 * Algorithm initializes a 2D Matrix then decides if there'll be a stair or a door to clear the room.
 *
 * If stair, randomize and make sure stair is accessible
 * -Set all edges as walls, randomize pillars or tiles. Add a stair randomly,
 * override a random adjacent tile next to stair into a floor tile to ensure the stair is accessible
 * from at least one side
 *
 * If door, randomize, then put door at edge of map
 * -Set all edges as walls, randomize pillars or tiles, then choose a wall tile, replace with door.
 * Override the adjacent non-wall tile next to door into a floor tile to ensure the door is accessible
 */

using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;


[System.Serializable]
public class SemiRandom : BaseRuleset {

	public SemiRandom() {
		row = 8;
		col = 8;
		map = new Tile[row,col];
		mapValidFuncs = new MapValidationFunctions();
	}

	public SemiRandom(int r, int c) {
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
		int exitx;
		int exity;

		for(int i = 0; i < row; i++)
		{
			map[i, 0].property = TileType.OuterWall1;
			map[i, col-1].property = TileType.OuterWall1;
		}

		for(int j = 0; j < col; j++)
		{
			map[0, j].property = TileType.OuterWall1;
			map[row-1,j].property = TileType.OuterWall1;
		}

		for(int i = 1; i < row-1; i++)
			for(int j = 1; j < col-1; j++)
				if (j != 0 || i != 0 || j != (col-1) || i != (row-1)) {
					map[i,j].property = ( Random.Range(0, 100) < 85 ) ? TileType.Floor1 : TileType.Pillar1;
				}

		int doororstair = ( Random.Range(0, 100) < 50 ) ? 0 : 1;

		if (doororstair == 0) {
		int where; int place;
			where = ( Random.Range(0, 100) < 50 ) ? 0 : 1;
		if (where == 0) {
			place = Random.Range(0, row);
			while (place == 0 || place == row-1) {
				place = Random.Range(0, row);
			}
			int colwhere = Random.Range(0, 2);
			if (colwhere == 0) {
					map[place, 0].property = TileType.Door1;
					exitx = place;
					exity = 0;
					map[place, 1].property = TileType.Floor1; //override the adjacent non-wall tile next to door into floor tile
			}
			else {
					map[place, col-1].property = TileType.Door1;
					exitx = place;
					exity = col-1;
					map[place, col-2].property = TileType.Floor1; //override the adjacent non-wall tile next to door into floor tile
			}
		}
		else {
			place = Random.Range(0, col);
			while (place == 0 || place == col-1) {
				place = Random.Range(0, col);
			}
			int rowwhere = Random.Range(0, 2);
			if (rowwhere == 0) {
					map[0, place].property = TileType.Door1;
					exitx = 0;
					exity = place;
					map[1, place].property = TileType.Floor1; //override the adjacent non-wall tile next to door into floor tile
			}
			else {
					map[row-1, place].property = TileType.Door1;
					exitx = row-1;
					exity = place;
					map[row-2, place].property = TileType.Floor1; //override the adjacent non-wall tile next to door into floor tile
			}

		}
		}
		else {
			int i = Random.Range(0, row); //randomizes the row index for the stair
			while (i == 0 || i == row-1) {
				i = Random.Range(0, row);
			}

			int j = Random.Range(0, col); //randomizes the column index for the stair
			while (j == 0 || j == col-1) {
				j = Random.Range(0, col);
			}
			map[i, j].property = TileType.Stair1;
			exitx = i;
			exity = j;
			int space = Random.Range(0, 4);
			while (space > 0 && space <= 4) {
				
				if (space == 1) {
					if (i-1 != 0) {
						map[i-1,j].property = TileType.Floor1;
						break;
					}
					else {
						space = Random.Range(0, 4);
						continue;
					}
				}
				else if (space == 2) {
					if ((i+1) != (row - 1)) {
						map[i+1,j].property = TileType.Floor1;
						break;
					}
					else {
						space = Random.Range(0, 4);
						continue;
					}
				}
				else if (space == 3) {
					if (j-1 != 0) {
						map[i,j-1].property = TileType.Floor1;
						break;
					}
					else {
						space = Random.Range(0, 4);
						continue;
					}
				}
				else if (space == 4) {
					if (j+1 != col - 1) {
						map[i,j+1].property = TileType.Floor1;
						break;
					}
					else {
						space = Random.Range(0, 4);
						continue;
					}

		}
	}
		}

		// Validation portion, propose a shuffled spot and warp the player there.
		mapValidFuncs.warpPlayer (map); 
		GameObject playerChar = GameObject.FindGameObjectWithTag ("Player");
		mapValidFuncs.FloodFillCheck(map, exitx, exity, new Coord(exitx,exity), new Coord((int)playerChar.transform.position.x,(int)playerChar.transform.position.y));
		Debug.Log (mapValidFuncs.clearable);
	}

	public override void initializeMap(){
		for(int i = 0; i < row; i++)
			for(int j = 0; j < col; j++) 
				map[i,j] = new Tile();
	}
}