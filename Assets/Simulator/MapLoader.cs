using UnityEngine;
using System.Collections;

public class MapLoader : MonoBehaviour {

	// Use this for initialization
	void Start () {
		// Load map
		string path = Application.dataPath + "/maps/map.vlmap";
        MapController mapController = GameObject.Find("Map").GetComponent<MapController>();
		MapSerializer mapSerializer = new MapSerializer();
		Map map = mapSerializer.LoadMap(path);
		mapController.RegenerateMap(map);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
