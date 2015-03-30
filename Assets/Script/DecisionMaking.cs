using UnityEngine;
using System.Collections;

public class DecisionMakng : MonoBehaviour {
	
	public GUISkin Property; 
	//public Texture aTexture;
	
	
	
	
	
	
	void OnGUI(){
		GUI.skin = Property;
		
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
		GUI.EndGroup ();
	}
	
}







