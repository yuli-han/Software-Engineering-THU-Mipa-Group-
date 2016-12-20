using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Net;
using System.Net.Sockets;

//Netlink用于直接地传输信息：
//最好的做法是任何事件发生时向远端传输一个等价事件，然后远端取下事件后作为等价事件拿出。

public class Netlink
{
	private TcpListener listener;
	private TcpClient client;
	
	
	public int Host(int port)
	{
		listener=new TcpListener(port);
		listener.Start(10);
		StartCoroutine(WaitForClient());

		return 0;
	}
	
	private IEnumerator WaitForClient()
	{
		client=listener.AcceptTcpClient();
		//然后做点什么广播来声明你接收了一个连接
	}

	public int Client(string address,int port)
	{
		IPEndPoint remotePoint=new IPEndPoint(IPAdress.Parse(address),port);
		client=new TcpClient();
		client.Connect(remotePoint);

		//然后应该检测是否成功地连接到了对方
		return 0;
	}

	public int CloseLink()
	{
		if(client!=null)
			client.close();
		client=null;
	}



	public void SendMessage()
	{
		if(client==null)return;
	}

	public void RecvMessage()
	{
		if(client==null)return;
	}
}