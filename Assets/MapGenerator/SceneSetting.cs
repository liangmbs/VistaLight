using UnityEngine;
using System.Collections;

public class SceneSetting : MonoBehaviour {

	void Awake() {
		DontDestroyOnLoad(transform.gameObject);
		DestoryIfInstanceExist ();
	}

	private static SceneSetting instance = null;
	void DestoryIfInstanceExist() {
		if (instance != null) {
			Destroy (gameObject); 
			return;
		}
		instance = this;
	}

	// This field is telling if the gameobjects are in a real game, or in 
	// the map editor
	public bool AllowMapEditing;

	// Checks if the game is in toturial mode
	public bool inTutorial;

	// The cargo to unload per second. The same for all industries
	public double UnloadingSpeed = 1;

	// The ship speed. We assume that all ship travels at the same speed 
	// in the map.
	public double ShipSpeed = 3.0;

	// The map to load
	public string MapName = "";

	// Whether or not give recommendation
	public bool GiveRecommendation = false;

	// Show the reason why the recommendation is given
	public bool RecommendWithJustification = false;
}
