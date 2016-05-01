using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class CargoTypeTooltipController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	public GameObject tooltipPrefab;
	public GameObject UICanvas;

	private GameObject tooltip;
	private bool isTooltipShowing;

	void Awake() {
		UICanvas = GameObject.Find ("UI");
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (isTooltipShowing) {
			// Debug.Log (Input.mousePosition);
			Vector2 mousePosition = Input.mousePosition;
			tooltip.GetComponent<RectTransform> ().anchoredPosition = 
				new Vector2 (mousePosition.x + 2, mousePosition.y + 2);
		}	
	}

	public void OnPointerEnter(PointerEventData e) {
		tooltip = GameObject.Instantiate (tooltipPrefab);
		tooltip.transform.SetParent (UICanvas.transform);

		isTooltipShowing = true;
	}

	public void OnPointerExit(PointerEventData e) {
		Destroy (tooltip);
		isTooltipShowing = false;
	}
}
