using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic; 
using SimpleJSON;


public class ShipsManager : MonoBehaviour {


	public GameObject breakbulk;
	public GameObject petrochemical;
	public GameObject bulk;

	// List of all ships
	SortedDictionary<int, GameObject> ships = 
		new SortedDictionary<int, GameObject> ();

	SortedDictionary<int, GameObject> mooredships = 
		new SortedDictionary<int, GameObject> ();

	SortedDictionary<int, GameObject> freemovingships =
		new SortedDictionary<int, GameObject> ();

	SortedDictionary<int, GameObject> anchorships =
		new SortedDictionary<int, GameObject> ();

	SortedDictionary<int, GameObject> underwaysships =
		new SortedDictionary<int, GameObject> ();
	
	void Update(){
	}
		
	/*
	 * Move the Ship
	 */
	
	public void MoveShip(JSONNode json, int shipID){
		
		GameObject ship = GameObject.Find ("ship_" + shipID);
		ship.GetComponent<Ship> ().Move (json);
	}

	/*
	 * Remove the Ship
	 */
	public void RemoveShip(JSONNode json, int shipID){
		GameObject ship = GameObject.Find ("ship_" + shipID);
		GameObject.Destroy (ship);
		ships.Remove (shipID);
	}

	/*
	 * Update Ships information
	 */
	public void updateShipInformation(JSONNode json)
	{
		int updateshipid = json["vehicle"]["vehicle_id"].AsInt;
		GameObject ship = GameObject.Find ("ship_" + updateshipid);
		UpdateStatustoLists (json,ship);
		ship.GetComponent<Ship> ().updateInformation(json);

	}
	
	/*
	 * Creat Ship and information to prefab
	 */
	public void CreatShip(JSONNode json){
		
		// Postion
		float x = json ["vehicle"]["position"]["x"].AsFloat;
		float y = json ["vehicle"]["position"]["y"].AsFloat;
		float z = json ["vehicle"]["position"]["z"].AsFloat;
		
		// Heading 
		float rZ = json ["vehicle"]["heading"].AsFloat;
		
		// Industry
		string industry = json["vehicle"]["company"]["industry"];
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
		ship.name = "ship_" + shipID;

		// Set up Ship Information to Prefab
		ship.GetComponent<Ship>().SetupShip (json);

		//adding ships to the shipmanager's list
		ships.Add (shipID, ship);
	}



	public void UpdateStatustoLists(JSONNode json, GameObject ship){

		string currentstatus = ship.GetComponent<Ship> ().status;
		string updatedstatus = json["vehicle"]["status"];
		int priority = ship.GetComponent<Ship> ().priority;

		if (currentstatus != updatedstatus) {
			UpdateList(ship, updatedstatus, currentstatus, priority);
		}
	}

	private SortedDictionary <int, GameObject> getListByStatus(string status){
	switch (status) {

		case "underway":
			return underwaysships;
			break;

		case "freemoving":
			return freemovingships;
			break;

		case "moored":
			return mooredships;
			break;

		case "anchor":
			return anchorships;
			break;

		default:
			throw new Exception("Do not support such form" + status);

		}

	}

	public void UpdateList(GameObject ship, string updatedStatus, string currentStatus, int priority){

		// Get current list
		SortedDictionary<int, GameObject> current_list = getListByStatus (currentStatus);

		// Remove ship from current_list
		current_list.Remove (priority);


		// Get updated list
		SortedDictionary<int, GameObject> updated_list = getListByStatus (updatedStatus);
		
		// Insert into new list
		updated_list.Add (priority, ship);
	
	}
}
