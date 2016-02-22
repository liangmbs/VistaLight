using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class ShipSchedule {
	public List<ShipTask> tasks = new List<ShipTask>();

	public DateTime ETA {
		// Assumes that the tasks are ordered by time
		get { return tasks[tasks.Count - 1].EndTime; }
	}

	public void AppendTask(ShipTask task) {
		tasks.Add(task);
	}

	public void Postpone(TimeSpan time) {
		tasks.ForEach(task => Postpone(time));
	}

	public ShipTask GetNextTask() {
		if (tasks.Count == 0) return null;
		return tasks[0];
	}

	public void CompleteNextTask() {
		tasks.RemoveAt(0);
	}
}
