using UnityEngine;
using System.Collections;

public class MainScene_HoldGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown(){
		Debug.Log ("进入游戏界面");
		Application.LoadLevel ("GamePlayScene");
	}
}
