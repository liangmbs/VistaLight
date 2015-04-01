using UnityEngine;
using System.Collections;
using SimpleJSON;
using System.Linq;
using System;

public class JSOCreatShip : MonoBehaviour {
	
	public GameObject breakbulk;
	public GameObject petrochemical;
	public GameObject bulk;
	public static bool accept;
	public ClientSocket send;


	/*
	 * Company's Icon and GUI Skin attached
	 */

	public Texture company1;
	//public Texture company2;
	//public Texture compnay3;
	//public Texture company4;



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
			print ("Creating ship:");
			print (json);
			CreatShip (N);
			break;

		case "ship move":

			int shipID = N ["vehicle"]["vehicle_id"].AsInt;
			MoveShip (N, shipID);
			break;

		}
	}

	/*
	 * Creat Ship and information to prefab
	 */

	 void CreatShip(JSONNode json){

		// Postion
		float x = json ["vehicle"]["position"]["x"].AsFloat;
		float y = json ["vehicle"]["position"]["y"].AsFloat;
		float z = json ["vehicle"]["position"]["z"].AsFloat;
		
		// Heading 
		float rZ = json ["vehicle"]["heading"].AsFloat;

		// Industry
		string industry = json["vehicle"]["company"]["industry"];
		print (industry);

		GameObject ship;
	
		// Creat Ship
		switch (industry) {

		case "BREAKBULK":
		
			ship = Instantiate (breakbulk, 
		            new Vector3 ((x / 1000.0f) - 50.0f, -((y / 1000.0f) - 50.0f), z), 
		            Quaternion.Euler (new Vector3 (0, 0, rZ))
					) as GameObject;
			break;
		

		case "PETROCHEMICAL":
		
			ship = Instantiate (petrochemical, 
				   new Vector3 ((x / 1000.0f) - 50.0f, -((y / 1000.0f) - 50.0f), z), 
				   Quaternion.Euler (new Vector3 (0, 0, rZ))
				   ) as GameObject;
			break;
		

		case "BULK":

			ship = Instantiate (bulk, 
			                    new Vector3 ((x / 1000.0f) - 50.0f, -((y / 1000.0f) - 50.0f), z), 
			                    Quaternion.Euler (new Vector3 (0, 0, rZ))
			                    ) as GameObject;
			break;

		case "PORT":
			
			ship = Instantiate (breakbulk, 
			                    new Vector3 ((x / 1000.0f) - 50.0f, -((y / 1000.0f) - 50.0f), z), 
			                    Quaternion.Euler (new Vector3 (0, 0, rZ))
			                    ) as GameObject;
			break;

		default: 
			
			throw new Exception("Do not support such form" + industry);
			
		}
	

		// Name Ship
		int shipID = json["vehicle"]["vehicle_id"].AsInt;
		print(shipID);
		ship.name = "ship_" + shipID;
		ship.GetComponent<Ship> ().icon = company1;

		// Set up Ship Information to Prefab
		ship.GetComponent<Ship>().SetupShip (json);
	}


	/*
	 * Move the Ship
	 */

	 public void MoveShip(JSONNode json, int shipID){

		GameObject ship = GameObject.Find ("ship_" + shipID);
		ship.GetComponent<Ship> ().Move (json);
	}

}
