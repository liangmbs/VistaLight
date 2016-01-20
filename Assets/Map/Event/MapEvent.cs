using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization;

public abstract class MapEvent : ISerializable{

	public double X;
	public double Y;
	public DateTime Time;

	// Default constructure
	public MapEvent() { }

	// Serialization
	public void GetObjectData(SerializationInfo info, StreamingContext context) {
		info.AddValue("X", X);
		info.AddValue("Y", Y);
		info.AddValue("Time", Time);
	}

	// Deserialiation
	public MapEvent(SerializationInfo info, StreamingContext context) {
		X = (double) info.GetValue("X", typeof(double));
		Y = (double) info.GetValue("Y", typeof(double));
		Time = (DateTime) info.GetValue("Time", typeof(DateTime));
	}
}
