using UnityEngine;
using System.Collections;

public class MainScene_LastPage : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown(){
		int CardGroupNumber =  MainScene_CardGroupManager.CardGroupNumber;
		if (MainScene_CardGroupManager.PageNumber == 1) {
			Debug.Log ("这已经是第一页");
		}
		else {
			Debug.Log ("进入上一页");
			MainScene_CardGroupManager.PageNumber = MainScene_CardGroupManager.PageNumber - 1;
			MainScene_CardGroupManager.IsClick [0] = 0;
			MainScene_CardGroupManager.IsClick [1] = 0;
			MainScene_CardGroupManager.IsClick [2] = 0;
		}
	}
}
