using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour {

	public double sensitivity = 30;
	public int right_space = 0;
	public int bottom_space = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float mouseWheel = Input.GetAxis("Mouse ScrollWheel");
		ZoomInAndOut(mouseWheel);

		MoveWithMouseMiddleButton();
		MoveWithWASD();

		double widthPercent = ((double)Screen.width - right_space) / (double)Screen.width;
		double heightPercent = ((double)Screen.height - bottom_space) / (double)Screen.height;
		gameObject.GetComponent<Camera>().rect = new Rect(
			0, (float)(1-heightPercent), 
			(float)widthPercent, 
			(float)heightPercent);
	}

	void MoveWithMouseMiddleButton() {
		if (Input.GetMouseButton(2)) {
			double cameraSize = gameObject.GetComponent<Camera>().orthographicSize;
			transform.position += new Vector3(
				(float)(-Input.GetAxis("Mouse X") * cameraSize / sensitivity),
				(float)(-Input.GetAxis("Mouse Y") * cameraSize / sensitivity), 0);
		}
	}

	void MoveWithWASD() {
		double cameraSize = gameObject.GetComponent<Camera>().orthographicSize;
		double moveLength = cameraSize / sensitivity;
		if (Input.GetKey("w")) {
			transform.position += new Vector3(
				0, (float)(moveLength), 0);
		} else if (Input.GetKey("a")) {
			transform.position += new Vector3(
				(float)(-moveLength), 0, 0);
		} else if (Input.GetKey("s")) {
			transform.position += new Vector3(
				0, (float)(-moveLength), 0);
		} else if (Input.GetKey("d")) {
			transform.position += new Vector3(
				(float)(moveLength), 0, 0);
		}
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
