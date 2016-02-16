using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization;

[Serializable()]
public class ShipGenerationEvent : MapEvent {

	public Ship Ship;

	public ShipGenerationEvent() { 
	}

	// Serialization
	public override void GetObjectData(SerializationInfo info, StreamingContext context) {
		base.GetObjectData(info, context);
		info.AddValue("Ship", Ship);
	}

	// Deserialiation
	public ShipGenerationEvent(SerializationInfo info, StreamingContext context) : 
		base(info, context) {
		this.Ship = (Ship) info.GetValue("Ship", typeof(Ship));
		Debug.Log("ship generation event.");
	}
}
