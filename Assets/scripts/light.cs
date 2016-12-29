using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class light : MonoBehaviour {
	
	public Sprite[] ball;
	int index;
	// Use this for initialization
	void Start () {
		index = 0;
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<Image>().sprite = ball[index];
		index++;
		if(index >= ball.Length)
		{
			index = 0;
			//Debug.Log("耗时" + Time.deltaTime);
		}
			
	}
}
