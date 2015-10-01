using UnityEngine;
using System.Collections;
using SimpleJSON;
using System.IO;

public class JSONFile : MonoBehaviour {

	// Use this for initialization
	void Start () {
		SimpleJSON.JSONNode node = SimpleJSON.JSONNode.Parse (Resources.Load<TextAsset>);

		File.WriteAllText(@"/Users/chishengliang/Documents/VistaLight/Assets/test", JSON);

	}
	
	// Update is called once per frame
	void Update () {
	}
}
