using System;

public class Reservation {

	public DateTime StartTime;
	public DateTime EndTime;

	public bool ConflictWithTask(DateTime taskStartTime, DateTime taskEndTime) {
		if (taskStartTime >= StartTime && taskStartTime <= EndTime) {
			return true;
		}

		if (taskEndTime >= StartTime && taskEndTime <= EndTime) {
			return true;	
		}

		if (taskStartTime <= StartTime && taskEndTime >= EndTime) {
			return true;
		}

		return false;
	}

}