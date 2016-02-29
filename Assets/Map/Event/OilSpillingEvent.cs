using UnityEngine;
using System.Collections;
using System.Runtime.Serialization;
using System;

[Serializable()]
public class OilSpillingEvent : MapEvent {

	public double Amount;
	public double Radius;

	public OilSpillingEvent() { 
	}

	// Serialization
	public override void GetObjectData(SerializationInfo info, StreamingContext context) {
		base.GetObjectData(info, context);
		info.AddValue("Amount", Amount);
		info.AddValue("Radius", Radius);
	}

	// Deserialiation
	public OilSpillingEvent(SerializationInfo info, StreamingContext context) : 
		base(info, context) {
		Amount = info.GetDouble("Amount");
		Radius = info.GetDouble("Radius");
	}
}
