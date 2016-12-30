//搞几个示例代码......？不对，是直接搞几个类好了。

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
            
        }
	}

	//有了群伤就有群恢，才是人间正道嘛
	public class AllHeal: Trigger.TriggerResult
	{
	}

	//大家好我是群体属性变化。可以利用这个做到污手党的特性变化哟哟哟
	public class AllStaticChange:Trigger.TriggerResult
	{
	}

	//群体设置，大城管或者生平
	public class AllStaticSet:Trigger.TriggerResult
	{
	}

	//public class FACAI，咳咳，总而言之模拟一发宇宙流玩玩
	//就是雷诺的效果哟
    public class ConditionHeal : Trigger.TriggerResult
	{
	}

	//红龙的效果，或者雷诺的效果。
    public class SetHpTo : Trigger.TriggerResult
	{
	}

	//就是冲锋，这个效果容易实现
	public class CanStrike
	{
	}
}