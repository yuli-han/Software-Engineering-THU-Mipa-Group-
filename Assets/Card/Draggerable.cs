using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class Draggerable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler{
	
	public Transform parentToReturnTo = null;
	public Transform placeholderParent = null;
	bool ifclick;
	
	GameObject placeholder = null;
	void Awake()
	{
		ifclick=false;
		
	}
	
	
	public void OnBeginDrag(PointerEventData eventData){
		Debug.Log("OnBeginDrag");
		
		/*placeholder = new GameObject();
		placeholder.transform.SetParent( this.transform.parent );
		LayoutElement le = placeholder.AddComponent<LayoutElement>();
		le.preferredWidth = this.GetComponent<LayoutElement>().preferredWidth;
		le.preferredHeight = this.GetComponent<LayoutElement>().preferredHeight;
		le.flexibleWidth = 0;
		le.flexibleHeight = 0;
		
		placeholder.transform.SetSiblingIndex(this.transform.GetSiblingIndex());
		Debug.Log(this.transform.GetSiblingIndex().ToString());
		parentToReturnTo = this.transform.parent;
		placeholderParent = parentToReturnTo;
		
		this.transform.SetParent(this.transform.parent.parent);*/
		ifclick = true;
		GetComponent<CanvasGroup>().blocksRaycasts = false;
	}
	
	public void OnDrag(PointerEventData eventData){
		
		//Debug.Log("OnDrag");
		
		this.transform.position = eventData.position;
		
		if(placeholder.transform.parent != placeholderParent)
			placeholder.transform.SetParent(placeholderParent);
		
		int newSiblingIndex = placeholderParent.childCount;
		
		for(int i=0; i<placeholderParent.childCount; i++){
			if(this.transform.position.x < placeholderParent.GetChild(i).position.x){
				newSiblingIndex = i;
				if(placeholder.transform.GetSiblingIndex() < newSiblingIndex){
					newSiblingIndex--;
				}
				break;
			}
		}
		
		placeholder.transform.SetSiblingIndex(newSiblingIndex);
	}
	
	public void OnEndDrag(PointerEventData eventData){
		
		Debug.Log("OnEndDrag");
		
		if(placeholder!=null)
		{
			this.transform.SetParent(parentToReturnTo);
			this.transform.SetSiblingIndex( placeholder.transform.GetSiblingIndex() );
			GetComponent<CanvasGroup>().blocksRaycasts = true;
		
			Destroy(placeholder);
		}
		ifclick = false;
	}
	
	public void OnPointerEnter(PointerEventData eventData){
	
		Debug.Log("Mouse Enter Card");
		placeholder = new GameObject();
		placeholder.transform.SetParent( this.transform.parent );
		LayoutElement le = placeholder.AddComponent<LayoutElement>();
		le.preferredWidth = this.GetComponent<LayoutElement>().preferredWidth;
		le.preferredHeight = this.GetComponent<LayoutElement>().preferredHeight;
		le.flexibleWidth = 0;
		le.flexibleHeight = 0;
		
		placeholder.transform.SetSiblingIndex(this.transform.GetSiblingIndex());
		Debug.Log(this.transform.GetSiblingIndex().ToString());
		parentToReturnTo = this.transform.parent;
		placeholderParent = parentToReturnTo;
		
		this.transform.SetParent(this.transform.parent.parent);
		float up = 40f;
		Vector3 localposition = this.transform.position;
		localposition.y+=up;
		this.transform.position = localposition;
	}
	
	public void OnPointerExit(PointerEventData eventData){
		
		Debug.Log("Mouse Leave Card");
		if(!ifclick)
		{
			this.transform.SetParent(parentToReturnTo);
			this.transform.SetSiblingIndex( placeholder.transform.GetSiblingIndex() );
			GetComponent<CanvasGroup>().blocksRaycasts = true;
		
			Destroy(placeholder);
		}
		
	}
}
