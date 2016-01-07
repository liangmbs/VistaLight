using UnityEngine;
using UnityEditor;
using System.Collections;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.IO;

public class MapInfoSidePanelController : MonoBehaviour {

	public GameObject mapInformationSetting;
	public MapController mapController;
	public Map map;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void updateMapInforDisplay() {
		if (map != null) {
			GameObject.Find("MapNameInput").GetComponent<InputField>().text = map.Name;
			GameObject.Find("StartTimeInput").GetComponent<InputField>().text = 
				map.StartTime.ToString(Map.DateTimeFormat);
		}
	}

	public void NewMap() {
		if (mapController.Map != null) {
			CloseMap();
		}
		map = new Map();
		mapController.Map = map;

		mapInformationSetting.SetActive(true);
		updateMapInforDisplay();
	}

	public void LoadMap() {

		CloseMap();

		string[] mapTypes = {
			"VistaLights Map Files", "vlmap",
			"All Files", "*"
		};
		var path = EditorUtility.OpenFilePanel("Load map", "", "vlmap");

		try {
			FileStream file = File.Open(path, System.IO.FileMode.Open);
			BinaryFormatter deserializer = new BinaryFormatter();
			this.map = (Map)deserializer.Deserialize(file);
			file.Close();
		} catch (FileNotFoundException e) {
			Debug.Log(e.Message);
		} catch (IOException e) {
			Debug.Log(e.Message);
		}

		this.updateMapInforDisplay();
	} 

	public void SaveMap() {
		var path = EditorUtility.SaveFilePanel("Select file location", "", "map", "vlmap");

		// Serialize
		BinaryFormatter serializer = new BinaryFormatter();
		System.IO.FileStream file = System.IO.File.Create(path);

		serializer.Serialize(file, map);
		file.Close();

	}

	public void SaveMapAs() {
	}

	public void CloseMap() {
	}
}
