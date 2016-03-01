using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class WelfareVO : MonoBehaviour {

	public WelfareCounter WelfareCounter;
	public List<Image> stars = new List<Image>();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		double welfare = WelfareCounter.Welfare;
		double numHalfStars = welfare * 2;
		for (int i = 1; i < 11; i++) {
			if (i <= numHalfStars) {
				stars[i - 1].gameObject.SetActive(true);
			} else {
				stars[i - 1].gameObject.SetActive(false);
			}
		}
	}
}
