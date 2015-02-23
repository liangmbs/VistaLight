using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	

	public void StartGameScene(string SceneName)
	{
		Application.LoadLevel (SceneName);
	}

	/* You need to set up a loading menu
	public void LoadGameScene(string SceneName)
	{
		Application.LoadLevel (SceneName);
	}*/
	
}
