using UnityEngine;
using System.Collections;
using System;

public class MapController : MonoBehaviour {

	private Map map;

	public GameObject nodePrefab;
	public GameObject connectionPrefab;
	public GameObject dockPrefab;

	private int nextNodeId = 1;

	public Map Map { 
		get { return map; }
		set { map = value; }
	}

	public GameObject AddNode(Vector3 position) {
		Node node = new Node();
		node.Id = nextNodeId;
		node.Map = this;
		node.Position = new Vector3(position.x, position.y, -1);
		nextNodeId++;

		// Instantiate connection gameobject
		GameObject nodeGO = Instantiate(nodePrefab, Vector3.zero, Quaternion.identity) as GameObject;
		nodeGO.GetComponent<NodeVO>().node = node;

		return nodeGO;
	}

	public GameObject AddConnection(GameObject start, GameObject end, bool isBidirectional) {
		Connection connection = new Connection();

		// Set the parameters of the connection
		connection.StartNode = start.GetComponent<NodeVO>().node;
		connection.EndNode = end.GetComponent<NodeVO>().node;
		connection.Bidirectional = isBidirectional;

		// Instantiate connection gameobject
		GameObject connectionGO = Instantiate(connectionPrefab, Vector3.zero, Quaternion.identity) as GameObject;
		connectionGO.GetComponent<ConnectionVO>().connection = connection;

		// Return the newly created connection
		return connectionGO;
	}

	public void SelectNode(GameObject node) {
		node.transform.FindChild("NodeDot").GetComponent<SpriteRenderer>().enabled = false;
		node.transform.FindChild("NodeDotSelected").GetComponent<SpriteRenderer>().enabled = true;
	}

	public void DeselectNode(GameObject node) {
		if (node != null) {
			node.transform.FindChild("NodeDot").GetComponent<SpriteRenderer>().enabled = true;
			node.transform.FindChild("NodeDotSelected").GetComponent<SpriteRenderer>().enabled = false;
		}
	}

	internal void RemoveNode(GameObject gameObject) {
		throw new NotImplementedException();
	}
}

