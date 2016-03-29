using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverSceneController : MonoBehaviour {

	public Text title;

	public Text budgetAchievedText;
	public Text ecoEfficiencyAchievedText;
	public Text welfareAchievedText;
	public Text procTimeAchievedText;

	public Text budgetTargetText;
	public Text ecoEfficiencyTargetText;
	public Text welfareTargetText;
	public Text procTimeTargetText;

	public double budgetTarget;
	public double ecoEfficiencyTarget;
	public double welfareTarget;
	public TimeSpan procTimeTarget;

	public double budgetAchieved;
	public double ecoEfficiencyAchieved;
	public double welfareAchieved;
	public TimeSpan procTimeAchieved;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		double money = GameObject.Find("BudgetCounter").GetComponent<BudgetCounter>().money;
		double welfare = GameObject.Find("WelfareCounter").GetComponent<WelfareCounter>().Welfare;

		Timer timer = GameObject.Find ("Timer").GetComponent<Timer> ();
		TimeSpan virtualTimePassed = timer.VirtualTime - timer.gameStartTime;

		GetTargetValues ();
		ShowTargetValues ();

		GetAchievedValues ();
		ShowAchievedtValues ();

		if (IsPlayerWin()) {
			title.text = "You Win";
		} else {
			title.text = "You Lose";
		}

	}

	private void GetTargetValues() {
		MapController mapController = GameObject.Find ("Map").GetComponent<MapController> ();
		Map map = mapController.Map;

		budgetTarget = map.TargetBudget;
		welfareTarget = map.TargetWelfare;
		procTimeTarget = map.EndTime - map.StartTime;
		ecoEfficiencyTarget = budgetTarget / procTimeTarget.TotalHours;
	}

	private void ShowTargetValues() {
		budgetTargetText.text = string.Format ("${0:N0}", budgetTarget);
		welfareTargetText.text = string.Format ("{0:F2}", welfareTarget);
		procTimeTargetText.text = string.Format ("{0:F1} hours", procTimeTarget.TotalHours);
		ecoEfficiencyTargetText.text = string.Format ("${0:N0} / hour", ecoEfficiencyTarget);
	}

	private void GetAchievedValues() {
		budgetAchieved = GameObject.Find ("BudgetCounter").GetComponent<BudgetCounter> ().money;
		welfareAchieved = GameObject.Find ("WelfareCounter").GetComponent<WelfareCounter> ().Welfare;

		Timer timer = GameObject.Find ("Timer").GetComponent<Timer> ();
		procTimeAchieved = timer.VirtualTime - timer.gameStartTime;

		ecoEfficiencyAchieved = budgetAchieved / procTimeAchieved.TotalHours;
	}

	private void ShowAchievedtValues() {
		budgetAchievedText.text = string.Format ("${0:N0}", budgetAchieved);
		welfareAchievedText.text = string.Format ("{0:F2}", welfareAchieved);
		procTimeAchievedText.text = string.Format ("{0:F1} hours", procTimeAchieved.TotalHours);
		ecoEfficiencyAchievedText.text = string.Format ("${0:N0} / hour", ecoEfficiencyAchieved);
	}

	private bool IsPlayerWin() {
		if (budgetAchieved < budgetTarget) {
			return false;
		}

		if (welfareAchieved < welfareTarget) {
			return false;
		}

		return true;
	}

	public void BackButtonClickHandler() {
		foreach (GameObject o in GameObject.FindObjectsOfType<GameObject>()) {
			if (o != gameObject) {
				Destroy (o);
			}
		}
		SceneManager.LoadScene("TaskSelection", LoadSceneMode.Single);
	}


}
