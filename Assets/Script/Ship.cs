using UnityEngine;
using System.Collections;
using System;
using SimpleJSON;

public class Ship : MonoBehaviour {
	/*
	 * Pop up Window
	 */
	//public Rect windowRect = new Rect (20, 20, 120, 50);
	public GUISkin property;
	public Texture icon;

	/*
	 * Location
	 */

	public float x;
	public float y;
	public float z;

	/*
	 * Heading Rotation
	 */

	public float rZ;

	/*
	 * Vehicle
	 */
	public int shipID;
	public int priority;
	public string Type;
	public double Heading;

	/*
	 * Company
	 */

	public string Industry;
	public string Name;

	/*
	 * Input ShipInformation
	 */

	public void SetupShip(JSONNode json){

		shipID = json["vehicle"]["vehicle_id"].AsInt;
		priority = json ["vehicle"]["priority"].AsInt;

		Heading = json ["vehicle"]["heading"].AsDouble;
		x = json ["vehicle"]["position"]["x"].AsFloat;
		y = json ["vehicle"]["position"]["y"].AsFloat;
		z = json ["vehicle"]["position"]["z"].AsFloat;
		
	
		rZ = json ["vehicle"]["heading"].AsFloat;
	}
 
	/*
	 * Ship Move
	 */
	public void Move(JSONNode json){

		x = json ["vehicle"]["position"]["x"].AsFloat;
		y = json ["vehicle"]["position"]["y"].AsFloat;
		z = json ["vehicle"]["position"]["z"].AsFloat;

		rZ = json ["vehicle"]["heading"].AsFloat;

		transform.position = new Vector3 ((x / 1000.0f) - 50.0f, -((y / 1000.0f) - 50.0f), z);
		transform.rotation = Quaternion.Euler(new Vector3 (0, 0, -rZ));
	}


	public void setpriority(string prio){
		priority = int.Parse(prio);
		
	}
}
