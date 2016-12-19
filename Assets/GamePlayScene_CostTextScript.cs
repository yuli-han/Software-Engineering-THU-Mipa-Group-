using UnityEngine;
using System.Collections;

public class GamePlayScene_CostTextScript : MonoBehaviour
{

	public int number;//代表谁的文字

	void update()
	{
		if(number==0)
		{
			int now=GameObject.Find("GameCenter").GetComponent<GamePlayScene_GameCenterScript>().nowcost;
			int maxint=GameObject.Find("GameCenter").GetComponent<GamePlayScene_GameCenterScript>().maxcost;
			GetComponent<TextMash>().Text=now+"/"+max;
		}
		if(number==1)
		{
			int now=GameObject.Find("GameCenter").GetComponent<GamePlayScene_GameCenterScript>().nowcost;
			int maxint=GameObject.Find("GameCenter").GetComponent<GamePlayScene_GameCenterScript>().maxcost;
			GetComponent<TextMash>().Text=now+"/"+max;
		}

	}

}
