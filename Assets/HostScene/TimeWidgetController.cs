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

	public RoundManager roundManager;
	public NotificationSystem notificationSystem;


	// Use this for initialization
	void Start () {
		// SelectedImage = Resources.Load ("image/SpeedButtonSelected.png") as Sprite;
		// UnselectedImage = Resources.Load ("image/SpeedButton.png") as Sprite;
	}

	public void PauseGameButtonClickHandler(){
		if (roundManager.phase == GamePhase.Simulation) {
			notificationSystem.Notify (NotificationType.Warning, 
				"Cannot Pause the game during the simulation phase");
			return;
		}
		PauseGame ();
	}

	public void PauseGame() {
		
		SetGameSpeed (0);
		SelectButton (PauseButtonImage);
		PauseRedBorder.SetActive (true);
	}

	public void SpeedOneButtonClickHandler() {
		if (roundManager.phase == GamePhase.Decision) {
			notificationSystem.Notify (NotificationType.Warning, 
				"Please use the \"Submit and Continue\" button to submit your decision and enter simulation phase");
			return;
		}
		SetSpeedOne ();
	}

	public void SetSpeedOne() {
		SetGameSpeed (100);
		SelectButton (Speed1ButtonImage);
		PauseRedBorder.SetActive (false);
	}

	public void SpeedTwoButtonClickHandler() {
		if (roundManager.phase == GamePhase.Decision) {
			notificationSystem.Notify (NotificationType.Warning, 
				"Please use the \"Submit and Continue\" button to submit your decision and enter simulation phase");
			return;
		}
		SetSpeedTwo ();
	}

	public void SetSpeedTwo() {
		if (roundManager.phase == GamePhase.Decision) {
			notificationSystem.Notify (NotificationType.Warning, 
				"Please use the \"Submit and Continue\" button to submit your decision and enter simulation phase");
			return;
		}
		SetGameSpeed (400);
		SelectButton (Speed2ButtonImage);
		PauseRedBorder.SetActive (false);
	}

	public void SpeedThreeButtonClickHandler() {
		if (roundManager.phase == GamePhase.Decision) {
			notificationSystem.Notify (NotificationType.Warning, 
				"Please use the \"Submit and Continue\" button to submit your decision and enter simulation phase");
			return;
		}
		SetSpeedThree ();
	}

	public void SetSpeedThree() {
		if (roundManager.phase == GamePhase.Decision) {
			notificationSystem.Notify (NotificationType.Warning, 
				"Please use the \"Submit and Continue\" button to submit your decision and enter simulation phase");
			return;
		}
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
