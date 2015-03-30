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
				//showproperty = !showproperty;
				hitObject = hit.collider;
				GameObject shipObject = GameObject.Find(hitObject.name);
				print (hitObject.name);
				print (window);
				window.name = "window";
				window.GetComponentInChildren<Window>().setShip(shipObject);
				//textfield = (hitObject.GetComponent<Ship> ().priority).ToString ();
			}
		}

	}


	/*
	 * Draw the Window
	 */

	/*
	void DrawProperty(string name){

		GameObject ship = GameObject.Find (name);
		//textfield = (hitObject.GetComponent<Ship> ().priority).ToString ();
		PropertyWindow ();

	}*/

	public void PropertyWindow(){
		/*
		//windowRect = GUI.Window(shipID,windowRect,DoMyWindow, new GUIContent(x.ToString ()));
		GUI.skin = hitObject.GetComponent<Ship>().property;
		GUI.BeginGroup(new Rect(Screen.width/2-150,Screen.height/2-150,300,300));
			GUI.Box(new Rect(0,0,300,300),"Ship Property");
			GUI.DrawTexture(new Rect(10, 50, 80,60), hitObject.GetComponent<Ship>().icon,ScaleMode.ScaleToFit, true, 1.33F);
			//GUI.Label (new Rect (100, 50, 150, 20), "company: " + company);
			GUI.Label (new Rect (100, 70,150, 20), "Ship ID: " + hitObject.GetComponent<Ship>().shipID);
			//GUI.Label (new Rect (100, 90, 150, 20), "capacity: " + capacity);
			if (GUI.Button (new Rect (0, 0, 20, 20), "closed")) {
			showproperty = !showproperty;
		}
			GUI.Label(new Rect (30,120,60,20),"Priority: ");
			textfield = GUI.TextField(new Rect(90,120,40,20), textfield,100);
			if(GUI.Button (new Rect(80,140,70,30),"Submit")){
			hitObject.GetComponent<Ship>().priority = int.Parse(textfield);
		}
		GUI.EndGroup ();*/

	}


}

	