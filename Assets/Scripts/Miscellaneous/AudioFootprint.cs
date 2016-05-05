/**
 * Main characters need audio footprints!
 * Let's try this out and hope that this bell and jingle
 * will warm the hearts of many.
 * 
 * This is a trigger event script.
 */


using UnityEngine;
using System.Collections;

public class AudioFootprint : MonoBehaviour {

	public AudioClip stepSound;

	private AudioSource source;
	private float pitchLowRange = .5f;
	private float pitchHighRange = 1.25f;
	private float volLowRange = .75f;
	private float volHighRange = 1.25f;

	// Use this for initialization
	void Awake () {
		source = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if ( Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Vertical") ) {
			source.pitch = Random.Range (pitchLowRange, pitchHighRange);
			float vol = Random.Range (volLowRange, volHighRange);
			source.PlayOneShot(stepSound,vol);
		}
	}
}
