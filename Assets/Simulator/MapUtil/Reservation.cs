using System;

public class Reservation {

	public DateTime StartTime;
	public DateTime EndTime;

	public bool ConflictWithTask(DateTime taskStartTime, DateTime taskEndTime) {

		if (taskEndTime < StartTime) {
			return false;
		}

		if (taskStartTime > EndTime) {
			return false;
		}

		return true;
	}

}