using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using SimpleJSON;
using System.Linq;
using System;

public class JsonCreateShip : MonoBehaviour {

	public static bool accept;
	public Text time;
	public DateTime dateValue;
	

	void Start(){
		time = GameObject.Find ("/UI/TimeBoard/Time").GetComponent<Text> (); 
	}

	void Update(){
		 
	}

	/*
	 * Detect the Ship Action
	 */
	public void Detection(string json){

		JSONNode N = JSON.Parse (json);
		string action = N["action"];

		switch (action) {

		case "time":
			string showtime = N["time"];
			Showtime(showtime);
			break;

		case "ship create":
			GameObject.Find ("ShipsManager").GetComponent<ShipsManager>().CreateShip (N);
			break;

		case "ship move":
			int shipID = N ["vehicle"]["vehicle_id"].AsInt;
			GameObject.Find ("ShipsManager").GetComponent<ShipsManager>().MoveShip(N,shipID);
			GameObject.Find ("ShipsManager").GetComponent<ShipsManager>().updateShipInformation (N);
			break;

		case "ship remove":
			int removedshipID = N ["vehicle"]["vehicle_id"].AsInt;
			GameObject.Find ("ShipsManager").GetComponent<ShipsManager>().RemoveShip(N, removedshipID);
			//GameObject.Find ("ShipManager").GetComponent<ShipsManager>().updateShipInformation (N);
			break;

		case "score update":
			updateScore(N);
			break;

		case "game over":
			Application.LoadLevel("GameOverScene");
			break;
		}
	}

	/*
	 * Creat Ship and information to prefab
	 */

	public void updateScore(JSONNode json){
		float moneyScore = json["score"]["score"].AsFloat;
		GameObject.Find ("ScoreManager").GetComponent<ScoreManager> ().score = moneyScore;
 		GameObject.Find("UI").GetComponent<BarPresent>().currentMoney = moneyScore;
	}

	public void Showtime(string globaltime){
			if (DateTime.TryParse (globaltime, out dateValue)) {
			time.text = dateValue.ToString();
		}
			else
				Console.WriteLine("unable to parse", globaltime);
		}

}
