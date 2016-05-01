using UnityEngine;
using System.Collections;

public class QuitGameButtonController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ClickHandler() {
		Application.Quit ();
		Debug.Break ();
	}
}
