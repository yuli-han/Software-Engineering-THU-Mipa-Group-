using UnityEngine;
using System.Collections;

public class But1 : MonoBehaviour {

	void OnMouseDown()
	{
		Common_NowCardSet.Length=9;
        Common_NowCardSet.CardSet=new int[9]{1,2,3,4,5,6,7,1,5};
//Debug.Log("BeforeLink");
		Netlink.Host(12345);
//Debug.Log("AfterLink");
		Application.LoadLevel("GamePlayScene");
	}

}
