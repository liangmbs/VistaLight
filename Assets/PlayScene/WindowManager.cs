using UnityEngine;
using System.Collections;
using System;
using SimpleJSON;
using UnityEngine.UI;
using System.Collections.Generic; 

public class WindowManager: MonoBehaviour {

	public Button toggleButton;

	public GameObject infoBar;
	public bool bottomPanelOpen = false;

	public GameObject tab1;
	public GameObject tab2;
	public GameObject tab3;

	public Camera ViewPortCamera;

	public void UpdateCameraSize() {
		int screenWidth = Screen.width;
		int screenHeight = Screen.height;

		float rectWidth = 1;

		float rectHeight = 1;
		if (bottomPanelOpen) {
			rectHeight = (float) (1.0 * (screenHeight - 240) / screenHeight);
		} else {
			rectHeight = (float) (1.0 * (screenHeight - 40) / screenHeight);
		}
		 
		ViewPortCamera.rect = new Rect (0, 1 - rectHeight, rectWidth, rectHeight);

	}

	public void OpenBottomPanel() {
		bottomPanelOpen = true;

		infoBar.GetComponent<RectTransform> ().transform.Translate (0, 200, 0);
		toggleButton.transform.rotation = Quaternion.Euler(0,0,180);

		UpdateCameraSize ();
	}

	public void CloseBottomPanel() {
		bottomPanelOpen = false;

		infoBar.GetComponent<RectTransform> ().transform.Translate (0, -200, 0);
		toggleButton.transform.rotation = Quaternion.Euler(0,0,0);

		UpdateCameraSize ();
	}


	public void ToggleBottomInformationPanel(){
		if (!bottomPanelOpen) {
			OpenBottomPanel ();
		} else {
			CloseBottomPanel ();
		}
	}
		

	public void ShowTab1(){
		tab1.gameObject.transform.SetSiblingIndex (3);
		tab2.transform.FindChild ("Panel").gameObject.SetActive(false);
		tab3.transform.FindChild ("Panel").gameObject.SetActive(false);

		OpenBottomPanel ();
	}

	public void ShowTab2(){
		tab2.gameObject.transform.SetSiblingIndex (2);
		tab2.transform.FindChild ("Panel").gameObject.SetActive(true);
		tab3.transform.FindChild ("Panel").gameObject.SetActive(false);

		OpenBottomPanel ();
	}

	public void ShowTab3(){
		tab3.transform.FindChild ("Panel").gameObject.SetActive(true);
		tab3.gameObject.transform.SetSiblingIndex (3);

		OpenBottomPanel ();
	}




}
		