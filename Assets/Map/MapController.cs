using UnityEngine;
using System.Collections;
using System;

public class MapController : MonoBehaviour {

	public static readonly double MapZIndex = -1;

	private Map map;

	public GameObject nodePrefab;
	public GameObject connectionPrefab;
	public GameObject dockPrefab;

	private int nextNodeId = 1;

	public Map Map { 
		get { return map; }
		private set { map = value; }
	}

	public GameObject AddNode(Vector3 position) {
		Node node = new Node();
		node.Id = nextNodeId;
		node.X = position.x;
		node.Y = position.y;
		nextNodeId++;
		map.AddNode(node);

		// Instantiate connection gameobject
		GameObject nodeGO = CreateNodeGameObject(node);

		return nodeGO;
	}

	private GameObject CreateNodeGameObject(Node node) {
		GameObject nodeGO = Instantiate(nodePrefab, Vector3.zero, Quaternion.identity) as GameObject;
		nodeGO.GetComponent<NodeVO>().node = node;
		nodeGO.transform.parent = GameObject.Find("Map").transform;

		return nodeGO;
	}

	public GameObject AddConnection(GameObject start, GameObject end, bool isBidirectional) {
		Connection connection = new Connection();

		// Set the parameters of the connection
		connection.StartNode = start.GetComponent<NodeVO>().node;
		connection.EndNode = end.GetComponent<NodeVO>().node;
		connection.Bidirectional = isBidirectional;
		map.AddConnection(connection);

		// Instantiate connection gameobject
		GameObject connectionGO = CreateConnectionGameObject(connection);
		
		// Return the newly created connection
		return connectionGO;
	}

	private GameObject CreateConnectionGameObject(Connection connection) { 
		// Instantiate connection gameobject
		GameObject connectionGO = Instantiate(connectionPrefab, Vector3.zero, Quaternion.identity) as GameObject;
		connectionGO.GetComponent<ConnectionVO>().connection = connection;
		connectionGO.transform.parent = GameObject.Find("Map").transform;

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
	
	public void RemoveNode(GameObject gameObject) {
		throw new NotImplementedException();
	}


	public void RegenerateMap(Map map) {
		this.map = map;

		// Regenerate all nodes
		foreach (Node node in map.nodes) {
			CreateNodeGameObject(node);
		}

		// Regenerate all connections
		foreach (Connection connection in map.connections) {
			CreateConnectionGameObject(connection);
		}
		
    }

	public void CloseMap() {
		// Clean all game objects
		foreach (Transform child in GameObject.Find("Map").transform) {
			Destroy(child.gameObject);
		}
	}
}

