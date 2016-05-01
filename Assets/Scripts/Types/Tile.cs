/**
 * Tile.cs
 * 
 * Enumerator for the tiles of a map.
 * These tiles should correspond to Prefab tile states.
 * Update this list accordingly.
 * 
 * TODO:
 * RESIZE TILESET SO THAT WALLS AREN'T BAD
 * BECAUSE WE'RE STUCK
 */

using UnityEngine;
using System.Collections;

public enum TileType {
	// --- SET 1 - Dummy
	// -- FLOOR TILES
	Floor1,
	// -- WALL TILES
	OuterWall1,
	// -- PILLAR TILES
	Pillar1,
	// -- DOOR TILES
	Door1,
	// -- STAIR TILES
	Stair1
};

public class Tile{
	public int mark { get; set; }
	public TileType property { get; set; }

	public Tile() {
		mark = 0;
		property = TileType.Floor1;
	}
};