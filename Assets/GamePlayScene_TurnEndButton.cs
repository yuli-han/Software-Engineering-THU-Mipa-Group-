using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GamePlayScene_TurnEndButton :MonoBehaviour
{
	[SerializeField]private bool ended=false;


	void OnMouseDown()
	{
		Debug.Log("OnMouseDown");
		if(GameObject.Find("GameCenter").GetComponent<GamePlayScene_GameCenterScript>().ifsuspend)
			return;
		Debug.Log("suspend");
		if(ended)return;
		Debug.Log("ended");
        StartCoroutine(ButtonInside());
        Netlink.SendMessage(NetMessage.TurnChange, new Trigger.TriggerInput(null, null));
		Debug.Log("Netlink");
		GameObject.Find("GameCenter").GetComponent<GamePlayScene_GameCenterScript>().TurnChange();
		Debug.Log("TurnChange");

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