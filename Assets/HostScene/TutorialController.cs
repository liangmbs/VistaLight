using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class TutorialController : MonoBehaviour {

	public SceneSetting sceneSetting;
	public GameObject tutorialWindow;
	public Text MinimizeButtonText;

	public List<GameObject> TutorialTabs = new List<GameObject> ();
	public List<Image> TutorialIndicators = new List<Image> ();

	public Sprite indicatorImage;
	public Sprite indicatorSelectImage;

	private bool isMinimized = false;
	private int tabShowing = 0;

	void Awake() {
		sceneSetting = GameObject.Find ("SceneSetting").GetComponent<SceneSetting> ();

		// indicatorImage = Resources.Load ("image/node.png") as Sprite;
		// indicatorSelectImage = Resources.Load ("image/node_select.png") as Sprite;

	}

	// Use this for initialization
	void Start () {
		if (sceneSetting.inTutorial) {
			ShowTutorialWindow ();
		}
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ShowTutorialWindow() {
		tutorialWindow.SetActive (true);
	}

	public void MinimizeTutorialWindow() {
		int x = -(Screen.width + 800 - 60) / 2;
		StartCoroutine(MoveTutorialWindow(new Vector2(x, 0)));

		isMinimized = true;

		MinimizeButtonText.text = ">";
	}

	public void RecoverTutorialWindow() {
		StartCoroutine(MoveTutorialWindow(new Vector2(0, 0)));

		isMinimized = false;

		MinimizeButtonText.text = "<";
	}

	public IEnumerator MoveTutorialWindow(Vector2 destination) {
		int totalFrame = 20;

		RectTransform rect = tutorialWindow.GetComponent<RectTransform> ();
		Vector2 begin = rect.anchoredPosition;

		for (int i = 0; i <= totalFrame; i++) {
			rect.anchoredPosition = Vector2.Lerp (begin, destination, (float)1.0 * i / totalFrame);
			yield return null;
		}
	}

	public void ToggleMinization() {
		if (isMinimized) {
			RecoverTutorialWindow ();
		} else {
			MinimizeTutorialWindow ();
		}
	}

	public void SelectTutorialTab(int index) {
		tabShowing = index;
		for (int i = 0; i < TutorialTabs.Count; i++) {
			if (i == index) {
				TutorialTabs [i].SetActive (true);
				TutorialIndicators [i].sprite = indicatorSelectImage;
			} else {
				TutorialTabs [i].SetActive (false);
				TutorialIndicators [i].sprite = indicatorImage;	
			}
		}
	}

	public void NextTutorialTab() {
		if (tabShowing >= TutorialTabs.Count - 1) {
			tabShowing = 0;
		} else {
			tabShowing += 1;
		}
		SelectTutorialTab (tabShowing);
	}
}
