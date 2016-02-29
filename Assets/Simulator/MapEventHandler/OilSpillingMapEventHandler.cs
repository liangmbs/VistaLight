using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class OilSpillingMapEventHandler : IMapEventHandler {

	public OilSpillingEvent OilSpillingEvent;

	public OilSpillingMapEventHandler(OilSpillingEvent oilSpillingEvent) {
		this.OilSpillingEvent = oilSpillingEvent;
	}

	public void Process() {
		GameObject oilSpillingPrefab = Resources.Load("OilSpilling") as GameObject;
		
		GameObject oilSpillingGO = GameObject.Instantiate(oilSpillingPrefab);

		OilSpillingController oilSpillingController = oilSpillingGO.GetComponent<OilSpillingController>();
		oilSpillingController.position = new Vector2((float)OilSpillingEvent.X, (float)OilSpillingEvent.Y);
		oilSpillingController.Radius = OilSpillingEvent.Radius;
		oilSpillingController.Amount = OilSpillingEvent.Amount;
	}

}
