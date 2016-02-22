using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[Serializable()]
public class Dock {
	public int id;
	public Node node;
	public string name = "";
	public IndustryType type;
}
