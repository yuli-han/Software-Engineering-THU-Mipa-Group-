using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Common_CardInfo : MonoBehaviour {
    
    //卡片数据暂存器。将这个Script附着在卡片上面作为卡片数据，以方便计算以及显示。

    
    public class BaseInfo
    {
        //基本数据
        public string name;
        public string description;
        public int cost;
        public int atk;
        public int hp;
        //额外数据
        public int id;
        public int itemId;
        public int position;
        //void* Trigger;//触发器，类型未定。强烈限制每个卡片只允许拥有一个效果
    }

    public BaseInfo cardInfo;

    
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.FindChild("Card Name").GetComponent<Text>().text = cardInfo.name;
        this.transform.Find("Description").GetComponent<Text>().text = cardInfo.description;
        this.transform.Find("Cost").GetComponent<Text>().text = cardInfo.cost.ToString();
        this.transform.Find("Attack").GetComponent<Text>().text = cardInfo.atk.ToString();
        this.transform.Find("Life").GetComponent<Text>().text = cardInfo.hp.ToString();

	}
}
