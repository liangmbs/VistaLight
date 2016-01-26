using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization;

[Serializable()]
public class ShipGenerationEvent : MapEvent {

	public ShipGenerationEvent() { 
	}

	// Serialization
	public override void GetObjectData(SerializationInfo info, StreamingContext context) {
		base.GetObjectData(info, context);
	}

	// Deserialiation
	public ShipGenerationEvent(SerializationInfo info, StreamingContext context) : 
		base(info, context) {
		Debug.Log("ship generation event.");
	}
}
