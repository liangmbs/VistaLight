using UnityEngine;
using System.Collections;

// Basic script to connect logger to a Unity scene and start it up.
public class BasicLoggerStartup : MonoBehaviour
{
	public string gameId;
	public string version;
	public string condition;
	
	public bool displayErrors = true;
	
	private const string PLAYER_ID_KEY = "LoggingPlayerId";
	
	
	
	void Awake()
	{
		if (!TheLogger.instance.IsSessionBegun()) {
			string player_id = null;
			if (PlayerPrefs.HasKey(PLAYER_ID_KEY)) {
				player_id = PlayerPrefs.GetString(PLAYER_ID_KEY);
			} else {
				player_id = System.Guid.NewGuid().ToString();
				PlayerPrefs.SetString(PLAYER_ID_KEY, player_id);
			}
			
			IAdapter[] adapters = GetComponents<IAdapter>();
			
			string adapterInfo;
			if (adapters.Length == 0) {
				adapterInfo = "No logging adapters found.";
			} else {
				adapterInfo = "Found " + adapters.Length.ToString() + " logging adapters:";
				foreach (IAdapter adapter in adapters) {
					adapterInfo += " " + adapter.GetType().Name;
				}
			}
			Debug.Log(adapterInfo);
			
			TheLogger.instance.BeginSession(adapters, gameId, player_id, null, version, condition, null);
			TheLogger.instance.BeginRun("game", null);
		}
	}
	
	void OnGUI()
	{
		if (displayErrors) {
			string error = TheLogger.instance.Error ();
			if (error != null) {
				GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "Logging error: " + error);
			}
		}
	}

	void OnDestory() {
		TheLogger.instance.EndRun(null);
		TheLogger.instance.EndSession(null);
	}
}
