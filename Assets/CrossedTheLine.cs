using UnityEngine;
using System.Collections;

public class CrossedTheLine : MonoBehaviour {

	public AudioClip bossBGM;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		int playerX = (int)player.transform.position.x;
		int playerY = (int)player.transform.position.y;
		
		if(playerY < 16) {
			AudioSource source = GetComponent<AudioSource>();
			source.clip = bossBGM;
			source.Play();
			Destroy (this);
		}
	}
}
