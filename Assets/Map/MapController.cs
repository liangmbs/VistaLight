﻿using UnityEngine;
using System.Collections;
using System;

public class MapController : MonoBehaviour {

	public static readonly double MapZIndex = -1;
	public static readonly double BuildingZIndex = -2;
	public static readonly double MapEventZIndex = -3;

	private Map map;

	public GameObject nodePrefab;
	public GameObject connectionPrefab;
	public GameObject dockPrefab;
	public GameObject mapEventPrefab;

	private int nextNodeId = 1;
	private int nextDockId = 1;

	

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
		connection.StartNode = start.GetComponent<NodeVO>().node;
		connection.EndNode = end.GetComponent<NodeVO>().node;
		connection.Bidirectional = isBidirectional;
		map.AddConnection(connection);

		GameObject connectionGO = CreateConnectionGameObject(connection);
		
		return connectionGO;
	}

	public GameObject AddDock(GameObject node, DockType type) {
		Dock dock = new Dock();
		dock.id = nextDockId;
		dock.node = node.GetComponent<NodeVO>().node;
		dock.type = type;
		dock.name = type.ToString() + nextDockId.ToString();
		nextDockId++;
		map.AddDock(dock);

		GameObject dockObject = CreateDockGameObject(dock);

		return dockObject;
	}

	public GameObject CreateDockGameObject(Dock dock) {
		GameObject dockObject = GameObject.Instantiate(dockPrefab,
				new Vector3((float)dock.node.X, (float)dock.node.Y, (float)MapController.BuildingZIndex),
				Quaternion.identity) as GameObject;
		dockObject.GetComponent<DockVO>().Dock = dock;
		dockObject.transform.parent = GameObject.Find("Map").transform;
		return dockObject;
	}

	private GameObject CreateConnectionGameObject(Connection connection) { 
		GameObject connectionGO = Instantiate(connectionPrefab, Vector3.zero, Quaternion.identity) as GameObject;
		connectionGO.GetComponent<ConnectionVO>().connection = connection;
		connectionGO.transform.parent = GameObject.Find("Map").transform;
		return connectionGO;
	}

	public GameObject CreateMapEvent(Vector3 position) {
		MapEvent mapEvent = new MapEvent();
		mapEvent.X = position.x;
		mapEvent.Y = position.y;
		mapEvent.Time = map.StartTime;
		GameObject mapEventGO = CreateMapEventGameObject(mapEvent);
		return mapEventGO;
	}

	public GameObject CreateMapEventGameObject(MapEvent mapEvent) {
		GameObject mapEventGO = Instantiate(mapEventPrefab, Vector3.zero, Quaternion.identity) as GameObject;
		MapEventVO mapEventVO = mapEventGO.GetComponent<MapEventVO>();
		mapEventVO.MapEvent = mapEvent;
		mapEventGO.transform.SetParent(GameObject.Find("Map").transform);
		return mapEventGO;
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

	public void SelectMapEvent(GameObject mapEvent) {
		mapEvent.transform.FindChild("EventSelected").gameObject.SetActive(false);	
		mapEvent.transform.FindChild("Event").gameObject.SetActive(true);	
	}

	public void DeselectMapEvent(GameObject mapEvent) { 
		mapEvent.transform.FindChild("EventSelected").gameObject.SetActive(true);	
		mapEvent.transform.FindChild("Event").gameObject.SetActive(false);	
	}

	public void RemoveNode(GameObject gameObject) {
		throw new NotImplementedException();
	}


	public void RegenerateMap(Map map) {
		this.map = map;

		// Regenerate all nodes
		foreach (Node node in map.nodes) {
			CreateNodeGameObject(node);
			if(nextNodeId <= node.Id) {
				this.nextNodeId = node.Id + 1;
			}
		}

		// Regenerate all connections
		foreach (Connection connection in map.connections) {
			CreateConnectionGameObject(connection);
		}

		// Regenerate all docks
		foreach (Dock dock in map.docks) {
			CreateDockGameObject(dock);
			if (nextDockId <= dock.id) {
				nextDockId = dock.id + 1;
			}
		}
		
    }

	public void CloseMap() {
		foreach (Transform child in GameObject.Find("Map").transform) {
			Destroy(child.gameObject);
		}
		nextNodeId = 1;
		nextDockId = 1;
	}
}
