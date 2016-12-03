using UnityEngine;
using System.Collections;

public class Common_CardInfo : MonoBehaviour {
    
    //卡片数据暂存器。将这个Script附着在卡片上面作为卡片数据，以方便计算以及显示。

    //基本数据
    string name;
    string description;
    int cost;
    int atk;
    int hp;

    //额外数据
    int id;
    int itemid;
    int position;
    //void* Trigger;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
