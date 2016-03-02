using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class OilSpillingMapEventSidePanelController : MonoBehaviour {

	public OilSpillingEvent OilSpillingEvent;
	public InputField TimeInput;
	public InputField RadiusInput;
	public InputField AmountInput;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void UpdateDisplay() {
		TimeInput.text = OilSpillingEvent.Time.ToString(Map.DateTimeFormat);
		RadiusInput.text = OilSpillingEvent.Radius.ToString();
		AmountInput.text = OilSpillingEvent.Amount.ToString();
	}

	public void UpdateData() {
		OilSpillingEvent.Time = DateTime.Parse(TimeInput.text);
		OilSpillingEvent.Radius = double.Parse(RadiusInput.text);
		OilSpillingEvent.Amount = double.Parse(AmountInput.text);

		UpdateDisplay();
	}
}
