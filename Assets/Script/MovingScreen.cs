using UnityEngine;
using System.Collections;

public class MovingScreen : MonoBehaviour {

	protected float HorizontalSpeed =5.0f;
	protected float VerticalSpeed =5.0f;
	public GameObject BackGound;
	public BoxCollider Bounds;
	private Vector3 min,max; 
	public Transform UI;           // Reference to the player's transform.

	public BoxCollider2D InfoBar;
	public BoxCollider2D background;

	void Start(){
		min = Bounds.bounds.min;
		max = Bounds.bounds.max;
	}
	
	void LateUpdate(){
		
		if (Input.GetButton ("Fire1") && ViewingPosCheck() 	) {
			
			float h = HorizontalSpeed * Input.GetAxis ("Mouse Y");
			float v = VerticalSpeed * Input.GetAxis ("Mouse X");

			transform.Translate (v, h, 0);	
		}
		
		if (Input.GetAxis ("Mouse ScrollWheel") >0 ) {
			if(Camera.main.orthographicSize < 10)
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


	bool ViewingPosCheck ()
	{
		RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

		if (hit.collider != null) {
			// This position isn't appropriate.
			print (hit);
			return false;
		}
		// If we haven't hit anything or we've hit the player, this is an appropriate position.
		else {
			return true;
		}
	}
}
