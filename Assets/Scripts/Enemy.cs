/*
using UnityEngine;
using System.Collections;

namespace Completed
{

	// Enemy inherits from MovingObject, which is the base
	// class for objects that can move.
    public class Enemy : MovingObject
    {
        public int playerDamage;
        private Animator animator;
        private Transform target;
        private bool skipMove;

	    // Use this for initialization
	    protected override void Start()
	    {
	        GameManager.instance.AddEnemyToList(this);
	        Animator = GetComponent<Animator>();
	        target = GameObject.FindGameObjectWithTag("Player").transform;
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

	        if (Mathf.Abs(AvatarTarget.position.x - transform.position.x) < float.Epsilon)
	            yDir = AvatarTarget.position.y > transform.position.y ? 1 : -1;
	        else
	            xDir = AvatarTarget.position.x > transform.position.x ? 1 : -1;

	        AttemptMove<Player>(xDir, yDir);

	    }

	    protected override void OnCantMove<T>(T component)
	    {
	        Player hitPlayer = component as Player;

	        //hitPlayer.LoseFood(playerDamage); Find something to replace this mechanic with

	    }
	    // Update is called once per frame
	}
}
*/