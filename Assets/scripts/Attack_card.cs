using UnityEngine;
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
		}
	}
}
