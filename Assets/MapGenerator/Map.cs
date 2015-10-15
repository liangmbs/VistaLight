using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Map : MonoBehaviour {

	public GameObject nodePreFab;
	public GameObject connectionPreFab;

	private List<Node> nodes = new List<Node>();
	private List<Connection> connections = new List<Connection>();
	//public List<Vector3> ports;

	public Map() {}

	public string ToString() { return "1234"; }

	public Node AddNode(Vector3 position){
		GameObject node = Instantiate (nodePreFab, position, transform.rotation) as GameObject;
		nodes.Add (node.GetComponent<Node>());
		return node.GetComponent<Node>();
	}
 
	public Connection AddConnection(Node startNode, Node endNode, bool isBidirectional){
		// Instantiate the connection
		GameObject connectionObject = Instantiate (connectionPreFab, Vector3.zero, Quaternion.identity) as GameObject;
		Connection connection = connectionObject.GetComponent<Connection> ();

		// Set the parameters of the connection
		connection.StartNode = startNode;
		connection.EndNode = endNode;
		connection.Bidirectional = isBidirectional;

		// Update the position of a connection
		Vector3 startPosition = startNode.gameObject.transform.position;
		Vector3 endPosition = endNode.gameObject.transform.position;
		connection.transform.position = (startPosition + endPosition) / 2.0f;
		connection.transform.localScale = new Vector3(Vector3.Distance(endPosition, startPosition), 0.1f, 0.01f);
		connection.transform.rotation = Quaternion.FromToRotation(new Vector3(1, 0, 0), startPosition - endPosition);

		// Return the newly created connection
		return connection;
	}
	
	/*
	public void addPort(Vector3 port, Vector3 replacednode){

		ports.Add(port);
		foreach (Vector3 element in nodes) {
			if(element = replacednode){
				nodes.Remove(element);
			}
		}
	}
	*/


}
