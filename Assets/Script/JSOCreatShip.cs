using UnityEngine;
using System.Collections;
using SimpleJSON;
using System.Linq;
using System;

public class JSOCreatShip : MonoBehaviour {
	public GameObject breakbulk;
	public static bool accept;
	public ClientSocket send;

	void Start(){
		send = GameObject.Find("ClientSocketObject").GetComponent <ClientSocket>();
	}

	void Update(){
		// Send Message
		//send.Send ("hello");
	}

	/*
	 * Detect the Ship Action
	 */

	public void Detection(string json){

		JSONNode N = JSON.Parse (json);
		string action = N["action"]; 

		switch (action) {
		case "ship create":

			CreatShip (N);
			break;

		case "ship move":

			string shipID = N ["VehicleID"];
			MoveShip (N, shipID);
			break;

		default: 

			throw new Exception("Do not support such form" + action);

		}
	}

	/*
	 * Creat Ship and information to prefab
	 */

	 void CreatShip(JSONNode json){

		// Postion
		float x = json ["position"]["x"].AsFloat;
		float y = json ["position"]["y"].AsFloat;
		float z = json ["position"]["z"].AsFloat;
		
		// Heading 
		float rX = json ["heading"]["x"].AsFloat;
		float rY = json ["heading"]["y"].AsFloat;
		float rZ = json ["heading"]["z"].AsFloat;

		// Creat Ship
		GameObject ship = Instantiate (breakbulk, 
		            new Vector3 ((x/1000.0f)-50.0f, -((y/1000.0f)-50.0f), z), 
		            Quaternion.Euler (new Vector3 (rX, rY, rZ))
	                            ) as GameObject;

		// Name Ship
		int shipID = json["VehicleID"].AsInt;
		ship.name = "ship_" + shipID;

		// Set up Ship Information to Prefab
		ship.GetComponent<Ship>().SetupShip (json);
	}


	/*
	 * Move the Ship
	 */

	 public void MoveShip(JSONNode json, string shipID){

		GameObject ship = GameObject.Find ("ship_" + shipID);
		ship.GetComponent<Ship> ().Move (json);
	}

}
