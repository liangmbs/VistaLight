using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShipGenerationEventSidePanelController : MonoBehaviour {

    public ShipGenerationEvent shipGenerationEvent;
    public InputField timeInput;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void UpdateDisplay() {
        if (shipGenerationEvent == null) return;

        timeInput.text = shipGenerationEvent.Time.ToString(Map.DateTimeFormat);
    }
}
