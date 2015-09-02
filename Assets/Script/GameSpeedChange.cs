using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class GameSpeedChange : MonoBehaviour {

	public ClientSocket send;
	public GameObject paused;
	public GameObject times1;
	public GameObject times2;
	public GameObject times3;

	void Start(){
//		send = GameObject.Find("ClientSocketObject").GetComponent <ClientSocket>();
		paused.GetComponent<Button> ().onClick.AddListener (() => {
			pausing ();});
		times1.GetComponent<Button> ().onClick.AddListener (() => {
			time1 ();});
		times2.GetComponent<Button> ().onClick.AddListener (() => {
			time2 ();});
		times3.GetComponent<Button> ().onClick.AddListener (() => {
			time3 ();});
	}

	private void pausing(){
		send.Send ("{\"action\": \"game speed\",\"speed\": 0}");
	}

	private void time1(){
		send.Send ("{\"action\": \"game speed\",\"speed\": 1}");
	}

	private void time2(){
		send.Send ("{\"action\": \"game speed\",\"speed\": 2}");
	}

	private void time3(){
		send.Send ("{\"action\": \"game speed\",\"speed\": 3}");
	}



}
