using UnityEngine;
using System.Collections;

[System.Serializable]
public class iamdog : MonoBehaviour {

	// Use this for initialization
	void Start () {
		CellularAutomata hotLoad = new CellularAutomata(5,6);
		hotLoad.generateMap();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
