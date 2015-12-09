using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Collections;
using System.Threading;
using System.IO;
//using KoF_Database;

public class ClientSocket
{
	protected Socket listenerC;
	protected Thread clientThread;
	protected ManualResetEvent connectDone = new ManualResetEvent(false);
//	protected ManualResetEvent receiveDone = new ManualResetEvent(false);
	protected AutoResetEvent sendingDone = new AutoResetEvent(false);
	protected AutoResetEvent disconnectDone = new AutoResetEvent(false);
	protected AutoResetEvent exitDone = new AutoResetEvent(false);
	protected bool shutdownFlag;
	protected bool activate;
	protected StreamWriter sw;
	public delegate void ReceiveClientMessage(string msg);
	public event ReceiveClientMessage rcm;

	public List<Packet> PacketList = new List<Packet>(); // add dungeonplayer
	public object SyncRoot = new object(); // add dungeonplayer
	ManualResetEvent PacketReceived = new ManualResetEvent (false); // add dungeonplayer

	public ClientSocket()
	{
		shutdownFlag = false;
		activate = false;
		sw = null;
	}
	
	public ManualResetEvent ConnectEvent
	{
		get { return connectDone; }
	}
	
	/// <summary>
	/// 初回接続を確立します
	/// </summary>
	/// <param name="ep">IPのエンドポイント</param>
	/// <param name="waitTime">待ち時間</param>
	/// <returns></returns>
	public bool Connect(string username, IPEndPoint ep, int waitTime)
	{
		//if (sw == null) sw = new StreamWriter("log¥¥" + username + "_log.txt");
		
		listenerC = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
		
		listenerC.BeginConnect(ep, new AsyncCallback(ConnectCallback), listenerC);
		bool result = false;
		result = ConnectEvent.WaitOne(waitTime, false);
		if (result) activate = true;
		
		clientThread = new Thread(new ThreadStart(ReceiveThread));
		clientThread.IsBackground = true;
		clientThread.Start();
		return result;
	}
	
	/// <summary>
	/// 接続を確立している場合は切断処理を行います。
	/// </summary>
	/// <param name="bb"></param>
	/// <param name="waitTime"></param>
	public void Disconnect(byte[] bb, int waitTime)
	{
		if (activate)
		{
			listenerC.BeginSend(bb, 0, bb.Length, 0, new AsyncCallback(SendCallbackC), listenerC);
			disconnectDone.WaitOne();
			
			shutdownFlag = true;
			//receiveDone.Set();
			PacketReceived.Set();
			Thread.Sleep(waitTime);
			exitDone.WaitOne();
			listenerC.Shutdown(SocketShutdown.Both);
			listenerC.Close();
			activate = false;
		}
	}
	
	/// <summary>
	/// 強制的に切断しておきます（Abortをコール）
	/// </summary>
	public void ForceDisconnect()
	{
		clientThread.Abort();
	}
	
	/// <summary>
	///サーバへ任意のメッセージを送信します。
	/// </summary>
	/// <param name="bb"></param>
	/// <returns></returns>
	public bool SendMessage(byte[] bb)
	{
		listenerC.BeginSend(bb, 0, bb.Length, 0, new AsyncCallback(SendCallbackC), listenerC);
		sendingDone.WaitOne();
		return true;
	}
	/// <summary>
	///  最初のサーバとの接続確立を行うスレッド
	/// </summary>
	/// <param name="ar"></param>
	private void ConnectCallback(IAsyncResult ar)
	{
		try
		{
			Socket client = (Socket)ar.AsyncState;
			client.EndConnect(ar);
			//publicMessageText.AppendText(client.RemoteEndPoint.ToString() + "に接続できました。¥r¥n");
			ConnectEvent.Set();
		}
		catch (Exception)
		{
			//publicMessageText.AppendText(ee.ToString());
		}
	}
	
	/// <summary>
	///  サーバへ転送した事を確認するスレッド
	/// </summary>
	/// <param name="ar"></param>
	public void SendCallbackC(IAsyncResult ar)
	{
		Socket client = (Socket)ar.AsyncState;
		int SentSize = client.EndSend(ar);
		sendingDone.Set();
		disconnectDone.Set();
	}
	
	/// <summary>
	///  サーバから転送されるデータを受け取るスレッド
	/// </summary>
	public void ReceiveThread()
	{
		StateObject state = new StateObject();
		state.workSocket = listenerC;
		try
		{
			while (!shutdownFlag)
			{
				// Begin receiving the data from the remote device.
				PacketReceived.Reset();
				listenerC.BeginReceive(state.buffer, 0, StateObject.bufferSize, 0, new AsyncCallback(ReadCallbackC), state); // 複数接続でエラー発生あり
				PacketReceived.WaitOne();
			}
		}
		catch (System.Net.Sockets.SocketException se)
		{
			if (se.ErrorCode == 10054) // サーバタイムアウトによる切断の場合はここを通る。
			{
			}
			else
			{
				//sw.WriteLine(se.ToString());
			}
		}
		catch (Exception ee)
		{
			//sw.WriteLine(ee.ToString());
		}
		finally
		{
			exitDone.Set();
		}
//		List<Packet> packets;
//		while (!shutdownFlag) {
//			PacketReceived.Reset();
//			lock(SyncRoot) {
//				packets = PacketList;
//				PacketList = new List<Packet>();
//			}
//			foreach(Packet packet in packets) {
//				// Process the packet
//			}
//			PacketReceived.WaitOne();
//		}
	}

	List<string> receiveString = new List<string>();
	private void ReadCallbackC(IAsyncResult ar)
	{
		StateObject state = (StateObject)ar.AsyncState;
		Socket handler = state.workSocket;

		try
		{
			int bytesRead = handler.EndReceive(ar);
			if (bytesRead > 0) {
				byte[] bb1 = new byte[bytesRead];
				Array.Copy(state.buffer, bb1, bytesRead);
				string msg1 = Encoding.UTF8.GetString(bb1);
				this.receiveString.Add(msg1);
				string totalMessage = string.Empty;
				foreach( string str in receiveString) {
					totalMessage += str;
				}
				OnReceive(totalMessage);
				receiveString.Clear();
			}

			PacketReceived.Set();
			handler.BeginReceive(state.buffer, 0, StateObject.bufferSize, 0, new AsyncCallback(ReadCallbackC), state);
		}
		catch (System.Net.Sockets.SocketException se)
		{
			if (se.ErrorCode == 995)
			{
				//sw.WriteLine(se.ToString());
			}
			else
			{
				//sw.WriteLine(se.ToString());
				//throw new System.Net.Sockets.SocketException();
			}
		}
		catch (Exception ee)
		{
			//sw.WriteLine(ee.ToString());
			//throw new System.Exception();
			//publicMessageText.AppendText(ee.ToString() + "¥r¥n");
		}

		//		try
		//		{
		//			StateObject so = (StateObject)ar.AsyncState;
		//			int read = so.workSocket.EndReceive(ar);
		//			if (debug != null) { debug.text += "\r\n readlen: " + read.ToString(); }
		//
		//			if (read > 0) {
		//				Packet packet = new Packet(so.buffer, read);	
		//				lock(SyncRoot) {
		//					PacketList.Add(packet);
		//				}
		//				PacketReceived.Set();
		//			}
		//
		//			so.workSocket.BeginReceive(so.buffer, 0, so.buffer.Length, 0, ReadCallbackC, so);
		//		} catch (ObjectDisposedException e0) {
		//			if (debug != null) { debug.text += "\r\n object exception: " + e0.ToString(); }
		//		} catch (Exception e) { 
		//			if (debug != null) { debug.text += "\r\n exception: " + e.ToString(); }
		//		}
	}
	
	protected virtual void OnReceive(string msg)
	{
		if (rcm != null)
		{
			rcm(msg);
		}
	}
	
}
