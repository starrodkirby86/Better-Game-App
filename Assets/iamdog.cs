using UnityEngine;
using System.Collections;

[System.Serializable]
public class iamdog : MonoBehaviour {
	
	//private RoomManager roomScript;
	public GameObject gameManager;          //GameManager prefab to instantiate.
	
	// Use this for initialization
	void Start () { 
		//roomScript = GetComponent<RoomManager>();

		if(GameManager.instance == null) {
			Instantiate(gameManager);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
