using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public enum DockType { 
	Petro, Bulk, BreakBulk, Port
}

public class Dock : MonoBehaviour {
	private Node node;
	private DockType type;
	private Map map;

	public Node Node {
		get { return node; }
		set { node = value;  }
	}

	public DockType Type { 
		get { return type; }
		set { type = value; }
	}

	public Map Map { 
		get { return map; }
		set { map = value; }
	}

	public void UpdateGameObject() {
		foreach (DockType type in Enum.GetValues(typeof(DockType))) {
			if (type == this.type) {
				gameObject.transform.FindChild(type.ToString()).GetComponent<SpriteRenderer>().enabled = true;
			} else {
				gameObject.transform.FindChild(type.ToString()).GetComponent<SpriteRenderer>().enabled = false;
			}
		}
	}

	public void Update() {
		gameObject.transform.position = new Vector3(
				node.Position.x, 
				node.Position.y, 
				-2);
		if (Input.GetMouseButtonDown(1)) {
			RaycastHit2D ray = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
			if (ray.collider.gameObject == this.gameObject) {
				map.RemoveDock(this);
			}
		}
	}

	public void OnMouseDrag() {
		RaycastHit2D ray = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
		node.Position = new Vector3(ray.point.x, ray.point.y, -1);
	}


}
