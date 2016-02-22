using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BudgetCounterVO : MonoBehaviour {

	public BudgetCounter budget;
	public Text moneyText;
	public Text moneyChangeText;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		moneyText.text = string.Format("${0:N}", budget.money);
		if (budget.moneyChange < 0) {
			moneyChangeText.text = string.Format("${0:N}", budget.moneyChange);
			moneyChangeText.color = Color.red;
		} else {
			moneyChangeText.text = string.Format("${0:N}", budget.moneyChange);
			moneyChangeText.color = Color.green;
		}
	}
}
