using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/* TO-DO: There is an inheritance issue with the player health and animator
we will have to find out how to include the variables from the Player script
in this script */
    
public class PlayerMovement : Movement
{
    public float restartLevelDelay = 0.5f;
    [SerializeField] PlayerStats playerStats;
    Animator animator;
    private int playerHealth;

// Use this for initialization
protected override void Start()
    {
        animator = GetComponent<Animator>();
		playerStats = GetComponent<PlayerStats>();
		base.Start ();
    }

    private void OnDisable()
    {
        //GameManager.instance.playerHealth = playerHealth;
    }
    // Update is called once per frame, checks for player's turn and performs 2D movement
    protected override void Update()
    {
		updateUI ();

        if (!GameManager.instance.playersTurn) return;

        int horizontal = 0;
        int vertical = 0;

        horizontal = (int)Input.GetAxisRaw("Horizontal");
        vertical = (int)Input.GetAxisRaw("Vertical");

		if(horizontal != 0)
			vertical = 0;

		if(vertical != 0)
			horizontal = 0;

		//Check if we have a non-zero value for horizontal or vertical
		if(horizontal != vertical)
		{
			//Call AttemptMove passing in the generic parameter Wall, since that is what Player may interact with if they encounter one (by attacking it)
			//Pass in horizontal and vertical as parameters to specify the direction to move Player in.
			AttemptMove<Wall>(horizontal, vertical);
		}
    }

    // Function that handles actions that occur on movement
    protected override void AttemptMove <T> (int xDir, int yDir)
    {
        base.AttemptMove<T>(xDir, yDir);

        //RaycastHit2D hit;
        //CheckIfGameOver();
        GameManager.instance.playersTurn = false;
		return;
    }

    // Function that is invoked when character touches doors, enemies, exits, etc.
    private void OnTriggerEnter2D (Collider2D other)
    {
        if (other.tag == "Door")
        {
            Invoke("Restart", restartLevelDelay);
            enabled = false;
        }
        else if (other.tag == "Enemy")
        {
            // Add enemy encounter code
        }
    }

    // Function that handles case where player cannot move
    // TO-DO: modify this to work with collisions; the other implementation
    // destroys the walls instead of just colliding
    protected override void OnCantMove <T> (T component)
    {
		//GameManager.instance.GameOver();
    }

    public void Restart()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    public void LoseHealth(int loss)
    {
        animator.SetTrigger("playerHit");
        playerHealth -= loss;
        CheckIfGameOver();
    }

    // Checks if the game is over by constantly comparing the player health variable
    private void CheckIfGameOver()
    {
        if (playerHealth <= 0)
			Debug.Log ("Ass tits");
            //GameManager.instance.GameOver();
    }

	// Update UI
	private void updateUI() {
		// Could be better BUT OH WELL LOL
		// Updates the relevant UI items for the player.
		GameObject UIName = GameObject.Find ("UI_Name");
		GameObject UIHP = GameObject.Find ("UI_HP");
		Text playerName = UIName.GetComponent<Text>() as Text; // This should be the player's name
		playerName.text = playerStats.playerName;
		Text playerHP = UIHP.GetComponent<Text>() as Text; // This should be the player's HP
		playerHP.text = playerStats.playerCurrentHP.ToString () + "/" + playerStats.playerHealth.ToString ();
	}
}
