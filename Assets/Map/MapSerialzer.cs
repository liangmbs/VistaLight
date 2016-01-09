using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class MapSerialzer {

	public void SaveMap(Map map, string path) {
		BinaryFormatter serializer = new BinaryFormatter();
		FileStream file = File.Create(path);

		serializer.Serialize(file, map);
		file.Close();
	}

	public Map LoadMap(string path) {
		Map map = null;
		try {
			FileStream file = File.Open(path, FileMode.Open);
			BinaryFormatter deserializer = new BinaryFormatter();
			map = (Map)deserializer.Deserialize(file);
			file.Close();
		} catch (FileNotFoundException e) {
			Debug.Log(e.Message);
		} catch (IOException e) {
			Debug.Log(e.Message);
		}
		return map;
	}
}
