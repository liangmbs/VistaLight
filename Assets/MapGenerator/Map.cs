using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Map : MonoBehaviour {

	public List<Node> nodes = new List<Node>();
	public List<Connection> connections = new List<Connection>();
	//public List<Vector3> ports;

	public Map() {}

	public string ToString() { return "1234"; }

	public void addNode(Node node){
		nodes.Add (node);
	}
 
	public void addConnection(Node startNode, Node endNode, bool isBidirectional){
		connections.Add (new Connection (startNode, endNode, isBidirectional));
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
