using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Map : MonoBehaviour {

	public GameObject nodePreFab;

	private List<GameObject> nodes = new List<GameObject>();
	// private List<GameObject> connections = new List<GameObject>();
	//public List<Vector3> ports;

	public Map() {}

	public string ToString() { return "1234"; }

	public GameObject addNode(Vector3 position){
		GameObject node = Instantiate (nodePreFab, position, transform.rotation) as GameObject;
		nodes.Add (node);
		return node;
	}
 
	public void addConnection(Node startNode, Node endNode, bool isBidirectional){
		// connections.Add (new Connection (startNode, endNode, isBidirectional));
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
