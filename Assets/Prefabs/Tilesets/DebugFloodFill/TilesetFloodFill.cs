/**
 * Flood Fill Tileset
 * Assign the contents in a prefab.
 * (May not be needed in the end if the prefabbed object takes care of it all.)
 * But the thing is, we probably can store some auxiliary or special information here.
 * I'm keeping it safe here for now though.
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class TilesetFloodFill : Tileset {
	// You've been spooked by the spooky skeleton.

	/**
	 * This particular function just does a simple job of converting
	 * wall tiles into a more specific wall-based kind of tile. And not the bush.
	 * Please. Anything but that bush tile.
	 */
	public override void autoTiler(Tile[,] map) {
		// This is where we'll try to implement Colin's autotiling algorithm
		// A 0 == floor
		// A nonzero is a wall

		int rows = map.GetLength (0);
		int columns = map.GetLength (1);
		for(int x = 0; x < rows; x++) {
			for(int y = 0; y < columns; y++) {
				GameObject instantiateMe;

				// Check for wall tile:
				if( map[x,y].property == TileType.OuterWall1 ) {
					// For Flood Fill, we don't need to worry about weird things like that.
					instantiateMe = wallTiles[0];
				}
				else if ( map[x,y].property == TileType.Floor1 ) {
					// If that tile is marked, then we should denote the highlighted tile.
					// Otherwise, we're good.
					if(map[x,y].mark != 0)
						instantiateMe = floorTile[1];
					else
						instantiateMe = floorTile[0];
				}
				else if ( map[x,y].property == TileType.Door1 ) {
					instantiateMe = doorTile[0];
				}
				else {
					instantiateMe = wallTiles[0];
				}

				GameObject instance = Instantiate(instantiateMe, new Vector3 (x, y, 0), Quaternion.identity) as GameObject;
				instance.transform.SetParent(this.transform);
			}
		}
	}
}
