using UnityEngine;
using System.Collections;
using System;
using SimpleJSON;

public class Timer : MonoBehaviour {
	public double speed;

	public DateTime gameStartTime;
	private DateTime virtualTime = new DateTime(2015, 10, 10, 10, 10, 10);

	private TimeSpan timeElapsed = new TimeSpan(0, 0, 0);

	private double previousTime;

	void Awake() {
		DontDestroyOnLoad(transform.gameObject);
	}

	// Use this for initialization
	void Start () {
		previousTime = Time.time;
		GetComponent<PhotonView> ().ObservedComponents.Add (this);
	}
	
	// Update is called once per frame
	void Update () {
		if (settings.IsMaster) {
			double currentTime = Time.time;

			double virtualTimeAdvance = (currentTime - previousTime) * speed;
			virtualTime = virtualTime.AddSeconds(virtualTimeAdvance);
			timeElapsed = TimeSpan.FromSeconds(virtualTimeAdvance);

			previousTime = currentTime;
		}
	}

	public DateTime VirtualTime {
		get { return virtualTime; }
		set { virtualTime = value; }
	}

	public TimeSpan TimeElapsed {
		get { return timeElapsed; }
	}

	public double Speed {
		get { return speed; }
		set { speed = value; } 
	}

	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
		if (stream.isWriting) {
			stream.SendNext (Speed);
			stream.SendNext (gameStartTime.ToBinary());
			stream.SendNext (VirtualTime.ToBinary());
			stream.SendNext (TimeElapsed.Ticks);
		} else {
			Speed = (double)stream.ReceiveNext ();
			gameStartTime = DateTime.FromBinary((System.Int64)stream.ReceiveNext ());
			VirtualTime= DateTime.FromBinary((System.Int64)stream.ReceiveNext ());
			timeElapsed = TimeSpan.FromTicks((long)stream.ReceiveNext ());
		}
	}
}
