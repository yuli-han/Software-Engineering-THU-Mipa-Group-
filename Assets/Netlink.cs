using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Text;


//Netlink用于直接地传输信息：
//最好的做法是任何事件发生时向远端传输一个等价事件，然后远端取下事件后作为等价事件拿出。

public class Netlink : MonoBehaviour
{
	private static TcpListener listener;
    private static TcpClient client;
    private static NetworkStream netStream;
    private static StreamReader reader;
    private static StreamWriter writer;

    public static int id;

    public static int Host(int port)
	{
        listener = new TcpListener(port);
		listener.Start(10);
		client=listener.AcceptTcpClient();
        //Debug.Log("OK Accept");
        netStream = client.GetStream();
        reader = new StreamReader(netStream);
        writer = new StreamWriter(netStream);
        id = 0;
        //Debug.Log("OK Stream");
        //一旦发生连接，立刻完成的工作：1，交换随机数种子；2，交换双方卡组

        //交换随机数种子
        Common_Random.init();
        int next = Common_Random.random(0, 32767);
        Common_Random.init(next);
        //writer.WriteLine(next.ToString());
        byte[] data = Encoding.ASCII.GetBytes(next.ToString() + "\r\n");
        netStream.Write(data, 0, data.Length);
        Debug.Log("OK RandomSeed");
        //交换双方卡组
        writer.WriteLine(Common_NowCardSet.Length.ToString());
        for (int i = 0; i < Common_NowCardSet.Length; i++)
        {
            writer.WriteLine(Common_NowCardSet.CardSet[i].ToString());
        }
        Debug.Log("OK WriteInfo");
        Common_NowCardSet.Length_op = int.Parse(reader.ReadLine());
        for (int i = 0; i < Common_NowCardSet.Length_op; i++)
        {
            Common_NowCardSet.CardSet_op[i] = int.Parse(reader.ReadLine());
        }
        Debug.Log("OK ReadInfo");
		//然后应该检测是否成功地连接到了对方
		return 0;
	}

    public static int Client(string address, int port)
	{
		IPEndPoint remotePoint=new IPEndPoint(IPAddress.Parse(address),port);
		client=new TcpClient();
		client.Connect(remotePoint);
        Debug.Log("Connect");
        netStream = client.GetStream();
        reader = new StreamReader(netStream);
        writer = new StreamWriter(netStream);
        Debug.Log("GetStream");
        id = 1;
        //交换随机数种子
        int nextRan = int.Parse(reader.ReadLine());
        Common_Random.init(nextRan);
        Debug.Log("ReadLine!");
        //交换双方卡组
        writer.WriteLine(Common_NowCardSet.Length.ToString());
        for (int i = 0; i < Common_NowCardSet.Length; i++)
        {
            writer.WriteLine(Common_NowCardSet.CardSet[i].ToString());
        }
        Common_NowCardSet.Length_op = int.Parse(reader.ReadLine());
        for (int i = 0; i < Common_NowCardSet.Length_op; i++)
        {
            Common_NowCardSet.CardSet_op[i] = int.Parse(reader.ReadLine());
        }


		//然后应该检测是否成功地连接到了对方
		return 0;
	}

    public static int CloseLink()
	{
        if (reader != null)
            reader.Close();
        reader = null;
        if (writer != null)
            writer.Close();
        writer = null;
        if (netStream != null)
            netStream.Close();
        netStream = null;

		if(client!=null)
			client.Close();
		client=null;
        if (listener != null)
            listener.Stop();
        listener = null;
        return 0;
	}

    public static void SendMessage(int input, Trigger.TriggerInput inputTrigger)
	{
		if(client==null)return;
        NetMessage tempmsg = NetMessage.toMSG(input, inputTrigger);
        writer.WriteLine(tempmsg.ToString());
	}

    public static void SendMessage(NetMessage inputMessage)
	{
		if(client==null)return;
        writer.WriteLine(inputMessage.ToString());
	}

    public static NetMessage RecvMessage()
	{
		if(client==null)return null;
        string next = reader.ReadLine();
        return NetMessage.toMSG(next);
	}
}