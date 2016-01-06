using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Map {

	private string mapName = "map";

	public List<Node> nodes = new List<Node>();
	public List<Connection> connections = new List<Connection>();
	public List<Dock> docks = new List<Dock>();

	public string Name { 
		get { return mapName;  }
		set { mapName = value; }
	}

	public void AddNode(Node node){
		this.nodes.Add(node);
	}

	public void AddConnection(Connection connection){
		this.connections.Add(connection);
	}

	public void RemoveNode(Node node) {
		// node.Remove();
		// nodes.Remove(node);
	}
	
	public void AddDock(Node node, DockType type){
		/*
		// Create dock object
		GameObject dockObject = GameObject.Instantiate(dockPreFab, 
			node.Position, Quaternion.identity) as GameObject;
		Dock dock = dockObject.GetComponent<Dock>();
		docks.Add(dock);

		// Setup dock information
		dock.Node = node;
		dock.Type = type;
		dock.Map = this;

		// Update dock apperance
		dock.UpdateGameObject();
		*/
	}

	public void RemoveDock(Dock dock) {
		docks.Remove(dock);
		GameObject.Destroy(dock.gameObject);
	}

	public void StartOver() {
		throw new NotImplementedException();
	}

}
