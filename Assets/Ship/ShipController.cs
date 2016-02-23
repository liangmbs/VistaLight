using UnityEngine;
using System.Collections;
using System;

public class ShipController : MonoBehaviour {

	public ShipSchedule schedule;
	public Ship ship;
	public GameObject shipGO;
	public ShipVO shipVO;

	public double heading = 0;

	public bool highLighted = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		if (schedule != null && schedule.tasks.Count > 0) {
			DateTime currentTime = GameObject.Find("Timer").GetComponent<Timer>().VirtualTime;
			ShipTask task = schedule.GetNextTask();

			if (task.isOverDue(currentTime)) {
				ForceComplete(task);
				schedule.CompleteNextTask();
			}

			if (task.isInTaskTime(currentTime)) {
				ProcessTask(task);
			}
		}

		GameObject nameGO = transform.Find("Name").gameObject;
		TextMesh textMesh = nameGO.GetComponent<TextMesh>();
		if (this.highLighted) {
			nameGO.SetActive(true);
			textMesh.text = ship.Name;
		} else {
			nameGO.SetActive(false);
		}

		CalculateCargoMaintainenceCost();
		CalculateCargoOverDueCost();
	}

	public int GetShipPriority() {
		NetworkScheduler networkScheduler = GameObject.Find("NetworkScheduler").GetComponent<NetworkScheduler>();
		return networkScheduler.priorityQueue.GetPriority(this);
	}

	private void CalculateCargoOverDueCost() {
		double cargoOverDueCost = 0.0005;
		Timer timer = GameObject.Find("Timer").GetComponent<Timer>();
		if (timer.VirtualTime >= ship.dueTime) {
			double moneyToSpend = ship.cargo * cargoOverDueCost * timer.TimeElapsed.TotalSeconds;
			GameObject.Find("BudgetCounter").GetComponent<BudgetCounter>().SpendMoney(moneyToSpend);
		}
	}

	private void CalculateCargoMaintainenceCost() {
		double cargoMaintainenceCost = 0.00002;
		Timer timer = GameObject.Find("Timer").GetComponent<Timer>();
		double moneyToSpend = ship.cargo * cargoMaintainenceCost * timer.TimeElapsed.TotalSeconds;
		GameObject.Find("BudgetCounter").GetComponent<BudgetCounter>().SpendMoney(moneyToSpend);
	}

	public DateTime GetUnloadlingEta() {
		foreach (ShipTask task in schedule.tasks) {
			if (task is UnloadingTask) {
				return task.EndTime;
			}
		}
		return DateTime.MinValue;
	}

	private void ForceComplete(ShipTask task) {
		if (task is ShipMoveTask) {
			ForceCompleteShipMoveTask((ShipMoveTask)task);
		} else if (task is UnloadingTask) {
			ForceCompleteUnloadingTask((UnloadingTask)task);
		} else if (task is VanishTask) {
			ForceCompleteVanishTask((VanishTask)task);
		};
	}

	private void ForceCompleteVanishTask(VanishTask task) {
		GameObject.Destroy(shipGO);
		GameObject.Find("NetworkScheduler").GetComponent<NetworkScheduler>().RemoveShip(this);
		GameObject.Find("ShipList").GetComponent<ShipListController>().RemoveShip(this);
	}

	private void ForceCompleteUnloadingTask(UnloadingTask task) {
		Unload(ship.cargo);
	}

	private void Unload(double cargo) {
		ship.cargo -= cargo;
		double moneyToEarn = cargo * ship.value;
        GameObject.Find("BudgetCounter").GetComponent<BudgetCounter>().EarnMoney(moneyToEarn);
	}

	private void ForceCompleteShipMoveTask(ShipMoveTask task) {
        ship.X = task.Position.x;
		ship.Y = task.Position.y;
	}

	private void ProcessTask(ShipTask task) {
		if (task is ShipMoveTask) {
			processShipMoveTask((ShipMoveTask)task);
		} else if (task is UnloadingTask) {
			processUnloadingTask((UnloadingTask)task);
		} else if (task is VanishTask) {
			processVanishTask((VanishTask)task);
		}
	}

	private void processVanishTask(VanishTask task) {
	}

	private void processUnloadingTask(UnloadingTask task) {
		Timer timer = GameObject.Find("Timer").GetComponent<Timer>();
		DateTime currentTime = timer.VirtualTime;
		TimeSpan timeElapsed = timer.TimeElapsed;

		double cargoRemaining = ship.cargo;
		TimeSpan timeRemaining = task.EndTime.Subtract(currentTime);
		double speedRequired = cargoRemaining / timeRemaining.TotalSeconds;
		double cargoCanUnload = timeElapsed.TotalSeconds * speedRequired;
		if (cargoCanUnload > cargoRemaining) {
			cargoCanUnload = cargoRemaining;
		}

		Unload(cargoCanUnload);
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
		double turnSpeed = 0.1;
		double targetHeading = Math.Atan2(-vectorToMove.x, vectorToMove.y) / Math.PI * 180;
		double angleDiff = targetHeading - heading;
		double angleCanTurn = timeElapsed.TotalSeconds * turnSpeed * Math.Sign(angleDiff);
		if (Math.Abs(angleCanTurn) > Math.Abs(angleDiff)) {
			angleCanTurn = angleDiff;
		}
		heading += angleCanTurn;
		if (heading >= 360) heading %= 360;
    }
}
