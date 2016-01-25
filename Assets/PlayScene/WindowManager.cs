using UnityEngine;
using System.Collections;
using System;
using SimpleJSON;
using UnityEngine.UI;
using System.Collections.Generic; 

public class WindowManager: MonoBehaviour {

	public Button toggleButton;
	public GameObject infoBar;
	public bool open = false;

	public void ToggleBottomInformationPanel(){
		open = !open;
		if (open) {
			infoBar.GetComponent<RectTransform> ().localPosition= new Vector3(0,0,0);
		}
	}


}
		