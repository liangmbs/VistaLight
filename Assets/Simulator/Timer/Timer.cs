using UnityEngine;
using System.Collections;
using System;

public class Timer : MonoBehaviour {

	public double speed;
	public DateTime virtualTime = new DateTime(2015, 10, 10, 10, 10, 10);
	private TimeSpan timeElapsed = new TimeSpan(0, 0, 0);
	private Vector3 position;

	private double previousTime;


	// Use this for initialization
	void Start () {
		previousTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		double currentTime = Time.time;

		double virtualTimeAdvance = (currentTime - previousTime) * speed;
		virtualTime = virtualTime.AddSeconds(virtualTimeAdvance);
		timeElapsed = TimeSpan.FromSeconds(virtualTimeAdvance);

		previousTime = currentTime;
	}

	public DateTime VirtualTime {
		get { return virtualTime; }
	}

	public TimeSpan TimeElapsed {
		get { return timeElapsed; }
	}

	public double Speed {
		get { return speed; }
		set { speed = value; } 
	}

	public void Pause() {
		speed = 0;
	}

	public void SetSpeedOne() {
		speed = 100;
	}

	public void SetSpeedTwo() {
		speed = 300;
	}

	public void SetSpeedThree() {
		speed = 1000;
	}
}
