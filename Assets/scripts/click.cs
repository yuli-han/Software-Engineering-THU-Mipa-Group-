using UnityEngine;
using System.Collections;

public class click : MonoBehaviour {

	void OnMouseDown()
	{
		Debug.Log("Hello!");
		GamePlayScene_CardFactory newCard=GameObject.Find("CardFactory").GetComponent<GamePlayScene_CardFactory>();
		GameObject c=newCard.CreateNewCard(3,3,3,"盐","研请",0);
		c.transform.SetParent(GameObject.Find("Canvas/Field").transform);
	}
}
