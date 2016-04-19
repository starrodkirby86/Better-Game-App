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

	public SemiRandom(int r, int c) {
		row = r;
		col = c;
		map = new Tile[row,col];
	}

	public override void generateMap() {

		for(int i = 0; i < row; i++)
		{
			map[i, 0] = Tile.OuterWall1;
			map[i, col-1] = Tile.OuterWall1;
		}

		for(int j = 0; j < col; j++)
		{
			map[0, j] = Tile.OuterWall1;
			map[row-1,j] = Tile.OuterWall1;
		}

		for(int i = 1; i < row-1; i++)
			for(int j = 1; j < col-1; j++)
				if (j != 0 || i != 0 || j != (col-1) || i != (row-1)) {
					map[i,j] = ( Random.Range(0, 100) < 85 ) ? Tile.Floor1 : Tile.Pillar1;
				}

		int where; int place;
			where = ( Random.Range(0, 100) < 50 ) ? 0 : 1;
		if (where == 0) {
			place = Random.Range(0, row);
			while (place == 0 || place == row - 1) {
				place = Random.Range(0, row);
			}
			int colwhere = Random.Range(0, 2);
			if (colwhere == 0) {
				map[place, 0] = Tile.Door1;
				map[place, 1] = Tile.Floor1; //override the adjacent non-wall tile next to door into floor tile
			}
			else {
				map[place, col-1] = Tile.Door1;
				map[place, col-2] = Tile.Floor1; //override the adjacent non-wall tile next to door into floor tile
			}
		}
		else {
			place = Random.Range(0, col);
			while (place == 0 || place == col-1) {
				place = Random.Range(0, col);
			}
			int rowwhere = Random.Range(0, 2);
			if (rowwhere == 0) {
				map[0, place] = Tile.Door1;
				map[1, place] = Tile.Floor1; //override the adjacent non-wall tile next to door into floor tile
			}
			else {
				map[row-1, place] = Tile.Door1;
				map[row-2, place] = Tile.Floor1; //override the adjacent non-wall tile next to door into floor tile
			}

		}
	}
}