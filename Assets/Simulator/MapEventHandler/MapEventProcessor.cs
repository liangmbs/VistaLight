using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public class MapEventProcessor : MonoBehaviour {

	public List<MapEvent> MapEvents { 
		get { 
			if (mapController == null || mapController.Map == null) {
				return new List<MapEvent> ();
			}
			return mapController.Map.mapEvents; 
		}
	}

	public MapController mapController;
	public Timer timer;

	private DateTime previousTime = new DateTime(1970, 1, 1);

	private MapEventHandlerFactory mapEventHandlerFactory = new MapEventHandlerFactory();

	// Use this for initialization
	void Start () {
	}



	private bool ShouldHappen(MapEvent mapEvent) {
		DateTime currentTime = timer.VirtualTime;

		if (mapEvent.Time.CompareTo(previousTime) > 0 &&
			mapEvent.Time.CompareTo(currentTime) <= 0) {
			return true;
		}

		return false;
	}

	private void Happen(MapEvent mapEvent) {
		IMapEventHandler handler = mapEventHandlerFactory.Produce(mapEvent);
		handler.Process();
	}
	
	// Update is called once per frame
	void Update () {
		if (SceneSetting.Instance.IsMaster) {
			List<MapEvent> triggeredMapEvent = new List<MapEvent> ();
			foreach (MapEvent mapEvent in MapEvents) {
				if (ShouldHappen (mapEvent)) {
					Happen (mapEvent);
					triggeredMapEvent.Add (mapEvent);
				}
			}

			MapEvents.RemoveAll (mapEvent => triggeredMapEvent.Contains (mapEvent));

			previousTime = timer.VirtualTime;
		}
    }
}
