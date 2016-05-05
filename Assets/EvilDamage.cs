using UnityEngine;
using System.Collections;

public class EvilDamage : MonoBehaviour {


	public AudioClip haha;
	private AudioSource supportive;

	// Use this for initialization
	void Start () {

		supportive = GetComponent<AudioSource>();

		// This is some instantiation.
		// We want it so that if the player is within a + direction from the particle, they will get hurt.
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		int playerX = (int)player.transform.position.x;
		int playerY = (int)player.transform.position.y;

		Coord myPlace = new Coord((int)this.transform.position.x, (int)this.transform.position.y);
		Coord playerCoord = new Coord(playerX, playerY);
		Coord n, w, s, e;
		n = playerCoord.nextCoord (Direction.North);
		w = playerCoord.nextCoord (Direction.West);
		s = playerCoord.nextCoord (Direction.South);
		e = playerCoord.nextCoord (Direction.East);

		if( myPlace.isEqual (playerCoord) ||
		    myPlace.isEqual (n) ||
		    myPlace.isEqual (w) ||
		    myPlace.isEqual (s) ||
		    myPlace.isEqual (e) ) {
			PlayerMovement hitPlayer = player.GetComponent<PlayerMovement>();
			supportive.PlayOneShot(haha,1f);
			hitPlayer.LoseHealth(750); }

		Destroy (this.gameObject, 3);

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
