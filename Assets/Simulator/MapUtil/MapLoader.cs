using UnityEngine;
using System.Collections;

public class MapLoader : MonoBehaviour {

	public Timer timer;
	public TimeWidgetController timeWidgetController;
	public string mapName = "houston_game_1";

	public void LoadMap () {
		// Load map
		string path = Application.dataPath + "/maps/" + mapName + ".vlmap";
        MapController mapController = GameObject.Find("Map").GetComponent<MapController>();
		MapSerializer mapSerializer = new MapSerializer();
		Map map = mapSerializer.LoadMap(path);
		mapController.RegenerateMap(map);

		timer.VirtualTime = map.StartTime;
		timer.gameStartTime = map.StartTime;

		// GameObject.Find ("RoundManager").GetComponent<RoundManager> ().SimulationPhaseStartTime = map.StartTime;
		// timeWidgetController.SetSpeedOne();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
