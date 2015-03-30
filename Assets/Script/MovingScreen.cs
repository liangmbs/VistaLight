using UnityEngine;
using System.Collections;

public class MovingScreen : MonoBehaviour {

	protected float HorizontalSpeed =5.0f;
	protected float VerticalSpeed =5.0f;
	
	public BoxCollider Bounds;
	//public float h;
	//public float v;
	//public float z;
	private Vector3 min,max; 
	
	
	void Start(){
		min = Bounds.bounds.min;
		max = Bounds.bounds.max;
	}
	
	
	void LateUpdate(){
		
		if (Input.GetButton ("Fire1")) {
			
			float h = HorizontalSpeed * Input.GetAxis ("Mouse Y");
			float v = VerticalSpeed * Input.GetAxis ("Mouse X");
			
			transform.Translate (v, h, 0);	
		}
		
		if (Input.GetAxis ("Mouse ScrollWheel") >0) {
			if(Camera.main.orthographicSize < 33)
				Camera.main.orthographicSize++;
			else{
				
			}
		}
		
		if (Input.GetAxis ("Mouse ScrollWheel") <0) {
			if(Camera.main.orthographicSize > 5)
				Camera.main.orthographicSize --;
			else{
				
			}
		}
		transform.position = new Vector3 (
			Mathf.Clamp (transform.position.x, min.x, max.x),
			Mathf.Clamp (transform.position.y, min.y, max.y),
			Mathf.Clamp (transform.position.z, min.z, max.z));
	}
}
