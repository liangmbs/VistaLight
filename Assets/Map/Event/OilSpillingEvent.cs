using UnityEngine;
using System.Collections;
using System.Runtime.Serialization;
using System;

[Serializable()]
public class OilSpillingEvent : MapEvent {

	public OilSpillingEvent() { 
	}

	// Serialization
	public override void GetObjectData(SerializationInfo info, StreamingContext context) {
		base.GetObjectData(info, context);
	}

	// Deserialiation
	public OilSpillingEvent(SerializationInfo info, StreamingContext context) : 
		base(info, context) {
		Debug.Log("oil spilling event.");
	}
}
