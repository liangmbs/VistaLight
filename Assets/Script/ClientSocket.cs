using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Net.Sockets;

public class ClientSocket : MonoBehaviour {

	bool socketReady = false;
	private InformationInput information;
	TcpClient mySocket;
	NetworkStream theStream;
	StreamWriter theWriter;
	StreamReader theReader;
	string Host = " ";
	Int32 Port = 8000; 
	string test;


/*	void Start(){
		Debug.Log("sTART");
		setupSocket ();
		writeSocket("steven");
		test = readSocket ();
		Debug.Log (test);
	}*/

	void Awake()
	{

		information = GetComponent <InformationInput>();
		DontDestroyOnLoad (transform.gameObject);
	}


	public void setupSocket(){

		try{
			Host = PlayerPrefs.GetString("IPaddress");
			print (Host);
			mySocket = new TcpClient(Host, Port);
			theStream = mySocket.GetStream();
			theWriter = new StreamWriter(theStream);
			theReader = new StreamReader(theStream);
			socketReady = true;
			/*writeSocket("steven");
			test = readSocket ();
			Debug.Log (test);*/
			maintainConnection();
			if(socketReady ==  true)
				StartGameScene("GamePlay");
		
		}

		catch(Exception e){
			Debug.Log ("socket error: " + e);

		}
	}

	public void StartGameScene(string SceneName)
	{
		Application.LoadLevel (SceneName);
	}


	public void writeSocket (string theLine){
		if (!socketReady)
			return;
		string tmpString = theLine + "\r\n";
		//Debug.Log (theLine);
		theWriter.Write (tmpString);
		theWriter.Flush ();
	}


	public string readSocket(){
		if (!socketReady)
			return "";
		if (theStream.DataAvailable)
			return theReader.ReadLine ();
		return " ";
	}



	public void closeSocket(){
		if (!socketReady)
			return;
		theWriter.Close ();
		theReader.Close ();
		mySocket .Close ();
		socketReady = false;

	}


	public void maintainConnection(){
		if (!theStream.CanRead) {
			setupSocket ();
		}


	}
}


