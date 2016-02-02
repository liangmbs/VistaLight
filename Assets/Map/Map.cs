using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[Serializable()]
public class Map {

	private string mapName = "map";
	private DateTime startTime = new DateTime(2016, 1, 1, 12, 0, 0);
	public static readonly string DateTimeFormat = "yyyy-MM-dd HH:mm:ss";

	public List<Node> nodes = new List<Node>();
	public List<Connection> connections = new List<Connection>();
	public List<Dock> docks = new List<Dock>();
	public List<Ship> ships = new List<Ship>();
	public List<MapEvent> mapEvents = new List<MapEvent>();

	public string Name {
		get { return mapName; }
		set { mapName = value; }
	}

	public DateTime StartTime {
		get { return startTime; }
		set { startTime = value; }
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
	
	public void AddDock(Dock dock){
		this.docks.Add(dock);
	}

	public void RemoveDock(Dock dock) {
		// docks.Remove(dock);
		// GameObject.Destroy(dock.gameObject);
	}

	public void AddShip(Ship ship) {
		this.ships.Add(ship);
	}

	public void RemoveShip(Ship ship) {
		this.ships.Remove(ship);
	}

	public Ship GetShipById(int id) {
		foreach (Ship ship in ships) {
			if (ship.shipID == id) {
				return ship;
			}
		}
		throw new KeyNotFoundException();
	}

	public void AddMapEvent(MapEvent mapEvent) {
		mapEvents.Add(mapEvent);
	}

	public void RemoveMapEvent(MapEvent mapEvent) {
		mapEvents.Remove(mapEvent);
	}

	public void RemoveConnection(Connection connection) {
		connections.Remove(connection);
	}
}
