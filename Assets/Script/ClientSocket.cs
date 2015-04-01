using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using SimpleJSON;
using System.Linq;


public class ClientSocket : MonoBehaviour {

	bool socketReady = false;
	string Host = "";
	Int32 Port = 8000; 
	Socket client;
	List<string> sharedMemory = new List<string>() ;
	public string saved_string;
	public JSOCreatShip detection;
	private StringBuilder jsonStringBuilder = new StringBuilder ();
	private int bracketCount = 0;
	private ManualResetEvent connectDone = new ManualResetEvent(false);


	public class StateObject{
		//client Socket
		public Socket workSocket = null;
		//size of receive buffer.
		public const int BufferSize = 256;
		//receive buffer
		public byte[] buffer = new byte[BufferSize];
		//received data string
		public StringBuilder sb = new StringBuilder();

	}

	void Start(){
		detection = GameObject.Find("Main Camera").GetComponent <JSOCreatShip>();
		//Debug.Log(detection);

	}

	void OnLevelWasLoaded(int level){
		detection = GameObject.Find("Main Camera").GetComponent <JSOCreatShip>();
		Debug.Log(detection);
	}


	void Awake()
	{

		//information = GetComponent <InformationInput>();
		DontDestroyOnLoad (transform.gameObject);

	}


	public void setupSocket(){

		try{
			Host = PlayerPrefs.GetString("IPaddress");
			print (Host);
			//mySocket = new TcpClient(Host, Port);
			client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

			//connect to the remote endpoint
			client.BeginConnect(Host, Port, new AsyncCallback(ConnectCallback), client);

			connectDone.WaitOne();
			socketReady = true;
			Receive();
		
		}

		catch(Exception e){
			Debug.Log ("socket error: " + e);

		}
	}



	private void ConnectCallback(IAsyncResult ar){
		try {
			//retrieve the socket from the state object.
			Socket client = (Socket)ar.AsyncState;
			
			//complete the connection.
			client.EndConnect (ar);
			Console.WriteLine ("Socket Connected Touch {0}", client.RemoteEndPoint.ToString ());

			//signal that the connection has been made
			connectDone.Set ();

			
		} catch (Exception e) {
			Console.WriteLine (e.ToString ());
			
		}
	}



	private void Receive(){
		try {
			//create the state object.
			StateObject state = new StateObject ();
			state.workSocket = client;

			//Begin receiving the data from the remote device
			client.BeginReceive (state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback (ReceiveCallback), state);

		} catch (Exception e) {
			Debug.Log (e.ToString ());
		}
	}

	private void ReceiveCallback(IAsyncResult ar){
		try {
			//retrieve the state object and the client socket from the asynchronous state object
			StateObject state = (StateObject)ar.AsyncState;
			Socket client = state.workSocket;

			//read data from the remote device
			int bytesRead = client.EndReceive (ar);

			if (bytesRead > 0) {

				string words = Encoding.ASCII.GetString (state.buffer, 0, bytesRead);
				split (words);
				client.BeginReceive(state.buffer,0, StateObject.BufferSize,0, new AsyncCallback(ReceiveCallback),state);



			} 
		} catch (Exception e) {
			Console.WriteLine (e.ToString ());
		}
	}


	public void Send(String data) {
		// Convert the string data to byte data using ASCII encoding.
		byte[] byteData = Encoding.ASCII.GetBytes(data);



		//if (JSOCreatShip.accept == true) {
			// Begin sending the data to the remote device.
			client.BeginSend (byteData, 0, byteData.Length, SocketFlags.None,
		                 new AsyncCallback (SendCallback), client);
		//}
	}
	
	
	private void SendCallback(IAsyncResult ar) {
		try {
			// Retrieve the socket from the state object.
			Socket client = (Socket) ar.AsyncState;
			
			// Complete sending the data to the remote device.
			int bytesSent = client.EndSend(ar);
			Console.WriteLine("Sent {0} bytes to server.", bytesSent);

			/*if(JSOCreatShip.accept == true){
				Console.WriteLine("Sent {0} bytes to server.", bytesSent);
				JSOCreatShip.accept = false;
			}*/

		} catch (Exception e) {
			Console.WriteLine(e.ToString());
		}
	}


	void Update(){
		ReadMomory();
	}




	void ReadMomory(){
		for (int i =0; i< sharedMemory.Count; i++) {
			string json = sharedMemory[i];
			detection.Detection(json);
		}
		sharedMemory.Clear();

	}

	void split(string words){
		
		for (int i = 0; i < words.Length; i++) {
			jsonStringBuilder.Append(words[i]);
			
			if (words[i] == '{') {
				bracketCount++;
			} else if (words[i] == '}') {
				bracketCount--;
				if (bracketCount == 0){
					sharedMemory.Add (jsonStringBuilder.ToString());
					jsonStringBuilder = new StringBuilder();
				} else if (bracketCount < 0) {
					jsonStringBuilder = new StringBuilder();
				}
			}
		}
		
	}

	
	public void StartGameScene(string SceneName)
	{
		Application.LoadLevel (SceneName);
	}


}


