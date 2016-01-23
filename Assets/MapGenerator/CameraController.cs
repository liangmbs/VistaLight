using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float mouseWheel = Input.GetAxis("Mouse ScrollWheel");
		ZoomInAndOut(mouseWheel);

		double widthPercent = ((double)Screen.width - 200.0) / (double)Screen.width;
		double heightPercent = ((double)Screen.height - 80.0) / (double)Screen.height;
		gameObject.GetComponent<Camera>().rect = new Rect(
			0, (float)(1-heightPercent), 
			(float)widthPercent, 
			(float)heightPercent);
	}

	void ZoomInAndOut(float mouseWheel) {

		// If over gui, do not zoom
		if (EventSystem.current.IsPointerOverGameObject()) {
			return;
		}

		double scrollSpeed = 1;
		double minCameraSize = 100;
		double maxCameraSize = 50000;

		if (mouseWheel != 0) {
			double cameraSize = gameObject.GetComponent<Camera>().orthographicSize;
			cameraSize = cameraSize - mouseWheel * cameraSize * scrollSpeed;
			if (cameraSize < minCameraSize) cameraSize = minCameraSize;
			if (cameraSize > maxCameraSize) cameraSize = maxCameraSize;
			gameObject.GetComponent<Camera>().orthographicSize = (float)cameraSize;
		}
	}
}
