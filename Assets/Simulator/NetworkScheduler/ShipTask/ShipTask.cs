using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class ShipTask {

	public DateTime StartTime;
	public DateTime EndTime;

	public Vector2 Position;

	public void Postpone(TimeSpan time) {
		StartTime.Add(time);
		EndTime.Add(time);
	}

	public bool isInTaskTime(DateTime time) {
		if (time.CompareTo(StartTime) >= 0 && time.CompareTo(EndTime) <= 0) {
			return true;	
		}
		return false;
	}

	public bool isOverDue(DateTime time) {
		if (time.CompareTo(EndTime) > 0) {
			return true;
		}
		return false;
	}

}
