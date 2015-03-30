using UnityEngine;
using System.Collections;
using System;

public class GamePlayTest : MonoBehaviour {

	public GUISkin Property; 
	//public Texture aTexture;

	public int priority;





	void OnGUI(){
		/*GUI.skin = Property;

		GUI.BeginGroup(new Rect(Screen.width/2-150,Screen.height/2-150,300,300));
			GUI.Box(new Rect(0,0,300,300),"Decision Making");
			//GUI.DrawTexture(new Rect(10, 50, 80,60), aTexture,ScaleMode.ScaleToFit, true, 1.33F);
			 if (GUI.Button (new Rect (100, 50, 150, 20), "Decision1"))
					print ("decision1");
			 if(GUI.Button (new Rect (100, 70 ,150, 20),  "Decision2" ))
					print ("decision2");	
			 if(GUI.Button (new Rect (100, 90, 150, 20),  "Decision3"))
					print ("decision3");
			 if(GUI.Button (new Rect (100, 110, 150, 20), "Decision4"))
					print ("decision4");
		GUI.Button (new Rect (0, 0, 20, 20), "closed");
			GUI.EndGroup ();*/



		//windowRect = GUI.Window(shipID,windowRect,DoMyWindow, new GUIContent(x.ToString ()));
		//GUI.skin = hitObject.GetComponent<Ship>().property;
		GUI.BeginGroup(new Rect(Screen.width/2-150,Screen.height/2-150,300,300));
		GUI.Box(new Rect(0,0,300,300),"Ship Property");
		//GUI.DrawTexture(new Rect(10, 50, 80,60), hitObject.GetComponent<Ship>().icon,ScaleMode.ScaleToFit, true, 1.33F);
		//GUI.Label (new Rect (100, 50, 150, 20), "company: " + company);
		//GUI.Label (new Rect (100, 70,150, 20), "Ship ID: " + hitObject.GetComponent<Ship>().shipID);
		//GUI.Label (new Rect (100, 90, 150, 20), "capacity: " + capacity);
		//if (GUI.Button (new Rect (0, 0, 20, 20), "closed")) {
		//	showproperty = !showproperty;
		//}
		GUI.Label(new Rect (30,200,60,20),"Priority: ");	
		priority = Convert.ToInt32(GUI.TextField(new Rect(100,200,40,20), priority.ToString(),100));
		//hitObject.GetComponent<Ship>().priority = priority;
		GUI.Button (new Rect (120, 250, 50, 50), "Submit");
		GUI.EndGroup ();
	}

}







