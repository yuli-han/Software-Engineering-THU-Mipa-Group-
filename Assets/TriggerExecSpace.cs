//搞几个示例代码......？不对，是直接搞几个类好了。

using System.Collections.Generic;
//所有Trigger实现，将写在此类里面.....也不一定。可以写在不同的文件里，但是都要在这个namespace里面
using UnityEngine;

namespace TriggerExecSpace
{
/*
	public class DealRandomDamage : Trigger.TriggerResult
	{
		int DamageTotal;
		public DealRandomDamage(int total)
		{
			DamageTotal=total;
		}
		public override void exec(Trigger.TriggerInput input)
		{
			//实现事情的做法可以分为两步：获得所有合适的单位；然后随机造成伤害。
			//所以需要的第一步是判断可打击范围。这个使用专门的函数为佳

			//第二步则是随机造成伤害。每次1点。每点伤害呼叫一次触发器
		}
	}*/

	public class DealDamage : Trigger.TriggerResult
	{
		int thisDamage;
		public DealDamage(int damage)
		{
			thisDamage=damage;
		}
		public override void doMove(Trigger.TriggerInput input,int extra = 0)
		{
			base.doMove(input,thisDamage);
		}
		public override void exec(Trigger.TriggerInput input)
		{
			
			input.CardTarget.GetComponent<Common_CardInfo>().cardInfo.hp-=thisDamage;
		}
	}

	public class HealUnit : Trigger.TriggerResult
	{
		int thisHeal;
		public HealUnit(int heal)
		{
			thisHeal=heal;
		}
		public override void exec(Trigger.TriggerInput input)
		{
            input.CardTarget.GetComponent<Common_CardInfo>().cardInfo.hp += thisHeal;
			if(input.CardTarget.GetComponent<Common_CardInfo>().cardInfo.hp>input.CardTarget.GetComponent<Common_CardInfo>().cardInfo.maxhp)
			input.CardTarget.GetComponent<Common_CardInfo>().cardInfo.hp=input.CardTarget.GetComponent<Common_CardInfo>().cardInfo.maxhp;
		}
	}
	
	public class DrawCard : Trigger.TriggerResult
	{
		int CardNum;//抽卡数；一般是1张，但也可能多张
		public DrawCard(int number=1)
		{
			CardNum=number;
		}
		public override void exec(Trigger.TriggerInput input)
		{
			//需要在卡片里保留一个方法来获得自己属于哪边
			for(int i=0;i<CardNum;i++)
			if(input.CardUser.GetComponent<Common_CardInfo>().cardInfo.position<=2)
			GameObject.Find("GameCenter").GetComponent<GamePlayScene_GameCenterScript>().DrawCard();
	else
		{
	GameObject.Find("GameCenter").GetComponent<GamePlayScene_GameCenterScript>().DrawCard_op();

		}
		}
	}

//属性变化，例如+1/+1或-1/-1这类。这个只算了残阳那类永久加成。毕竟沉默做不来
//顺便一提，这个玩意儿甚至可以做成污手党那样的结果，捂脸
	public class StaticChange : Trigger.TriggerResult
	{
		int atk;
		int hp;
		public StaticChange(int atkch,int hpch)
		{
			atk=atkch;
			hp=hpch;
		}
		public override void exec(Trigger.TriggerInput input)
		{
			input.CardTarget.GetComponent<Common_CardInfo>().cardInfo.atk+=atk;
			if(input.CardTarget.GetComponent<Common_CardInfo>().cardInfo.atk<0)input.CardTarget.GetComponent<Common_CardInfo>().cardInfo.atk=0;
			input.CardTarget.GetComponent<Common_CardInfo>().cardInfo.maxhp+=hp;
			if(hp>0)input.CardTarget.GetComponent<Common_CardInfo>().cardInfo.hp+=hp;
			else if(input.CardTarget.GetComponent<Common_CardInfo>().cardInfo.hp>input.CardTarget.GetComponent<Common_CardInfo>().cardInfo.maxhp)
			input.CardTarget.GetComponent<Common_CardInfo>().cardInfo.hp=input.CardTarget.GetComponent<Common_CardInfo>().cardInfo.maxhp;
		}
	}

    //设置属性，例如大小城管
    //也可以用于红龙雷诺，但是不会设置最大生命值
    public class StaticSet : Trigger.TriggerResult
    {
        int atk;
        int hp;

        //注：数字为零代表不设置哟！显然不允许设置为0攻这种可怕的事情
        public StaticSet(int atkst, int hpst)
        {
            atk = atkst;
            hp = hpst;
        }

        public override void exec(Trigger.TriggerInput input)
        {
            if (atk != 0)
                input.CardTarget.GetComponent<Common_CardInfo>().cardInfo.atk = atk;
            if (hp != 0)
            {
                //不是英雄时才会设定最大生命值哟！！
                if (input.CardTarget.GetComponent<Common_CardInfo>().cardInfo.CardType != Common_CardInfo.BaseInfo.Hero)
                    input.CardTarget.GetComponent<Common_CardInfo>().cardInfo.maxhp = hp;

                input.CardTarget.GetComponent<Common_CardInfo>().cardInfo.hp = hp;
            }
        }
    }

	//大家好我是AOE
	public class AllDamage: Trigger.TriggerResult
	{
		int damage;
        public AllDamage(int tdamage)
        {
            damage = tdamage;
        }
        public override void exec(Trigger.TriggerInput input)
        {
            List<GameObject> target = Trigger.Trigger.MarkTarget(input.CardUser, input.CardUser.GetComponent<Common_CardInfo>().cardInfo.thisTrigger.thisTarget);
            Trigger.TriggerResult tempTrigger = new DealDamage(damage);
            foreach (GameObject obj in target)
            {
                tempTrigger.exec(new Trigger.TriggerInput(input.CardUser,obj));
            }
        }
	}

	//有了群伤就有群恢，才是人间正道嘛
	public class AllHeal: Trigger.TriggerResult
	{
        int heal;
        public AllHeal(int theal)
        {
            heal = theal;
        }
        public override void exec(Trigger.TriggerInput input)
        {
            List<GameObject> target = Trigger.Trigger.MarkTarget(input.CardUser, input.CardUser.GetComponent<Common_CardInfo>().cardInfo.thisTrigger.thisTarget);
            Trigger.TriggerResult tempTrigger = new HealUnit(heal);
            foreach (GameObject obj in target)
            {
                tempTrigger.exec(new Trigger.TriggerInput(input.CardUser, obj));
            }
        }
	}

	//大家好我是手牌群体属性变化。可以利用这个做到污手党的特性变化哟哟哟

    //捂脸，这个忽略吧。加手牌懒得搞了
/*	public class AllStaticChangeInHand:Trigger.TriggerResult
	{
        int hp;
        int atk;
        public AllStaticChangeInHand(int hpc, int atkc)
        {
            hp = hpc;
            atk = atkc;

        }

        public override void exec(Trigger.TriggerInput input)
        {
            if (input.CardUser.GetComponent<Common_CardInfo>().cardInfo.position <= 2)
            {
                
            }
            else
            {

            }
        }
	}
*/
	//群体设置，大城管或者生平
	public class AllStaticSet:Trigger.TriggerResult
	{
        int hp;
        int atk;

        public AllStaticSet(int hpset, int atkset)
        {
            hp = hpset;
            atk = atkset;
        }

        public override void exec(Trigger.TriggerInput input)
        {
            List<GameObject> target = Trigger.Trigger.MarkTarget(input.CardUser, input.CardUser.GetComponent<Common_CardInfo>().cardInfo.thisTrigger.thisTarget);
            Trigger.TriggerResult tempTrigger = new StaticSet(hp,atk);
            foreach (GameObject obj in target)
            {
                tempTrigger.exec(new Trigger.TriggerInput(input.CardUser, obj));
            }
        }
	}

	//public class FACAI，咳咳，总而言之模拟一发宇宙流玩玩
	//就是雷诺的效果哟
    public class ConditionHeal_Collection_No_Same : Trigger.TriggerResult
	{
        public ConditionHeal_Collection_No_Same()
        {
        }

        public override void exec(Trigger.TriggerInput input)
        {
            int[] idlist = new int[300];
            bool abled=true;
            if (input.CardUser.GetComponent<Common_CardInfo>().cardInfo.position <= 2)
            {
                List<GameObject> mylist=GameObject.Find("GameCenter").GetComponent<GamePlayScene_GameCenterScript>().CardCollection;
                foreach (GameObject obj in mylist)
                {
                    int nowid = obj.GetComponent<Common_CardInfo>().cardInfo.id;
                    if (idlist[nowid] == 1) abled = false;
                }
                if (abled)
                {
                    Trigger.TriggerResult newRes = new StaticSet(0, 30);
                    newRes.exec(new Trigger.TriggerInput(GameObject.Find("Hero"),null));
                }
            }
            else
            {
                List<GameObject> mylist = GameObject.Find("GameCenter").GetComponent<GamePlayScene_GameCenterScript>().CardCollection_op;
                foreach (GameObject obj in mylist)
                {
                    int nowid = obj.GetComponent<Common_CardInfo>().cardInfo.id;
                    if (idlist[nowid] == 1) abled = false;
                }
                if (abled)
                {
                    Trigger.TriggerResult newRes = new StaticSet(0, 30);
                    newRes.exec(new Trigger.TriggerInput(GameObject.Find("Hero_op"), null));
                }
            }
            
        }
	}



	//就是冲锋，这个效果容易实现
    //冲锋相当于战吼：本回合可以攻击，捂脸
    public class CanStrike : Trigger.TriggerResult
	{
        public CanStrike()
        {
        }

        public override void exec(Trigger.TriggerInput input)
        {
            input.CardUser.GetComponent<Common_CardInfo>().cardInfo.attack = true;

        }
	}
}