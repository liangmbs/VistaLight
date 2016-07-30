using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public enum ShipStatus { 
	Unloading,
	Entering,
	Leaving,
	Waiting,
	NoRoute,
	Scheduling,
	RedSignal
}

public class ShipController : MonoBehaviour {
	public ShipSchedule schedule;
	private Ship ship;
	private double originalCargoAmount;

	public GameObject shipGO;
	public ShipVO shipVO;
	public ShipStatus status = ShipStatus.Waiting;

	public GameObject ShipInfoPanel;
	public Text NameText;
	public Text PriorityText;
	public Text StatusText;
	public Text RemainingTime;
	public Text IndustryText;
	public GameObject CargoBar;

	public DateTime ShipCreateTime;

	public ShipListEntryController ShipEntry;

	public double heading = 0;

	public bool highLighted = false;

	public Ship Ship{
		get { return ship; }
		set { 
			this.ship = value; 
			originalCargoAmount = ship.cargo;
		}
	}

	// Use this for initialization
	void Start () {
		GetComponent<PhotonView> ().ObservedComponents.Add (this);
		GetComponent<PhotonView> ().ObservedComponents.Add (this.shipVO);
		this.ship = shipVO.ship;
	}
	
	// Update is called once per frame
	void Update () {
		if (SceneSetting.Instance.IsMaster) {
			if (ship == null)
				return;
		
			if (schedule != null && schedule.tasks.Count > 0) {
				DateTime currentTime = GameObject.Find ("Timer").GetComponent<Timer> ().VirtualTime;
				ShipTask task = schedule.GetNextTask ();

				if (task.isOverDue (currentTime)) {
					ForceComplete (task);
					schedule.CompleteNextTask ();
				}

				if (task.isInTaskTime (currentTime)) {
					ProcessTask (task);
				}
			}

			UpdateStatusPanel ();

			CheckClick ();

			CalculateCargoMaintainenceCost ();
			CalculateCargoOverDueCost ();
			CalculateCargoOverDueWelfareImpact ();
		}
	}

	public void UpdateStatusPanel() {
		if (highLighted) {
			ShipInfoPanel.SetActive (true);
			ShipInfoPanel.GetComponent<RectTransform> ().localScale = new Vector3 ((float)0.1, (float)0.1, 1);
		} else {
			ShipInfoPanel.SetActive (false);
			// ShipInfoPanel.GetComponent<RectTransform> ().localScale = new Vector3 ((float)0.04, (float)0.04, 1);
		}

		IndustryText.text = ship.Industry.ToString () + " Ship";
		NameText.text = ship.Name;
		PriorityText.text = String.Format ("Pri: {0}", GetShipPriority () + 1);
		StatusText.text = String.Format ("Sta: {0}", status.ToString());


		DateTime currentTime = GameObject.Find ("Timer").GetComponent<Timer> ().VirtualTime;
		DateTime dueTime = ship.dueTime;
		TimeSpan timeLeft = dueTime - currentTime;
		RemainingTime.text = string.Format("{0} days {1}:{2}", timeLeft.Days, Math.Abs(timeLeft.Hours), Math.Abs(timeLeft.Minutes));
		if (timeLeft > TimeSpan.Zero) {
			CargoBar.GetComponent<Image> ().color = new Color ((float)0.13, (float)0.82, (float)0.29);
		} else {
			CargoBar.GetComponent<Image> ().color = new Color ((float)0.82, (float)0.16, (float)0.067);
		}

		double remainingSeconds = timeLeft.TotalSeconds;
		double totalSeconds = (ship.dueTime - ShipCreateTime).TotalSeconds;
		if (remainingSeconds <= 0) {
			CargoBar.transform.localScale = new Vector3 (0, 1, 0);	
		} else {
			CargoBar.transform.localScale = new Vector3 ((float)(1.0 * remainingSeconds / totalSeconds), 1, 0);
		}

	}

	private void CheckClick() {
		if (!Input.GetMouseButtonDown (0))
			return;
		
		RaycastHit2D ray = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

		if (ray.collider == null) return;

		if (ray.collider == gameObject.GetComponent<PolygonCollider2D>()) {
			ToggleHighLight ();
		}

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

	private void CalculateCargoOverDueWelfareImpact() {
		double cargoOverDueCost = 1e-9;
		Timer timer = GameObject.Find("Timer").GetComponent<Timer>();
		if (ship.Industry == IndustryType.Cruise && timer.VirtualTime >= ship.dueTime) {
			double welfareImpact = ship.cargo * cargoOverDueCost * timer.TimeElapsed.TotalSeconds;
			GameObject.Find ("WelfareCounter").GetComponent<WelfareCounter> ().ReduceWelfare (welfareImpact);
		}
	}

	public DateTime GetUnloadlingEta() {
		if (schedule == null) {
			return DateTime.MinValue;
		}
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

		string content = string.Format ("{0} ship {1} left the port area!", ship.Industry.ToString(), ship.Name);
		GameObject.Find ("NotificationSystem").GetComponent<NotificationSystem> ().Notify (NotificationType.Success, content);
	}

	private void ForceCompleteUnloadingTask(UnloadingTask task) {
		Unload(ship.cargo, task.dock);

		string content = string.Format ("{0} ship {1} finished unloading!", ship.Industry.ToString(), ship.Name);
		GameObject.Find ("NotificationSystem").GetComponent<NotificationSystem> ().Notify (NotificationType.Success, content);
	}

	private void Unload(double cargo, Dock dock) {
		ship.cargo -= cargo;
		double moneyToEarn = cargo * ship.value;
        GameObject.Find("BudgetCounter").GetComponent<BudgetCounter>().EarnMoney(moneyToEarn);

		Timer timer = GameObject.Find("Timer").GetComponent<Timer>();
		dock.AddUtilizeTime (timer.TimeElapsed);
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

		Unload(cargoCanUnload, task.dock);

		this.status = ShipStatus.Unloading;
	}

	private void processShipMoveTask(ShipMoveTask task) {
		Timer timer = GameObject.Find("Timer").GetComponent<Timer>();
		DateTime currentTime = timer.VirtualTime;
		TimeSpan timeElapsed = timer.TimeElapsed;

		if (timeElapsed != TimeSpan.Zero) {

			double currentX = ship.X;
			double currentY = ship.Y;
			double targetX = task.Position.x;
			double targetY = task.Position.y;

			double distanceLeft = Math.Pow (Math.Pow (currentX - targetX, 2) + Math.Pow (currentY - targetY, 2), 0.5);
			TimeSpan timeRemaining = task.EndTime.Subtract (currentTime); 
			double speedRequired = distanceLeft / timeRemaining.TotalSeconds;
			double distanceCanTravel = speedRequired * timeElapsed.TotalSeconds;
			if (distanceCanTravel > distanceLeft) {
				distanceCanTravel = distanceLeft;
			}

			Vector2 vectorToMove = new Vector2 ((float)(targetX - currentX), (float)(targetY - currentY));
			vectorToMove = vectorToMove.normalized;
			vectorToMove = new Vector2 ((float)(vectorToMove.x * distanceCanTravel), (float)(vectorToMove.y * distanceCanTravel));

			double nextX = currentX + vectorToMove.x;
			double nextY = currentY + vectorToMove.y;		

			ship.X = nextX;
			ship.Y = nextY;

			// Update the heading of the ship
			double turnSpeed = 0.1;
			double targetHeading = Math.Atan2 (-vectorToMove.x, vectorToMove.y) / Math.PI * 180;
			double angleDiff = targetHeading - heading;
			if (angleDiff > 180) {
				angleDiff -= 360;
			} else if (angleDiff < -180) {
				angleDiff += 360;
			}
			double angleCanTurn = timeElapsed.TotalSeconds * turnSpeed * Math.Sign (angleDiff);
			if (Math.Abs (angleCanTurn) > Math.Abs (angleDiff)) {
				angleCanTurn = angleDiff;
			}
			heading += angleCanTurn;
			if (heading >= 360)
				heading %= 360;
		}

		if (ship.cargo == 0) {
			this.status = ShipStatus.Leaving;
		} else {
			this.status = ShipStatus.Entering;
		}
		
    }

	public void ToggleHighLight() {
		highLighted = !highLighted;
		if (highLighted) {
			ShipEntry.HighlightOn ();
		} else {
			ShipEntry.HighlightOff ();
		}
	}

	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
			if (stream.isWriting) {
			stream.SendNext (heading);
			} else {
			heading = (double)stream.ReceiveNext ();
		}
	}
}
