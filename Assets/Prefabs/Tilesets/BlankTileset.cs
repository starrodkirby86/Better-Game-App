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

[System.Serializable]
public abstract class Tileset : MonoBehaviour {

	// The floor
	public GameObject[] floorTile;

	// The walls
	public GameObject[] wallTiles;

	// The door
	public GameObject[] doorTile;

	// The stairs
	public GameObject[] stairTiles;

	public abstract void autoTiler(Tile[,] map);
}
