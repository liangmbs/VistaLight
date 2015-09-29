using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class DotGenerator : MonoBehaviour {

	public int DotID = 0;
	public GameObject Dots;
	public List<GameObject> CreatedDots= new List<GameObject>();


	public GameObject SingleDirectionLane;
	public GameObject BiDirectionalLane;
	public GameObject Intersection;
	public GameObject Line;

	public int property;

	// Use this for initialization
	void Start () {
			SingleDirectionLane.GetComponent<Button> ().onClick.AddListener (() => {
			 getButton("SingleDirenctional");});
		BiDirectionalLane.GetComponent<Button> ().onClick.AddListener (() => {
			getButton ("BiDirectional");});
		Intersection.GetComponent<Button> ().onClick.AddListener (() => {
			getButton ("Intersection");});
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			RaycastHit2D ray = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
			if(ray.collider != null){
				GameObject CreatedDot = Instantiate (Dots, ray.point, transform.rotation) as GameObject;
				CreatedDots.Add (CreatedDot);
				CreatedDots [DotID].name = DotID.ToString();	
				DotID++;
			}
			if(DotID > 1){

				print (CreatedDots.Count);
				Vector3 objectLine = (CreatedDots[DotID-1].transform.position - CreatedDots[DotID-2].transform.position);
				print ("Here\n");
				float segmentDistance = 2.0f;
				float distance = objectLine.magnitude;
				print (distance);
				float remainingDistance = distance;
				Vector3 previousDot = CreatedDots[DotID - 2].transform.position	;
				while (remainingDistance > segmentDistance) {
					Vector3 nextPoint = previousDot + objectLine.normalized * segmentDistance;
					previousDot = nextPoint;
					print (nextPoint);
					remainingDistance -= segmentDistance;
					GameObject line = Instantiate (Line, nextPoint, Quaternion.identity) as GameObject;
				}
				/*
				Vector3 creationPoint = CreatedDots[DotID-2].transform.position;
				float creationPointDistance = (creationPoint- 
				                               CreatedDots[DotID-2].transform.position).magnitude;


				while(creationPointDistance < distance){

					creationPoint += objectLine.normalized * creationPointDistance;
					creationPointDistance = (creationPoint- 
					                         CreatedDots[DotID-2].transform.position).magnitude;
					if( creationPointDistance < distance){
						GameObject line = Instantiate (Line, creationPoint, Quaternion.identity) as GameObject;
					}
				}
				*/
			}
		}
		
	}



	public void getButton(string mission){

		switch (mission) {

		case "SingleDirectional":
			property = 1;
			break;

		case "BiDirectional":
			property = 2;
			break;

		case "Intersection":
			property = 3;
			break;
		}
	}

}
