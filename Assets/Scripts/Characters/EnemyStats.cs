using UnityEngine;
using System.Collections;

/**********************************************************************************/
// The purpose of this script is to provide a location where all of the enemy's
// combat statistics are stored and operated on. An example of an operation would
// be the calculation to determine when the enemy should level up, or how the
// enemy's character statistics are calculated. This is done with basic maths
// operations such as exponential functions and randomization for variance.
/**********************************************************************************/

// TODO: We need to write code to handle when the user distributes their skill points upon leveling up

[System.Serializable]
public class EnemyStats : MonoBehaviour {
	public string enemyName { get; set; }
    public int enemyHealth { get; set; }
    public int enemyMana { get; set; }
    public int enemyStrength { get; set; }
    public int enemyDefense { get; set; }
    public int enemyIntelligence { get; set; }
    public int enemyLevel { get; set; }
    public int enemyExperience { get; set; }
	private static RandomNames randomNames;

    // Initialized variables for character values
    public float restartLevelDelay = 1f;
    //public int enemyHealth, enemyMana, enemyStrength, enemyDefense, enemyIntelligence, enemyLevel = 1, enemyExperience = 0;

    // Initialized variables for manually upgrading character stats
    public int enemyHPSkill, enemyMPSkill, enemyStrSkill, enemyDefSkill, enemyIntSkill;

	public int enemyCurrentHP, enemyCurrentMP;

    // Function which will randomly assign character statistics with a +/- 20% variance based on enemy level
    // and will account for manual skill upgrades when the user levels up
    // TO-DO: Refactor this code to improve readability and usability later on, in other words,
    // separate functionalities into their individual functions

	// Hi Unity.
	void Awake() {
		randomNames = new RandomNames();
		enemyName = randomNames.getName (nameList.funny);
		enemyHealth = Random.Range ((50 * enemyLevel - 10 * enemyLevel) + 10 * enemyHPSkill, (50 * enemyLevel + 10 * enemyLevel) + 10 * enemyHPSkill);
		enemyMana = Random.Range ((20 * enemyLevel - 4 * enemyLevel) + 4 * enemyMPSkill, (20 * enemyLevel + 4 * enemyLevel) + 4 * enemyMPSkill);
		enemyStrength = Random.Range((10 * enemyLevel - 2 * enemyLevel) + 2 * enemyStrSkill, (10 * enemyLevel + 2 * enemyLevel) + 2 * enemyStrSkill);
		enemyDefense = Random.Range((10 * enemyLevel - 2 * enemyLevel) + 2 * enemyDefSkill, (10 * enemyLevel + 2 * enemyLevel) + 2 * enemyDefSkill);
		enemyIntelligence = Random.Range((10 * enemyLevel - 2 * enemyLevel) + 2 * enemyIntSkill, (10 * enemyLevel + 2 * enemyLevel) + 2 * enemyIntSkill);

		enemyCurrentHP = enemyHealth;
		enemyCurrentMP = enemyMana;
	}

}
