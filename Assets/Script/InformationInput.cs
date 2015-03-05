using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class InformationInput : MonoBehaviour {

	public Text CharacterName;
	public Text IPaddress;
	public GameObject ClientSocketObject;
	private ClientSocket connection;

	void Start() {

		CharacterName = GameObject.Find ("CharacterName").GetComponent<Text> ();
		//CharacterName = CharacterName.ToString ();
		IPaddress = GameObject.Find ("IPaddress").GetComponent<Text> ();
		//IPaddress = IPaddress.convert.ToString ();
	}

	/*void Awake(){

		//DontDestroyOnLoad()
	}*/


	public void CharacterField(string inputFiledName)
	{
		CharacterName.text = inputFiledName;	
	}	

	public void IPField(string inputFiledIP)
	{
		IPaddress.text = inputFiledIP;	
		
	}

	public void ConfirmInformation(){
		PlayerPrefs.SetString ("CharacterName", CharacterName.text);
		PlayerPrefs.SetString ("IPaddress", IPaddress.text);
		print (PlayerPrefs.GetString ("IPaddress"));
	}

	public void click()
	{
		ConfirmInformation ();
		connection = ClientSocketObject.GetComponent<ClientSocket>();
		connection.setupSocket ();
		Application.LoadLevel("GamePlay");
		
	}

	/*void OnGUI(){

		if (GUI.Button (new Rect (Screen.width * 0.75f, Screen.height * 0.1f, Screen.width * .2f, Screen.height * .1f),  "Confirm")) {
				
		}*/
	/*void buttonSetup(Button button)
	{
		button.onClick.
	}*/
	                          


}