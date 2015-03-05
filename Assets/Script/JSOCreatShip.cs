using UnityEngine;
using System.Collections;
using SimpleJSON;
using System.Linq;

public class JSOCreatShip : MonoBehaviour {
	public TextAsset  Json;
	public GameObject breakbulk;



	void Update(){
		//JSONNode N = JSON.Parse (Json.text);
		//string action = N["action"]; //access object member
		//Detection (N.Serialize());

	}


	public void Detection(string json){
		JSONNode N = JSON.Parse (json);
		//print (json);
		string action = N["action"]; 
		switch (action) {
		case "ship create":
			//postion
			float x = N ["position"]["x"].AsFloat;
			float y = N ["position"]["y"].AsFloat;
			float z = N ["position"]["z"].AsFloat;

			//Heading 
			float Anglex = N ["heading"]["x"].AsFloat;
			float Angley = N ["heading"]["y"].AsFloat;
			float Anglez = N ["heading"]["z"].AsFloat;
			
			//Vehicle ID
			
			string shipID = N["VehicleID"];
			CreatShip(x, y, z, Anglex, Angley,  Anglez,  shipID);
			break;
		
		case "ship move":
			print ("moved");
			break;

		}

	}

	 void CreatShip(float x, float y, float z, float Anglex, float Angley, float Anglez, string shipID){
		GameObject ship = Instantiate (breakbulk, new Vector3 ((x/1000.0f)-50.0f, -((y/1000.0f)-50.0f), z), Quaternion.Euler (new Vector3 (Anglex, Angley, Anglez))) as GameObject;
		ship.name = "ship_" + shipID;
	}


}
