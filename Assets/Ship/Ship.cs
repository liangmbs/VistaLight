using UnityEngine;
using System.Collections;
using System;
using SimpleJSON;

public enum IndustryType {
	Bulk, BreakBulk, Cruise, Petro
}

public class IndustryColor {
	static public Color GetIndustryColor(IndustryType industry) {
		switch (industry) {
		case IndustryType.Bulk:
			return new Color((float)236.0/255, (float)220.0 /255, (float)0.0);
		case IndustryType.BreakBulk:
			return new Color((float)187.0/255, (float)20.0/255, (float)26.0/255);
		case IndustryType.Cruise:
			return new Color(1, 1, 1);
		case IndustryType.Petro:
			return new Color((float)40.0 / 255, (float)37.0 / 255, (float)18.0 / 255);
		}
		return new Color(0, 0, 0);
	}
}

[Serializable]
public class Ship {
	public int shipID = 0;
	public string Name = "Ship";

	public double cargo = 100;
	public double value = 100;
	public IndustryType Industry = IndustryType.Bulk;
	public DateTime dueTime = new DateTime(2016, 1, 1, 12, 0, 0);

	public double X;
	public double Y;
}
