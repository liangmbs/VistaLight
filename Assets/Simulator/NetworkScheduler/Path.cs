using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Path {
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

	public Path ConcatenatePath(Path path) {
		Path newPath = new Path();
		foreach (Node node in this.path) {
			newPath.AppendNode(node);
		}
		foreach (Node node in path.path) {
			newPath.AppendNode(node);
		}
		return newPath;
	}
}
