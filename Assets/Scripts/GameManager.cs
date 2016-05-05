using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class GameManager : MonoBehaviour {

	public float turnDelay = .05f;
	public static GameManager instance = null;
    private RoomManager roomScript;

    public int playerHealth = 100; // initialized player health variable
    public int playerMana = 100; // initialized player mana variable
    [HideInInspector]
    public bool playersTurn = true; // Since the game is turn-based, boolean must be initialized for player turn

	private List<Enemy> enemies; // observers subscribed to gamemanager
	private bool enemiesMoving;

    // Initializes game object instance
    void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

		enemies = new List<Enemy>();

        DontDestroyOnLoad(gameObject);

        InitGame();
    }

    // Use this for initialization
    void InitGame() {
		enemies.Clear();
		roomScript = GetComponent<RoomManager>();
    }

    // Initializes game over event to be false until determined true
    public void GameOver() {
        enabled = false;
    }
	// Update is called once per frame
	void Update () {
		//Check that playersTurn or enemiesMoving or doingSetup are not currently true.
		if(playersTurn || enemiesMoving)
			
			//If any of these are true, return and do not start MoveEnemies.
			return;
		
		//Start moving enemies.
		StartCoroutine (MoveEnemies ());
	}

	public void AddEnemyToList(Enemy script)
	{
		//Add Enemy to List enemies.
		enemies.Add(script);
	}

	public void DetachEnemyFromList(Enemy removeMe)
	{
		// Removes a certain enemy from the list.
		enemies.Remove (removeMe);
	}

	IEnumerator MoveEnemies()
	{
		enemiesMoving = true;
		yield return new WaitForSeconds(turnDelay);
		if(enemies.Count == 0)
		{
			yield return new WaitForSeconds(turnDelay);
		}

		for (int i = 0; i < enemies.Count; i++)
		{
			enemies[i].MoveEnemy();
			yield return new WaitForSeconds(enemies[i].moveTime);
		}

		playersTurn = true;

		enemiesMoving = false;
	}

	IEnumerator DamageEnemies(int damage)
	{
		for(int i = 0; i < enemies.Count; i++)
			enemies[i].loseHealth(damage);

		yield return new WaitForSeconds(turnDelay);
	}

	public List<Enemy> getListOfEnemies() {
		return enemies;
	}
}