using UnityEngine;
using System.Collections;
using System;

public class DockVO : MonoBehaviour {

	private Dock dock;

	public Dock Dock { 
		get { return dock; }
		set { dock = value; }
	}

	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	public void Update() {
		gameObject.transform.position = new Vector3(
				(float)dock.Node.X,
				(float)dock.Node.Y,
				-2);
		gameObject.transform.localScale = new Vector3(
				(float)(Camera.main.orthographicSize / 5),
				(float)(Camera.main.orthographicSize / 5),
				(float)1);

		foreach (DockType type in Enum.GetValues(typeof(DockType))) {
			if (type == dock.Type) {
				gameObject.transform.FindChild(type.ToString()).GetComponent<SpriteRenderer>().enabled = true;
			} else {
				gameObject.transform.FindChild(type.ToString()).GetComponent<SpriteRenderer>().enabled = false;
			}
		}

		if (Input.GetMouseButtonDown(1)) {
			RaycastHit2D ray = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
			if (ray.collider.gameObject == this.gameObject) {
				// map.RemoveDock(this);
			}
		}
	}


	public void OnMouseDrag() {
		RaycastHit2D ray = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
		dock.Node.X = ray.point.x;
		dock.Node.Y = ray.point.y;
	}
}
