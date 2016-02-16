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

	public GameObject tab1;
	public GameObject tab2;
	public GameObject tab3;




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
		

	public void ShowTab1(){
		tab1.transform.FindChild ("Panel").gameObject.SetActive(true);
		tab2.transform.FindChild ("Panel").gameObject.SetActive(false);
		tab3.transform.FindChild ("Panel").gameObject.SetActive(false);
	}

	public void ShowTab2(){
		tab1.transform.FindChild ("Panel").gameObject.SetActive(false);
		tab2.transform.FindChild ("Panel").gameObject.SetActive(true);
		tab3.transform.FindChild ("Panel").gameObject.SetActive(false);
	}

	public void ShowTab3(){
		tab1.transform.FindChild ("Panel").gameObject.SetActive(false);
		tab2.transform.FindChild ("Panel").gameObject.SetActive(false);
		tab3.transform.FindChild ("Panel").gameObject.SetActive(true);
	}




}
		