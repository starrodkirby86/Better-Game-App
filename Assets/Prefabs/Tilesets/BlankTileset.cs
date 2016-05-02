/**
 * BLANK TILESET:
 * 
 * This is a prefab object that contains
 * the tileset you want to use for the map.
 * Tilesets offer the ease of selecting how each tile is composed
 * and what graphic/blocking should be associated with it.
 * 
 * The current support for the tileset are:
 * -> Floor
 * -> Walls:
 * 		--> Follows the format of the generic school tileset (top and bottom)
 * -> Door
 * -> Stairs
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Tileset : MonoBehaviour {

	// N - NW - W - SW - S - SE - E - NE
	Dictionary<int,GameObject> tileDictionary = new Dictionary<int, GameObject>();
	MapValidationFunctions mvf = new MapValidationFunctions();

	// The floor
	public GameObject[] floorTile;

	// The walls
	public GameObject[] wallTiles;

	// The door
	public GameObject[] doorTile;

	// The stairs
	public GameObject[] stairTiles;

	public virtual void autoTiler(Tile[,] map){}

	/**
	 * Generates a hexadecimal key corresponding its surroundings.
	 * This should yield back the appropriate key that can be used for the tileDictionary,
	 * which can then correspond with placing the appropriate graphic.
	 */
	public int makeKey(Tile[,] map, Coord target) {
		int key = 0;

		// NORTH
		if( mvf.isSolid (map, target.nextCoord (Direction.North)) ) {
			key += 0x80;
			// W AND NW
			if ( mvf.isSolid (map, target.nextCoord (Direction.West)) && mvf.isSolid (map, target.crossCoord (Direction.North, Direction.West)) )
				key += 0x40;
			// E and NE
			if( mvf.isSolid (map, target.nextCoord (Direction.East)) && mvf.isSolid (map, target.crossCoord (Direction.North, Direction.East)) )
				key += 0x01;
		}

		// SOUTH
		if( mvf.isSolid (map, target.nextCoord (Direction.South)) ) {
			key += 0x08;
			// W AND SW
			if ( mvf.isSolid (map, target.nextCoord (Direction.West)) && mvf.isSolid (map, target.crossCoord (Direction.South, Direction.West)) )
				key += 0x10;
			// E and SE
			if( mvf.isSolid (map, target.nextCoord (Direction.East)) && mvf.isSolid (map, target.crossCoord (Direction.South, Direction.East)) )
				key += 0x04;
		}

		// WEST
		if( mvf.isSolid (map, target.nextCoord (Direction.West)) ) {
			key += 0x20;
		}

		// EAST
		if( mvf.isSolid (map, target.nextCoord (Direction.East)) ) {
			key += 0x02;
		}
		return key;
	}

	/**
	 * Fill dictionary.
	 * Be sure to call this upon initialization. Or else the autotiler will fail.
	 */
	public void fillDictionary(){
		// Populate the dictionary with the corresponding keys
			tileDictionary.Add (0x0E,wallTiles[0]);
			tileDictionary.Add (0x3E,wallTiles[1]);
			tileDictionary.Add (0x28,wallTiles[2]);
			tileDictionary.Add (0x08,wallTiles[3]);
			tileDictionary.Add (0xFB,wallTiles[4]);
			tileDictionary.Add (0xEF,wallTiles[5]);
			tileDictionary.Add (0xBE,wallTiles[6]);
			tileDictionary.Add (0xEA,wallTiles[7]);
			tileDictionary.Add (0x8F,wallTiles[8]);
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
			tileDictionary.Add (0xAE,wallTiles[36-1]); // 35
			tileDictionary.Add (0xAB,wallTiles[37-1]); // 30
			// the last two
			// tiles are floors
		}

}
