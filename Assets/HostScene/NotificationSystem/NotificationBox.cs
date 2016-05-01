using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class NotificationBox : MonoBehaviour {

	public GameObject box;

	public GameObject BackgroundDisaster;
	public GameObject BackgroundInformation;
	public GameObject BackgroundRecommendataion;
	public GameObject BackgroundWarning;
	public GameObject BackgroundSuccess;

	public Notification Notification;

	public Text TimeBox;
	public Text ContentBox;


	// Use this for initialization
	void Start () {
		ContentBox.text = Notification.content;
		TimeBox.text = Notification.time.ToString (Map.DateTimeFormat);

		SelectBackground ();
	}

	private void SelectBackground() {
		BackgroundDisaster.SetActive (false);
		BackgroundInformation.SetActive (false);
		BackgroundRecommendataion.SetActive (false);
		BackgroundWarning.SetActive (false);
		BackgroundSuccess.SetActive (false);

		switch (Notification.type) {
		case NotificationType.Disaster:
			BackgroundDisaster.SetActive (true);
			break;

		case NotificationType.Information:
			BackgroundInformation.SetActive (true);
			break;

		case NotificationType.Recommendataion:
			BackgroundRecommendataion.SetActive (true);
			break;

		case NotificationType.Warning:
			BackgroundWarning.SetActive (true);
			break;

		case NotificationType.Success:
			BackgroundSuccess.SetActive (true);
			break;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

}
