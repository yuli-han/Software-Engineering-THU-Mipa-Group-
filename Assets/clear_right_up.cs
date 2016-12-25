using UnityEngine;
using System.Collections;

public class clear_right_up : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		GameObject MyCanvas = GameObject.Find("Canvas/Panel_right_up_scoll");
		for(int i = 0;i < MyCanvas.transform.childCount;i++)
		{
			Transform a = MyCanvas.transform.GetChild(i);
			if(a.GetComponent<Draggerable_edit>())
			{
				bool D = a.GetComponent<Draggerable_edit>().IfDelete;
				if(D == true)
				{
					Destroy(MyCanvas.transform.GetChild(i).gameObject);
					return;
				}
			}
		}	
	}
}
