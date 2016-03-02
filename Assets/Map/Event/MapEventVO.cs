using UnityEngine;
using System.Collections;
using System;

public class MapEventVO : MonoBehaviour, MapSelectableVO {

	public MapEvent MapEvent;
	public bool IsSelected = false;

	public GameObject shipGenerationEvent;
	public GameObject shipGenerationEventSelected;
	public GameObject oilSpillingEvent;
	public GameObject oilSpillingEventSelected;

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

		HideAllChildren();
		if (IsSelected) {
			if (MapEvent is ShipGenerationEvent) {
				shipGenerationEventSelected.SetActive(true);
			} else if (MapEvent is OilSpillingEvent) {
				oilSpillingEventSelected.SetActive(true);
            }
		} else { 
			if (MapEvent is ShipGenerationEvent) {
				shipGenerationEvent.SetActive(true);
			} else if (MapEvent is OilSpillingEvent) {
				oilSpillingEvent.SetActive(true);
            }
		}
	}

	public void OnMouseDrag() {
		RaycastHit2D ray = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
		MapEvent.X = ray.point.x;
		MapEvent.Y = ray.point.y;
	}

	public void HideAllChildren() {
		shipGenerationEvent.SetActive(false);
		shipGenerationEventSelected.SetActive(false);
		oilSpillingEvent.SetActive(false);
		oilSpillingEventSelected.SetActive(false);
	}

	public void Select() {
		IsSelected = true;
	}

	public void Deselect() {
		IsSelected = false;
	}

    public GameObject GetSidePanel()
    {
		if (MapEvent is ShipGenerationEvent) {
			GameObject sidePanel = GameObject.Find("SidePanels").transform.FindChild("ShipGenerationMapEventSidePanel").gameObject;
			ShipGenerationEventSidePanelController controller = sidePanel.GetComponent<ShipGenerationEventSidePanelController>();
			controller.shipGenerationEvent = (ShipGenerationEvent)MapEvent;
			controller.UpdateDisplay();
			return sidePanel;
		} else if (MapEvent is OilSpillingEvent) {
			GameObject sidePanel = GameObject.Find("SidePanels").transform.FindChild("OilSpillingMapEventSidePanel").gameObject;
			OilSpillingMapEventSidePanelController controller = sidePanel.GetComponent<OilSpillingMapEventSidePanelController>();
			controller.OilSpillingEvent = (OilSpillingEvent)MapEvent;
			controller.UpdateDisplay();
			return sidePanel;
		}
		return null;
    }
}
