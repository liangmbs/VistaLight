using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ShipScheduler {

	private ShipController ship;
	public ShipController Ship { 
		set { ship = value; }
	}

	private List<Path> paths = new List<Path>();
	private List<ShipSchedule> schedules = new List<ShipSchedule>();
	

	public void Schedule() {
		Debug.Log(string.Format("Scheduling ship {0}", ship.ship.Name));
		FindAllPaths();
		PathsToSchedules();
		ResolveConflict();
		ApplyBestSchedule();
	}

	private void ApplyBestSchedule() {
		DateTime earliestEta = DateTime.MaxValue;
		ShipSchedule bestSchedule = null;
		foreach(ShipSchedule schedule in schedules) {
			if (schedule.ETA.CompareTo(earliestEta) < 0) {
				earliestEta = schedule.ETA;
				bestSchedule = schedule;
			}
		}

		ship.schedule = bestSchedule;

	}

	private void ResolveConflict() {
		// Currently not doing anything
	}

	private void PathsToSchedules() {
		schedules.Add(PathToSchedule(paths[0]));	
	}

	private ShipSchedule PathToSchedule(Path path) {
		double shipSpeed = 3.0;
		ShipSchedule schedule = new ShipSchedule();

		DateTime currentTime = GameObject.Find("Timer").GetComponent<Timer>().VirtualTime;
		Vector2 currentPosition = new Vector2((float)ship.ship.X, (float)ship.ship.Y);

		foreach (Node node in path.path) {
			ShipMoveTask moveTask = new ShipMoveTask();
			moveTask.Position = new Vector2((float)node.X, (float)node.Y);

			double distance = Math.Pow(Math.Pow(node.X - currentPosition.x, 2) + Math.Pow(node.Y - currentPosition.y, 2), 0.5);
			TimeSpan duration = new TimeSpan(0, 0, (int)Math.Round(distance/shipSpeed));
			moveTask.StartTime = currentTime;
			moveTask.EndTime = currentTime.Add(duration);

			currentTime = currentTime.Add(duration);
			currentPosition = new Vector2((float)node.X, (float)node.Y);

			schedule.AppendTask(moveTask);
		}
		return schedule;
	}

	private void FindAllPaths() {
		/*
		if (ship.cargo == 0) {
			FindAllPathsToLeaveMap();
        } else {
			FindAllPathsToUnloadAndLeaveMap();
		}
		*/
		FindAllPathsToLeaveMap();
	}

	private void FindAllPathsToLeaveMap() {
		MapUtil mapUtil = GameObject.Find("MapUtil").GetComponent<MapUtil>();

		Node startNode = mapUtil.FindNearestNode(ship.ship.X, ship.ship.Y);

		List<Node> exitNodes = mapUtil.ExitNodes();

		foreach (Node exitNode in exitNodes) {
			List<Path> pathsToExit = mapUtil.FindPath(startNode, exitNode);
			Debug.Log(String.Format("{0} paths to exit found", pathsToExit.Count));
			paths.AddRange(pathsToExit);
		}
	}

	private void FindAllPathsToUnloadAndLeaveMap() { 
	}

}
