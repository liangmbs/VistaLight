using UnityEngine;
using System.Collections;

public class SceneSetting : MonoBehaviour {

	// This field is telling if the gameobjects are in a real game, or in 
	// the map editor
	public bool AllowMapEditing;

	// The cargo to unload per second. The same for all industries
	public double unloadingSpeed = 1;

	// The ship speed. We assume that all ship travels at the same speed 
	// in the map.
	public double shipSpeed = 3.0;

}
