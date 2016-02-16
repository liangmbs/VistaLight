using UnityEngine;
using System.Collections;

public class MapEventHandlerFactory {

	public IMapEventHandler Produce(MapEvent mapEvent) {

		if (mapEvent is ShipGenerationEvent) {
			return new ShipGenerationMapEventHandler((ShipGenerationEvent) mapEvent);
		}
		
		return new DullMapEventHandler();
	}

}
