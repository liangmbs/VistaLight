using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class ControlPanel: MonoBehaviour {


	public GameObject shipButton;
	public RectTransform freemovingPanel;
	public RectTransform mooredPanel;
	public RectTransform anchorPanel;
	public RectTransform underwayPanel;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private RectTransform getPanelbyStatus(string status){
	
		switch (status) {
			
		case "underway":
			return underwayPanel;
			
		case "freemoving":
			return freemovingPanel;
			
		case "moored":
			return mooredPanel;
			
		case "anchor":
			return anchorPanel;
			
		default:
			throw new Exception ("Do not support such form" + status);
		}
	}

	/*
	public void ClickedforWindow(GameObject ship){

		GameObject.Find ("Main Camera").GetComponent <WindowManager> ().ClickOnShip (ship);

	}
*/



	public void InstantiateButton(GameObject ship, string startstatus){
		GameObject newButton = (GameObject)Instantiate (shipButton, new Vector3 (1, 1, 1), Quaternion.identity);
		newButton.GetComponentInChildren<Text> ().text = ship.name;
		newButton.name = "button " + ship.name;
		RectTransform selectedPanel = getPanelbyStatus (startstatus);
		newButton.transform.SetParent (selectedPanel, false);
		newButton.transform.localScale = new Vector3 (1, 1, 1);
	}


	public void swithPanel(GameObject ship, string currentStatus, string updatedStatus){

		RectTransform current_Panel = getPanelbyStatus (currentStatus);
		RectTransform updated_Panel = getPanelbyStatus (updatedStatus);
		gameObject.transform.SetParent (updated_Panel.transform, false);
	}

	}


}
