using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameOverSceneStarter : MonoBehaviour {

	public PriorityQueue priorityQueue;
	public PriorityQueue waitList;

	public BudgetCounter budgetCounter;
	public WelfareCounter welfareCounter;

	public bool gameStarted = false;

	public VistaLightsLogger logger;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (!gameStarted) {
			if (priorityQueue.GetCount() != 0 || waitList.GetCount() != 0) {
				gameStarted = true;
			}
		} else {
			if (priorityQueue.GetCount() == 0 && waitList.GetCount() == 0) {
				LoadGameOverScene();
			} else if (welfareCounter.Welfare <= 0) {
				LoadGameOverScene();
			}
		}
	}

	void LoadGameOverScene() {
		budgetCounter.freeze = true;
		welfareCounter.freeze = true;

		logger.LogGameOver (budgetCounter.money, welfareCounter.Welfare);

		SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
    }
}
