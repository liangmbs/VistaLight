using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.IO;
using System.Xml.Serialization;

public class MapInfoSidePanelController : MonoBehaviour {

	public GameObject mapInformationSetting;
	public MapController mapController;
	public ShipPanelController shipPannelController;
	public Map map;
    public ToolSelector toolSelector;

	public InputField mapNameInput;
	public InputField startTimeInput;
	public InputField endTimeInput;
	public InputField targetBudgetInput;
	public InputField targetWelfareInput;
	public Slider targetWelfareSlider;

	public bool hasModification = false;
	public string path = "";

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void NewMap() {
		if (mapController.Map != null) {
			CloseMap();
		}
		map = new Map();
		mapController.RegenerateMap(map);
		hasModification = false;

		mapInformationSetting.SetActive(true);
		UpdateDisplay();
	}

	public void LoadMap() {
		CloseMap();

		#if UNITY_EDITOR
		path = EditorUtility.OpenFilePanel("Load map", "", "vlmap");
		#endif

		MapSerializer mapSerializer = new MapSerializer();
		this.map = mapSerializer.LoadMap(path);

		mapController.RegenerateMap(map);
		shipPannelController.RegenerateShip(map.ships);
		mapController.RegenerateMapEvents();

		mapInformationSetting.SetActive(true);
		UpdateDisplay ();
	} 

	

	public void SaveMap() {
		if (path == "") {
			#if UNITY_EDITOR
			path = EditorUtility.SaveFilePanel("Select file location", "", "map", "vlmap");
			#endif
		}

		MapSerializer mapSerializer = new MapSerializer();
		mapSerializer.SaveMap(map, path);
	}

	public void SaveMapAs() {
	}

	public void CloseMap() {
		map = null;
		mapController.CloseMap();
		shipPannelController.ClearShips();

		mapInformationSetting.SetActive(false);

		path = "";
		hasModification = false;

        toolSelector.DeselectCurrentTool();
	}

	public void UpdateData() {
		map.Name = mapNameInput.text;
		map.StartTime = DateTime.Parse(startTimeInput.text);
		map.EndTime = DateTime.Parse (endTimeInput.text);
		map.TargetBudget = double.Parse (targetBudgetInput.text);
		map.TargetWelfare = targetWelfareSlider.value;

		UpdateDisplay();
	}

	public void UpdateDisplay() {
		mapNameInput.text = map.Name;
		startTimeInput.text = map.StartTime.ToString(Map.DateTimeFormat);
		endTimeInput.text = map.EndTime.ToString (Map.DateTimeFormat);
		targetBudgetInput.text = map.TargetBudget.ToString ();
		targetWelfareInput.text = map.TargetWelfare.ToString ("F");
		targetWelfareSlider.value = (float)map.TargetWelfare;
	}
}
