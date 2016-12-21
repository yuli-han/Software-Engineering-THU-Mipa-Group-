using UnityEngine;
using System.Collections;


//NetMessage:网络数据传输用的接口。期待传输的数据类都应当以此为接口往复传输数据

public class NetMessage
{
	public interface MSGPack
	{
        //int unitId;
	    string sendMSG();//将类转换为MSG并予以传输
		void recvMSG(string input);//将MSG转换回类
	}
	
	static int Attack=1;
	static int DrawCard=2;
	static int Summon=3;
	static int SpellCard=4;
}