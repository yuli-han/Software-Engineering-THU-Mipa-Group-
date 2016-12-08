//总而言之用于判断是否在范围内，的函数。
//这个范围的意思，是指：给予一个GameObject,给予一个Trigger.TriggerTarget，判断是否属于范围？

using UnityEngine;
using System.Collections;

//似乎还需要一个使用目标范围
public bool IsInRange(GameObject user,GameObject target,Trigger.TriggerTarget range)
{
	//按顺序判断是否正确了
	//任何的情况下，直接为真
	if(range&Trigger.TriggerTarget.Anytime==1)return true;
	
	//每个组一一判断，有一组不对的时候判定为假并跳出。没有跳出则在最后为真运行。
	
	//敌友组
	if(!(range&Trigger.TriggerTarget.Enemy==0 && range&Trigger.TriggerTarget.Friend==0))
	{
		int userPos;
		int targetPos;

		if(user.GetComponent<Common_CardInfo>().cardInfo.position<=2) userPos=1;else userPos=2;
		if(target.GetComponent<Common_CardInfo>().cardInfo.position<=2) targetPos=1;else targetPos=2;
		if(userPos==targetPos && !(range&Trigger.TriggerTarget.Friend)) return false;
		if(userPos!=targetPos && !(range&Trigger.TriggerTarget.Enemy)) return false;
	}

	//自身组
	if(!(range&Trigger.TriggerTarget.Myself==0 && range&Trigger.TriggerTarget.Others==0))
	{
		if(user==target && !(range&Trigger.TriggerTarget.Myself))return false;
		if(user!=target && !(range&Trigger.TriggerTarget.Others))return false;
	}



	return true;
}