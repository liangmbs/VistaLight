using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MouseInputPosition : MonoBehaviour {

	public GameObject ship;
	public bool clicked;
	public List<float> movingx;
	public List<float> movingy;
	public List<float> movingz;
	public bool moved = false;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void LateUpdate() {
		if (Input.GetButton("Fire1")) {
			RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);
			if (hit.collider != null ) {
				clicked = true;
			}
		}
		else{
			clicked = false;
		}
	}
	
	void Update(){
		if (clicked == true) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			/*movingx.Add (ray.origin.x);
			movingy.Add (ray.origin.y);
			movingz.Add (ray.origin.z);*/
			print (ray.origin);
			moved = true;
		}
		if (Input.GetKeyDown("p")) {
			foreach(float x in movingx){
				print (x);
			}
			movingx.Clear();
			moved = false;
		}
	}

/*
	void OnMouseDown(){


		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;

		Physics.Raycast (ray, out hit);

		if (hit.collider.gameObject == gameObject) {


		}

	}


*/
}
