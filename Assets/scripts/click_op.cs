using UnityEngine;
using System.Collections;

public class click_op : MonoBehaviour {
	
	int id = 1;
	void OnMouseDown()
	{
		//Debug.Log(this.transform.position.ToString());
		//GamePlayScene_CardFactory newCard=GameObject.Find("GamePlayScene_CardFactory").GetComponent<GamePlayScene_CardFactory>();
		GameObject c=Common_DataBase.GetCard(id);
		id = id+1;
		if(id > 5)
			id = 1;
		c.transform.SetParent(GameObject.Find("Canvas/Field_op").transform);
		c.GetComponent<Common_CardInfo>().cardInfo.position = c.transform.parent.GetComponent<canvas_position>().position;
	}
}
