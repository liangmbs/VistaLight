using UnityEngine;
using System;
using System.Collections;
using System.IO;
using System.Text;

public class JSONFile : MonoBehaviour {
	/*
	public string path;

	public void NameFiles(){


	}
*/
	// Use this for initialization
	public void Generate () {
		string path = @"/Users/chishengliang/Documents/VistaLight/Assets/MapGenerator/mapbase.json";
		if (!File.Exists (path)) {

			string createText = "Hello this is json file" + Environment.NewLine;
			File.WriteAllText(path, createText);
			print ("generated");
		}
		string appendText = "this is extra text" + Environment.NewLine;
		File.AppendAllText (path, appendText);
	}
}
