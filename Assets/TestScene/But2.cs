using UnityEngine;
using System.Collections;

public class But2 : MonoBehaviour {

	void OnMouseDown()
	{
		Common_NowCardSet.Length=9;
        Common_NowCardSet.CardSet=new int[9]{1,2,3,4,5,6,7,1,5};
//Debug.Log("BefortClient");
		Netlink.Client("127.0.0.1",12345);
//Debug.Log("AfterClient");
		Application.LoadLevel("GamePlayScene");
	}
}
