using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallReset : MonoBehaviour {
	
	Vector3 checkpoint;

	// Use this for initialization
	void Start () 
	{
		checkpoint = new Vector3(0,5,0); //position of restart on map
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (transform.position.y < -10) //set position of threshold of reset
		{
			transform.position = checkpoint;
			GetComponent<Rigidbody> ().velocity = Vector3.zero;
			GetComponent<Rigidbody> ().angularVelocity = Vector3.zero;
		}
		
	}
}
