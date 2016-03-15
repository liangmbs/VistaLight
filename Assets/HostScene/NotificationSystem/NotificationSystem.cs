using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NotificationSystem : MonoBehaviour {

	public GameObject NotificationPrefab;
	public List<NotificationBox> NotificationBoxes = new List<NotificationBox>();
	public GameObject notificationList;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		int currentY = -10;	
		foreach (NotificationBox notificatinoBox in NotificationBoxes) {
			notificatinoBox.gameObject.GetComponent<RectTransform>().localPosition = new Vector3(10, currentY, 0);
			currentY -= 90 + 10;
		}

		float currentX = notificationList.GetComponent<RectTransform> ().sizeDelta.x;
		notificationList.GetComponent<RectTransform> ().sizeDelta = new Vector2 (currentX, -currentY);

		// Remove expired notifications
		for (int i = NotificationBoxes.Count - 1; i >= 0; i--) {
			NotificationBox box = NotificationBoxes[i];
			if (box.IsExpired()) {
				NotificationBoxes.RemoveAt(i);
				Destroy(box.gameObject);
			}
		}
	}

	// Add a notification to the system
	public void AddNotification(Notification notification) {

		GameObject notificationGO = GameObject.Instantiate(NotificationPrefab);
		NotificationBox notificationBox = notificationGO.GetComponent<NotificationBox>();
		notificationBox.Notification = notification;
		notificationBox.CreateTime = Time.time;
		notificationBox.ContentBox.text = notification.content;

		NotificationBoxes.Add(notificationBox);

		notificationGO.transform.SetParent(notificationList.transform);
	}
}
