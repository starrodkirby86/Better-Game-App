using UnityEngine;
using System.Collections;

namespace Completed
{
    /* TO-DO: There is an inheritance issue with the player health and animator
    we will have to find out how to include the variables from the Player script
    in this script */
    public int playerHealth { get; set; }
    public int playerHealth { get; set; }
    public int playerHealth { get; set; }
    public class PlayerMovement : Movement
    {

        // Use this for initialization
        protected override void Start()
        {
            animator = GetComponent<Animator>();

            playerHealth = GameManager.instance.playerHealth;
        }

        private void OnDisable()
        {
            GameManager.instance.playerHealth = playerHealth;
        }
        // Update is called once per frame, checks for player's turn and performs 2D movement
        void Update()
        {
            if (!GameManager.instance.playersTurn) return;

            int horizontal = 0;
            int vertical = 0;

            horizontal = (int)Input.GetAxisRaw("Horizontal");
            vertical = (int)Input.GetAxisRaw("Vertical");

            if (horizontal != 0)
                vertical = 0;

            // AttemptMove() is a function inherited from the Movement script, we
            // are simply calling on it and adding the Wall and horiz/vertical variables
            if (horizontal != 0 || vertical != 0)
                AttemptMove<Wall>(horizontal, vertical);
        }

        // Function that handles actions that occur on movement
        protected override void AttemptMove <T> (int xDir, int yDir)
        {
            base.AttemptMove<T>(xDir, yDir);

            RaycastHit2D hit;
            CheckIfGameOver();
            GameManager.instance.playersTurn = false;
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
            Wall hitWall = component as Wall;
        }

        public void Restart()
        {
            Application.LoadLevel(Application.loadedLevel);
        }
        // Checks if the game is over by constantly comparing the player health variable
        private void CheckIfGameOver()
        {
            if (playerHealth <= 0)
                GameManager.instance.GameOver();
        }
    }
}
