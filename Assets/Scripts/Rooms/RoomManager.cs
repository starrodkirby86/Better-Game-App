﻿/**
 * RoomManager.cs
 * This handles the procedural generation of the level.
 * There are various implementations that we could approach:
 *
 * Generate a 2D Matrix of Ints.
 * --> This method takes a ruleset heuritsic for a map
 * 	   to generate certain tiles. When this method is
 *     complete, it will return a set of 2D Ints for
 *     the next process.
 *
 * Convert the 2D Matrix into walls/transformations.
 * --> You may wonder why we don't do this all the way.
 *     The main goal is to create our own ASCII or
 *     manual datasets for rooms that we can import into
 *     the room instead.
 *
 * Decide whether to randomize or hard-code foreground sprites.
 * --> How will enemies, items, or things like that be
 *     organized? There should be a master rulebook to
 *     control gaming difficulty.
 *
 * ROOM GENERATION
 * 	Decide what algorithm we want to use. Maybe we want to use...
 * 		-> Growing Tree Algorithm
 * 		-> Cellular Automata
 */


using UnityEngine;
using System;
using System.Collections.Generic;		// To use lists.
using Random = UnityEngine.Random;		// Unity Random


public class RoomManager : MonoBehaviour {

	// SOME HARD VAULES FOR TEMPORARY MEASURE
	public int rows = 8;
	public int columns = 8;
	public Rulesets rulesetToGenerate;
	private TilesetType tilesetToGenerate;

	private BaseRuleset selectedRule;
	private RuleManager ruleMan;
	private Tileset selectedTileset;
	private TilesetManager tileMan;

	private int candidX;
	private int candidY;

	// Sigh
	public GameObject tilesetToUse;

	/*
	// Some dummy tiles
	// TODO: Cleaner implementation of conversion between
	// Tile enumerator and the corresponding tiles.
	public GameObject floorTile;
	public GameObject wallTile;
	public GameObject doorTile;
	public GameObject pillarTile;
	*/

	// ENEMIES
	public int spawnCount;
	public GameObject jiggles;
	public GameObject wiggles;

	// SOME PRIVATES HAHAHA
	private Transform boardHolder; // Holds up all the tile objects

	public virtual void Start() {
		ruleMan = new RuleManager();
		//tileMan = new TilesetManager();

		//selectedTileset = tileMan.getTileset(tilesetToGenerate);

		selectedRule = ruleMan.getRule(rulesetToGenerate); // Default upon startup unless...
		selectedRule.setRowCol (rows, columns);

		boardSetup ();

		// For our dummy case, we can set up some enemy characters
		// So wiggles and jiggles I guess
		dummyEnemySetup();

		Coord playerLocation = MapValidationFunctions.warpPlayer (selectedRule.map);

		pickWarpLocation(playerLocation);

		// And we may as well do a player one too. What the hey.
		//dummyPlayerSetup();
	}

	/*
	// Autotiler will be kind of built upon this.
	public void convertTiles( Tile[,] mapConvert ) {
		for(int x = 0; x < rows; x++) {
			for(int y = 0; y < columns; y++) {
				GameObject instantiateMe;

				// Todo: Change this into a general function that
				// takes in an enum and does a better job with tile conversion.
				if( mapConvert[x,y].property == TileType.OuterWall1 )
					instantiateMe = wallTile;
				else if ( mapConvert[x,y].property == TileType.Floor1 )
					instantiateMe = floorTile;
				else if ( mapConvert[x,y].property == TileType.Door1 )
					instantiateMe = doorTile;
				else
					instantiateMe = pillarTile;
				
				GameObject instance = Instantiate(instantiateMe, new Vector3 (x, y, 0), Quaternion.identity) as GameObject;
				instance.transform.SetParent(boardHolder);
			}
		}
	}
	*/

	public virtual void boardSetup() {

		boardHolder = new GameObject ("Board").transform;

		// Generate a map from Cellular Automata.
		// CellularAutomata foo = new CellularAutomata(rows, columns);
		//PureRandom foo = new PureRandom(rows, columns);
		GameObject playerChar = GameObject.FindGameObjectWithTag("Player");
		selectedRule.initializeMap ();
		selectedRule.generateMap ();
		Tile[,] mapConvert = selectedRule.map;


		// This is the autotiler phase.
		//
		// Let's try this method of instantiating the prefab of our choice...
		GameObject tileInstance = Instantiate(tilesetToUse, new Vector3(0,0,0), Quaternion.identity) as GameObject;

		selectedTileset = tileInstance.GetComponent<Tileset>();
		selectedTileset.autoTiler( mapConvert ); 

		//convertTiles( mapConvert );

	}

	public void dummyEnemySetup(){
		int enemyCount = 0;
		while(enemyCount < spawnCount) {
			candidX = Random.Range(0, rows);
			candidY = Random.Range(0, columns); 
			Tile[,] candidMap = selectedRule.map;
			if(candidMap[candidX,candidY].property == TileType.Floor1) {
				GameObject instantiateMe;
				if(Random.Range (0,2) == 0)
					instantiateMe = wiggles;
				else
					instantiateMe = jiggles;

				GameObject instance = Instantiate (instantiateMe, new Vector3(candidX, candidY, 0), Quaternion.identity) as GameObject;
				enemyCount++;
			}
		}
	}

	public void pickWarpLocation(Coord playerLocation) {
		// GOOD LOC OK
		int counter = 0;
		MapValidationFunctions mvf = new MapValidationFunctions();
		GameObject playerChar = GameObject.FindGameObjectWithTag ("Player");
		bool mercyLength = false;
		int playerX = playerLocation.x;
		int playerY = playerLocation.y;
		int candidX = 0;
		int candidY = 0;
		while(true && counter < 200){

			if(counter > 100 && !mercyLength) {
				Debug.Log ("Lasted too long, give a mercy kill for the distance metric.");
				mercyLength = true;
			}

			do {
				candidX = Random.Range(1, rows-1);
				candidY = Random.Range(1, columns-1);
			} while (candidX == playerX && candidY == playerY);

			Tile[,] candidMap = selectedRule.map;
			MapValidationFunctions.clearMapMark(candidMap);
			Debug.Log ("Proposing point " + candidX.ToString () + " and " + candidY.ToString());
			mvf.FloodFillCheck( candidMap, new Coord(candidX, candidY), new Coord(playerX, playerY));
			if((candidMap[candidX,candidY]).property == TileType.Floor1
			   && MapValidationFunctions.clearable
			   && ( MapValidationFunctions.manhattanDistance( new Coord(candidX, candidY), new Coord(playerX, playerY) ) >= (int)(rows)
			        || MapValidationFunctions.manhattanDistance( new Coord(candidX, candidY), new Coord(playerX, playerY) ) >= (int)(columns)
			   		|| mercyLength)) {
				// We need to move the player to this position.
				Vector3 moveMe = new Vector3(candidX, candidY);
				GameObject warpObj = GameObject.FindGameObjectWithTag("Warp");
				warpObj.transform.position = moveMe;
				Debug.Log ("Found. Warp is at (" + candidX + "," + candidY + ")");
				return;
			}
			Debug.Log ("Failed for " + playerX.ToString () + " and " + playerY.ToString () + " to " + candidX.ToString () + " and " + candidY.ToString());
			counter++;
		}

		// You really don't want to be at this spot.
		Debug.Log ("I give up. Default warp to the midpoint.");
		Vector3 findme = new Vector3(rows/2,columns/2);
		GameObject ok = GameObject.FindGameObjectWithTag("Warp");
		ok.transform.position = findme;
		return;
	}
}
