using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour {

	public double sensitivity = 30;
	public int right_space = 0;

	bool dragging = false;
	private Vector3 dragOrigin;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float mouseWheel = Input.GetAxis("Mouse ScrollWheel");
		ZoomInAndOut(mouseWheel);

		MoveWithMouseLeftButton();
		MoveWithWASD();
	}

	void MoveWithMouseLeftButton() {
		RaycastHit2D ray = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
		if ((Input.GetMouseButtonDown(0) && (ray.collider == null || ray.collider.tag == "Background")) || Input.GetMouseButtonDown(2) ) {
			dragOrigin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			dragging = true;
			return;
		}

		if (!dragging) {
			return;
		}

		if (!(Input.GetMouseButton (0) || Input.GetMouseButton(2))) {
			dragging = false;
			return;
		}

		Vector3 pos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		Vector3 move = pos - dragOrigin;

		transform.Translate (-move, Space.World);
	}

	void MoveWithWASD() {
		double cameraSize = gameObject.GetComponent<Camera>().orthographicSize;
		double moveLength = cameraSize / sensitivity;
		if (Input.GetKey("w") || Input.GetKey(KeyCode.UpArrow)) {
			transform.position += new Vector3(
				0, (float)(moveLength), 0);
		} else if (Input.GetKey("a")|| Input.GetKey(KeyCode.LeftArrow)) {
			transform.position += new Vector3(
				(float)(-moveLength), 0, 0);
		} else if (Input.GetKey("s")|| Input.GetKey(KeyCode.DownArrow)) {
			transform.position += new Vector3(
				0, (float)(-moveLength), 0);
		} else if (Input.GetKey("d")|| Input.GetKey(KeyCode.RightArrow)) {
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
