using UnityEngine;
using System.Collections;

public class SceneSetting : MonoBehaviour {

	// This field is telling if the gameobjects are in a real game, or in 
	// the map editor
	public bool AllowMapEditing;

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

	void Awake() {
		DontDestroyOnLoad(transform.gameObject);
	}
}
