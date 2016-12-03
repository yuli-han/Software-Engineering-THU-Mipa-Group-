using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;


public class Draggerable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler{
	
	public Transform parentToReturnTo = null;
	public Transform placeholderParent = null;
	//bool ifclick;
	GameObject bigCard;
	
	GameObject placeholder = null;
	
	Vector3 BigCardPosition;
	
	void Awake(){
		BigCardPosition = new Vector3(160f,600f,0f);
	}
	
	
	public void OnBeginDrag(PointerEventData eventData){
		
		if(!GameObject.Find("GameCenter").GetComponent<GamePlayScene_GameCenterScript>().ifclick)
		{
			//Debug.Log("OnBeginDrag");
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
			if(bigCard!=null)
			{
				Destroy(bigCard);
			}
			
			GetComponent<CanvasGroup>().blocksRaycasts = false;
			GameObject.Find("GameCenter").GetComponent<GamePlayScene_GameCenterScript>().ifclick = true;
			//ifclick = true;
		}
		
	}
	
	public void OnDrag(PointerEventData eventData){
		
		if(placeholder == null)
		{
			Debug.Log(eventData.pointerDrag.name  + "OnDrag Fail");
			if(!GetComponent<CanvasGroup>().blocksRaycasts)
				GetComponent<CanvasGroup>().blocksRaycasts = true;
			return;
		}
		
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
		
		
		if(GameObject.Find("GameCenter").GetComponent<GamePlayScene_GameCenterScript>().ifclick)
		{
			//Debug.Log("OnEndDrag");
			if(placeholder!=null)
			{
				this.transform.SetParent(parentToReturnTo);
				this.transform.SetSiblingIndex( placeholder.transform.GetSiblingIndex() );
				GetComponent<CanvasGroup>().blocksRaycasts = true;
			
				Destroy(placeholder);
			}
			GameObject.Find("GameCenter").GetComponent<GamePlayScene_GameCenterScript>().ifclick = false;
		}
		
		//ifclick = false;
	}
	
	public void OnPointerEnter(PointerEventData eventData){
		
		if(GameObject.Find("GameCenter").GetComponent<GamePlayScene_GameCenterScript>().ifclick)
		{
			return ;
		}
		if(placeholder!=null)
		{
			this.transform.SetParent(parentToReturnTo);
			this.transform.SetSiblingIndex( placeholder.transform.GetSiblingIndex() );
			GetComponent<CanvasGroup>().blocksRaycasts = true;
		
			Destroy(placeholder);
			return;
		}
		
		//draw big card
		int bigId = GetComponent<Common_CardInfo>().cardInfo.id;
		bigCard=Common_DataBase.GetCard(bigId,true);
		bigCard.transform.SetParent(GameObject.Find("Canvas").transform);
		bigCard.transform.position = BigCardPosition;
		
		
		//Debug.Log("Mouse Enter Card");
		placeholder = new GameObject();
		placeholder.transform.SetParent( this.transform.parent );
		LayoutElement le = placeholder.AddComponent<LayoutElement>();
		le.preferredWidth = this.GetComponent<LayoutElement>().preferredWidth;
		le.preferredHeight = this.GetComponent<LayoutElement>().preferredHeight;
		le.flexibleWidth = 0;
		le.flexibleHeight = 0;
		
		placeholder.transform.SetSiblingIndex(this.transform.GetSiblingIndex());
		//Debug.Log(this.transform.GetSiblingIndex().ToString());
		parentToReturnTo = this.transform.parent;
		placeholderParent = parentToReturnTo;
		
		this.transform.SetParent(this.transform.parent.parent);
		float up = 40f;
		Vector3 localposition = this.transform.position;
		localposition.y+=up;
		this.transform.position = localposition;
	}
	
	public void OnPointerExit(PointerEventData eventData){
		
		//Debug.Log("Mouse Leave Card");
		if(bigCard!=null)
			Destroy(bigCard);
		if(!GameObject.Find("GameCenter").GetComponent<GamePlayScene_GameCenterScript>().ifclick && placeholder!=null)
		{
			this.transform.SetParent(parentToReturnTo);
			this.transform.SetSiblingIndex( placeholder.transform.GetSiblingIndex() );
			GetComponent<CanvasGroup>().blocksRaycasts = true;
		
			Destroy(placeholder);
		}
		
	}
}
