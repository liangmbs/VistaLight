using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Map : MonoBehaviour {

	public List<Vector3> nodes;
	public List<Vector3> connections;
	public List<Vector3> ports;

	public void addNode(Vector3 startNode, Vector3 endNode){

		Vector3 objectLine = endNode - startNode;
		float segmentDistance = 2.0f;
		float distance = objectLine.magnitude;
		float remainingDistance = distance;
		Vector3 previousDot = startNode;
		while (remainingDistance > segmentDistance) {
			nodes.Add (previousDot);
			Vector3 nextPoint = previousDot + objectLine.normalized * segmentDistance;
			previousDot = nextPoint;
			print (nextPoint);
			remainingDistance -= segmentDistance;
		}
	}
 
	public void addConnection(Vector3 startNode, Vector3 endNode){

	


	}

	public void addPort(Vector3 port, Vector3 replacednode){

		ports.Add(port);
		foreach (Vector3 element in nodes) {
			if(element = replacednode){
				nodes.Remove(element);

			}


		}
	}


}
