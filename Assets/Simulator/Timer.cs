using UnityEngine;
using System.Collections;
using System;

public class Timer : MonoBehaviour {

	public double speed;
	public DateTime virtualTime = new DateTime(2015, 10, 10, 10, 10, 10);

	private double previousTime;

	// Use this for initialization
	void Start () {
		previousTime = Time.time;
		speed = 100;
	}
	
	// Update is called once per frame
	void Update () {
		double currentTime = Time.time;

		double virtualTimeAdvance = (currentTime - previousTime) * speed;
		virtualTime = virtualTime.AddSeconds(virtualTimeAdvance);

		previousTime = currentTime;
	}

	public DateTime VirtualTime {
		get { return virtualTime; }
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
