using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

//GamePlayScene核心的代码。嘛，也就是整体控制器

public class GamePlayScene_GameCenterScript : MonoBehaviour {

	public List<GameObject> CardCollection;//提示：这个只存储牌库里的牌，而不管理手牌等
	public List<GameObject> CardCollection_op;
	//需要保存对手的牌库！捂脸中
	//还有双方的法力水晶
	public int nowcost;//我方当前法力水晶
	public int maxcost;//我方最大法力水晶	
	public int nowcost_op;//敌方当前法力水晶
	public int maxcost_op;//敌方最大法力水晶
	public int nowturn;//当前是谁的回合
	public int thisplayer;//这个数字用于提示自己的玩家编号
	public GameObject suspend;
	public bool ifsuspend;
	public bool ifclick;
	public GameObject[] Hint;
	
	public AnimationCurve scaleCurve;
    public float duration = 0.5f;
	
	
	// Use this for initialization
	void Start () {
	    
		
		//初始化第一步：根据已有的数据来编造卡组
	
	    //此处为测试用的简单卡组
        /*Debug.Log("Warning: You are using TestCardSet!");
	    length=9;
	    cardSet=new int[9]{1,2,3,4,5,6,7,1,5};*/
	if(Netlink.id==0)
	{
		setCardSet(0);
		setCardSet(1);
	}
	else
	{
		setCardSet(1);
		setCardSet(0);
	}
		
        //初始化第二步：根据英雄信息，将头像和英雄技能按钮置于场上。
		
		GameObject.Find("Hero").GetComponent<Common_CardInfo>().cardInfo.itemId = ++Common_DataBase.nowItemId;
        GameObject.Find("Hero").GetComponent<Common_CardInfo>().cardInfo.position = 2;
        GameObject.Find("Hero").GetComponent<Common_CardInfo>().cardInfo.hp = 30;
        GameObject.Find("Hero").GetComponent<Common_CardInfo>().cardInfo.maxhp = 30;
        GameObject.Find("Hero").GetComponent<Common_CardInfo>().cardInfo.CardType = Common_CardInfo.BaseInfo.Hero;
        GameObject.Find("Hero").GetComponent<Common_CardInfo>().cardInfo.atk = 0;
        GameObject.Find("Hero").GetComponent<Common_CardInfo>().cardInfo.thisTrigger = new Trigger.Trigger();//暂时都是火冲，我们都是大法师
		GameObject.Find("Hero").GetComponent<Common_CardInfo>().cardInfo.thisTrigger.thisTarget.target=Trigger.TriggerTarget.Anyone;
		GameObject.Find("Hero").GetComponent<Common_CardInfo>().cardInfo.thisTrigger.thisResult=new TriggerExecSpace.DealDamage(1);
		GameObject.Find("Hero").GetComponent<Common_CardInfo>().cardInfo.thisTrigger.thisResult.thisMove=CardMove.spellDamage;
        //GameObject.Find("Hero").name = "Card"+GameObject.Find("Hero").GetComponent<Common_CardInfo>().cardInfo.itemId.ToString();

        GameObject.Find("Hero_op").GetComponent<Common_CardInfo>().cardInfo.itemId = ++Common_DataBase.nowItemId;
        GameObject.Find("Hero_op").GetComponent<Common_CardInfo>().cardInfo.position = 3;
        GameObject.Find("Hero_op").GetComponent<Common_CardInfo>().cardInfo.hp = 30;
        GameObject.Find("Hero_op").GetComponent<Common_CardInfo>().cardInfo.maxhp = 30;
        GameObject.Find("Hero_op").GetComponent<Common_CardInfo>().cardInfo.CardType = Common_CardInfo.BaseInfo.Hero;
        GameObject.Find("Hero_op").GetComponent<Common_CardInfo>().cardInfo.atk = 0;
        GameObject.Find("Hero_op").GetComponent<Common_CardInfo>().cardInfo.thisTrigger = new Trigger.Trigger();//暂时都是火冲，我们都是大法师
		GameObject.Find("Hero_op").GetComponent<Common_CardInfo>().cardInfo.thisTrigger.thisTarget.target=Trigger.TriggerTarget.Anyone;
		GameObject.Find("Hero_op").GetComponent<Common_CardInfo>().cardInfo.thisTrigger.thisResult=new TriggerExecSpace.DealDamage(1);
        GameObject.Find("Hero_op").GetComponent<Common_CardInfo>().cardInfo.thisTrigger.thisResult.thisMove = CardMove.spellDamage;
        //GameObject.Find("Hero_op").name = "Card"+GameObject.Find("Hero_op").GetComponent<Common_CardInfo>().cardInfo.itemId.ToString();


        //初始化第三步：初始化各个控件的信息
	Common_Random.init();//随机数种子理论上应该从网络获取以同步。

        //初始化第四步：设置状态为初始状态。
		nowturn=0;
		nowcost=0;
		maxcost=0;
		nowcost_op=0;
		maxcost_op=0;
		thisplayer=0;
		ifclick = false;
		ifsuspend = false;
		suspend = null;
        //初始化第五步：决定先后手并抽牌，游戏开始。

        //提示：nowturn等于1意味着下一位将等于0所以为先手；反之为后手
        nowturn = System.Math.Abs(Common_Random.random(0, 1)-Netlink.id);
        StartCoroutine(startDraw());
        TurnChange();
	}

    IEnumerator startDraw()
    {
         if (nowturn == 0)
        {
            for (int i = 0; i < 3; i++)
            {
                DrawCard_op();
                yield return new WaitForSeconds(0.2f);
            }
            for (int i = 0; i < 4; i++)
            {
                DrawCard();
                yield return new WaitForSeconds(0.2f);
            }
        }
        else
        {
            for (int i = 0; i < 3; i++)
            {
                DrawCard();
                yield return new WaitForSeconds(0.2f);
            }
            for (int i = 0; i < 4; i++)
            {
                DrawCard_op();
                yield return new WaitForSeconds(0.2f);
            }
        }
         yield return 0;
    }

    void setCardSet(int id)
    {
        if (id == 0)
        {        //数据来源：Common_NowCardSet.CardSet&.Length

            //提示：测试时，可以将此行注释掉，改为手动设定
            int length = Common_NowCardSet.Length;
            int[] cardSet = Common_NowCardSet.CardSet;

            CardCollection = new List<GameObject>();
            //生成的卡片按顺序铺在场上
            for (int i = 0; i < length; i++)
            {
                CardCollection.Add(Common_DataBase.GetCard(cardSet[i]));
            }

            //然后应该要通过网络获取对方卡组。
            //没有加入联网测试功能时，选择将双方卡组设为相同。

        }
        else
        {
            int length_op = Common_NowCardSet.Length_op;
            int[] cardSet_op = Common_NowCardSet.CardSet_op;

            CardCollection_op = new List<GameObject>();

            for (int i = 0; i < length_op; i++)
            {
                CardCollection_op.Add(Common_DataBase.GetCard(cardSet_op[i]));
            }
            
            //对于敌方回合，接下来应当等待敌方回复

        }
    }

	//也就是回合结束->回合开始
	public void TurnChange()
	{
		nowturn=1-nowturn;
		//Debug.Log("现在是" + nowturn + "的回合");
		if(nowturn==0)
		{
			//回合更新3部曲：
			//抽卡
			StopCoroutine(TurnRound(0));
			StartCoroutine(TurnRound(0));
			DrawCard();
			//复原法力水晶
			if(maxcost<10)maxcost++;
			nowcost=maxcost;
            //复原英雄技能（暂空）

			//复原所有随从是否攻击
			GameObject myPanal = GameObject.Find("Canvas/Field");
			for(int i=0; i<myPanal.transform.childCount; i++)
				myPanal.transform.GetChild(i).GetComponent<Common_CardInfo>().cardInfo.attack=true;
		}
		else
		{
			//回合更新3部曲：
			//抽卡
			StopCoroutine(TurnRound(1));
			StartCoroutine(TurnRound(1));
			DrawCard_op();
			//复原法力水晶
			if(maxcost_op<10)maxcost_op++;
			nowcost_op=maxcost_op;
            //复原英雄技能

			//复原所有随从是否攻击
			GameObject myPanal = GameObject.Find("Canvas/Field_op");
			for(int i=0; i<myPanal.transform.childCount; i++)
				myPanal.transform.GetChild(i).GetComponent<Common_CardInfo>().cardInfo.attack=true;
            
            //EnemyTurn();
            StartCoroutine(EnemyTurn());
		}
	}



	IEnumerator TurnRound(int id){
		GameObject HintTextEnd;
		GameObject HintTextStart;
		if(id == 1){
			HintTextEnd = Instantiate(Hint[0]);
			HintTextEnd.transform.SetParent(GameObject.Find("Canvas").transform);
		}
		else{
			HintTextEnd = Instantiate(Hint[2]);
			HintTextEnd.transform.SetParent(GameObject.Find("Canvas").transform);
		}
		
		
		//fly1
		float time = 0f;
		Vector3 startPosition = new Vector3(520f,380f,0f);
		HintTextEnd.transform.position  = startPosition;
		 while (time <= 2f)
        {
            float scale = scaleCurve.Evaluate(time/2f);
            time += Time.deltaTime / duration;

            Vector3 localScale = HintTextEnd.transform.localScale;
            localScale.x = scale;
			if(time < 1)
				HintTextEnd.transform.localScale = localScale;
			else
			{
				Color c = HintTextEnd.GetComponent<Text>().color;
				c.a = scale;
				HintTextEnd.GetComponent<Text>().color = c;
			}

            yield return new WaitForFixedUpdate();
        }
		
		Destroy(HintTextEnd);
		
		//等将来对手有操作的话这两部分应该分开写；
		yield return new WaitForSeconds(0.5f);
		//fly2
		
		time = 0f;
		if(id == 1){
			HintTextStart = Instantiate(Hint[1]);
			HintTextStart.transform.SetParent(GameObject.Find("Canvas").transform);
		}
		else{
			HintTextStart = Instantiate(Hint[3]);
			HintTextStart.transform.SetParent(GameObject.Find("Canvas").transform);
		}
		HintTextStart.transform.position  = startPosition;
		 while (time <= 2.5f)
        {
            float scale = scaleCurve.Evaluate(time/2.5f);
            time += Time.deltaTime / duration;

            Vector3 localScale = HintTextStart.transform.localScale;
            localScale.x = scale;
			if(time < 1)
				HintTextStart.transform.localScale = localScale;
			else
			{
				Color c = HintTextStart.GetComponent<Text>().color;
				c.a = scale;
				HintTextStart.GetComponent<Text>().color = c;
			}

            yield return new WaitForFixedUpdate();
        }
		
		Destroy(HintTextStart);
	}
	
	public void DrawCard(int user=0)
	{
	if(user==1)
	{
		DrawCard_op();
		return;
	}
		if(CardCollection.Count!=0)
		{
			int num=Common_Random.random(0,CardCollection.Count-1);
			//Debug.Log("给我抽牌！！！");
			CardCollection[num].GetComponent<CardMove>().flyAndFlip();
			CardCollection[num].GetComponent<Common_CardInfo>().cardInfo.position=1;
			CardCollection.RemoveAt(num);
		}

	}
	public void DrawCard_op()
	{
		if(CardCollection_op.Count!=0)
		{
			int num=Common_Random.random(0,CardCollection_op.Count-1);
			CardCollection_op[num].GetComponent<CardMove>().flyAndFlip(1);
			CardCollection_op[num].GetComponent<Common_CardInfo>().cardInfo.position=4;
			CardCollection_op.RemoveAt(num);
		}

	}

	//说明：根据itemid获得对应的卡片，从全局
	public GameObject GetCard(int itemid)
	{
        //先检测是不是英雄，再检测随从
        GameObject tempHero = GameObject.Find("Hero");
        if (tempHero.GetComponent<Common_CardInfo>().cardInfo.itemId == itemid) return tempHero;
        tempHero = GameObject.Find("Hero_op");
        if (tempHero.GetComponent<Common_CardInfo>().cardInfo.itemId == itemid) return tempHero;

		return GameObject.Find("Card"+itemid);
	}

	//在对手回合时，无限读取对手操作直到对手发送结束（或者认输）的指令
	//只要这个过程能正确执行，网络的问题就解决了一半
    IEnumerator EnemyTurn() //是不是应该使用协程？否则没法正常检查对手操作
	//void EnemyTurn()
	{
		while(true)
		{
            yield return new WaitForSeconds(3f);//没法实现异步，只能暂时这样。如果能够在各种动画后面加入结束动画之类的标记....就可以改用WaitFor方法了

			NetMessage nextMSG=Netlink.RecvMessage();
            if (nextMSG == null) break;//为空代表显然我们没有联网，而是单机测试
            Debug.Log("New Message:" + nextMSG.infoType + " " + nextMSG.addint1 + " " + nextMSG.addint2);
			if(nextMSG.infoType==NetMessage.Attack)
			{
				GameObject user=GetCard(nextMSG.addint1);
				GameObject target=GetCard(nextMSG.addint2);
				user.GetComponent<CardMove>().cardAttack(user,target);
				user.GetComponent<Common_CardInfo>().cardInfo.attack = false;

			}
			if(nextMSG.infoType==NetMessage.DrawCard)
			{//暂不需要，一般来说各种法术都回自动调用
			}
			if(nextMSG.infoType==NetMessage.Summon)
			{
				GameObject user=GetCard(nextMSG.addint1);
				int point=nextMSG.addint2;
				//暂缺
				user.GetComponent<Draggerable>().SummonUnit(point);
                user.GetComponent<Common_CardInfo>().cardInfo.attack = false;
			}
			if(nextMSG.infoType==NetMessage.SpellCard)
			{
				GameObject user=GetCard(nextMSG.addint1);
				GameObject target=GetCard(nextMSG.addint2);
				
				Trigger.TriggerInput newInput = new Trigger.TriggerInput(user,target);
				user.GetComponent<Common_CardInfo>().cardInfo.thisTrigger.exec(newInput);
			GameObject.Find("GameCenter").GetComponent<GamePlayScene_GameCenterScript>().nowcost-=user.GetComponent<Common_CardInfo>().cardInfo.cost;

			}
			if(nextMSG.infoType==NetMessage.TriggerExec)
			{//注：TriggerExec特指发动随从效果；因为法术效果直接作为SpellCard的效果
				GameObject user=GetCard(nextMSG.addint1);
				GameObject target=GetCard(nextMSG.addint2);
				Trigger.TriggerInput newInput = new Trigger.TriggerInput(user,target);
				user.GetComponent<Common_CardInfo>().cardInfo.thisTrigger.exec(newInput);

			}
			if(nextMSG.infoType==NetMessage.TurnChange)
			{
				this.TurnChange();
				break;
			}

		}
	}
}
