using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeWidgetController : MonoBehaviour {

	public Timer timer;

	public Image PauseButtonImage;
	public Image Speed1ButtonImage;
	public Image Speed2ButtonImage;
	public Image Speed3ButtonImage;

	public Sprite SelectedImage;
	public Sprite UnselectedImage;

	public VistaLightsLogger logger;

	public GameObject PauseRedBorder;




	// Use this for initialization
	void Start () {
		// SelectedImage = Resources.Load ("image/SpeedButtonSelected.png") as Sprite;
		// UnselectedImage = Resources.Load ("image/SpeedButton.png") as Sprite;
	}

	public void PauseGame() {
		SetGameSpeed (0);
		SelectButton (PauseButtonImage);
		PauseRedBorder.SetActive (true);
	}

	public void SetSpeedOne() {
		SetGameSpeed (100);
		SelectButton (Speed1ButtonImage);
		PauseRedBorder.SetActive (false);
	}

	public void SetSpeedTwo() {
		SetGameSpeed (400);
		SelectButton (Speed2ButtonImage);
		PauseRedBorder.SetActive (false);
	}

	public void SetSpeedThree() {
		SetGameSpeed (1500);
		SelectButton (Speed3ButtonImage);
		PauseRedBorder.SetActive (false);
	}

	private void SetGameSpeed(double speed) {
		timer.speed = speed;
		logger.LogTimer(speed);
	}

	private void DeselectAllButtons() {
		PauseButtonImage.sprite = UnselectedImage;
		Speed1ButtonImage.sprite = UnselectedImage;
		Speed2ButtonImage.sprite = UnselectedImage;
		Speed3ButtonImage.sprite = UnselectedImage;
	}

	private void SelectButton(Image Button) {
		DeselectAllButtons ();
		Button.sprite = SelectedImage;
	}
}
