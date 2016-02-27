using UnityEngine;
using System.Collections;

public abstract class Movement : MonoBehaviour {

    public float moveTime = .1f; // Initialized variable for movement
    public LayerMask blockingLayer; // Will be used to check collision

    // Initialized variables for 2D character movement
    private BoxCollider2D boxCollider;
    private Rigidbody2D rb2D;

    private float inverseMoveTime; // Initialized variable for inversed movement

	// Use this for initialization
	void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();
        inverseMoveTime = 1f / moveTime;
	}
	
    // Function that will handle whether or not the character will move position
    protected bool Move (int xDir, int yDir, out RaycastHit2D hit)
    {
        // Vector that will begin at the current position
        Vector2 start = transform.position;
        Vector2 end = start + new Vector2(xDir, yDir);

        // BoxCollider initialized to false until character encounters collision
        boxCollider.enabled = false;
        hit = Physics2D.Linecast(start, end, blockingLayer);
        boxCollider.enabled = true;

        // Checks if the character collides, and ends movement if true
        if (hit.transform == null)
        {
            StartCoroutine(SmoothMovement(end));
            return true;
        }
        return false;
    }

    // Calculates the position of the character by comparing its position to the destination
    // While loop is used to continually check the position of the character to get smooth movement
    protected IEnumerator SmoothMovement(Vector3 end)
    {
        // Defines float var that is set equal to the character's position - the destination location
        float sqrRemainingDistance = (transform.position - end).sqrMagnitude;

        // Using previously defined float var, the location of the character is moved until remaining distance == 0
        while (sqrRemainingDistance > float.Epsilon)
        {
            Vector3 newPosition = Vector3.MoveTowards(rb2D.position, end, inverseMoveTime * Time.deltaTime);

            // Position of our object is set to this new position
            rb2D.MovePosition(newPosition);
            sqrRemainingDistance = (transform.position = end).sqrMagnitude;
            
            // Used to exit the loop when necessary
            yield return null;
        }
    }

    protected virtual void AttemptMove <T> (int xDir, int yDir)
        where T : Component
    {
        RaycastHit2D hit;
        bool canMove = Move(xDir, yDir, out hit);

        if (hit.transform == null)
            return;

        T hitComponent = hit.transform.GetComponent<T>();

        if (!canMove && hitComponent != null)
            OnCantMove(hitComponent);
    }

    // Function that will handle cases in which the character shouldn't be able to move, i.e. collision
    protected abstract void OnCantMove <T> (T component)
        where T : Component;

	// Update is called once per frame
	void Update()
    {
	    
	}
}
