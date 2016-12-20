using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

//新加的script，尽当卡片位置处于3（敌方场上）才触发，
public class Attack_card : MonoBehaviour,IDropHandler{

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
				OnAttackEvent(d,this.gameObject);
				d.GetComponent<Common_CardInfo>().cardInfo.attack = false;
			}
			else
				if(d.GetComponent<Common_CardInfo>().cardInfo.CardType >= Common_CardInfo.BaseInfo.aimSpell)
				{
					//Debug.Log("Card " + d.GetComponent<Common_CardInfo>().cardInfo.name + " releases on " + "card " + this.GetComponent<Common_CardInfo>().cardInfo.name);
					if(Trigger.Trigger.IsInRange(d,this.gameObject,d.GetComponent<Common_CardInfo>().cardInfo.thisTrigger.thisTarget)){
						//Debug.Log("PrepareToExec");
						Trigger.TriggerInput newInput = new Trigger.TriggerInput(d,this.gameObject);
						d.GetComponent<Common_CardInfo>().cardInfo.thisTrigger.exec(newInput);
						d.GetComponent<Common_CardInfo>().cardInfo.ifdelete=true;
						GameObject.Find("GameCenter").GetComponent<GamePlayScene_GameCenterScript>().nowcost-=d.GetComponent<Common_CardInfo>().cardInfo.cost;
					}
				}
		}
	}
	
	public void OnAttackEvent(GameObject user,GameObject target)
	{
		//血量结算
		user.GetComponent<Common_CardInfo>().cardInfo.hp-=target.GetComponent<Common_CardInfo>().cardInfo.atk;
		target.GetComponent<Common_CardInfo>().cardInfo.hp-=user.GetComponent<Common_CardInfo>().cardInfo.atk;
	
	}
	
}
