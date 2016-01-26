using System;
using System.Collections.Generic;
using UnityEngine;

public class RoadTool : IMapEditorTool
{
	public MapController mapController;
	public float segmentLength = 1000;

	private GameObject currentNode = null;
	private bool isBiDirection = true;
	private bool isStarted = false;

	private GameObject nodePrefab;
	private GameObject connectionPrefab;
	private GameObject tempNode;

	private GameObject tempRoad;
	private List<GameObject> nodeInTempRoad = new List<GameObject>();
	private List<GameObject> connectionInTempRoad = new List<GameObject>(); 


	public RoadTool (MapController mapController)
	{
		this.mapController = mapController;
		nodePrefab = Resources.Load("Node", typeof(GameObject)) as GameObject;
		connectionPrefab = Resources.Load("Connection", typeof(GameObject)) as GameObject;
		
		this.ShowTemporaryNode();
		tempRoad = new GameObject();
	}

	public bool BiDirection {
		get { return isBiDirection; }
		set { isBiDirection = value;  }
	}

	private Vector2 MousePosition() {
		RaycastHit2D ray = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
		return ray.point;
	}

	private GameObject MouseOnNode() {
		RaycastHit2D ray = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
		if (ray.collider != null && ray.collider.tag == "Node") {
			return ray.collider.gameObject;
		}
		return null;
	}

	public void Destory() {
		UnityEngine.Object.Destroy(tempRoad);
		UnityEngine.Object.Destroy(tempNode);
	}

	public void RespondMouseLeftClick() {
		if (!isStarted) {
			GameObject nodeMouseOn = MouseOnNode();
			if (nodeMouseOn != null) {
				currentNode = nodeMouseOn;
				SelectNode(currentNode);
			} else {
				PutNode();
			}
			isStarted = true;
			tempNode.SetActive(false);
			tempRoad.SetActive(true);
			UpdateTemporaryRoad();
		} else {
			PutRoad(false, PutNodeOnRoad, PutConnectionOnRoad);
		}
	}

	private void SelectNode(GameObject node) { 
		GameObject.Find("MapEditorController").GetComponent<MapEditorController>().SelectOne(node.GetComponent<NodeVO>());
	}

	private void PutNode() {
		currentNode = mapController.AddNode(MousePosition());
		SelectNode(currentNode);
	}

	public void RespondMouseLeftUp() {
	}

	public void RespondMouseMove(float x, float y) {
		if (!isStarted) {
			UpdateTemporaryNodePosition();
		} else {
			UpdateTemporaryRoad();
		}
	}

	private void ShowTemporaryNode() {
		tempNode = GameObject.Instantiate(nodePrefab);
		tempNode.tag = "Untagged";
		tempNode.GetComponent<CircleCollider2D>().enabled = false;
		tempNode.transform.FindChild("NodeDot").GetComponent<SpriteRenderer>().color = Color.blue;
	}

	private void UpdateTemporaryNodePosition() {
		Vector2 mousePosition = MousePosition();
		GameObject nodeMouseOn = MouseOnNode();
		if (nodeMouseOn != null) {
			mousePosition.x = nodeMouseOn.transform.position.x;
			mousePosition.y = nodeMouseOn.transform.position.y;
		}
		Node node = tempNode.GetComponent<NodeVO>().node;
		node.X = mousePosition.x;
		node.Y = mousePosition.y;
	}

	private void UpdateTemporaryRoad() {
		foreach (Transform child in tempRoad.transform) {
			child.gameObject.SetActive(false);	
		}

		PutRoad(true, PutTempNodeOnRoad, PutTempConnectionOnRoad);
	}

	private GameObject PutTempNodeOnRoad(Vector2 position, int index) {
		GameObject nodeGO = null;
		if (index < nodeInTempRoad.Count) {
			nodeGO = nodeInTempRoad[index]; 
		} else {
			nodeGO = GameObject.Instantiate(nodePrefab);
			nodeInTempRoad.Add(nodeGO);
		}

		nodeGO.SetActive(true);
		NodeVO nodeVO = nodeGO.GetComponent<NodeVO>();
		Node node = new Node();
		nodeVO.node = node;
		node.X = position.x;
		node.Y = position.y;
		nodeVO.Update();
		nodeGO.tag = "Untagged";
		nodeGO.transform.SetParent(tempRoad.transform);
		nodeGO.transform.FindChild("NodeDot").GetComponent<SpriteRenderer>().color = Color.blue;
		
		return nodeGO;
	}

	private GameObject PutTempConnectionOnRoad(GameObject fromNode, GameObject toNode) {
		return null;	
	}

	private GameObject PutNodeOnRoad(Vector2 position, int index) {
		GameObject node = mapController.AddNode(position);
		currentNode = node;
		SelectNode(node);
		return node;
	}

	private GameObject PutConnectionOnRoad(GameObject fromNode, GameObject toNode) {
		GameObject connection = mapController.AddConnection(fromNode, toNode, isBiDirection);
		return connection;
	}

	private void PutRoad(bool isTemp, Func<Vector2, int, GameObject> putNodeOnRoad, Func<GameObject, GameObject, GameObject> putConnection) {
		RaycastHit2D ray = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
		bool endOnNode = false;
		GameObject endNode = null;
		Vector2 mousePosition = MousePosition();

		if (ray.collider != null && ray.collider.tag == "Background") {

		} else if (ray.collider != null && ray.collider.tag == "Node") {
			endOnNode = true;
			endNode = ray.collider.gameObject;
			mousePosition = new Vector2(endNode.transform.position.x, endNode.transform.position.y);
		}
		Debug.Log(endOnNode);

		Node node = currentNode.GetComponent<NodeVO>().node;
		Vector2 currentPosition = new Vector2((float)node.X, (float)node.Y);
		Vector2 vector = mousePosition - currentPosition;
		double distance = vector.magnitude;
		int numSegment = (int)Math.Ceiling(distance / segmentLength);
		double lengthPerSegment = distance / numSegment;
		GameObject previousNode = currentNode;

		if (endOnNode) {
			numSegment = numSegment - 1;		
		}

		for (int i = 0; i < numSegment; i++) {
			currentPosition = Vector2.MoveTowards(currentPosition, mousePosition, (float)lengthPerSegment);
			GameObject nextNode = putNodeOnRoad(currentPosition, i);
			putConnection(previousNode, nextNode);
			previousNode = nextNode;
		}

		if (endOnNode) {
			putConnection(previousNode, endNode);
			if (!isTemp) {
				currentNode = endNode;
				SelectNode(currentNode);
			}
		}
	}

	public void RespondMouseRightClick() {
		this.isStarted = false;
		this.tempRoad.SetActive(false);
		UpdateTemporaryNodePosition();
        tempNode.SetActive(true);
	}

	public bool CanDestroy() {
		if(isStarted) {
			return false;
		} else {
			return true;
		}
	}
}


