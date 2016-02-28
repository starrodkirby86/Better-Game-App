/**
 * Grid.cs
 * 
 * !! IMPORTANT !!
 * 
 * Contents in here:
 * 
 * Coordinate class that has an (X,Y).
 * 
 * Enumerator for the directions in a map.
 * That way, you can write North, West, South, East
 * and stuff like that.
 * 
 * Also if you provide an (X,Y) and a direction,
 * code from this script can return you the new result.
 * 
 * 
 */

using UnityEngine;
using System.Collections;

public enum Direction {
	North,
	West,
	South,
	East
};

public class Coord {
	// Coord class...
	public int x { get; set; }
	public int y { get; set; }

	public Coord(int a = 0, int b = 0) {
		x = 0;
		y = 0;
	}

	public Coord nextCoord(Direction d) {
		// Returns a new coordinate based on the direction given.
		// This does NOT overwrite the existing coordinate item.
	switch(d) {
			case Direction.North:
				return new Coord(x,y-1);
			case Direction.West:
				return new Coord(x-1,y);
			case Direction.South:
				return new Coord(x,y+1);
			case Direction.East:
				return new Coord(x+1,y);
			default:
				return new Coord(x,y);
		}
	}

	public bool isOOB(int r, int c, Direction d) {
		// Checks when given the direction and the upper limits
		// of the map, would this result in going out of bounds?
		Coord successor = nextCoord (d);
		return ( successor.x < 0 ||
		         successor.y < 0 ||
		         successor.x >= r ||
		         successor.y >= c
		        );
	}
}