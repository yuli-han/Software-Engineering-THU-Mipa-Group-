using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler{
	
	public void OnPointerEnter(PointerEventData eventData){
		//Debug.Log("in Panal");
		if(eventData.pointerDrag ==null)
			return;
		if(eventData.pointerDrag.GetComponent<Common_CardInfo>().cardInfo.position == 2)
			return;
		if(eventData.pointerDrag.GetComponent<Common_CardInfo>().cardInfo.CardType >= Common_CardInfo.BaseInfo.aimSpell)
			return;
		Draggerable d = eventData.pointerDrag.GetComponent<Draggerable>();
		if(d != null){
			d.placeholderParent = this.transform;
		}
	}
	
	public void OnPointerExit(PointerEventData eventData){
		//Debug.Log("leave Panal");
		if(eventData.pointerDrag ==null)
			return;
		if(eventData.pointerDrag.GetComponent<Common_CardInfo>().cardInfo.position == 2)
			return;
		if(eventData.pointerDrag.GetComponent<Common_CardInfo>().cardInfo.CardType >= Common_CardInfo.BaseInfo.aimSpell)
			return;
		Draggerable d = eventData.pointerDrag.GetComponent<Draggerable>();
		if(d != null && d.placeholderParent==this.transform){
			d.placeholderParent = d.parentToReturnTo;
		}
		
	}
	
	public void OnDrop(PointerEventData eventData){
		if(eventData.pointerDrag ==null)
			return;
		//Debug.Log(eventData.pointerDrag.name + "was dropped on" + gameObject.name);
		if(eventData.pointerDrag.GetComponent<Common_CardInfo>().cardInfo.position == 2)
			return;
		if(eventData.pointerDrag.GetComponent<Common_CardInfo>().cardInfo.CardType >= Common_CardInfo.BaseInfo.aimSpell)
			return;
		Draggerable d = eventData.pointerDrag.GetComponent<Draggerable>();
		if(d != null){
			d.parentToReturnTo = this.transform;
		}
	}
}
