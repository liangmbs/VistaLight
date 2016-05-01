using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ReservationManager : MonoBehaviour {

	Dictionary<Connection, List<Reservation>> reservationOnConnections = 
		new Dictionary<Connection, List<Reservation>>();	
	Dictionary<Dock, List<Reservation>> reservationOnDocks = 
		new Dictionary<Dock, List<Reservation>>();
	TimeSpan safetyTime = new TimeSpan(0, 15, 0);

	public void PostponeScheduleToResolveConflict(ShipSchedule schedule) {
		bool conflict = hasConflict(schedule);
		while (conflict) {
			// schedule.Postpone(new TimeSpan(1, 0, 0));
			foreach (ShipTask task in schedule.tasks) {
				ResolveConflictForTask(task, schedule);
			}
			conflict = hasConflict(schedule);
		}
	}

	private void ResolveConflictForTask(ShipTask task, ShipSchedule schedule) {
		if (task is ShipMoveTask) {
			ResolveConflictForShipMoveTask((ShipMoveTask)task, schedule);
			return;
		} else if (task is UnloadingTask) {
			ResolveConflictForUnloadingTask((UnloadingTask)task, schedule);
			return;
		}
	}

	private void ResolveConflictForUnloadingTask(UnloadingTask task, ShipSchedule schedule) {
		if (!UnloadingTaskHasConflict(task)) {
			return;
		}	
			
		List<Reservation> reservationList = reservationOnDocks[task.dock];
		TimeSpan minTimeSpan = TimeSpan.MaxValue;
		foreach (Reservation reservation in reservationList) {
			TimeSpan timeDiff = reservation.EndTime + safetyTime - task.StartTime;
			if (timeDiff.CompareTo(TimeSpan.Zero) < 0) {
				continue;
			}

			if (reservation.ConflictWithTask(task.StartTime + timeDiff, task.EndTime + timeDiff)) {
				continue;
			}

			if (timeDiff < minTimeSpan) {
				minTimeSpan = timeDiff;
			}
		}

		if (minTimeSpan == TimeSpan.Zero) {
			minTimeSpan = minTimeSpan.Add (new TimeSpan (0, 0, 1));
		}

		schedule.Postpone(minTimeSpan);

	}

	private void ResolveConflictForShipMoveTask(ShipMoveTask task, ShipSchedule schedule) {
		if (!ShipMoveTaskHasConflict(task)) {
			return;
		}	
			
		List<Reservation> reservationList = reservationOnConnections[task.connection];
		TimeSpan minTimeSpan = TimeSpan.MaxValue;
		foreach (Reservation reservation in reservationList) {
			TimeSpan timeDiff = reservation.EndTime + safetyTime - task.StartTime;
			if (timeDiff < TimeSpan.Zero) {
				continue;
			}

			if (reservation.ConflictWithTask(task.StartTime + timeDiff, task.EndTime + timeDiff)) {
				continue;
			}

			if (timeDiff < minTimeSpan) {
				minTimeSpan = timeDiff;
			}
		}

		if (minTimeSpan == TimeSpan.Zero) {
			minTimeSpan = minTimeSpan.Add (new TimeSpan (0, 0, 1));
		}

		schedule.Postpone(minTimeSpan);
	}

	public bool hasConflict(ShipSchedule schedule) {
		foreach (ShipTask task in schedule.tasks) {
			if (taskHasConflict(task)) {
				return true;	
			}
		}
		return false;
	}

	private bool taskHasConflict(ShipTask task) {
		if (task is ShipMoveTask) {
			return ShipMoveTaskHasConflict((ShipMoveTask)task);
		} else if (task is UnloadingTask) {
			return UnloadingTaskHasConflict((UnloadingTask)task);
		}
		return false;
	}

	private bool UnloadingTaskHasConflict(UnloadingTask task) {
		if (!reservationOnDocks.ContainsKey(task.dock)) {
			return false;	
		} 
		
		List<Reservation> reservationList = reservationOnDocks[task.dock];
		foreach (Reservation reservation in reservationList) {
			if (reservation.ConflictWithTask(task.StartTime, task.EndTime)) {
				return true;
			}
		}
		return false;
	}

	private bool ShipMoveTaskHasConflict(ShipMoveTask task) {
		if (task.connection == null) {
			return false;
		}
		if (!reservationOnConnections.ContainsKey(task.connection)) {
			return false;	
		} 

		List<Reservation> reservationList = reservationOnConnections[task.connection];
		foreach (Reservation reservation in reservationList) {
			if (reservation.ConflictWithTask(task.StartTime, task.EndTime)) {
				return true;
			}
		}
		return false;
	}

	public void Reserve(ShipSchedule schedule) {
		foreach (ShipTask task in schedule.tasks) {
			if (task is ShipMoveTask) {
				ReserveShipMoveTask((ShipMoveTask)task);
			} else if (task is UnloadingTask) {
				ReserveUnloadingTask((UnloadingTask)task);
			}
		}
	}

	private void ReserveUnloadingTask(UnloadingTask task) {
		if (!reservationOnDocks.ContainsKey(task.dock)) {
			reservationOnDocks.Add(task.dock, new List<Reservation>());
		}

		List<Reservation> reservationList = reservationOnDocks[task.dock];
		Reservation reservation = new Reservation();
		reservation.StartTime = task.StartTime - safetyTime;
		reservation.EndTime = task.EndTime + safetyTime;
		reservationList.Add(reservation);
	}

	private void ReserveShipMoveTask(ShipMoveTask task) {
		if (task.connection == null) {
			return;
		}

		if (!reservationOnConnections.ContainsKey(task.connection)) {
			reservationOnConnections.Add(task.connection, new List<Reservation>());
		}

		List<Reservation> reservationList = reservationOnConnections[task.connection];
		Reservation reservation = new Reservation();
		reservation.StartTime = task.StartTime - safetyTime;
		reservation.EndTime = task.EndTime + safetyTime;
		reservationList.Add(reservation); ;
	}

	public void ClearAll() {
		reservationOnConnections = new Dictionary<Connection, List<Reservation>>();
		reservationOnDocks = new Dictionary<Dock, List<Reservation>>();
	}

}
