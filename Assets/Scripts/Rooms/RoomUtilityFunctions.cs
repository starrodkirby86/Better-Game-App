/**
 * RoomUtilityFunctions.cs
 * 
 * Sometimes you need a set of generic functions to help you get along your way.
 * This is what this script does. It'll host plenty of generic room generation
 * functions that can be helpful in providing the coder useful information
 * or to make generating something easier.
 */

using System;
using UnityEngine;
using System.Collections.Generic;


/**
 * Generic functions that help make adding certain
 * tiles when generating algorithms.
 * -> Explode tile
 */
public class TileFunctions {

}

/**
 * Functions that help verify aspects of a map to 
 * help ensure that the map made is a "good" one.
 * -> DFS
 * -> Floodfill a particular point and return the area
 */
public class MapValidationFunctions {
	Boolean clearable = false;

public void FloodFillCheck(Tile[,] map, int x, int y, Tile[,] oldChar, Tile[,] newChar) {
	// The recursive algorithm. Starting at x and y, changes any adjacent
	// characters that match oldChar to newChar.
		int mapWidth = map.GetLength(0);
		int mapHeight = map.GetLength(1);

		if (oldChar == null) {
			oldChar = map[x][y];
		}

		if (map[x][y] != oldChar)
			// Base case. If the current x, y character is not the oldChar,
			// then do nothing.
			return

				// Change the character at world[x][y] to newChar
				map[x][y] = newChar;

				// Recursive calls. Make a recursive call as long as we are not on the
				// boundary (which would cause an Index Error.)
		if (x > 0) // left
			FloodFillCheck(map, x-1, y, oldChar, newChar);

		if (y > 0) // up
			FloodFillCheck(map, x, y-1, oldChar, newChar);

		if (x < mapWidth-1) // right
			FloodFillCheck(map, x+1, y, oldChar, newChar);

		if (y < mapHeight-1) // down
			FloodFillCheck(map, x, y+1, oldChar, newChar);
						}
}