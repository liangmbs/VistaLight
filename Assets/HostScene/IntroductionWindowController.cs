using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class IntroductionWindowController : MonoBehaviour {

	public MapController mapController;
	public Text descriptionText;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void UpdateText() {
		string description = descriptionText.text;

		description = FillInTimeText (description);
		description = FillInMoneyText (description);
		description = FillInWelfareText (description);

		descriptionText.text = description;
	}

	private string FillInTimeText(string template) {
		Map map = mapController.Map;
		DateTime startTime = map.StartTime;
		DateTime endTime = map.EndTime;
		TimeSpan timeSpan = map.EndTime - map.StartTime;

		string timeString = string.Format ("{0:N0} days", timeSpan.TotalDays);

		return template.Replace ("{time}", timeString);

	}

	private string FillInMoneyText(string template) {
		Map map = mapController.Map;
		double money = map.TargetBudget;

		string moneyString = string.Format ("${0:N0}", money);

		return template.Replace ("{money}", moneyString);
	}

	private string FillInWelfareText(string template) {
		Map map = mapController.Map;
		double welfare = map.TargetWelfare;

		string welfareString = string.Format ("{0:N2}", welfare);

		return template.Replace ("{welfare}", welfareString);
	}
}
