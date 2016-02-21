using UnityEngine;
using System.Collections;
using System;

public class ShipController : MonoBehaviour {

	public ShipSchedule schedule;
	public Ship ship;
	public GameObject shipGO;
	public ShipVO shipVO;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		if (schedule != null) {
			DateTime currentTime = GameObject.Find("Timer").GetComponent<Timer>().VirtualTime;
			ShipTask task = schedule.GetNextTask();

			if (task.isOverDue(currentTime)) {
				schedule.CompleteNextTask();
			}

			if (task.isInTaskTime(currentTime)) {
				ProcessTask(task);
			}
					
		}
	}

	private void ProcessTask(ShipTask task) {
		if (task is ShipMoveTask) {
			processShipMoveTask((ShipMoveTask)task);
			
		}
	}

	private void processShipMoveTask(ShipMoveTask task) {
		Timer timer = GameObject.Find("Timer").GetComponent<Timer>();
		DateTime currentTime = timer.VirtualTime;
		TimeSpan timeElapsed = timer.TimeElapsed;

		double currentX = ship.X;
		double currentY = ship.Y;
		double targetX = task.Position.x;
		double targetY = task.Position.y;

		double distanceLeft = Math.Pow(Math.Pow(currentX - targetX, 2) + Math.Pow(currentY - targetY, 2), 0.5);
		TimeSpan timeRemaining = task.EndTime.Subtract(currentTime); 
		double speedRequired = distanceLeft / timeRemaining.TotalSeconds;
		double distanceCanTravel = speedRequired * timeElapsed.TotalSeconds;
		if (distanceCanTravel > distanceLeft) {
			distanceCanTravel = distanceLeft;
		}

		Vector2 vectorToMove = new Vector2((float)(targetX - currentX), (float)(targetY - currentY));
		vectorToMove = vectorToMove.normalized;
		vectorToMove = new Vector2((float)(vectorToMove.x * distanceCanTravel), (float)(vectorToMove.y * distanceCanTravel));

		double nextX = currentX + vectorToMove.x;
		double nextY = currentY + vectorToMove.y;		

		ship.X = nextX;
		ship.Y = nextY;

    }
}
