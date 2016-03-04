using UnityEngine;
using System.Collections;

[System.Serializable]
public class iamdog : MonoBehaviour {
	
	private RoomManager roomScript;
	
	// Use this for initialization
	void Start () { 
		roomScript = GetComponent<RoomManager>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
