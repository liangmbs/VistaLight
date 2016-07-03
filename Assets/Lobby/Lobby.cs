using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using Photon;

public class Lobby : PunBehaviour {
	public InputField RoomNameInput;

	// Use this for initialization
	void Start () {
		PhotonNetwork.ConnectUsingSettings ("");
		PhotonNetwork.automaticallySyncScene = true;
	}

	private string PrintError(object[] codeAndMsg) {
		string error = System.String.Format ("Failed: {0}: {1}", codeAndMsg [0], codeAndMsg [1]);
		RoomNameInput.text = "";
		RoomNameInput.placeholder.GetComponent<Text> ().text = error;
		RoomNameInput.placeholder.GetComponent<Text> ().color = Color.red;
		return error;
	}

	public void Create() {
		PhotonNetwork.CreateRoom (RoomNameInput.text);

		// Only allow the master client into the task selection scene
		PhotonNetwork.automaticallySyncScene = false;
		PhotonNetwork.LoadLevel ("HostScene/TaskSelectionScene/TaskSelection");
	}

	public override void OnPhotonCreateRoomFailed(object[] codeAndMsg) {
		throw new System.Exception (PrintError (codeAndMsg));
	}

	public void Join() {
		PhotonNetwork.JoinRoom (RoomNameInput.text);
	}

	public override void OnPhotonJoinRoomFailed(object[] codeAndMsg) {
		throw new System.Exception (PrintError (codeAndMsg));
	}
}
