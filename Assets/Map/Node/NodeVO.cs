using UnityEngine;
using System.Collections;
using System;

public class NodeVO : MonoBehaviour, MapSelectableVO {

	public Node node;
    public GameObject sidePanel;

	public void Start () {
	
	}
	
	public void Update () {
		if (node != null) {
			gameObject.transform.position = new Vector3((float)node.X, (float)node.Y, (float)MapController.MapZIndex);
			gameObject.transform.localScale = new Vector3(
				Camera.main.orthographicSize / 200, Camera.main.orthographicSize / 200, 1);

			if (node.IsExit) {
				gameObject.transform.Find("Exit").gameObject.SetActive(true);
			}
		}
	}

	public void OnMouseDrag() {
		RaycastHit2D ray = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
		if (ray.point.x == 0 && ray.point.y == 0) return;
		node.X = ray.point.x;
		node.Y = ray.point.y;
	}

	public void Select() {
		gameObject.transform.FindChild("NodeDot").gameObject.SetActive(false);
		gameObject.transform.FindChild("NodeDotSelected").gameObject.SetActive(true);
	}

	public void Deselect() {
		gameObject.transform.FindChild("NodeDot").gameObject.SetActive(true);
		gameObject.transform.FindChild("NodeDotSelected").gameObject.SetActive(false);
	}

    public GameObject GetSidePanel()
    {
		GameObject sidePanel = GameObject.Find("SidePanels").transform.FindChild("NodeInformationSidePanel").gameObject;
		NodeInformationSidePanelController controller = sidePanel.GetComponent<NodeInformationSidePanelController>();
		controller.node= node;
		controller.UpdateDisplay();
		return sidePanel;
	}
}
