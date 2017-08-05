using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerText;
	private float startTime;
	private bool finished = false; //variable for completing a level to stop the timer

	// Use this for initialization
	void Start () {
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {

		if (finished) //if finished becomes true, then stop the timer
			return;

		float t = Time.time - startTime; //will count amount since timer has started

		string minutes = ((int)t / 60).ToString ();
		string seconds = (t % 60).ToString ("f2"); //f2 part decides only 2 decimal places. write f0 for no decimals

		timerText.text = minutes + ":" + seconds;
	}
}
