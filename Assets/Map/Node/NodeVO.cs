using UnityEngine;
using System.Collections;
using System;

public class NodeVO : MonoBehaviour {

	public Node node;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (node != null) {
			gameObject.transform.position = node.Position;
			gameObject.transform.localScale = new Vector3(
				Camera.main.orthographicSize / 300, Camera.main.orthographicSize / 300, 1);
		}
	}

	public void OnMouseDrag() {
		RaycastHit2D ray = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
		node.Position = new Vector3(ray.point.x, ray.point.y, 0);
	}

}
