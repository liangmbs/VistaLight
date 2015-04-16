using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class BarPresent : MonoBehaviour {

	public RectTransform moneytransform;
	private float cachedY;
	private float minXValue ;
	private float maxXValue;
	public float currentMoney;
	public float maxMoney = 10000.0f;

	public Image visualMoney;

	void Start(){

		maxXValue = moneytransform.position.x;
		minXValue = moneytransform.position.x - moneytransform.rect.width;
	}

	void Update(){
		HandleMoney();
	}

	private void HandleMoney(){
		cachedY = moneytransform.position.y;
		float currentXValue = MapValues (currentMoney, -10000.0f, maxMoney, minXValue, maxXValue);

		moneytransform.position = new Vector3 (currentXValue, cachedY);

		if (currentMoney > 0) {
			visualMoney.color = new Color32 ((byte)MapValues(currentMoney,maxMoney/2 , maxMoney, 255, 0 ), 255, 0, 255);
		}

		if (currentMoney <= 0){

			visualMoney.color = new Color32 (255, (byte)MapValues(currentMoney,-10000.0f , maxMoney/2, 0, 255 ), 0, 255);
		}
	}
	
	private float MapValues(float x, float inMin, float inMax, float outMin, float outMax){

		return (x - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
	}


}
