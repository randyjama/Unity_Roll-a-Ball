using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour 
{
	public int value;
	public float rotateSpeed;


	// Update is called once per frame
	void Update () 
	{
		gameObject.transform.Rotate (Vector3.forward * Time.deltaTime * rotateSpeed);
	}

	void OnTriggerEnter()
	{
		AudioSource source = GetComponent<AudioSource> ();
		source.Play ();
	}

}
