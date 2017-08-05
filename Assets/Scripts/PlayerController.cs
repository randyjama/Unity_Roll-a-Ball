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
	}

	void FixedUpdate() ///Called before physics calcs (physics code)
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical); ///get float values into a vector 3 value
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