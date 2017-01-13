using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;

public class LidarClient : MonoBehaviour {

	private string mIpAddress;
	private int mPortNumber;
	private Socket mClientSocket;
	private IPAddress mIP;
	private IPEndPoint mIPEP;
	private byte[] mBuffer;
	private byte[] mBufferThread;
	private bool mIsConnectionStarted;
	private byte[] mReceivedBuffer;
	private bool mIsEndOfFrame;
	private Thread ReceivedThread;
	[SerializeField]
	private Text mConsoleText;
	[SerializeField]
	private GameObject mLidar;
	private AutoResetEvent mResetEvent;
	// Use this for initialization
	void Start () 
	{
		mIpAddress = "127.0.0.1";
		mPortNumber = 4321;
		mClientSocket = new Socket (AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
		mIP = IPAddress.Parse (mIpAddress);
		mIPEP = new IPEndPoint (mIP, mPortNumber);
		mBuffer = mLidar.GetComponent<RotateLidar>().GetBufferToSend();
		mResetEvent = new AutoResetEvent (false);
		
	}
	void Update()
	{
		mBuffer = mLidar.GetComponent<RotateLidar> ().GetBufferToSend();
		if(mReceivedBuffer!=null)
		{
			string Osef=Encoding.ASCII.GetString(mReceivedBuffer);
			mConsoleText.text=Encoding.ASCII.GetString(mReceivedBuffer,0,mReceivedBuffer.Length);
		}
	}
	void FixedUpdate()
	{
		mResetEvent.Set ();
	}
	public void StartConnection()
	{
		ReceivedThread = new Thread (ThreadClient);
		ReceivedThread.Start ();
	}
	public void CloseConnection()
	{
		ReceivedThread.Abort ();
		mClientSocket.Disconnect (true);
	}
	
	private void ThreadClient()
	{
		while(true)
		{
			mResetEvent.WaitOne();
			
			if(!mClientSocket.IsBound)
				mClientSocket.Connect(mIPEP);
			else
			{
				mClientSocket.Send (mBuffer, mBuffer.Length, SocketFlags.None);
				if(mReceivedBuffer==null)
					mReceivedBuffer=new byte[mClientSocket.ReceiveBufferSize];
				else
					mClientSocket.Receive(mReceivedBuffer);
			}
		}
	}
	
	void OnApplicationQuit()
	{
		Debug.Log ("Quitting");
		ReceivedThread.Abort ();
	}
}
