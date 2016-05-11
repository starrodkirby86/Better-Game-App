/**
 * I'M DOING MAGIC AND SHIZ
 * THAT MAGIC SHIZ IS DOPE WIZ KHALIFA
 * 
 * This spell checks for enemies within a radius of spellRadius
 * and sees if the enemy's position lands there. If so, Damage!
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerSpellcast : MonoBehaviour {

	// Audio feedback
	public AudioClip magicSound;
	private AudioSource source;
	private float pitchLowRange = .5f;
	private float pitchHighRange = 1.5f;
	private float volLowRange = .75f;
	private float volHighRange = 1.25f;

	// Graphical feedback
	public GameObject particleGfx;

	// Decide range and damage
	public int spellRadius;
	public int spellCost;
	public int spellDamage;

	// Use this for initialization
	void Awake () {
		source = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if ( Input.GetButtonDown("Fire1") ) {

			// Do we have enough MP?
			PlayerMovement thatsMe = GetComponent<PlayerMovement>();
			if(thatsMe.getPlayerCurrentMP() > 0) {

				int thatsMyMP = thatsMe.getPlayerCurrentMP();
				thatsMyMP -= spellCost;

				if(thatsMyMP < 0) thatsMyMP = 0;

				thatsMe.setPlayerCurrentMP (thatsMyMP);

				source.pitch = Random.Range (pitchLowRange, pitchHighRange);
				float vol = Random.Range (volLowRange, volHighRange);
				source.PlayOneShot(magicSound,vol);

				Coord playerCoord = new Coord((int)thatsMe.transform.position.x, (int)thatsMe.transform.position.y);
				Coord rangeNorth, rangeSouth, rangeWest, rangeEast;

				// OK so we're going to figure out what enemies are in our range
				List<Enemy> newEnemyList = new List<Enemy>();
				List<Enemy> allEnemies = GameManager.instance.getListOfEnemies();
				for(int i = 0; i < allEnemies.Count; i++) {

					// Store coordinates for calculations
					Coord enemyCoord = new Coord((int)allEnemies[i].transform.position.x, (int)allEnemies[i].transform.position.y);
					bool sameSpot = false;
					if(enemyCoord.isEqual(playerCoord)) {
						newEnemyList.Add (allEnemies[i]);
						sameSpot = true;
					}
					rangeNorth = rangeSouth = rangeWest = rangeEast = playerCoord;
					// We'll have to travel down the particle's route
					// and see if the enemy lands squarely within that.
					// If so, then we're going to add it to the hit list and break
					// (we don't need to keep searching)
					for(int j = 1; j <= spellRadius && !sameSpot; j++) {
						rangeNorth = rangeNorth.nextCoord (Direction.North);
						rangeSouth = rangeSouth.nextCoord (Direction.South);
						rangeWest = rangeWest.nextCoord (Direction.West);
						rangeEast = rangeEast.nextCoord (Direction.East);
						if(enemyCoord.isEqual (rangeNorth) || 
						   enemyCoord.isEqual (rangeSouth) || 
						   enemyCoord.isEqual (rangeWest) || 
						   enemyCoord.isEqual (rangeEast) ) {
							newEnemyList.Add (allEnemies[i]);
							break;
						}

					}
				}

				// This is just particle effects
				rangeNorth = rangeSouth = rangeWest = rangeEast = playerCoord;
				for(int i = 1; i <= spellRadius; i++) {
					rangeNorth = rangeNorth.nextCoord (Direction.North);
					rangeSouth = rangeSouth.nextCoord (Direction.South);
					rangeWest = rangeWest.nextCoord (Direction.West);
					rangeEast = rangeEast.nextCoord (Direction.East);
					GameObject objectN = Instantiate(particleGfx, new Vector3 (rangeNorth.x, rangeNorth.y, 0), Quaternion.identity) as GameObject;
					GameObject objectS = Instantiate(particleGfx, new Vector3 (rangeSouth.x, rangeSouth.y, 0), Quaternion.identity) as GameObject;
					GameObject objectW = Instantiate(particleGfx, new Vector3 (rangeWest.x, rangeWest.y, 0), Quaternion.identity) as GameObject;
					GameObject objectE = Instantiate(particleGfx, new Vector3 (rangeEast.x, rangeEast.y, 0), Quaternion.identity) as GameObject;

					Destroy (objectN, 1f);
					Destroy (objectS, 1f);
					Destroy (objectW, 1f);
					Destroy (objectE, 1f);
				
					}

				// When we're here, it's time to inflict damage
				for(int i = 0; i < newEnemyList.Count; i++)
					newEnemyList[i].loseHealth (spellDamage);
			}
		}
	}
}
