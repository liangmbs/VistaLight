using UnityEngine;
using System.Collections;
using System;

public class DullMapEventHandler : MonoBehaviour, IMapEventHandler {
	public void Process() {
		Debug.Log("Something happened");
	}
}
