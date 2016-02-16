using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SubTrayController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void DeselectAllTools() {
		foreach (Transform child in gameObject.transform) {
			child.gameObject.GetComponent<Button>().image.color = Color.gray;
		}
	}
}
