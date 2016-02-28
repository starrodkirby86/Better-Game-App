/**
 * DrunkardWalk.cs
 * The drunkard walk algorithm is a dungeon-method algorithm that creates
 * a natural, staggered, linear movement of a corridor.
 * 
 * 
 *
 */

using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class DrunkardWalk : BaseRuleset {
	public DrunkardWalk(int r, int c) {
		row = r;
		col = c;
		map = new Tile[row,col];
	}
	
	public override void generateMap() {
	
	}

}
