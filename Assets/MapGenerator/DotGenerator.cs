using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DotGenerator : MonoBehaviour {

	public int DotID;
	public GameObject Dots;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			Instantiate (Dots, ray.origin, transform.rotation);
			print (ray);
		}
	}
}
