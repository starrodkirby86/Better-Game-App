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
	public Dictionary<int,GameObject> tileDictionary = new Dictionary<int, GameObject>();
	public MapValidationFunctions mvf = new MapValidationFunctions();

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

		//Debug.Log ("Given coord (" + target.x.ToString () + "," + target.y.ToString() + "), key is " + key.ToString ("X2"));

		return key;
	}

	/**
	 * Fill dictionary.
	 * Be sure to call this upon initialization. Or else the autotiler will fail.
	 */
	public virtual void fillDictionary(){
		}

}
