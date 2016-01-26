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
			float slidetime = 10.0f;
			infoBar.GetComponent<RectTransform> ().transform.Translate (0, 200, 0);
			toggleButton.transform.rotation= Quaternion.Euler(0,0,180);
		} else {
			infoBar.GetComponent<RectTransform> ().transform.Translate (0, -200, 0);
			toggleButton.transform.rotation= Quaternion.Euler(0,0,0);

		}
	}


}
		