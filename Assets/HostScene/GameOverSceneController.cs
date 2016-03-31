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
	public Text dockUtilAchievedText;

	public Text budgetTargetText;
	public Text ecoEfficiencyTargetText;
	public Text welfareTargetText;
	public Text dockUtilTargetText;

	public double budgetTarget;
	public double ecoEfficiencyTarget;
	public double welfareTarget;
	public double dockUtilTarget;

	public double budgetAchieved;
	public double ecoEfficiencyAchieved;
	public double welfareAchieved;
	public double dockUtilAchieved;

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
		TimeSpan procTime = map.EndTime - map.StartTime;
		ecoEfficiencyTarget = budgetTarget / procTime.TotalHours;
		dockUtilTarget = map.TargetDockUtilization;
	}

	private void ShowTargetValues() {
		budgetTargetText.text = string.Format ("${0:N0}", budgetTarget);
		welfareTargetText.text = string.Format ("{0:F2}", welfareTarget);
		dockUtilTargetText.text = string.Format ("{0:P2}", dockUtilTarget);
		ecoEfficiencyTargetText.text = string.Format ("${0:N0} / hour", ecoEfficiencyTarget);
	}

	private void GetAchievedValues() {
		budgetAchieved = GameObject.Find ("BudgetCounter").GetComponent<BudgetCounter> ().money;
		welfareAchieved = GameObject.Find ("WelfareCounter").GetComponent<WelfareCounter> ().Welfare;

		Timer timer = GameObject.Find ("Timer").GetComponent<Timer> ();
		TimeSpan procTime = timer.VirtualTime - timer.gameStartTime;

		ecoEfficiencyAchieved = budgetAchieved / procTime.TotalHours;

		DockUtilizationCounter dockUtilizationCounter = GameObject.Find ("DockUtilizationCounter").GetComponent<DockUtilizationCounter> ();
		dockUtilAchieved = dockUtilizationCounter.CalculateAverageUtilization ();
	}

	private void ShowAchievedtValues() {
		budgetAchievedText.text = string.Format ("${0:N0}", budgetAchieved);
		welfareAchievedText.text = string.Format ("{0:F2}", welfareAchieved);
		dockUtilAchievedText.text = string.Format ("{0:P2}", dockUtilAchieved);
		ecoEfficiencyAchievedText.text = string.Format ("${0:N0} / hour", ecoEfficiencyAchieved);
	}

	private bool IsPlayerWin() {
		if (budgetAchieved < budgetTarget) {
			return false;
		}

		if (welfareAchieved < welfareTarget) {
			return false;
		}

		if (dockUtilAchieved < dockUtilTarget) {
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
