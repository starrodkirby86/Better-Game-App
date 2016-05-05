/**
 * 
 * The ultimate class.
 * Don't think about it too hard.
 *
 */


using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// Enemy inherits from MovingObject, which is the base
// class for objects that can move.
[System.Serializable]
public class DrLiu : Movement
{
    public int playerDamage = 500;
	EnemyStats enemyStats;
    private Animator animator;
    private Transform target;
    private bool skipMove;
	
	public GameObject enemyCanvas;
	//private Slider enemyHPBar;

    // Use this for initialization
    protected override void Start()
    {
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
		enemyStats = GetComponent<EnemyStats>();
		makeUI ();
        base.Start();
    }

    protected override void AttemptMove<T>(int xDir, int yDir)
    {
        if (skipMove)
        {
            skipMove = false;
            return;
        }
        base.AttemptMove<T>(xDir, yDir);
        skipMove = true;
    }

    public void MoveEnemy()
    {
        int xDir = 0;
        int yDir = 0;

        if (Mathf.Abs(target.position.x - transform.position.x) < float.Epsilon)
			yDir = target.position.y > transform.position.y ? 1 : -1;
        else
			xDir = target.position.x > transform.position.x ? 1 : -1;

        AttemptMove<PlayerMovement>(xDir, yDir);

    }

    protected override void OnCantMove<T>(T component)
    {
		Debug.Log ("Doesn't happen.");
        PlayerMovement hitPlayer = component as PlayerMovement;
   		hitPlayer.LoseHealth(playerDamage); //Find something to replace this mechanic with
    }

	// Update is called once per frame
	protected override void Update() {
		updateUI ();
	}
	
	public void makeUI() {

		GameObject newCanvas = Instantiate( enemyCanvas, new Vector3(0,0,0), Quaternion.identity ) as GameObject;
		newCanvas.transform.SetParent (this.gameObject.transform, false);
		GameObject enemyText = newCanvas.transform.GetChild(0).gameObject;
		Text UIName = enemyText.GetComponent<Text>();
		UIName.text = enemyStats.enemyName;
		GameObject enemyHPBar = newCanvas.transform.GetChild(1).gameObject;
		Slider UIBar = enemyHPBar.GetComponent<Slider>();
		UIBar.maxValue = enemyStats.enemyHealth;
		UIBar.value = enemyStats.enemyCurrentHP;
	}

	public void updateUI() {
		GameObject thisCanvas = this.transform.GetChild(0).gameObject; // The first child of the enemy should be the enemy canvas
		GameObject enemyHPBar = thisCanvas.transform.GetChild(1).gameObject;
		Slider UIBar = enemyHPBar.GetComponent<Slider>();
		UIBar.value = enemyStats.enemyCurrentHP;
	}

	public void loseHealth(int loss) {
		enemyStats.enemyCurrentHP -= loss;
		if(enemyStats.enemyCurrentHP <= 0) {
			Destroy (this.transform.gameObject);
		}
	}
}
