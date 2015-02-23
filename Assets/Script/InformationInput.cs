using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class InformationInput : MonoBehaviour {

	public Text CharacterName;
	public Text IPaddress;
	
	public void IPField(string inputFiledIP)
	{
		IPaddress.text = inputFiledIP;	
		
	}
	void Start() {

		CharacterName = GameObject.Find ("CharacterName").GetComponent<Text> ();
		IPaddress = GameObject.Find ("IPaddress").GetComponent<Text> ();
	}

	public void CharacterField(string inputFiledName)
	{
		CharacterName.text = inputFiledName;	
	}
}