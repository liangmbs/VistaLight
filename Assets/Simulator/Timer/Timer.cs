﻿using UnityEngine;
using System.Collections;
using System;
using SimpleJSON;

public class Timer : MonoBehaviour {
	public double speed;

	public DateTime gameStartTime;
	private DateTime virtualTime = new DateTime(2015, 10, 10, 10, 10, 10);

	private TimeSpan timeElapsed = new TimeSpan(0, 0, 0);

	private double previousTime;

	void Awake() {
		DontDestroyOnLoad(transform.gameObject);
	}

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
		set { virtualTime = value; }
	}

	public TimeSpan TimeElapsed {
		get { return timeElapsed; }
	}

	public double Speed {
		get { return speed; }
		set { speed = value; } 
	}
}
