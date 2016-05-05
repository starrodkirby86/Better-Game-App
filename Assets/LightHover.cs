using UnityEngine;
using System.Collections;

public class LightHover : MonoBehaviour {

	public int howManyTimes;
	public float stepIncrement;

	private int myCounter = 0;
	private bool goingDown = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(!goingDown) {
			this.transform.Translate (new Vector3(0,stepIncrement,0));
			myCounter++;
			if(myCounter == howManyTimes)
				goingDown = true;
		}
		else {
			this.transform.Translate (new Vector3(0,stepIncrement*-1,0));
			myCounter--;
			if(myCounter < 0)
				goingDown = false;
		}
	}
}
