using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

//新加的script，尽当卡片位置处于3（敌方场上）才触发，
public class Attack_card : MonoBehaviour,IDropHandler,IPointerDownHandler{

	public void OnDrop(PointerEventData eventData){
		if(eventData.pointerDrag ==null)
			return;
		//Debug.Log(eventData.pointerDrag.name + "was dropped on" + gameObject.name);		
		GameObject d = eventData.pointerDrag;
		if(d != null){
			//trigger whatever you want, activator: d; accepter: this.
			//like: d attack this   ----- attack(d,this);
			if(d.GetComponent<Common_CardInfo>().cardInfo.position == 2){
				if(!d.GetComponent<Common_CardInfo>().cardInfo.attack)
					return;
				if(this.GetComponent<Common_CardInfo>().cardInfo.position != 3)
					return;
				Debug.Log("Card " + d.GetComponent<Common_CardInfo>().cardInfo.name + " attacks " + "card " + this.GetComponent<Common_CardInfo>().cardInfo.name);
				
				d.GetComponent<CardMove>().cardAttack(d,this.gameObject);
				d.GetComponent<Common_CardInfo>().cardInfo.attack = false;
				Trigger.TriggerInput input = new Trigger.TriggerInput(d,this.gameObject);
				Netlink.SendMessage(NetMessage.Attack,input);
			}
			else
				if(d.GetComponent<Common_CardInfo>().cardInfo.CardType >= Common_CardInfo.BaseInfo.aimSpell)
				{
					if(Trigger.Trigger.IsInRange(d,this.gameObject,d.GetComponent<Common_CardInfo>().cardInfo.thisTrigger.thisTarget)){
						//加入造成伤害动画
						Trigger.TriggerInput newInput = new Trigger.TriggerInput(d,this.gameObject);
						d.GetComponent<Common_CardInfo>().cardInfo.thisTrigger.exec(newInput);
						GameObject.Find("GameCenter").GetComponent<GamePlayScene_GameCenterScript>().nowcost-=d.GetComponent<Common_CardInfo>().cardInfo.cost;
						
						Netlink.SendMessage(NetMessage.SpellCard,newInput);
					}
				}
		}
	}
	
	public void OnPointerDown(PointerEventData eventData)
	{
		if(this.GetComponent<Common_CardInfo>().cardInfo.position == 4)
			return;
		if(GameObject.Find("GameCenter").GetComponent<GamePlayScene_GameCenterScript>().ifsuspend)
		{
			if(this.GetComponent<Draggerable>()!=null)
				if(this.GetComponent<Draggerable>().bigCard!=null)
					Destroy(this.GetComponent<Draggerable>().bigCard);
			Debug.Log("我点了 "+this.GetComponent<Common_CardInfo>().cardInfo.name);
			GameObject obj = GameObject.Find("GameCenter").GetComponent<GamePlayScene_GameCenterScript>().suspend;
			if(Trigger.Trigger.IsInRange(obj,this.gameObject,obj.GetComponent<Common_CardInfo>().cardInfo.thisTrigger.thisTarget))
			{
				//加入造成伤害的动画
				Trigger.TriggerInput newInput = new Trigger.TriggerInput(obj,this.gameObject);
				obj.GetComponent<Common_CardInfo>().cardInfo.thisTrigger.exec(newInput);
				Debug.Log("已经触发战吼");
				if(obj.GetComponent<fireBall>()!=null)
					obj.GetComponent<fireBall>().ifUse = true;
				GameObject.Find("GameCenter").GetComponent<GamePlayScene_GameCenterScript>().ifsuspend = false;
				Netlink.SendMessage(NetMessage.TriggerExec,newInput);
			}
						
		}
	}
		
}
