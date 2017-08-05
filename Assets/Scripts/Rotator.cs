using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rotator : MonoBehaviour
{
    public Text timerText;
	
	// Update is called once per frame. Don't need "fixedUpdate" because we are not dealing with any physics
	void Update () 
	{
		transform.Rotate (new Vector3 (15, 30, 45) * Time.deltaTime); //multiple by deltatime to make it smoother and independent of frame rate)
	}
}
