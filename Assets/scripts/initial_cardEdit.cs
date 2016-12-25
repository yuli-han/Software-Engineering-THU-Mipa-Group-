using UnityEngine;
using System.Collections;

public class initial_cardEdit : MonoBehaviour {
	
	int id = 1;
	
	void Start()
	{
		for(int i = 0;i < 25;i++)
		{
			GameObject c=Common_DataBase.GetCard(i%5+1,0);
			c.transform.SetParent(GameObject.Find("Canvas/Panel_right_up_scoll/Panel_right_up").transform);
		}
	}
	
	void OnMouseDown()
	{
		//Debug.Log("Hello!");
		//GamePlayScene_CardFactory newCard=GameObject.Find("GamePlayScene_CardFactory").GetComponent<GamePlayScene_CardFactory>();
		GameObject c=Common_DataBase.GetCard(id,1);
		id = id+1;
		if(id > 5)
			id = 1;
		c.transform.SetParent(GameObject.Find("Canvas/Panel_left_scoll/Panel_left").transform);
		//c.transform.SetParent(GameObject.Find("Canvas/Panel_right_up_scoll/Panel_right_up").transform);
	}
}
