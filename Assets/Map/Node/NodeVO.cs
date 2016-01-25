using UnityEngine;
using System.Collections;
using System;

public class NodeVO : MonoBehaviour {

	public Node node;

	// Use this for initialization
	public void Start () {
	
	}
	
	// Update is called once per frame
	public void Update () {
		if (node != null) {
			gameObject.transform.position = new Vector3((float)node.X, (float)node.Y, (float)MapController.MapZIndex);
			gameObject.transform.localScale = new Vector3(
				Camera.main.orthographicSize / 400, Camera.main.orthographicSize / 400, 1);
		}
	}

	public void OnMouseDrag() {
		RaycastHit2D ray = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
		node.X = ray.point.x;
		node.Y = ray.point.y;
	}

}
