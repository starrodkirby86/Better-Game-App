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

		//if( mvf.isSolid (map, 
		return key;
	}
}
