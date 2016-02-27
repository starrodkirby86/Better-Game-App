using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    // Initialized variables for character values
    public float restartLevelDelay = 1f;
    public int playerHealth;
    public int playerMana;
    public int playerStrength;
    public int playerDefense;
    public int playerIntelligence;
    public int playerLevel = 1;
    public int playerExperience = 0;

    // Initialized variables for manually upgrading character stats
    public int playerHPSkill;
    public int playerMPSkill;
    public int playerStrSkill;
    public int playerDefSkill;
    public int playerIntSkill;

    // Function which will randomly assign character statistics with a +/- 20% variance based on player level
    // and will account for manual skill upgrades when the user levels up
    // TO-DO: Refactor this code to improve readability and usability later on, in other words,
    // separate functionalities into their individual functions
    public void playerStats()
    {
        playerHealth = Random.Range ((50 * playerLevel - 10 * playerLevel) + 10 * playerHPSkill, (50 * playerLevel + 10 * playerLevel) + 10 * playerHPSkill);
        playerMana = Random.Range ((20 * playerLevel - 4 * playerLevel) + 4 * playerMPSkill, (20 * playerLevel + 4 * playerLevel) + 4 * playerMPSkill);
        playerStrength = Random.Range((10 * playerLevel - 2 * playerLevel) + 2 * playerStrSkill, (10 * playerLevel + 2 * playerLevel) + 2 * playerStrSkill);
        playerDefense = Random.Range((10 * playerLevel - 2 * playerLevel) + 2 * playerDefSkill, (10 * playerLevel + 2 * playerLevel) + 2 * playerDefSkill);
        playerIntelligence = Random.Range((10 * playerLevel - 2 * playerLevel) + 2 * playerIntSkill, (10 * playerLevel + 2 * playerLevel) + 2 * playerIntSkill);

        if (playerExperience > Mathf.Pow(playerLevel, 2))
        {
            playerLevel++;
        }

    }
    
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
