using System;
using System.Collections.Generic;
using System.ComponentModel;
//using System.Data;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Collections;
using System.Threading;
using System.IO;

/// <summary>
/// Description: State object for reading client data asynchronously.
/// </summary>
public class StateObject
{
	public string userNameID = string.Empty; // Client UserNameID
	public Socket workSocket = null; // Client socket
	public const int bufferSize = 4096; // Size of receive buffer
	public byte[] buffer = new byte[bufferSize]; // Received buffer
	public DateTime TimeStamp;
	
	public bool connected = false; // ID Received flag
	public Socket partnerSocket = null; // Partner socket 
	public StringBuilder sb = new StringBuilder(); // Received data String
	
	public bool groupHost = false; // GroupHost in playing [KingsOfField]
	public string currentMapName = ""; // Host Selected FieldMap[KingsOfField]
	public ArrayList groupMember; // Gropumember in playing [KingsOfField]
}
