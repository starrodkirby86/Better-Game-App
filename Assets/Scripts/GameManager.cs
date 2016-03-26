﻿using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;

    public BoardManager boardScript;
    public int playerHealth = 100; // initialized player health variable
    public int playerMana = 100; // initialized player mana variable
    [HideInInspector]
    public bool playersTurn = true; // Since the game is turn-based, boolean must be initialized for player turn

    // Initializes game object instance
    void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        boardScript = GetComponent<BoardManager>();
        InitGame();
    }

    // Use this for initialization
    void InitGame() {
        boardScript.SetupScene(level);
    }

    // Initializes game over event to be false until determined true
    public void GameOver() {
        enabled = false;
    }
	// Update is called once per frame
	void Update () {
	
	}
}
