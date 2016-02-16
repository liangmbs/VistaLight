using UnityEngine;
using System.Collections;

public class BottomAreaToggleButton : MonoBehaviour {

	public GameObject informationArea;
	public bool expanded = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Toggle() {
		if (expanded) {
			informationArea.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -210);
			expanded = false;
		} else {
			informationArea.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
			expanded = true;
		}
	}
}
