using UnityEngine;
using System.Collections;
using System;
using SimpleJSON;
using UnityEngine.UI;
using System.Collections.Generic; 

public class Tab2Manager: MonoBehaviour {

	public Button submitButton;

	public Toggle toggle1;
	public Toggle toggle2;
	public Toggle toggle3;
	public Toggle toggle4;



	public ToggleGroup togglegroup;


	void Update(){
		if (togglegroup.AnyTogglesOn ()) {
			//submitButton.gameObject.SetActive (true);
		}

	}

	public void Submit(){
		togglegroup.SetAllTogglesOff ();
		submitButton.gameObject.SetActive (false);
	}

}
		