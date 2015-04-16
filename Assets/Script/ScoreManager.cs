using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour {

	public float score;

	// Awake
	void Awake() {
		DontDestroyOnLoad (gameObject.transform);
	}
}
