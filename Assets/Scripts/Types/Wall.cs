using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour {

	private SpriteRenderer spriteRenderer; 

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Awake ()
	{
		//Get a component reference to the SpriteRenderer.
		spriteRenderer = GetComponent<SpriteRenderer> ();
	}
}
