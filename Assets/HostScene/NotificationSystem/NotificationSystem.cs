using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class NotificationSystem : MonoBehaviour {

	public GameObject NotificationPrefab;
	public List<NotificationBox> NotificationBoxes = new List<NotificationBox>();
	public GameObject notificationList;

	public Scrollbar scrollBar;
	private bool scrolling = false;

	public Timer timer; 

	// Use this for initialization
	void Start () {
	
	}

	public void ScrollToBottom() {
		if (!scrolling) {
			StartCoroutine (ScrollToBottomAnimation ());
		}
	}

	public IEnumerator ScrollToBottomAnimation() {
		scrolling = true;
		yield return new WaitForEndOfFrame ();
		while (true) {
			scrollBar.value -= (float) 0.01;
			if (scrollBar.value == 0) {
				break;
			}
			yield return null;
		}
		scrolling = false;
	}
	
	// Update is called once per frame
	void UpdateStyle () {
		int gap = 2;
		int currentY = -gap;	
		foreach (NotificationBox notificatinoBox in NotificationBoxes) {
			notificatinoBox.gameObject.GetComponent<RectTransform>().localPosition = new Vector3(0, currentY, 0);
			notificatinoBox.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector3(0, 90, 0);
			currentY -= 90 + gap;
		}
			
		// Update container size
		float currentX = notificationList.GetComponent<RectTransform> ().sizeDelta.x;
		notificationList.GetComponent<RectTransform> ().sizeDelta = new Vector2 (currentX, -currentY);
	}

	// Add a notification to the system
	private void AddNotification(Notification notification) {

		GameObject notificationGO = GameObject.Instantiate(NotificationPrefab);
		NotificationBox notificationBox = notificationGO.GetComponent<NotificationBox>();
		notificationBox.Notification = notification;


		NotificationBoxes.Add(notificationBox);

		notificationGO.transform.SetParent(notificationList.transform);

		UpdateStyle ();

		ScrollToBottom ();
	}

	public void Notify(NotificationType type, string content) {
		Notification notification = new Notification ();
		notification.time = timer.virtualTime;
		notification.content = content;
		notification.type = type;

		AddNotification (notification);
	}
}
