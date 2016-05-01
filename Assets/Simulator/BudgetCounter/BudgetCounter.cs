using UnityEngine;
using System.Collections;
using System;

public class BudgetCounter : MonoBehaviour {

	public double money = 0;
	public double moneyOfLastCycle = 0;
	public double moneyChange = 0;
	public bool freeze = false;

	void Awake() {
		DontDestroyOnLoad(transform.gameObject);
	}

	public void SpendMoney(double amount) {
		money -= amount;	
	}

	public void EarnMoney(double amount) {
		money += amount;
	}

	public void Update() {
		if (freeze) return;
		Timer timer = GameObject.Find("Timer").GetComponent<Timer>();
		if (timer.speed == 0) {
			return;
		};
		TimeSpan timeElapsed = timer.TimeElapsed;

		double newMoneyChange = (money - moneyOfLastCycle) / timeElapsed.TotalSeconds * 3600;
		if (double.IsNaN (newMoneyChange)) {
			newMoneyChange = 0;
		}
		double alpha = 3e-4;
		for (int i = 0; i < timeElapsed.TotalSeconds; i++) {
			moneyChange = (1 - alpha) * moneyChange + alpha * newMoneyChange;
		}
		moneyOfLastCycle = money;
		
	}

}
