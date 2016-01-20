using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public enum DockType { 
	Petro, Bulk, BreakBulk, Port
}

[Serializable()]
public class Dock {
	public int id;
	public Node node;
	public string name = "";
	public DockType type;
}
