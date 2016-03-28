using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeLabelVO : MonoBehaviour {

	public Timer timer;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		string labelText = timer.VirtualTime.ToString("HH:mm\nMM/dd/yyyy");
		gameObject.GetComponent<Text>().text = labelText;
	}
}
