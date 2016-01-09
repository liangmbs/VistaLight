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
	private Node node;
	private DockType type;

	public Node Node {
		get { return node; }
		set { node = value;  }
	}

	public DockType Type { 
		get { return type; }
		set { type = value; }
	}

}
