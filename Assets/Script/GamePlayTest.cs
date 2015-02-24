using UnityEngine;
using System.Collections;

public class GamePlayTest : MonoBehaviour {

	private ClientSocket clientsocket;
	public string test;
	public GameObject ClientSocketObject;

	void Awake()
	{
		print ("Awake");
		ClientSocketObject = GameObject.Find ("ClientSocketObject");
		clientsocket  = ClientSocketObject.GetComponent <ClientSocket>();
		//DontDestroyOnLoad (transform.gameObject);
		print (clientsocket);

	}



	// Use this for initialization
	void Start () {
		/*clientsocket.writeSocket("steven");
		test = clientsocket .readSocket ();
		Debug.Log (test);*/
	}
	
	// Update is called once per frame
	void Update () {
		clientsocket.writeSocket("steven");
		test = clientsocket.readSocket ();
		Debug.Log (test);

	}
}
