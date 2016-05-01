/**
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
	private BaseRuleset selectedRule;
	private RuleManager ruleMan;
	private int candidX;
	private int candidY;

	// Some dummy tiles
	// TODO: Cleaner implementation of conversion between
	// Tile enumerator and the corresponding tiles.
	public GameObject floorTile;
	public GameObject wallTile;
	public GameObject doorTile;
	public GameObject pillarTile;

	// ENEMIES
	public GameObject jiggles;
	public GameObject wiggles;

	// SOME PRIVATES HAHAHA
	private Transform boardHolder; // Holds up all the tile objects

	public void Start() {
		ruleMan = new RuleManager();
		selectedRule = ruleMan.getRule(rulesetToGenerate); // Default upon startup unless...
		selectedRule.setRowCol (rows, columns);
		boardSetup ();

		// For our dummy case, we can set up some enemy characters
		// So wiggles and jiggles I guess
		dummyEnemySetup();

		// And we may as well do a player one too. What the hey.
		//dummyPlayerSetup();
	}

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

	public void boardSetup() {

		boardHolder = new GameObject ("Board").transform;

		// Generate a map from Cellular Automata.
		// CellularAutomata foo = new CellularAutomata(rows, columns);
		//PureRandom foo = new PureRandom(rows, columns);
		GameObject playerChar = GameObject.FindGameObjectWithTag("Player");
		selectedRule.initializeMap ();
		selectedRule.generateMap ();
		Tile[,] mapConvert = selectedRule.map;

		convertTiles( mapConvert );

	}

	public void dummyEnemySetup(){
		int enemyCount = 0;
		while(enemyCount < 8) {
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

	public void dummyPlayerSetup(){
		// Simple function to put the player into some available spot in the map.
		while(true){
			int candidX = Random.Range(0, rows);
			int candidY = Random.Range(0, columns); 
			Tile[,] candidMap = selectedRule.map;
			if((candidMap[candidX,candidY]).property == TileType.Floor1) {
				// We need to move the player to this position.
				Vector3 moveMe = new Vector3(candidX, candidY);
				GameObject playerChar = GameObject.FindGameObjectWithTag("Player");
				Rigidbody2D rb2D = playerChar.GetComponent<Rigidbody2D>() as Rigidbody2D;
				rb2D.MovePosition(moveMe);
				return;
			}

		}
	}
}
