using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Image))]
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
		public int maxhp;

        //额外数据
        public int id;
        public int itemId;
        public int position;

		public bool ifdelete;

		public bool attack;//提示是否可以攻击；本系列没有风怒（但是也许有冲锋）所以只需要标识是否已经攻击过即可


        public Trigger.Trigger thisTrigger;//触发器。大体上是已经决定下来了使用方法
		public int CardType;//卡片类型。这里是为了提示使用卡片时是否需要指向目标；可选选项包括“普通法术”“指向法术”“普通随从”“普通战吼随从”“指向战吼随从”，其中指向法术需要指向target发动，指向战吼随从则需要先召唤后再指定一个可选单位发动战吼
		public static readonly int normalUnit=1;//普通随从，当然包括光环随从及亡语随从啦
		public static readonly int aimBattleUnit=2;//指向战吼随从
		public static readonly int noaimBattleUnit=3;//非指向战吼随从
		public static readonly int aimSpell=4;//指向法术
		public static readonly int noaimSpell=5;//非指向法术


    }

    public BaseInfo cardInfo;

    
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
    void Update()
    {
        this.transform.FindChild("Card Name").GetComponent<Text>().text = cardInfo.name;
        this.transform.Find("Description").GetComponent<Text>().text = cardInfo.description;
        this.transform.Find("Cost").GetComponent<Text>().text = cardInfo.cost.ToString();
        if (this.cardInfo.CardType < BaseInfo.aimSpell)
        {
            this.transform.Find("Attack").GetComponent<Text>().text = cardInfo.atk.ToString();
            this.transform.Find("Life").GetComponent<Text>().text = cardInfo.hp.ToString();
        }
        else
        {
            this.transform.Find("Attack").GetComponent<Text>().text = "";
            this.transform.Find("Life").GetComponent<Text>().text = "";
        }



        if (this.cardInfo.position == 2 || this.cardInfo.position == 3)
            if (this.cardInfo.attack)
            {
                this.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            }
            else
            {
                this.GetComponent<Image>().color = new Color(0.7f, 0.7f, 0.7f, 1.0f);
            }



        if (this.cardInfo.position == 1)
            if (GameObject.Find("GameCenter").GetComponent<GamePlayScene_GameCenterScript>().nowcost >= this.cardInfo.cost)
            {
                this.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            }
            else
            {
                this.GetComponent<Image>().color = new Color(0.9f, 0.9f, 0.9f, 1.0f);
            }

    }
}
