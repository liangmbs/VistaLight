using UnityEngine;
using System.Collections;

public class MapEventVO : MonoBehaviour {

	public MapEvent MapEvent;

	void Start () {
	
	}
	
	void Update () {
		gameObject.transform.position = new Vector3(
			(float)MapEvent.X, 
			(float)MapEvent.Y, 
			(float)MapController.MapEventZIndex);
		gameObject.transform.localScale = new Vector3(
			(float)(Camera.main.orthographicSize / 20),
			(float)(Camera.main.orthographicSize / 20),
			(float)1);
	}
}
