using UnityEngine;
using UnityEditor;
using System.Collections;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.IO;
using System.Xml.Serialization;

public class MapInfoSidePanelController : MonoBehaviour {

	public GameObject mapInformationSetting;
	public MapController mapController;
	public Map map;
	public bool hasModification = false;
	public string path = "";

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void updateMapInformationDisplay() {
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
		mapController.RegenerateMap(map);
		hasModification = false;

		mapInformationSetting.SetActive(true);
		updateMapInformationDisplay();
	}

	public void LoadMap() {

		CloseMap();

		string[] mapTypes = {
			"VistaLights Map Files", "vlmap",
			"All Files", "*"
		};
		path = EditorUtility.OpenFilePanel("Load map", "", "vlmap");

		try {
			FileStream file = File.Open(path, FileMode.Open);
			BinaryFormatter deserializer = new BinaryFormatter();
			this.map = (Map)deserializer.Deserialize(file);
			file.Close();
		} catch (FileNotFoundException e) {
			Debug.Log(e.Message);
		} catch (IOException e) {
			Debug.Log(e.Message);
		}

		mapController.RegenerateMap(map);

		mapInformationSetting.SetActive(true);
		this.updateMapInformationDisplay();

		Debug.Log(map.nodes.Count);
	} 

	public void SaveMap() {
		if (path == "") {
			path = EditorUtility.SaveFilePanel("Select file location", "", "map", "vlmap");
		}

		// Serialize
		BinaryFormatter serializer = new BinaryFormatter();
		FileStream file = File.Create(path);

		serializer.Serialize(file, map);
		file.Close();
	}

	public void SaveMapAs() {
	}

	public void CloseMap() {
		map = null;
		mapController.CloseMap();

		mapInformationSetting.SetActive(false);

		path = "";
		hasModification = false;
	}
}
