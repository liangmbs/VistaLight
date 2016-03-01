using UnityEngine;
using System.Collections;
using System;

public class OilSpillingController : MonoBehaviour {

	public double Radius;
	public double Amount;
	public Vector2 position;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(position.x, position.y, 0);
		transform.localScale = new Vector3((float)Radius / 2, (float)Radius / 2, 0);
		SpriteRenderer spriteRenderer = transform.Find("OilSpillingArea").GetComponent<SpriteRenderer>();
		spriteRenderer.color = new Color((float)0.2, (float)0.2, (float)0.2, (float)(Amount)/10000);

		ReduceWelfare();
	}

	private void ReduceWelfare() {
		WelfareCounter welfareCounter = GameObject.Find("WelfareCounter").GetComponent<WelfareCounter>();
		Timer timer = GameObject.Find("Timer").GetComponent<Timer>();

		double rate = 3e-9;
		double welfareChange = rate * Amount * timer.TimeElapsed.TotalSeconds;

		welfareCounter.ReduceWelfare(welfareChange);
		
	}
}
