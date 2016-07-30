using UnityEngine;
using System.Collections;

public class SceneSetting : Singleton<SceneSetting> {
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

	// The ship speed in oil spilling area
	public double ShipSpeedInOilSpill = 2.0;

	// The ship speed in dispersant area
	public double ShipSpeedInDispersantArea = 1.0;

	// The map to load
	public string MapName = "houston_game_0"; // Sane default for debugging

	// Whether or not give recommendation
	public bool GiveRecommendation = false;

	// Show the reason why the recommendation is given
	public bool RecommendWithJustification = false;

	// Is this player the master client?
	public bool IsMaster = true;

	void Start() {
		PhotonNetwork.offlineMode = true; // This should go somewhere else
	}

	[PunRPC]
	void SetMap(string mapName) {
		MapName = mapName;
	}
}
