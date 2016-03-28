using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShipPath {
	public List<Node> path = new List<Node>();

	public bool IsNodeOnPath(Node node) {
		return path.Contains(node);
	}

	public void AppendNode(Node node) {
		path.Add(node);
	}

	public void AddNodeFromBeginning(Node node) {
		path.Insert(0, node);
	}

	public ShipPath ConcatenatePath(ShipPath path) {
		ShipPath newPath = new ShipPath();
		foreach (Node node in this.path) {
			newPath.AppendNode(node);
		}
		foreach (Node node in path.path) {
			newPath.AppendNode(node);
		}
		return newPath;
	}
}
