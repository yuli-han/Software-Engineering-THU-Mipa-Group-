using UnityEngine;
using System.Collections;

public class BUT3 : MonoBehaviour {

	float life=0f;
	float lastlife=0f;

	// Use this for initialization
	void Start () {
	Common_Random.init(2222);
	}
	
	// Update is called once per frame
	void Update () {
		life+=Time.deltaTime;
		if(life>lastlife)
		{
			lastlife+=1f;
			Debug.Log(Common_Random.random(1,100));
			
		}
	}
}
