using System;

public class Reservation {

	public DateTime StartTime;
	public DateTime EndTime;

	public bool ConflictWithTask(DateTime taskStartTime, DateTime taskEndTime, 
		TimeSpan safetyTime) {
		if (taskStartTime > StartTime - safetyTime && 
			taskStartTime < EndTime + safetyTime) {
			return true;
		}

		if (taskEndTime > StartTime - safetyTime && 
			taskEndTime < EndTime + safetyTime) {
			return true;	
		}

		if (taskStartTime < StartTime - safetyTime && 
			taskEndTime > EndTime + safetyTime) {
			return true;
		}

		return false;
	}

}