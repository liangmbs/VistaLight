using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Net.Sockets;

public class ClientSocket : MonoBehaviour {

	bool socketReady = false;

	TcpClient mySocket;
	NetworkStream theStream;
	StreamWriter theWriter;
	StreamReader theReader;
	string Host = "127.0.0.1";
	Int32 Port = 8000; 
	string test;


	void Start(){

		Debug.Log("ss");
		setupSocket ();
		writeSocket("steven");
		test = readSocket ();
		Debug.Log (test);
	}


	public void setupSocket(){

		try{
			mySocket = new TcpClient(Host, Port);
			theStream = mySocket.GetStream();
			theWriter = new StreamWriter(theStream);
			theReader = new StreamReader(theStream);
			socketReady = true;
		
		}

		catch(Exception e){
			Debug.Log ("socket error: " + e);

		}
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


