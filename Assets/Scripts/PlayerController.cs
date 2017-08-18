using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public float speed; //public so we can adjust in the editor rather than the code
	public Text countText; //hold reference to UI text component
	public Text winText; //reference variable for UI text element

	private Rigidbody rb; ///create variable to hold the reference
	private int count; //player score

	public Text timerText;
	private float startTime;
	private string minutes;
	private string seconds;

	bool onGround = true; //variable for checking if on ground
	bool canDoubleJump = false; //can only double jump when NOT on ground

	public float maxSpeed = 10; //variables related to max movement speed
	public float horizontalMovement;

	void Start () //beginning of game
	{
		startTime = Time.time; //begin timer

		rb = GetComponent<Rigidbody> ();
		count = 0;
		SetCountText (); //function call
		winText.text = ""; //win text starts game as empty
	}

	void Update () ///Called before refering a frame (most game code)
	{
		float t = Time.time - startTime; //will count amount since timer has started

		minutes = ((int)t / 60).ToString ();
		seconds = (t % 60).ToString ("f2"); //f2 part decides only 2 decimal places. write f0 for no decimals

		timerText.text = minutes + ":" + seconds;

		RaycastHit hit; //projects a vector down from the center of the ball to see if the player is on the ground
		Vector3 physicsCentre = this.transform.position + this.GetComponent<SphereCollider>().center;

		Debug.DrawRay (physicsCentre, Vector3.down, Color.red, 1); //sets boolean variable onGround to true or false depending on if ray is touching ground
																	//debug is to be able to see the line
		if (Physics.Raycast (physicsCentre, Vector3.down, out hit, 1)) 
		{
			if (hit.transform.gameObject.tag != "Player") 
			{
				onGround = true;
			}
		} 
		else 
		{
			onGround = false;
		}

		Debug.Log (onGround);

		if (Input.GetKeyDown ("space") && !onGround && canDoubleJump) //if user is NOT on ground
		{
			Vector3 newVelocity = GetComponent<Rigidbody> ().velocity; //setting ONLY vertical velocity to zero for double jump
			newVelocity.y = 0;
			GetComponent<Rigidbody> ().velocity = newVelocity;
			GetComponent<Rigidbody> ().angularVelocity = Vector3.zero;
			this.GetComponent<Rigidbody> ().AddForce (Vector3.up * 500);
			canDoubleJump = false;
		}
		if (Input.GetKeyDown ("space") && onGround) //if user presses space AND player is touching ground then ball will jump
		{
			this.GetComponent<Rigidbody> ().AddForce (Vector3.up * 500);
			canDoubleJump = true;
		}

		Vector2 horizontalMovement = new Vector2 (rb.velocity.x, rb.velocity.z); //code to set max movement speed
		if (horizontalMovement.magnitude > maxSpeed)
		{
			horizontalMovement = horizontalMovement.normalized * maxSpeed;
		}
		rb.velocity = new Vector3 (horizontalMovement.x, rb.velocity.y, horizontalMovement.y);
	}

	void FixedUpdate() ///Called before physics calcs (physics code)
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		
		Vector3 movement = new Vector3 (moveHorizontal*2, 0.0f, moveVertical*2); ///get float values into a vector 3 value
			///xyz values determine direction of force we add to ball
			/// 0.0f as y value because we aren't moving the object vertically
	
		rb.AddForce (movement * speed); ///multiply movement by global var speed to make ball go faster

	}

	void OnTriggerEnter(Collider other) //function for when player collides with a trigger/collectible
	{
		if (other.gameObject.CompareTag("Pick Up")) //deactivates the game object that the trigger collider is attached to when player connects
		{
			other.gameObject.SetActive (false);
			count = count + 1; //add to player score
			SetCountText();
		}
	}

	void SetCountText() //sets count value based on how many collectibles we got
	{
		countText.text = "Count: " + count.ToString ();
		if (count >= 12)
		{
			winText.text = "Completed!";
			Time.timeScale = 0; //completely stops time (and the entire game)
		}
	}

}