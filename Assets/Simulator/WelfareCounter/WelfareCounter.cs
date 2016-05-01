using UnityEngine;
using System.Collections;

public class WelfareCounter : MonoBehaviour {

	public double Welfare = 5;
	public bool freeze = false;

	void Awake() {
		DontDestroyOnLoad(transform.gameObject);
	}

	public void Update() {
		if (freeze) return;
		AutoWelfareRecovery();
	}

	public void AutoWelfareRecovery() {
		Timer timer = GameObject.Find("Timer").GetComponent<Timer>();
		double recoverRate = 1e-8;
		double recoverAmount = timer.TimeElapsed.TotalSeconds * recoverRate;
		GainWelfare(recoverAmount);
	}

	public void GainWelfare(double amount) {
		Welfare += amount;
		LimitWelfareRange();	
	}

	public void ReduceWelfare(double amount) {
		Welfare -= amount;
		LimitWelfareRange();
	}

	private void LimitWelfareRange() {
		if (Welfare < 0) Welfare = 0;
		if (Welfare > 5) Welfare = 5;
	}

}
