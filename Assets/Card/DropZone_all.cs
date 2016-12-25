using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class DropZone_all : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler{
	
	public void Update(){
		//Debug.Log("Num: " + cardNum);
		
		GameObject MyCanvas = GameObject.Find("Canvas/Panel_right_up_scoll/Panel_right_up");
		for(int i = 0;i < MyCanvas.transform.childCount;i++)
		{
			Transform a = MyCanvas.transform.GetChild(i);
			if(a.GetComponent<Draggerable_edit>())
			{
				int type = a.GetComponent<Draggerable_edit>().GetComponent<Common_CardInfo>().cardInfo.copyType;
				if(type != 0)
				{
					a.GetComponent<Draggerable_edit>().GetComponent<Common_CardInfo>().cardInfo.copyType = 0;
				}
				bool D = a.GetComponent<Draggerable_edit>().IfDelete;
				if(D == true)
				{
					Destroy(MyCanvas.transform.GetChild(i).gameObject);
					return;
				}
			}
		}	
	}
	
	public void OnPointerEnter(PointerEventData eventData){
		//Debug.Log("in Panal");
		if(eventData.pointerDrag ==null)
			return;
		Draggerable_edit d = eventData.pointerDrag.GetComponent<Draggerable_edit>();
		// if(d != null){
			// d.placeholderParent = this.transform;
		// }
	}
	
	public void OnPointerExit(PointerEventData eventData){
		//Debug.Log("leave Panal");
		if(eventData.pointerDrag ==null)
			return;
		// Draggerable_edit d = eventData.pointerDrag.GetComponent<Draggerable_edit>();
		// if(d != null)
		// {
			// d.GetComponent<Common_CardInfo>().cardInfo.copyType = -1;
			// Debug.Log("-1");
		// }
		// if(d != null && d.placeholderParent==this.transform){
			// d.placeholderParent = d.parentToReturnTo;
		// }
		
	}
	
	public void OnDrop(PointerEventData eventData){
		//Debug.Log("OnDrop Panal");
		if(eventData.pointerDrag ==null)
			return;
		//Debug.Log(eventData.pointerDrag.name + "was dropped on" + gameObject.name);
		
		Draggerable_edit d = eventData.pointerDrag.GetComponent<Draggerable_edit>();
		if(d != null){
			d.parentToReturnTo = this.transform;
			d.GetComponent<Common_CardInfo>().cardInfo.copyType = 0;
		}
		//Debug.Log(d.parentToReturnTo);

	}
		
	
}
