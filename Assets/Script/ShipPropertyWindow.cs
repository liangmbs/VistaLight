using UnityEngine;
using System.Collections;
using System;
using SimpleJSON;
using UnityEngine.UI;

public class ShipPropertyWindow : MonoBehaviour {

	private bool showproperty;
	public Collider2D hitObject;
	public GameObject propertywindow;
	private GameObject window;


	void Update()
	{
		/*
		 * Mouse Button Pressed 
		 */
		RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
		if (Input.GetMouseButtonDown (0)) {
			if(hit.collider != null)
			{
				window = Instantiate (propertywindow, transform.position, transform.rotation) as GameObject;
				hitObject = hit.collider;
				GameObject shipObject = GameObject.Find(hitObject.name);
				window.name = "window";
				window.GetComponentInChildren<Window>().setShip(shipObject);

				//textfield = (hitObject.GetComponent<Ship> ().priority).ToString ();
			}
		}

	}
	

}

	