using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GamePlayScene_TurnEndButton :MonoBehaviour
{
	private bool ended=false;


	void OnMouseDown()
	{
		if(GameObject.Find("GameCenter").GetComponent<GamePlayScene_GameCenterScript>().ifsuspend)
			return;
		if(ended)return;
        StartCoroutine(ButtonInside());
        Netlink.SendMessage(NetMessage.TurnChange, new Trigger.TriggerInput(null, null));
		GameObject.Find("GameCenter").GetComponent<GamePlayScene_GameCenterScript>().TurnChange();


	}

//使按钮按下1秒
	private IEnumerator ButtonInside()
	{
		ended=true;
		transform.Translate(0,0,-1);
		yield return new WaitForSeconds(3f);
		transform.Translate(0,0,1);
		ended=false;
	}
}