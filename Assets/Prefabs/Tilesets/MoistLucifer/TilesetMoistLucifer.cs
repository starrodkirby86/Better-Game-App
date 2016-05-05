/**
 * Moist Lucifer Tileset
 * Assign the contents in a prefab.
 * (May not be needed in the end if the prefabbed object takes care of it all.)
 * But the thing is, we probably can store some auxiliary or special information here.
 * I'm keeping it safe here for now though.
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class TilesetMoistLucifer : Tileset {
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

		fillDictionary();

		int rows = map.GetLength (0);
		int columns = map.GetLength (1);
		for(int x = 0; x < rows; x++) {
			for(int y = 0; y < columns; y++) {
				GameObject instantiateMe;

				// Check for wall tile:
				if( map[x,y].property == TileType.OuterWall1 ) {
					// So this is supposedly a wall tile based on what
					// our ruleset has generated. Now it's time to convert
					// that to the proper autotiled item.
					instantiateMe = tileDictionary [ 0xFF ];

					int candidate = makeKey (map, new Coord(x,y));

					if(! tileDictionary.TryGetValue (candidate, out instantiateMe ) ) {
						Debug.Log ("Fringe case at " + candidate.ToString ("X2"));
						instantiateMe = tileDictionary [ 0xFF ];
					}
				}
				else if ( map[x,y].property == TileType.Floor1 ) {
					// We can make this either a regular floor or a
					// broken janky ass floor.
					if(Random.Range (0,100) > 90)
						instantiateMe = floorTile[1]; // Assumes this is broken tile
					else
						instantiateMe = floorTile[0]; // Assumes this is clear good tile
				}
				else if ( map[x,y].property == TileType.Door1 ) {
					instantiateMe = doorTile[0];
				}
				else {
					instantiateMe = tileDictionary[ 0xFF ];
				}

				GameObject instance = Instantiate(instantiateMe, new Vector3 (x, y, 0), Quaternion.identity) as GameObject;
				instance.transform.SetParent(this.transform);
			}
		}
	}

	/**
	 * Let's hope because it's local to here it knows what it's talking about.
	 */
	public override void fillDictionary(){

		// Populate the dictionary with the corresponding keys
		tileDictionary.Add (0x0E,wallTiles[0]);
		tileDictionary.Add (0x3E,wallTiles[1]);
		tileDictionary.Add (0x38,wallTiles[2]);
		tileDictionary.Add (0x08,wallTiles[3]);
		tileDictionary.Add (0xFB,wallTiles[4]);
		tileDictionary.Add (0xEF,wallTiles[5]);
		tileDictionary.Add (0xBE,wallTiles[6]);
		tileDictionary.Add (0xFA,wallTiles[7]);
		tileDictionary.Add (0xA8,wallTiles[8]);
		tileDictionary.Add (0xE8,wallTiles[9]);
		tileDictionary.Add (0x8F,wallTiles[10]);
		tileDictionary.Add (0xFF,wallTiles[11]);
		tileDictionary.Add (0xF8,wallTiles[12]);
		tileDictionary.Add (0x88,wallTiles[13]);
		tileDictionary.Add (0xFE,wallTiles[14]);
		tileDictionary.Add (0xBF,wallTiles[15]);
		tileDictionary.Add (0xAF,wallTiles[16]);
		tileDictionary.Add (0xEB,wallTiles[17]);
		tileDictionary.Add (0x8A,wallTiles[18]);
		tileDictionary.Add (0x8B,wallTiles[19]);
		tileDictionary.Add (0x83,wallTiles[20]);
		tileDictionary.Add (0xE3,wallTiles[21]);
		tileDictionary.Add (0xE0,wallTiles[22]);
		tileDictionary.Add (0x80,wallTiles[23]);
		tileDictionary.Add (0x0A,wallTiles[24]);
		tileDictionary.Add (0x28,wallTiles[25]);
		tileDictionary.Add (0xAE,wallTiles[26]);
		tileDictionary.Add (0xBA,wallTiles[27]);
		tileDictionary.Add (0xAA,wallTiles[28]);
		// Space 29 is a floor, so now we have to go offset
		// For convenience, I'll subtract these if you wanna look at the tileset
		tileDictionary.Add (0x02,wallTiles[30-1]); // 29
		tileDictionary.Add (0x22,wallTiles[31-1]); // 30
		tileDictionary.Add (0x20,wallTiles[32-1]); // 31
		tileDictionary.Add (0x00,wallTiles[33-1]); // 32
		tileDictionary.Add (0x82,wallTiles[34-1]); // 33
		tileDictionary.Add (0xA0,wallTiles[35-1]); // 34
		tileDictionary.Add (0xAB,wallTiles[36-1]); // 35
		tileDictionary.Add (0xEA,wallTiles[37-1]); // 36
		// the last two
		// tiles are floors
		// Missing case tiles -- I'll also subtract according to the dictionary
		tileDictionary.Add (0xBB,wallTiles[40-3]); // 37
		tileDictionary.Add (0xEE,wallTiles[41-3]); // 38
		tileDictionary.Add (0xA2,wallTiles[42-3]); // 39
		tileDictionary.Add (0x8E,wallTiles[43-3]); // 40
		tileDictionary.Add (0xB8,wallTiles[44-3]); // 41
		tileDictionary.Add (0x2E,wallTiles[45-3]); // 42
		tileDictionary.Add (0x3A,wallTiles[46-3]); // 43
		tileDictionary.Add (0x2A,wallTiles[52-8]); // 44
		//tileDictionary.Add (0x8B,wallTiles[53-8]); // 45
		//tileDictionary.Add (0xE8,wallTiles[54-8]); // 46
		tileDictionary.Add (0xA3,wallTiles[55-8]); // 47
		tileDictionary.Add (0xE2,wallTiles[56-8]); // 48
	}
}
