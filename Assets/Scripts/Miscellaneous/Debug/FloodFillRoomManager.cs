using UnityEngine;
using System.Collections;

public class FloodFillRoomManager : RoomManager {

	public GameObject floodStartMarker;
	public int floodStartX;
	public int floodStartY;
	public GameObject floodGoalMarker;
	public int floodGoalX;
	public int floodGoalY;
	
	private TilesetType tilesetToGenerate;
	
	private BaseRuleset selectedRule;
	private RuleManager ruleMan;
	private Tileset selectedTileset;
	private TilesetManager tileMan;
	
	private int candidX;
	private int candidY;

	// SOME PRIVATES HAHAHA
	private Transform boardHolder; // Holds up all the tile objects

	// Use this for initialization
	public override void Start () {
		ruleMan = new RuleManager();
		//tileMan = new TilesetManager();
		
		//selectedTileset = tileMan.getTileset(tilesetToGenerate);
		
		selectedRule = ruleMan.getRule(rulesetToGenerate); // Default upon startup unless...
		selectedRule.setRowCol (rows, columns);
		
		boardSetup ();	

		spawnPoints ();
	
	}

	public override void boardSetup() {
		
		boardHolder = new GameObject ("Board").transform;
		
		// Generate a map from Cellular Automata.
		// CellularAutomata foo = new CellularAutomata(rows, columns);
		//PureRandom foo = new PureRandom(rows, columns);
		GameObject playerChar = GameObject.FindGameObjectWithTag("Player");
		selectedRule.initializeMap ();
		selectedRule.generateMap ();
		Tile[,] mapConvert = selectedRule.map;

		MapValidationFunctions mvf = new MapValidationFunctions();

		Coord startPoint = new Coord(floodStartX,floodStartY); 
		Coord endPoint = new Coord(floodGoalX, floodGoalY);
		
		mvf.FloodFillCheck(selectedRule.map, startPoint, endPoint);

		// This is the autotiler phase.
		//
		// Let's try this method of instantiating the prefab of our choice...
		GameObject tileInstance = Instantiate(tilesetToUse, new Vector3(0,0,0), Quaternion.identity) as GameObject;
		
		selectedTileset = tileInstance.GetComponent<Tileset>();
		selectedTileset.autoTiler( mapConvert ); 

		if(MapValidationFunctions.clearable) Debug.Log ("The goal has been found!");
		else Debug.Log ("I can't find the goal.");
		
		//convertTiles( mapConvert );
		
	}

	public void spawnPoints () {
		Vector3 startPoint = new Vector3(floodStartX,floodStartY,0);
		Vector3 goalPoint = new Vector3(floodGoalX,floodGoalY,0);

		Instantiate (floodStartMarker, startPoint, Quaternion.identity);
		Instantiate (floodGoalMarker, goalPoint, Quaternion.identity);
	}

}
