using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler{
	
	public void OnPointerEnter(PointerEventData eventData){
		if(eventData.pointerDrag ==null)
			return;
		Draggerable d = eventData.pointerDrag.GetComponent<Draggerable>();
		if(d != null){
			d.placeholderParent = this.transform;
		}
	}
	
	public void OnPointerExit(PointerEventData eventData){
		Draggerable d = eventData.pointerDrag.GetComponent<Draggerable>();
		if(d != null && d.placeholderParent==this.transform){
			d.placeholderParent = d.parentToReturnTo;
		}
		
	}
	
	public void OnDrop(PointerEventData eventData){
		Debug.Log(eventData.pointerDrag.name + "was dropped on" + gameObject.name);
		
		Draggerable d = eventData.pointerDrag.GetComponent<Draggerable>();
		if(d != null){
			d.parentToReturnTo = this.transform;
		}
	}
}
