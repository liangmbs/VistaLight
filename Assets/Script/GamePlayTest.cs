using UnityEngine;
using System.Collections;

public class GamePlayTest : MonoBehaviour {
	/*
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
		clientsocket.writeSocket("steven");
		test = clientsocket .readSocket ();
		Debug.Log (test);
}
	
	// Update is called once per frame
	void Update () {
		clientsocket.writeSocket("steven");
		test = clientsocket.readSocket ();
		Debug.Log (test);

	}*/
	/*
		private Vector3 ResetCamera;
		private Vector3 Origin;
		private Vector3 Diference;
		private bool    Drag=false;

		void Start () {
			ResetCamera = Camera.main.transform.position;
		}
		
		void LateUpdate () {
			if (Input.GetMouseButton (0)) {
				Diference=(Camera.main.ScreenToWorldPoint (Input.mousePosition))- Camera.main.transform.position;
				if (Drag==false){
					Drag=true;
					Origin=Camera.main.ScreenToWorldPoint (Input.mousePosition);
				}
			} else {
				Drag=false;
			}
			if (Drag==true){
				Camera.main.transform.position = Origin-Diference;
			}
			//RESET CAMERA TO STARTING POSITION WITH RIGHT CLICK
			if (Input.GetMouseButton (1)) {
				Camera.main.transform.position=ResetCamera;
			}
		}*/

	protected float HorizontalSpeed =10.0f;
	protected float VerticalSpeed =10.0f;

	public BoxCollider Bounds;
	//public float h;
	//public float v;
	//public float z;
	private Vector3 min,max; 


	void Start(){
		min = Bounds.bounds.min;
		max = Bounds.bounds.max;
	}


	void LateUpdate(){

		if (Input.GetButton ("Fire1")) {

			float h = HorizontalSpeed * Input.GetAxis ("Mouse Y");
			float v = VerticalSpeed * Input.GetAxis ("Mouse X");

			transform.Translate (v, h, 0);	
		}

		if (Input.GetAxis ("Mouse ScrollWheel") >0) {
			if(Camera.main.orthographicSize < 33)
				Camera.main.orthographicSize++;
			else{

			}
		}

		if (Input.GetAxis ("Mouse ScrollWheel") <0) {
			if(Camera.main.orthographicSize > 5)
				Camera.main.orthographicSize --;
			else{

			}
		}
			transform.position = new Vector3 (
			Mathf.Clamp (transform.position.x, min.x, max.x),
			Mathf.Clamp (transform.position.y, min.y, max.y),
			Mathf.Clamp (transform.position.z, min.z, max.z));
		}
	
}



