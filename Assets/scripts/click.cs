using UnityEngine;
using System.Collections;

public class click : MonoBehaviour {
	
	int id = 1;
	void OnMouseDown()
	{
		//Debug.Log(this.transform.position.ToString());
		//GamePlayScene_CardFactory newCard=GameObject.Find("GamePlayScene_CardFactory").GetComponent<GamePlayScene_CardFactory>();
		GameObject c=Common_DataBase.GetCard(id);
		id = id+1;
		if(id > 7)
			id = 1;
		c.transform.SetParent(GameObject.Find("Canvas/Hand").transform);
		c.GetComponent<Common_CardInfo>().cardInfo.position = c.transform.parent.GetComponent<canvas_position>().position;
	}
}
