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
	//private InformationInput information;
	//TcpClient mySocket;
	//NetworkStream theStream;
	//StreamWriter theWriter;
	//StreamReader theReader;
	string Host = " ";
	Int32 Port = 8000; 
	//string test;
	//string tmpreader;
	Socket client;
	static List<string> sharedMemory = new List<string>() ;
	public static string saved_string;


	public static JSOCreatShip detection;

	private static ManualResetEvent connectDone = new ManualResetEvent(false);


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
	/*
	void FixedUpdate(){
		//writeSocket ("abcdefg");
		tmpreader = readSocket ();
		if(tmpreader != "")
			print (tmpreader);
	}
*/


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

			/*
			theStream = mySocket.GetStream();
			theWriter = new StreamWriter(theStream);
			theReader = new StreamReader(theStream);
			socketReady = true;
			maintainConnection();
			if(socketReady ==  true)
				StartGameScene("GamePlay");*/

			//send test data to the remote device
		
		}

		catch(Exception e){
			Debug.Log ("socket error: " + e);

		}
	}



	private static void ConnectCallback(IAsyncResult ar){
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

	private static void ReceiveCallback(IAsyncResult ar){
		try {
			//retrieve the state object and the client socket from the asynchronous state object
			StateObject state = (StateObject)ar.AsyncState;
			Socket client = state.workSocket;

			//read data from the remote device
			int bytesRead = client.EndReceive (ar);

			if (bytesRead > 0) {
				//There might be more data, so store the data received so far.
				//state.sb.Append (Encoding.ASCII.GetString (state.buffer, 0, bytesRead));
				//print (Encoding.ASCII.GetString (state.buffer, 0, bytesRead));

				//print (detection);
				//calling function from other scripts
				//detection.Detection(Encoding.ASCII.GetString (state.buffer, 0, bytesRead));
				//sharedMemory.Add (Encoding.ASCII.GetString (state.buffer, 0, bytesRead));
				string words = Encoding.ASCII.GetString (state.buffer, 0, bytesRead);
				//print (words);
				split(words);


				//Get the rest of the data.
				client.BeginReceive(state.buffer,0, StateObject.BufferSize,0, new AsyncCallback(ReceiveCallback),state);

			

				//print result
				//print (state.sb.ToString());

			} 
		} catch (Exception e) {
			Console.WriteLine (e.ToString ());
		}
	}

	void Update(){
		//if(socketReady){
			//Receive ();
			ReadMomory();
		//}
	}



	void ReadMomory(){
		for (int i =0; i< sharedMemory.Count; i++) {
			string json = sharedMemory[i];
			detection.Detection(json);
		}
		sharedMemory.Clear();

	}


	 static void split(string words){
		/*
		string[] temp = words.Split (';');

		int i = temp.Length - 1;

		saved_string = temp [i];
		if(saved_string ==)


		sharedMemory.Add (saved_string);
		for (int k = 1; k < temp.Length - 2; k++) {
			sharedMemory.Add (temp[k]);
		}
*/		string[] temp = words.Split (';');
		/*
		if(saved_string == "" && words[words.Length - 1] == ';'){
			try{
				sharedMemory.Add(temp[0]);
			}
			catch{
				saved_string = temp[0];
			}
		}
		if (saved_string == "" && words[words.Length - 1] != ';') {
			saved_string = temp[temp.Length - 1];
		}
		if (saved_string != "") {
			string add = saved_string + temp[0];
			sharedMemory.Add (add);
		}*/
		for (int i = 0; i< temp.Length-1; i++) {
			addSegment(temp[i]);
			print (temp[i])	;	
		}

	}



	static void addSegment(string segment){
		if (saved_string != "") {
			segment = saved_string + segment;
		}
		try{
			JSON.Parse(segment);
			sharedMemory.Add (segment) ;
			saved_string = "";
		}
		catch(Exception){
			saved_string  = segment ;
		}


	}

	public void StartGameScene(string SceneName)
	{
		Application.LoadLevel (SceneName);
	}


}


