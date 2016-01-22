using UnityEngine;
using System.Collections;

public class MapEventVO : MonoBehaviour {

	public MapEvent MapEvent;
	public bool IsSelected = false;

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

		if (IsSelected) {
			gameObject.transform.FindChild("EventSelected").gameObject.SetActive(true);
			gameObject.transform.FindChild("Event").gameObject.SetActive(false);
		} else { 
			gameObject.transform.FindChild("EventSelected").gameObject.SetActive(false);
			gameObject.transform.FindChild("Event").gameObject.SetActive(true);

		}
	}

	public void OnMouseDrag() {
		RaycastHit2D ray = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
		MapEvent.X = ray.point.x;
		MapEvent.Y = ray.point.y;
	}
}
