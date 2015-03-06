using UnityEngine;
using System.Collections;

using SimpleJSON;

public class ShipPropertyWindow : MonoBehaviour {

	private bool showproperty;
	private Collider2D hitObject;

	void Update()
	{
		/**
		 * Mouse Button Pressed 
		 */

		if (Input.GetMouseButtonDown (0)) {
			RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
			if(hit.collider != null)
			{
				showproperty = !showproperty;
				hitObject = hit.collider;
				
			}
		}
	}

	/*
	 * Continuously show the the Window
	 */
	void OnGUI(){
		if (showproperty) {

				DrawProperty(hitObject.name);

		
		}
	}

	/*
	 * Draw the Window
	 */
	void DrawProperty(string name){

		GameObject ship = GameObject.Find (name);
		ship.GetComponent<Ship>().PropertyWindow();

	}



}

	