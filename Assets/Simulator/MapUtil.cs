﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class MapUtil : MonoBehaviour {

	public MapController MapController;

	public List<Node> exitNodes = new List<Node>();

	public List<Node> ExitNodes() {
		if (exitNodes.Count == 0) {
			foreach (Node node in MapController.Map.nodes) {
				if (node.IsExit) {
					exitNodes.Add(node);
				}
			}
		}
		return exitNodes;
	}

	class TreeNode {
		public TreeNode parent;
		public Node node;

		public TreeNode(TreeNode parent, Node node) {
			this.parent = parent;
			this.node = node;
		}
	};

	public List<Path> FindPath(Node from, Node to) {
		List<Path> allPaths = new List<Path>();
		List<TreeNode> nodesToExpand = new List<TreeNode>();

		nodesToExpand.Add(new TreeNode(null, from));
		while (nodesToExpand.Count > 0) {
			TreeNode treeNodeToExpand = nodesToExpand[0];
			nodesToExpand.RemoveAt(0);

			List<Node> connectedNodes = GetAllConnectedNode(treeNodeToExpand.node);
			foreach (Node connectedNode in connectedNodes) {
				// This is per-path way to avoid repeatance. Consider change it to global way.
				if (isNodeAlreadyOnPath(connectedNode, treeNodeToExpand)) {
					continue;
				}

				TreeNode child = new TreeNode(treeNodeToExpand, connectedNode);

				if (connectedNode == to) {
					allPaths.Add(GetPathFromTreeNode(child));
				} else {
					nodesToExpand.Add(child);
				}
			}
		}

		return allPaths;
	}

	private Path GetPathFromTreeNode(TreeNode leaf) {
		Path path = new Path();
		while (leaf != null) {
			path.AddNodeFromBeginning(leaf.node);
			leaf = leaf.parent;
		}
		return path;
	}

	private bool isNodeAlreadyOnPath(Node node, TreeNode treeNodeToExpand) {
		TreeNode leaf = treeNodeToExpand;
		while (leaf != null) {
			if (leaf.node == node) {
				return true;
			}
			leaf = leaf.parent;
		}
		return false;
	}

	public List<Node> GetAllConnectedNode(Node from) {
		List<Node> connectedNodes = new List<Node>();
		foreach(Connection connection in MapController.Map.connections) {
			if (IsOutGoingLink(from, connection)) {
				connectedNodes.Add(TheOtherEndOfConnection(from, connection));
			}
		}
		return connectedNodes;
	}

	public bool IsOutGoingLink(Node node, Connection connection) {
		if (connection.StartNode == node) {
			return true;
		} else if (connection.EndNode == node && connection.Bidirectional) {
			return true;	
		}
		return false;
	}

	public Node TheOtherEndOfConnection(Node node, Connection connection) {
		if (connection.StartNode != node) {
			return connection.StartNode;
		}
		return connection.EndNode;
	}

	public Node FindNearestNode(double x, double y) {
		double min_distance = double.MaxValue;
		Node selectedNode = null;
		foreach (Node node in MapController.Map.nodes) {
			double distance = Math.Pow(Math.Pow(x - node.X, 2) + Math.Pow(y - node.Y, 2), 0.5);
			if (distance < min_distance) {
				min_distance = distance;
				selectedNode = node;
			}
		}
		return selectedNode;
	}

}