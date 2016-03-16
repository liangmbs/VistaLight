using UnityEngine;
using System.Collections;

public class MapLoader : MonoBehaviour {

	public Timer timer;
	public TimeWidgetController timeWidgetController;

	// Use this for initialization
	void Start () {
		// Load map
		string path = Application.dataPath + "/maps/Houston-easy.vlmap";
        MapController mapController = GameObject.Find("Map").GetComponent<MapController>();
		MapSerializer mapSerializer = new MapSerializer();
		Map map = mapSerializer.LoadMap(path);
		mapController.RegenerateMap(map);

		timer.virtualTime = map.StartTime;

		timeWidgetController.SetSpeedOne();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
