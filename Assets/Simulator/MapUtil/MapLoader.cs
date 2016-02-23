using UnityEngine;
using System.Collections;

public class MapLoader : MonoBehaviour {

	public Timer timer;

	// Use this for initialization
	void Start () {
		// Load map
		string path = Application.dataPath + "/maps/Houston-easy.vlmap";
        MapController mapController = GameObject.Find("Map").GetComponent<MapController>();
		MapSerializer mapSerializer = new MapSerializer();
		Map map = mapSerializer.LoadMap(path);
		mapController.RegenerateMap(map);

		timer.virtualTime = map.StartTime;
		timer.SetSpeedOne();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
