/**
 * PureRandom.cs
 * Generation rule for a pure randomness algorithm.
 * Derived from BaseRuleset
 */

using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

[System.Serializable]
public class PureRandom : BaseRuleset {
	
	public PureRandom(int r, int c) {
		row = r;
		col = c;
		map = new Tile[row,col];
	}
	
	public override void generateMap() {
		// This is where the pure randomness algorithm begins.
		// Step 1: Fill the map randomly based on MAX walls
		
		Debug.Log ("Step 1");
		
		for(int i = 0; i < row; i++)
			for(int j = 0; j < col; j++) 
				map[i,j] = ( Random.Range(0, 100) < 50 ) ? Tile.OuterWall1 : Tile.Floor1;
		
		// Also fill the corner wall tiles as walls
		for(int i = 0; i < row; i++)
		{
			map[i,0] = Tile.OuterWall1;
			map[i,col-1] = Tile.OuterWall1;
		}
		
		for(int j = 0; j < col; j++)
		{
			map[0,j] = Tile.OuterWall1;
			map[row-1,j] = Tile.OuterWall1;
		}

		// Print the map.
		for(int i = 0; i < row; i++)
			for(int j = 0; j < col; j++)
				Debug.Log( (map[i,j] == Tile.Floor1 ) ? "." : "x" );
	}
}
