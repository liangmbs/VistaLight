using UnityEngine;
using System.Collections;

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
		transform.localScale = new Vector3((float)Radius, (float)Radius, 0);
	}
}
