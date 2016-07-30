using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapLoader : MonoBehaviour {

	public Timer timer;
	public TimeWidgetController timeWidgetController;
	private Map map;

	public void LoadMap () {
		// Load map
		string mapName = SceneSetting.Instance.MapName;
		string path = Application.dataPath + "/maps/" + mapName + ".vlmap";
        MapController mapController = GameObject.Find("Map").GetComponent<MapController>();
		MapSerializer mapSerializer = new MapSerializer();
		map = mapSerializer.LoadMap(path);
		mapController.RegenerateMap(map);

		timer.VirtualTime = map.StartTime;
		timer.gameStartTime = map.StartTime;

		// GameObject.Find ("RoundManager").GetComponent<RoundManager> ().SimulationPhaseStartTime = map.StartTime;
		// timeWidgetController.SetSpeedOne();

		PreprocessOilSpillingEvent ();
	}

	 private void PreprocessOilSpillingEvent() {
		int numOilSpillingEvent = CountOilSpillingEvent();

		if(numOilSpillingEvent == 0) {
			return;
		}

		System.Random random = new System.Random ();
		int indexSelect = random.Next () % numOilSpillingEvent;

		int index = 0;
		List<MapEvent> mapEventsToRemove = new List<MapEvent>();
		foreach (MapEvent mapEvent in map.mapEvents) {
			if (mapEvent is OilSpillingEvent) {
				if (index != indexSelect) {
					mapEventsToRemove.Add (mapEvent);
				}
				index++;
			}
		}
		map.mapEvents.RemoveAll(mapEvent => mapEventsToRemove.Contains(mapEvent));
	}

	int CountOilSpillingEvent() {
		int count = 0;
		foreach (MapEvent mapEvent in map.mapEvents) {
			if (mapEvent is OilSpillingEvent) {
				count++;
			}
		}
		return count;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
