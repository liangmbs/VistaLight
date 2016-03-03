using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class NotificationBox : MonoBehaviour {

	public GameObject box;
	public Notification Notification;
	public double CreateTime;
	public double NotificationPresentTimeInSec = 10;

	public Text ContentBox;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void MoveTo(Vector3 target) { 
	}

	public bool IsExpired() {
		double currentTime = Time.time;
		if (CreateTime + NotificationPresentTimeInSec < currentTime) {
			return true;
		}
		return false;
	}
}
