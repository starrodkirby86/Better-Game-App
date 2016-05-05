using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

/**
 * Like the RuleManager, the TilesetManager acts as a vehicle to easily swap between tilesets,
 * especially through te Unity inspector. It's probably not the most elegant solution, but
 * in this time constraint, it'll do for now.
 */
public class TilesetManager
{
	
	private Dictionary<int,Tileset> tilesetDictionary = new Dictionary<int,Tileset>();
	
	public TilesetManager ()
	{
		fillDatabase ();
	}

	// Be sure this corresponds to TilesetType.
	public void fillDatabase(){
		var counter = 0;
		tilesetDictionary.Add (counter++, new TilesetSchool());
		tilesetDictionary.Add (counter++, new TilesetMoistLucifer());
	}
	
	public Tileset getTileset(TilesetType t) {
		return tilesetDictionary[(int)t];
	}
}

public enum TilesetType {
	School,
	MoistLucifer
};