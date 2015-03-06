using UnityEngine;
using System.Collections;
using System;
using SimpleJSON;

public class Ship : MonoBehaviour {

	/*
	 * Pop up Window
	 */
	public Rect windowRect = new Rect (20, 20, 120, 50);
	

	/*
	 * Location
	 */

	public float x;
	public float y;
	public float z;

	/*
	 * Heading Rotation
	 */
	public float rX;
	public float rY;
	float rZ;

	private int shipID;

	/*
	 * Input ShipInformation
	 */

	public void SetupShip(JSONNode json){

		shipID = json["VehicleID"].AsInt;

		x = json ["position"]["x"].AsFloat;
		y = json ["position"]["y"].AsFloat;
		z = json ["position"]["z"].AsFloat;
		
		rX = json ["heading"]["x"].AsFloat;
		rY = json ["heading"]["y"].AsFloat;
		rZ = json ["heading"]["z"].AsFloat;

	}
 
	/*
	 * Ship Move
	 */

	public void Move(JSONNode json){

		x = json ["position"]["x"].AsFloat;
		y = json ["position"]["y"].AsFloat;
		z = json ["position"]["z"].AsFloat;
		
		rX = json ["heading"]["x"].AsFloat;
		rY = json ["heading"]["y"].AsFloat;
		rZ = json ["heading"]["z"].AsFloat;

		transform.position = new Vector3 ((x / 1000.0f) - 50.0f, -((y / 1000.0f) - 50.0f), z);
		transform.rotation = Quaternion.Euler (new Vector3 (rX, rY, rZ));
	}

	/*
	 * Draw Ship Property Window
	 */

	public void PropertyWindow(){

		windowRect = GUI.Window(shipID,windowRect,DoMyWindow, new GUIContent(x.ToString ()));

	}

	/*
	 * Print Properties on the Window
	 */

	void DoMyWindow(int windowID){

	}
	
}
