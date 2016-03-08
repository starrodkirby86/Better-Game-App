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
	public BaseRuleset selectedRule;
	public GameObject floorTile;
	public GameObject wallTile;

	// SOME PRIVATES HAHAHA
	private Transform boardHolder; // Holds up all the tile objects

	public void Start() {
		selectedRule = new PureRandom(rows, columns); // Default upon startup unless...
		BoardSetup ();
	}

	public void BoardSetup() {

		boardHolder = new GameObject ("Board").transform;

		// Generate a map from Cellular Automata.
		// CellularAutomata foo = new CellularAutomata(rows, columns);
		//PureRandom foo = new PureRandom(rows, columns);
		selectedRule.generateMap ();

		Tile[,] mapConvert = selectedRule.map;

		for(int x = 0; x < rows; x++) {
			for(int y = 0; y < columns; y++) {
				GameObject instantiateMe;
				if( mapConvert[x,y] == Tile.OuterWall1 )
					instantiateMe = wallTile;
				else
					instantiateMe = floorTile;

				GameObject instance = Instantiate(instantiateMe, new Vector3 (x, y, 0), Quaternion.identity) as GameObject;
				instance.transform.SetParent(boardHolder);
			}
		}
	}
}
