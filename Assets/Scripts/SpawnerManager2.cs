using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager2 : MonoBehaviour
{
	public GameObject prefab;
	public int numberOfObjects;
	public float radius;
	public float heightTimesRadius;

	void Start()
	{
		for (int i = 0; i < numberOfObjects; i++)
		{
			float angle = i * Mathf.PI * 2 / numberOfObjects;
			Vector3 pos = new Vector3(Mathf.Cos(angle), heightTimesRadius + 2.05f , Mathf.Sin(angle)+12f) * radius; //adjustment factor for ring position is dividing the transform number by whatever the radius is
			Instantiate(prefab, pos, Quaternion.identity);
		}
	}
}