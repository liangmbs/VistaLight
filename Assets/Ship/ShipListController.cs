using UnityEngine;
using System.Collections;

public class ShipListController : MonoBehaviour {

	public GameObject entryPrefab;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void AddShip(ShipController shipController) {
		GameObject entryGO = GameObject.Instantiate(entryPrefab) as GameObject;
		ShipListEntryController entryController = entryGO.GetComponent<ShipListEntryController>();
		entryController.shipController = shipController;
		entryGO.transform.SetParent(this.transform);

		RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
		rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x,
			rectTransform.sizeDelta.y + 30);
	}

	public void RemoveShip(ShipController shipController) {
		foreach (Transform child in GameObject.Find("ShipList").transform) {
			GameObject childGO = child.gameObject;
			ShipListEntryController entryController = childGO.GetComponent<ShipListEntryController>();
			if (entryController != null && entryController.shipController == shipController) {
				Destroy(childGO);
			} 
		}

		RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
		rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x,
			rectTransform.sizeDelta.y - 30);
	}
}
