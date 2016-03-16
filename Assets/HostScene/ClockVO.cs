using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ClockVO : MonoBehaviour {

	public Timer Timer;
	public Image HourHand;
	public Image MinuteHand;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		int hour = Timer.VirtualTime.Hour;
		int minute = Timer.VirtualTime.Minute;
		int second = Timer.VirtualTime.Second;

		float hourAngle = -(float) (hour * 30.0 + minute / 2.0 + second * 30.0 / 3600.0);
		float minuteAngle = -(float) (minute * 6.0 + second / 10.0);

		HourHand.gameObject.GetComponent<RectTransform>().eulerAngles = new Vector3 (0, 0, hourAngle);
		MinuteHand.gameObject.GetComponent<RectTransform>().eulerAngles = new Vector3 (0, 0, minuteAngle);
	}
}
