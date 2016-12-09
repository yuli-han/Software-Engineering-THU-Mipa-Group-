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
		if(this.GetComponent<Common_CardInfo>().cardInfo.position != 3)
			return;
		
		GameObject d = eventData.pointerDrag;
		if(d != null){
			//trigger whatever you want, activator: d; accepter: this.
			//like: d attack this   ----- attack(d,this);
			Debug.Log("Card " + d.GetComponent<Common_CardInfo>().cardInfo.name + " attacks " + "card " + this.GetComponent<Common_CardInfo>().cardInfo.name);
			OnAttackEvent(d,this);
		}
	}
	
	public void OnAttackEvent(GameObject user,GameObject target)
	{
	//血量结算
	user.GetComponent<Common_CardInfo>().cardInfo.hp-=target.GetComponent<Common_CardInfo>().cardInfo.atk;
	target.GetComponent<Common_CardInfo>().cardInfo.hp-=user.GetComponent<Common_CardInfo>().cardInfo.atk;
	
	//判定自身是否死亡
	if(user.GetComponent<Common_CardInfo>().cardInfo.hp<=0)
	{
		Destroy(user);//user.destroy;
	}
	
	//判定对手是否死亡
	if(target.GetComponent<Common_CardInfo>().cardInfo.hp<=0)
	{
		Destory(target);//target.destroy;
	}
	
	}
}
