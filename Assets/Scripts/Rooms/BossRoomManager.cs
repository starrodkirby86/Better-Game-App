/**
 * BossRoomManager.cs
 * This handles the procedural generation of the level.
 * There are various implementations that we could approach:
 *
 * Special Dr. Liu magic.
 * 
 */


using UnityEngine;
using System;
using System.Collections.Generic;		// To use lists.
using Random = UnityEngine.Random;		// Unity Random


public class BossRoomManager : MonoBehaviour {

	// SOME HARD VAULES FOR TEMPORARY MEASURE
	public int rows = 8;
	public int columns = 8;
	public Rulesets rulesetToGenerate;
	private TilesetType tilesetToGenerate;

	public BaseRuleset selectedRule;
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
	public GameObject trueEvil;

	// SOME PRIVATES HAHAHA
	private Transform boardHolder; // Holds up all the tile objects

	public void Start() {
		ruleMan = new RuleManager();
		//tileMan = new TilesetManager();

		//selectedTileset = tileMan.getTileset(tilesetToGenerate);

		selectedRule = ruleMan.getRule(rulesetToGenerate); // Default upon startup unless...
		selectedRule.setRowCol (rows, columns);

		boardSetup ();

		bossEnemySetup();

		pickWarpLocation();

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

	public void boardSetup() {

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

	public void bossEnemySetup(){
		GameObject instance = Instantiate (trueEvil, new Vector3(7, 8, 0), Quaternion.identity) as GameObject;
	}
	

	public void pickWarpLocation() {
		// GOOD LOC OK
		int counter = 0;
		MapValidationFunctions mvf = new MapValidationFunctions();
		GameObject playerChar = GameObject.FindGameObjectWithTag ("Player");
		Vector3 startloc = new Vector3 (7, 30, 0);
		Rigidbody2D rb2D = playerChar.GetComponent<Rigidbody2D>() as Rigidbody2D;
		rb2D.MovePosition(startloc);

		return;
	}
}
