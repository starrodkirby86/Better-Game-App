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
	East,
	Stop
};

public class Coord {
	// Coord class...
	public int x { get; set; }
	public int y { get; set; }

	public Coord(int a = 0, int b = 0) {
		x = a;
		y = b;
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

	public Coord crossCoord(Direction d1, Direction d2){
		// Returns a new coordinate based on two given directions.
		// Basically, this allows you to write NW, NE, SW, SE
		// if you really wanted to.
		// If you're silly, you can also do things like NS or something...
		return (nextCoord (d1)).nextCoord (d2);
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

	public bool isEqual(Coord b) {
		return ( this.x == b.x && this.y == b.y );
	}

	public Direction rotate180(Direction d) {
		switch(d) {
		case Direction.North:
			return Direction.South;
		case Direction.West:
			return Direction.East;
		case Direction.South:
			return Direction.North;
		case Direction.East:
			return Direction.West;
		default:
			return d;
		}
	}

	public Direction rotate90Right(Direction d) {
		switch(d) {
		case Direction.North:
			return Direction.East;
		case Direction.West:
			return Direction.North;
		case Direction.South:
			return Direction.West;
		case Direction.East:
			return Direction.South;
		default:
			return d;
		}
	}

	public Direction rotate90Left(Direction d) {
		switch(d) {
		case Direction.North:
			return Direction.West;
		case Direction.West:
			return Direction.South;
		case Direction.South:
			return Direction.East;
		case Direction.East:
			return Direction.North;
		default:
			return d;
		}
	}
}