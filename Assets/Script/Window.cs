using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Window : MonoBehaviour {

	public GameObject ship;
	public GameObject window;
	public GameObject closed;
	public GameObject submited;
	public ClientSocket send;
	private GameObject destinationpoint;
	public GameObject destination;

	/*
	 * input text to send
	 */
	public InputField priority;

	/*
	 *  Ship's information
	 */
	public Text ShipName;
	public Text ShipType;
	public Text CurrentPriority;
	public Text ShipCapacity;
	public Text ScheduleTime;

	public float destinationpointX;
	public float destinationpointy;
	

	public void setShip(GameObject Ship){
		this.ship = Ship;
	}
	
	void Start(){
		ShipName = GameObject.Find ("/window/PropertyWindow/ShipName").GetComponent<Text> (); 
		CurrentPriority = GameObject.Find ("/window/PropertyWindow/CurrentPriority").GetComponent<Text> ();
		send = GameObject.Find("ClientSocketObject").GetComponent <ClientSocket>();
		ShipCapacity = GameObject.Find ("/window/PropertyWindow/ShipCapacity").GetComponent<Text>();
		ScheduleTime = GameObject.Find ("/window/PropertyWindow/ShipTime").GetComponent<Text> ();
		destinationpointX = ship.GetComponent<Ship>().destinationX;
		destinationpointy = ship.GetComponent<Ship>().destinationY;
		destinationpoint = Instantiate (destination, new Vector3(destinationpointX,destinationpointy), Quaternion.identity) as GameObject;
		destinationpoint.name = "destination";
		PresentInformation ();
	


		closed.GetComponent<Button> ().onClick.AddListener (() => {
			closedwindow ();});
		submited.GetComponent<Button> ().onClick.AddListener (() => {
			submitedwindow ();});
	}


	void Update(){
		CurrentPriority.text = (ship.GetComponent<Ship> ().priority).ToString();
		ShipCapacity.text = (ship.GetComponent <Ship> ().cargo).ToString ();
		ScheduleTime.text = ship.GetComponent<Ship> ().scheduletime;
	}


	public void closedwindow(){
		Destroy (window);
		Destroy (destinationpoint);
	}

	public void submitedwindow(){
		//textfield = priority.text;
		//GetComponent<ShipPropertyWindow>().hitObject.GetComponent<Ship>().priority = int.Parse(textfield);
		print ("clicked");
//		ship.GetComponent <Ship> ().setpriority (priority.text);

		string str = "{" +
			"\"action\":\"change priority\", " +
			"\"data\": {" +
				"\"vehicle_id\": " + ship.GetComponent <Ship> ().shipID + "," +
				"\"priority\": " + priority.text + "" +
				"}}";
		send.Send (str);
	}


	public void PresentInformation(){
		ShipName.text= ship.GetComponent<Ship>().Name;

	}


}
