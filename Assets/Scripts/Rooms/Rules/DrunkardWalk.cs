/**
 * DrunkardWalk.cs
 * The drunkard walk algorithm is a dungeon-method algorithm that creates
 * a natural, staggered, linear movement of a corridor.
 * 
 * A drunken approach takes a random cell, chooses a random legal
 * cardinal direction, move in that direction and repeat until the
 * quota finishes.
 * 
 * However, to aim for a stronger overall path, heuristics should
 * be given to gravitate towards the center. Furthermore, depending
 * on the scale, the spaces should be mangnified a bit.
 *
 */

using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class DrunkardWalk : BaseRuleset {

	public DrunkardWalk() {
		row = 8;
		col = 8;
		map = new Tile[row,col];
		mapValidFuncs = new MapValidationFunctions();
	}

	public DrunkardWalk(int r, int c) {
		row = r;
		col = c;
		map = new Tile[row,col];
	}

	public override void setRowCol(int r, int c) {
		row = r;
		col = c;
		map = new Tile[row,col];
	}
	
	public void explodeSpace(Tile[,] map, Coord state, int m, int r, int c) {
		// Carves additional spaces surrounding the block.
		// The magnitude value m how far the cursor should carve.
		// What doesn't get carved:
		// 	Illegal OOB spots.
		//	Border spaces
		for(int i = 0; i < m; i++) {
			// Getting there. :)
		}
	}

	public override void generateMap() {
	
	}

	public override void initializeMap(){
		for(int i = 0; i < row; i++)
			for(int j = 0; j < col; j++) 
				map[i,j] = new Tile();
	}
}
