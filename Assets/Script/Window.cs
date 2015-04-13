using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Window : MonoBehaviour {

	public GameObject ship;
	public GameObject window;
	public GameObject closed;
	public GameObject submited;
	public ClientSocket send;
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



	public void setShip(GameObject Ship){
		this.ship = Ship;
	}
	
	void Start(){
		ShipName = GameObject.Find ("/window/PropertyWindow/ShipName").GetComponent<Text> (); 
		CurrentPriority = GameObject.Find ("/window/PropertyWindow/CurrentPriority").GetComponent<Text> ();
		send = GameObject.Find("ClientSocketObject").GetComponent <ClientSocket>();
		PresentInformation ();
	


		closed.GetComponent<Button> ().onClick.AddListener (() => {
			closedwindow ();});
		submited.GetComponent<Button> ().onClick.AddListener (() => {
			submitedwindow ();});
	}


	void Update(){
		CurrentPriority.text = (ship.GetComponent<Ship> ().priority).ToString();
	}


	public void closedwindow(){
		Destroy (window);
	}

	public void submitedwindow(){
		//textfield = priority.text;
		//GetComponent<ShipPropertyWindow>().hitObject.GetComponent<Ship>().priority = int.Parse(textfield);
		print ("clicked");
//		ship.GetComponent <Ship> ().setpriority (priority.text);
		send.Send (ship.GetComponent <Ship>().shipID+"%"+priority.text+";");
	}


	public void PresentInformation(){
		ShipName.text= ship.GetComponent<Ship>().Name;

	}


}
