using UnityEngine;
using System.Collections;
using System;
using SimpleJSON;

public enum IndustryType {
	BULK, BREAKBULK, PORT, PETRO
}

[Serializable]
public class Ship {

	/*
	 * Location
	 */
	public float x;
	public float y;
	public float z;

	/*
	 * Heading Rotation
	 */
	public float rZ;

	/*
	 * Vehicle
	 */
	public int shipID;
	public int priority;
	public string Type;
	public double Heading;
	public double cargo;
	public float destinationX;
	public float destinationY;
	public string remainTime;
	public string status;

	/*
	 * Company
	 */
	public IndustryType Industry;
	public string Name;
 

}
