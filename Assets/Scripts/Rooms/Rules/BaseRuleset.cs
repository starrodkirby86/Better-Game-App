/**
 * BaseRuleset.cs
 * The base class for creating generation rules for the RoomManager.
 * The goal of derivative generation rules from the base ruleset is to
 * create a 2D array of tile enumerators that can be utilized by the RoomManager
 * to create the map into the game.
 * 
 * This is intended to be an abstract class that is called on the fly
 * rather than a Unity class specifically.
 * 
 */

using UnityEngine;
using System.Collections;

[System.Serializable]
public abstract class BaseRuleset {

	// Private values
	public Tile[,] map { get; set; }
	public int row { get; set; }
	public int col { get; set; }

	// Public
	public BaseRuleset() {
		row = 2;
		col = 2;
		map = new Tile[row,col];
	}

	public BaseRuleset(int r, int c) {
		row = r;
		col = c;
		map = new Tile[row,col];
	}

	// Functions
	public abstract void generateMap();
}