using UnityEngine;
using System.Collections;

public class MapEventHandlerFactory {

	public IMapEventHandler Produce(MapEvent mapEvent) {

		if (mapEvent is ShipGenerationEvent) {
			return new ShipGenerationMapEventHandler((ShipGenerationEvent)mapEvent);
		} else if (mapEvent is OilSpillingEvent) {
			return new OilSpillingMapEventHandler((OilSpillingEvent)mapEvent);
		}
		
		return new DullMapEventHandler();
	}

}
