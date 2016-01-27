using UnityEngine;
using System.Collections;
using System;

public class DockVO : MonoBehaviour, MapSelectableVO {

	public Dock dock;

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
				(float)dock.node.X,
				(float)dock.node.Y,
				-2);
		gameObject.transform.localScale = new Vector3(
				(float)(Camera.main.orthographicSize / 10),
				(float)(Camera.main.orthographicSize / 10),
				(float)1);

		foreach (DockType type in Enum.GetValues(typeof(DockType))) {
			if (type == dock.type) {
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
		dock.node.X = ray.point.x;
		dock.node.Y = ray.point.y;
	}

	public void Select() {
		gameObject.transform.FindChild("SelectCircle").gameObject.SetActive(true);
	}

	public void Deselect() {
		gameObject.transform.FindChild("SelectCircle").gameObject.SetActive(false);
	}

    public GameObject GetSidePanel()
    {
        return null;
    }
}
