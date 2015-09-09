using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DotGenerator : MonoBehaviour {

	public int DotID;
	public GameObject Dots;
	public List<GameObject> CreatedDots= new List<GameObject>();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			GameObject CreatedDot = Instantiate (Dots, ray.origin, transform.rotation) as GameObject;
			CreatedDots.Add (CreatedDot);
			CreatedDots [DotID].name = DotID.ToString();
			DotID++;
		}
	}
}
