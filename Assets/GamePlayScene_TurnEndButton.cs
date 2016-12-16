using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GamePlayScene_TurnEndButton :MonoBehaviour
{
	
	void OnMouseDown()
	{
		GameObject.Find("GameCenter").GetComponent<GamePlayScene_GameCenterScript>().TurnChange();
	}
}