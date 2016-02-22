using UnityEngine;
using System.Collections;
using System;

public class ShipController : MonoBehaviour {

	public ShipSchedule schedule;
	public Ship ship;
	public GameObject shipGO;
	public ShipVO shipVO;

	public double heading = 0;

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

		// Update the heading of the ship
		double turnSpeed = 0.5;
		double targetHeading = Math.Atan2(-vectorToMove.x, vectorToMove.y) / Math.PI * 180;
		Debug.Log(String.Format("Target heading: {0}", targetHeading));
		Debug.Log(String.Format("Current heading: {0}", heading));
		double angleDiff = targetHeading - heading;
		Debug.Log(String.Format("Angle diff: {0}", angleDiff));
		double angleCanTurn = timeElapsed.TotalSeconds * turnSpeed * Math.Sign(angleDiff);
		if (Math.Abs(angleCanTurn) > Math.Abs(angleDiff)) {
			angleCanTurn = angleDiff;
		}
		Debug.Log(String.Format("Angle can turn: {0}", angleCanTurn));
		heading += angleCanTurn;
		if (heading >= 360) heading %= 360;
		Debug.Log(String.Format("New heading: {0}", heading));

		
    }
}
