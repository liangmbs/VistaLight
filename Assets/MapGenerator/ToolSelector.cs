using UnityEngine;
using System.Collections;

public class ToolSelector : MonoBehaviour {

	public GameObject RoadSubTray;
	public GameObject DockSubTray;
	public GameObject EventSubTray;

	public IMapEditorTool toolSelected = null;

	// Use this for initialization
	void Start () {
		GrayOutAllToolButtons();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void CloseAllSubTrays() {
		RoadSubTray.SetActive(false);
		DockSubTray.SetActive(false);
		EventSubTray.SetActive(false);
	}

	public void OpenRoadSubTray() {
		CloseAllSubTrays();
		RoadSubTray.SetActive(true);
	}

	public void OpenDockSubTray() {
		CloseAllSubTrays();
		DockSubTray.SetActive(true);
	}

	public void OpenEventSubTray() {
		CloseAllSubTrays();
		EventSubTray.SetActive(true);
	}

	public void SelectTool(IMapEditorTool tool) {
		// Destory previous tool
		if (toolSelected != null) {
			toolSelected.Destory();
		}

		// Make all other buttons gray
		GrayOutAllToolButtons();

		// Select the new tool
		toolSelected = tool;
	}

	private void GrayOutAllToolButtons() {
		RoadSubTray.GetComponent<SubTrayController>().DeselectAllTools();
		DockSubTray.GetComponent<SubTrayController>().DeselectAllTools();
		EventSubTray.GetComponent<SubTrayController>().DeselectAllTools();
	}
}
