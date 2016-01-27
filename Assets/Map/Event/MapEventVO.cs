using UnityEngine;
using System.Collections;
using System;

public class MapEventVO : MonoBehaviour, MapSelectableVO {

	public MapEvent MapEvent;
	public bool IsSelected = false;

	void Start () {
	
	}
	
	void Update () {
		gameObject.transform.position = new Vector3(
			(float)MapEvent.X, 
			(float)MapEvent.Y, 
			(float)MapController.MapEventZIndex);
		gameObject.transform.localScale = new Vector3(
			(float)(Camera.main.orthographicSize / 20),
			(float)(Camera.main.orthographicSize / 20),
			(float)1);
	}

	public void OnMouseDrag() {
		RaycastHit2D ray = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
		MapEvent.X = ray.point.x;
		MapEvent.Y = ray.point.y;
	}

	public void Select() {
		gameObject.transform.FindChild("EventSelected").gameObject.SetActive(true);
		gameObject.transform.FindChild("Event").gameObject.SetActive(false);
	}

	public void Deselect() {
		gameObject.transform.FindChild("EventSelected").gameObject.SetActive(false);
		gameObject.transform.FindChild("Event").gameObject.SetActive(true);
	}

    public GameObject GetSidePanel()
    {
        if (MapEvent is ShipGenerationEvent) {
            GameObject sidePanel = GameObject.Find("SidePanels").transform.FindChild("ShipGenerationMapEventSidePanel").gameObject;
            ShipGenerationEventSidePanelController controller = sidePanel.GetComponent<ShipGenerationEventSidePanelController>();
            controller.shipGenerationEvent = (ShipGenerationEvent)MapEvent;
            controller.UpdateDisplay();
            return sidePanel;
        }

        return null;
    }
}
