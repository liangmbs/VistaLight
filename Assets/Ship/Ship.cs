using UnityEngine;
using System.Collections;
using System;
using SimpleJSON;

public enum IndustryType {
	Bulk, BreakBulk, Port, Petro
}

[Serializable]
public class Ship {
	public int shipID = 0;
	public string Name = "Ship";

	public double cargo = 100;
	public double value = 100;
	public IndustryType Industry = IndustryType.Bulk;
	public DateTime dueTime = new DateTime(2016, 1, 1, 12, 0, 0);
}
