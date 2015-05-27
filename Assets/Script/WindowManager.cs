using UnityEngine;
using System.Collections;
using System;
using SimpleJSON;
using UnityEngine.UI;

public class WindowManager: MonoBehaviour {

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
				hitObject = hit.collider;
				GameObject shipObject = GameObject.Find (hitObject.name);
				ClickOnShip(shipObject);
			}
		}
	}
	

	void OpenWindow() {
		window = Instantiate (propertywindow, transform.position, transform.rotation) as GameObject;
		GameObject shipObject = GameObject.Find (hitObject.name);
		window.name = "window";
	}
	
	public void ClickOnShip(GameObject hit){
		GameObject detection = GameObject.Find ("window");
		if (detection == null) {
			OpenWindow ();	
		}
		GameObject replacedshipObjct = hit;
		SetWindowShip(replacedshipObjct);
	}

	void SetWindowShip(GameObject shipobject){
		window.GetComponentInChildren<Window> ().setShip (shipobject);
	}


	
	
}

	