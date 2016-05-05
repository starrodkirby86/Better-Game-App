using UnityEngine;
using System.Collections;

public class NewRoom : MonoBehaviour {

	public string nextLevelDestination;
	public AudioClip stepSound;
	private AudioSource source;

	// Use this for initialization
	void Awake () {
		source = GetComponent<AudioSource>();
		Debug.Log ("Ohayou gozaimasu.");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void launchToNextLevel()
	{
		source.PlayOneShot (stepSound, 1);
		AutoFade.LoadLevel (nextLevelDestination,2,1,Color.black);
		//Application.LoadLevel(nextLevelDestination);
	}
}
