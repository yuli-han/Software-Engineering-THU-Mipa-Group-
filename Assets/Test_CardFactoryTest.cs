using UnityEngine;
using System.Collections;

public class Test_CardFactoryTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
		CardFactory newCard=GameObject.Find("CardFactory").GetComponent<CardFactory>();
		GameObject c=newCard.CreateNewCard(3,3,3,"盐","研请","Assets/Card/th.jpg");
		c.transform.SetParent(GameObject.Find("Canvas").transform);
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
