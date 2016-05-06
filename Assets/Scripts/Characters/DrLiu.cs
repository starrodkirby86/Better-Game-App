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
public class DrLiu : Enemy
{
	public AudioClip takeEasy;
	private AudioSource source;
	
	private bool encountered = false;
	
	public GameObject evilObject;
	//private Slider enemyHPBar;

    // Use this for initialization
    protected void Awake()
    {
		//GameManager.instance.AddEnemyToList(this);
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
		enemyStats = GetComponent<EnemyStats>();
		source = GetComponent<AudioSource>();
		//makeUI ();
        //base.Start();
    }

    protected override void AttemptMove<T>(int xDir, int yDir)
    {
        if (skipMove)
        {
			if(Random.Range (0,3) > 2)
            	skipMove = false;
            return;
        }
        base.AttemptMove<T>(xDir, yDir);
        skipMove = true;
    }

    public override void MoveEnemy()
    {
        // Dr Liu's special algorithm is to teleport every so often throughout the map
		// But only once you start encountering him.

		Debug.Log ("Nnn...");

		if (skipMove)
		{
			if(Random.Range (0,3) > 2)
				skipMove = false;
			return;
		}

		if(!encountered) {
			GameObject player = GameObject.FindGameObjectWithTag("Player");
			int playerX = (int)player.transform.position.x;
			int playerY = (int)player.transform.position.y;

			if(playerY < 12) encountered = true;

			if(enemyStats.enemyCurrentHP < enemyStats.enemyHealth) encountered = true;

			Debug.Log ("...");

			return;
		}

		// So here is when Dr. Liu unleashes himself.

		while(true){
			int candidX = Random.Range(0, 16);
			int candidY = Random.Range(0, 32); 
			BossRoomManager theRoom = GameManager.instance.GetComponent<BossRoomManager>();
			BaseRuleset selectedRule = theRoom.selectedRule;
			Tile[,] candidMap = selectedRule.map;
			if((candidMap[candidX,candidY]).property == TileType.Floor1) {
				// We need to move the player to this position.
				Vector3 currentLocation = this.gameObject.transform.position;
				Vector3 moveMe = new Vector3(candidX, candidY);
				Rigidbody2D rb2D = this.gameObject.GetComponent<Rigidbody2D>() as Rigidbody2D;
				rb2D.MovePosition(moveMe);
				// Now we need to put an evil particle where Dr Liu was.
				source.PlayOneShot(takeEasy, 0.8f);
				Instantiate( evilObject, currentLocation, Quaternion.identity );
				return;
			}
		}
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
	
	public override void makeUI() {

		GameObject newCanvas = Instantiate( enemyCanvas, new Vector3(0,0,0), Quaternion.identity ) as GameObject;
		newCanvas.transform.SetParent (this.gameObject.transform, false);
		GameObject enemyText = newCanvas.transform.GetChild(0).gameObject;
		Text UIName = enemyText.GetComponent<Text>();
		UIName.text = "Alex Liu";
		GameObject enemyHPBar = newCanvas.transform.GetChild(1).gameObject;
		Slider UIBar = enemyHPBar.GetComponent<Slider>();
		UIBar.maxValue = enemyStats.enemyHealth;
		UIBar.value = enemyStats.enemyCurrentHP;
	}

	public override void updateUI() {
		GameObject thisCanvas = this.transform.GetChild(0).gameObject; // The first child of the enemy should be the enemy canvas
		GameObject enemyHPBar = thisCanvas.transform.GetChild(1).gameObject;
		Slider UIBar = enemyHPBar.GetComponent<Slider>();
		UIBar.value = enemyStats.enemyCurrentHP;
	}

	public override void loseHealth(int loss) {
		enemyStats.enemyCurrentHP -= loss;
		if(enemyStats.enemyCurrentHP <= 0) {
			GameManager.instance.DetachEnemyFromList(this);
			Destroy (this.transform.gameObject);
			AutoFade.LoadLevel ("Success",4,2,Color.white);
		}
	}
}
