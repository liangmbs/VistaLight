using UnityEngine;
using System.Collections;

public class MovingScreen1 : MonoBehaviour {

	protected float HorizontalSpeed =5.0f;
	protected float VerticalSpeed =5.0f;
	public GameObject BackGound;
	
	void LateUpdate(){
		

		
		if (Input.GetAxis ("Mouse ScrollWheel") >0 ) {
			if(Camera.main.orthographicSize < 50)
				Camera.main.orthographicSize++;
			else{
				
			}
		}
		
		if (Input.GetAxis ("Mouse ScrollWheel") <0) {
			if(Camera.main.orthographicSize > 25)
				Camera.main.orthographicSize --;
			else{
				
			}
		}
	}

	/*
	bool ViewingPosCheck ()
	{
		RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
		print (hit.collider);

		if (hit.collider != null) {
			// This position isn't appropriate.
			print (hit);
			return false;
		}
		// If we haven't hit anything or we've hit the player, this is an appropriate position.
		else {
			return true;
		}
	}*/
}
