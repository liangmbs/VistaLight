using UnityEngine;
using System.Collections;

public class ShipPanelController : MonoBehaviour {

	public GameObject shipGenreateEntryPrefab;
	public GameObject shipListPanel;
	public MapController map;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void AddShip() {
		// Entry Height
		RectTransform entryTransform = shipGenreateEntryPrefab.GetComponent<RectTransform>();
		float entryHeight = entryTransform.sizeDelta.y;

		// Enlarge ship list panel
		RectTransform transform = shipListPanel.GetComponent<RectTransform>();
		Vector3 positionToPlaceNewEntry = new Vector3(0, -transform.sizeDelta.y, 0);
        transform.sizeDelta = new Vector2(transform.sizeDelta.x, transform.sizeDelta.y + entryHeight);

		// Add a new ship entry
		GameObject entry = GameObject.Instantiate(shipGenreateEntryPrefab);
		entry.transform.parent = shipListPanel.transform;
		entryTransform = entry.GetComponent<RectTransform>();
		entryTransform.localPosition = positionToPlaceNewEntry;

		// Create new ship

	}
}
