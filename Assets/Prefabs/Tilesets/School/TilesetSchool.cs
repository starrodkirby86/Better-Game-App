/**
 * School tileset.
 * Assign the contents in a prefab.
 * (May not be needed in the end if the prefabbed object takes care of it all.)
 * But the thing is, we probably can store some auxiliary or special information here.
 * I'm keeping it safe here for now though.
 */

using UnityEngine;
using System.Collections;

[System.Serializable]
public class TilesetSchool : Tileset {
	// You've been spooked by the spooky skeleton.

	public override void autoTiler(Tile[,] map) {
		// This is where we'll try to implement Colin's autotiling algorithm
	}
}
