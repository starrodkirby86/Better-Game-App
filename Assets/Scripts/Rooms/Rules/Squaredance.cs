/**
 * Squaredance.cs
 * Ruleset generating an even set of squares distributed throughout the map,
 * connected in between with lines. This creates a kind of series of corridors
 * effect that hopefully is exciting to play.
 */


using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

[System.Serializable]
public class Squaredance : BaseRuleset {

	public Squaredance() {
		row = 8;
		col = 8;
		map = new Tile[row,col];
	}

	public Squaredance(int r, int c) {
		row = r;
		col = c;
		map = new Tile[row,col];
	}

	public override void setRowCol(int r, int c) {
		row = r;
		col = c;
		map = new Tile[row,col];
	}

	public override void generateMap() {

		TileFunctions.clearMap (map,false);

		// FIrst, we need to guess how many squares we can actually fit
		// on a map like this.

		// Decide on a square width/height.
		int squareLength = (int)((row + col) / 8);

		// Hoe msny squares can we cleanly distribute?
		int proposedAmount = 0;
		int squareCounter = 0;
		while(proposedAmount < Mathf.Min (row, col)) {
			proposedAmount += squareLength + 4;
			squareCounter++;
		}
		// Great! Think of the centerpoints and distribute that evenly.

		var stepCount = 1;

		if(squareCounter != 0)
			stepCount = (int) ( Mathf.Min (row,col) / squareCounter );

		// And now make squares at these points.
		for(int i = stepCount; i < row; i += stepCount) {
			for(int j = stepCount; j < col; j += stepCount) {
				Coord topLeft = new Coord( i-(squareLength/2), j+(squareLength/2) );
				Coord bottomRight = new Coord(  i+(squareLength/2), j-(squareLength/2) );
				Debug.Log ("TopLeft is at " + topLeft.x.ToString() + "," + topLeft.y.ToString ());
				Debug.Log ("BottomRight is at " + bottomRight.x.ToString() + "," + bottomRight.y.ToString ());
				TileFunctions.makeRectangle( map, topLeft, bottomRight , true );
			}
		}

		// And now make connections
		for(int j = 0; j < col; j += stepCount)
			for(int i = 0; i < row; i++)
				map[i,j].property = TileType.Floor1;

		for(int j = 0; j < row; j += stepCount)
			for(int i = 0; i < col; i++)
				map[j, i].property = TileType.Floor1;

		TileFunctions.fillCorners(map);

		return;

	}

	public override void initializeMap(){
		for(int i = 0; i < row; i++)
			for(int j = 0; j < col; j++) 
				map[i,j] = new Tile();
	}
}
