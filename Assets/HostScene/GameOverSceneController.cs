using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOverSceneController : MonoBehaviour {

	public Text title;
	public Text moneyText;
	public Text welfareText;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		double money = GameObject.Find("BudgetCounter").GetComponent<BudgetCounter>().money;
		double welfare = GameObject.Find("WelfareCounter").GetComponent<WelfareCounter>().Welfare;

		if (welfare < 1e-3) {
			title.text = "You Lose.";
		} else {
			title.text = "You Win";
		}

		moneyText.text = string.Format("Money: {0:C}", money);
		welfareText.text = string.Format("Welfare: {0:F0}", welfare);
	}


}
