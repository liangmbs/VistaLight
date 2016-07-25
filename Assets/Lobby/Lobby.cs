using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using Photon;

public class Lobby : PunBehaviour {
	public InputField RoomNameInput;
	public SceneSetting Settings;

	private bool ConnectedToMaster = false;
	// Use this for initialization
	void Start () {
		PhotonNetwork.ConnectUsingSettings ("");
		PhotonNetwork.automaticallySyncScene = true;
	}

	public override void OnConnectedToMaster() {
		ConnectedToMaster = true;
	}

	public override void OnJoinedLobby() {
		ConnectedToMaster = true;
	}

	private string PrintError(object[] codeAndMsg) {
		string error = System.String.Format ("Failed: {0}: {1}", codeAndMsg [0], codeAndMsg [1]);
		RoomNameInput.text = "";
		RoomNameInput.placeholder.GetComponent<Text> ().text = error;
		RoomNameInput.placeholder.GetComponent<Text> ().color = Color.red;
		return error;
	}

	public void Create() {
		if (RoomNameInput.text == "" || !ConnectedToMaster) {
			return;
		}

		// Only allow the master client into the task selection scene
		PhotonNetwork.automaticallySyncScene = false;
		PhotonNetwork.LoadLevel ("HostScene/TaskSelectionScene/TaskSelection");
		Settings.IsMaster = true;
	}

	public override void OnPhotonCreateRoomFailed(object[] codeAndMsg) {
		throw new System.Exception (PrintError (codeAndMsg));
	}

	public void Join() {
		if (RoomNameInput.text == "" || !ConnectedToMaster) {
			return;
		}
		PhotonNetwork.JoinRoom (RoomNameInput.text);
		Settings.IsMaster = false;
	}

	public override void OnJoinedRoom() {
		if (SceneSetting.Instance.IsMaster) {
			// Only allow the master client into the task selection scene
			PhotonNetwork.automaticallySyncScene = false;
			PhotonNetwork.LoadLevel ("HostScene/TaskSelectionScene/TaskSelection");
		}
	}

	public override void OnPhotonJoinRoomFailed(object[] codeAndMsg) {
		throw new System.Exception (PrintError (codeAndMsg));
	}
}
