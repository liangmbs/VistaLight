using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class GameOverSceneController : MonoBehaviour {

	public Text title;
	public Text moneyText;
	public Text moneyPerHourText;
	public Text welfareText;
	public Text timeText;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		double money = GameObject.Find("BudgetCounter").GetComponent<BudgetCounter>().money;
		double welfare = GameObject.Find("WelfareCounter").GetComponent<WelfareCounter>().Welfare;

		Timer timer = GameObject.Find ("Timer").GetComponent<Timer> ();
		TimeSpan virtualTimePassed = timer.VirtualTime - timer.gameStartTime;

		if (welfare < 1e-3) {
			title.text = "You Lose.";
		} else {
			title.text = "You Win";
		}

		moneyText.text = string.Format("Money: {0:C}", money);
		moneyPerHourText.text = string.Format ("Money per Hour: {0:C}", money / virtualTimePassed.TotalHours);
		welfareText.text = string.Format("Welfare: {0:F2}", welfare);
		timeText.text = string.Format ("Time Used: {0:F2} hours", virtualTimePassed.TotalHours);
	}


}
