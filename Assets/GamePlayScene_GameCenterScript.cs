using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
        //数据来源：Common_NowCardSet.CardSet&.Length

        //提示：测试时，可以将此行注释掉，改为手动设定
        int length = Common_NowCardSet.Length;
        int[] cardSet = Common_NowCardSet.CardSet;
	
	//此处为测试用的简单卡组
	length=9;
	cardSet=new int[9]{1,2,3,1,2,3,1,2,3};

	CardCollection=new List<GameObject>();
        //生成的卡片按顺序铺在场上
        for (int i = 0; i < length; i++)
        {
            CardCollection.Add(Common_DataBase.GetCard(cardSet[i]));
        }
        
        //然后应该要通过网络获取对方卡组。因为还没有加入联网测试功能所以选择将双方卡组设为相同。

        //初始化第二步：根据英雄信息，将头像和英雄技能按钮置于场上。

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
        //初始化第五步：启动Trigger_GameStart，游戏开始。
	}
	
	//也就是回合结束->回合开始
	public void TurnChange()
	{
		nowturn=1-nowturn;
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
			//复原所有随从是否攻击
			GameObject myPanal = GameObject.Find("Canvas/Field_op");
			for(int i=0; i<myPanal.transform.childCount; i++)
				myPanal.transform.GetChild(i).GetComponent<Common_CardInfo>().cardInfo.attack=true;

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
		 while (time <= 2.5f)
        {
            float scale = scaleCurve.Evaluate(time/2.5f);
            time += Time.deltaTime / duration;

            Vector3 localScale = HintTextEnd.transform.localScale;
            localScale.x = scale;
            HintTextEnd.transform.localScale = localScale;

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
            HintTextStart.transform.localScale = localScale;

            yield return new WaitForFixedUpdate();
        }
		
		Destroy(HintTextStart);
	}
	
	void DrawCard(int user=0)
	{
	if(user==1)
	{
		DrawCard_op();
		return;
	}
		if(CardCollection.Count!=0)
		{
			int num=Common_Random.random(0,CardCollection.Count-1);
			CardCollection[num].GetComponent<CardMove>().flyAndFlip();
			//CardCollection[num].transform.SetParent(GameObject.Find("Canvas/Hand").transform);
			CardCollection[num].GetComponent<Common_CardInfo>().cardInfo.position=1;
			CardCollection.RemoveAt(num);
		}

	}
	void DrawCard_op()
	{
		if(CardCollection_op.Count!=0)
		{
			int num=Common_Random.random(0,CardCollection_op.Count-1);
			CardCollection_op[num].transform.SetParent(GameObject.Find("Canvas/Hand_op").transform);
			CardCollection_op[num].GetComponent<Common_CardInfo>().cardInfo.position=4;
			CardCollection_op.RemoveAt(num);
		}

	}
}
