using UnityEngine;
using System.Collections;
using System.Collections.Generic;


//基类就直接命名为Trigger了！（捂脸）不过继承类命名仍然要是"Trigger_"系列呀！
//工作方式：分为主动触发（法术或战吼）和被动触发。无论什么时候，输入时应有输入变量；主动触发类的要确认TriggerTarget，而被动触发类的则要确认TriggerCondition
namespace Trigger
{
	public class TriggerInput//输入Trigger时应有的变量。需要的变量应当尽可能少
	{
		//对于主动触发式，CardUser可以置为空；对于被动触发式，诸如进行攻击啥的，则应当把CardUser设为发动的卡片。
		public GameObject CardUser;
		public GameObject CardTarget;
		
		public TriggerInput(GameObject user, GameObject Target)
		{
			CardUser = user;
			CardTarget = Target;
		}
	}


	public class TriggerTarget//用于提示卡片使用条件的变量。这些使用条件代表法术或者战吼随从的使用目标。对于没有明确目标的，例如随机打1这样的，直接可以当作Anytime
	{
		//使用方法是利用&符号连接多个类。
		public int target;

		static public readonly int Anyone=1;//这个被标记就代表任何单位都可以
		//以下两个将一一排他。如果两个都没标记就代表均可。只标记了一个就代表不要另一个
		public static readonly int Enemy=2;//敌军
		public static readonly int Friend=4;//友军。这个是包括自己的。

		public static readonly int Myself=8;//使用单位自己。这个和下面那个估计没有用，是我蠢了，捂脸
		public static readonly int Others=16;//除了使用者以外。

		public static readonly int Hero=32;//英雄
		public static readonly int Unit=64;//随从

		//下面为各种种族。只要标记了一个，就意味着其他没标记的都当作不能被指向。当然你可以标记多个
		public static readonly int Animal=128;//野兽
		public static readonly int Tecnical=256;//机械
		public static readonly int Evavl=512;//恶魔，好吧我英语不好，之后改
		public static readonly int Dragon=1024;//龙
		public static readonly int Fish=2048;//鱼人。如果你吐槽说鱼不等于鱼人，你就输了。
	}
	

	public class TriggerCondition//触发条件。这里的触发条件是被动触发的trigger的被动触发条件。所有的触发条件只能允许一个。
	{
		public int conditionType;//触发类型
		public int conditionTarget;//触发目标
		public int conditionTime;//触发时机

//conditionType组
		public static readonly int OnAnyTime=0;
		public static readonly int OnAttack=1;
		public static readonly int OnAttacked=2;
		public static readonly int OnDead=3;
		public static readonly int OnSummon=4;//召唤时。这个时间不包括“召唤自己”
		public static readonly int OnPlayCard=5;

//conditionTarget组
		public static readonly int OnAnyUnit=0;
		public static readonly int OnEnemy=1;
		public static readonly int OnFriend=2;
		public static readonly int OnHero=4;
		public static readonly int OnUnit=8;

//conditimeTime组
		public static readonly int TimeAnyTime=0;//无时机
		public static readonly int TimeBefore=1;//事件发生前
		public static readonly int TimeAfter=2;//事件发生后
	}

//执行方式
	public class TriggerResult
	{
		public int thisMove=0;
		public virtual void doMove(TriggerInput input,int extra = 0)
		{
			input.CardUser.GetComponent<CardMove>().Move(input,thisMove,extra);
		}
		public virtual void exec(TriggerInput input)
		{
		}
	}


//构造函数就应该直接给定两个Trigger条件以供后用
    public class Trigger
    {
            public TriggerCondition thisCondition;
            public TriggerTarget thisTarget;
            public TriggerResult thisResult;
		    
		
            public Trigger()
            {
                thisCondition=new TriggerCondition();
                thisTarget=new TriggerTarget();
                thisResult=new TriggerResult();
			}

	    public Trigger(TriggerTarget target,TriggerCondition condition,TriggerResult result)
        {
	        thisTarget=target;
	        thisCondition=condition;
	        thisResult=result;
        }

//实际执行的执行方式
	    public void exec(TriggerInput input)
	    {
            //Debug.Log("beforeExec");
		   	this.thisResult.doMove(input);
	    }

        public static bool IsInRange(GameObject user, GameObject target, TriggerTarget range)
        {
		//首先最可怕的事情是手牌要排除排除
		if((target.GetComponent<Common_CardInfo>().cardInfo.position==1)||(target.GetComponent<Common_CardInfo>().cardInfo.position==4)) return false;
            //按顺序判断是否正确了
            //任何的情况下，直接为真
			//Debug.Log("IsInRange:"+(range.target & TriggerTarget.Anyone));
            if ((range.target & TriggerTarget.Anyone) == 1) return true;

            //每个组一一判断，有一组不对的时候判定为假并跳出。没有跳出则在最后为真运行。

            //敌友组
            if (!((range.target & TriggerTarget.Enemy) == 0 && (range.target & TriggerTarget.Friend) == 0))
            {
                int userPos;
                int targetPos;

                if (user.GetComponent<Common_CardInfo>().cardInfo.position <= 2) userPos = 1; else userPos = 2;
                if (target.GetComponent<Common_CardInfo>().cardInfo.position <= 2) targetPos = 1; else targetPos = 2;
                if (userPos == targetPos && (range.target & TriggerTarget.Friend) == 0) return false;
                if (userPos != targetPos && (range.target & TriggerTarget.Enemy) == 0) return false;
            }

            //自身组
            if (!((range.target & TriggerTarget.Myself) == 0 && (range.target & TriggerTarget.Others) == 0))
            {
                if (user == target && (range.target & TriggerTarget.Myself) == 0) return false;
                if (user != target && (range.target & TriggerTarget.Others) == 0) return false;
            }



            return true;
        }
    public static bool IfHaveTarget(GameObject user,TriggerTarget range)
{
	return MarkTarget(user,range).Count!=0;
}

public static List<GameObject> MarkTarget(GameObject user,TriggerTarget range)
{
	List<GameObject> output=new List<GameObject>();
	//第一步：获得所有单位
	GameObject Panal1=GameObject.Find("Canvas/Field");
	GameObject Panal2=GameObject.Find("Canvas/Field_op");
	//第二步：遍历每个单位，然后判断这个单位是否满足你的目标
	for(int i=0;i<Panal1.transform.childCount;i++)
	{
		if(Panal1.transform.GetChild(i).GetComponent<Common_CardInfo>()== null)
				continue;
		if(IsInRange(user,Panal1.transform.GetChild(i).gameObject,range))
			output.Add(Panal1.transform.GetChild(i).gameObject);
	}
	for(int i=0;i<Panal2.transform.childCount;i++)
	{
		if(Panal2.transform.GetChild(i).GetComponent<Common_CardInfo>()== null)
				continue;
		if(IsInRange(user,Panal2.transform.GetChild(i).gameObject,range))
			output.Add(Panal2.transform.GetChild(i).gameObject);
	}
	return output;
}

    }
}