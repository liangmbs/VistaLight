//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using UnityEngine;

public class Connection
{
	private Node startNode;
	private Node endNode;
	bool isBidirectional;

	public Connection (Node startNode, Node endNode, bool isBidirectional)
	{
		this.startNode = startNode;
		this.endNode = endNode;
		this.isBidirectional = isBidirectional;
	}
}


