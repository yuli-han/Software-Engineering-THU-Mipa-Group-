using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;


public class Draggerable_edit : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler{
	
	public bool IfDelete = false;
	
	public Transform parentToReturnTo = null;
	public Transform placeholderParent = null;
	public Transform initialcanvas = null;
	//bool ifclick;
	GameObject bigCard;
	
	GameObject placeholder = null;
	public int holderindex = -1;
	
	Vector3 BigCardPosition;
	
	void Awake(){
		//BigCardPosition = new Vector3(160f,600f,0f);
		BigCardPosition.z = 0f;
	}
	
	
	public void OnBeginDrag(PointerEventData eventData){
		
		if(!GameObject.Find("GameCenter").GetComponent<Group_GameCenterScript>().ifclick)
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
			
//			Debug.Log(initialcanvas);
			
		if(placeholder!=null)
		{
			this.transform.SetParent(parentToReturnTo);
			this.transform.SetSiblingIndex( placeholder.transform.GetSiblingIndex() );
			GetComponent<CanvasGroup>().blocksRaycasts = true;	
			Destroy(placeholder);
			return;
		}		
		if(GetComponent<Common_CardInfo>().cardInfo.copyType == 1)
		{
			placeholder = new GameObject();
		}			
		else
		{		
			placeholder = CardSet_DataBase.GetCard(GetComponent<Common_CardInfo>().cardInfo.id,
			GetComponent<Common_CardInfo>().cardInfo.copyType);
		}
		placeholder.transform.SetParent( this.transform.parent );
		LayoutElement le = placeholder.AddComponent<LayoutElement>();
		le.preferredWidth = this.GetComponent<LayoutElement>().preferredWidth;
		le.preferredHeight = this.GetComponent<LayoutElement>().preferredHeight;
		le.flexibleWidth = 0;
		le.flexibleHeight = 0;	
		holderindex = this.transform.GetSiblingIndex();
		placeholder.transform.SetSiblingIndex(holderindex);
		parentToReturnTo = this.transform.parent;
		this.transform.SetParent(this.transform.parent.parent);

			
			
		}
		if(bigCard!=null)
			{
				Destroy(bigCard);
			}
			
		GetComponent<CanvasGroup>().blocksRaycasts = false;
		GameObject.Find("GameCenter").GetComponent<Group_GameCenterScript>().ifclick = true;
			
		initialcanvas = this.transform.parent;
		this.transform.parent = this.transform.parent.parent;
		
	}
	
	public void OnDrag(PointerEventData eventData){
		
		if(placeholder == null)
		{
			Debug.Log("DragError");
			//Debug.Log(eventData.pointerDrag.name  + "OnDrag Fail");
			if(!GetComponent<CanvasGroup>().blocksRaycasts)
				GetComponent<CanvasGroup>().blocksRaycasts = true;
			return;
		}
		
		this.transform.position = eventData.position;
		
		// if(placeholder.transform.parent != placeholderParent)
			// placeholder.transform.SetParent(placeholderParent);
		
		//feng-seems to work as setting places
		//int newSiblingIndex = placeholderParent.childCount;		
		// for(int i=0; i<placeholderParent.childCount; i++){
			// if(this.transform.position.x < placeholderParent.GetChild(i).position.x){
				// newSiblingIndex = i;
				// if(placeholder.transform.GetSiblingIndex() < newSiblingIndex){
					// newSiblingIndex--;
				// }
				// break;
			// }
		// }		
		//placeholder.transform.SetSiblingIndex(newSiblingIndex);
		//feng-seems to work as setting places		
		
		//Debug.Log(eventData.pointerDrag.GetComponent<Draggerable_edit>().parentToReturnTo);
		//Debug.Log(eventData.pointerDrag.GetComponent<Draggerable_edit>().placeholderParent);

	}
	
	public void OnEndDrag(PointerEventData eventData){
		
		
		if(GameObject.Find("GameCenter").GetComponent<Group_GameCenterScript>().ifclick)
		{
			Debug.Log("OnEndDrag");
			if(placeholder!=null)
			{
				this.transform.SetParent(parentToReturnTo);
				this.transform.SetSiblingIndex( parentToReturnTo.childCount );				
				GetComponent<CanvasGroup>().blocksRaycasts = true;
				
				//Former card from canvas_all
				if(placeholder.GetComponent<Common_CardInfo>())
				{
					Debug.Log("ALL");
					if(placeholder.GetComponent<Common_CardInfo>().cardInfo.copyType == 0)
					{
						GameObject CopyCard = CardSet_DataBase.GetCard(placeholder.GetComponent<Common_CardInfo>().cardInfo.id,
						placeholder.GetComponent<Common_CardInfo>().cardInfo.copyType);	
						CopyCard.transform.SetParent( placeholder.transform.parent );
						LayoutElement le = placeholder.AddComponent<LayoutElement>();
						le.preferredWidth = this.GetComponent<LayoutElement>().preferredWidth;
						le.preferredHeight = this.GetComponent<LayoutElement>().preferredHeight;
						le.flexibleWidth = 0;
						le.flexibleHeight = 0;	
						CopyCard.transform.SetSiblingIndex(placeholder.transform.GetSiblingIndex());				
					}
					Destroy(placeholder);
					if(this.GetComponent<Common_CardInfo>().cardInfo.copyType != 1)
					{
						//Destroy the dragging card
						this.IfDelete = true;
					}
				}
				//Former card from canvas_selected
				else
				{
					Debug.Log("SELEETED");
					Destroy(placeholder);
					if(this.GetComponent<Common_CardInfo>().cardInfo.copyType == 1)
					{
						this.IfDelete = false;
					}
					else
					{
						this.IfDelete = true;
					}
				}
				
			}
			else
			{
				Debug.Log("BigBug");
				this.transform.SetParent(parentToReturnTo);
				this.transform.SetSiblingIndex( parentToReturnTo.childCount );				
				GetComponent<CanvasGroup>().blocksRaycasts = true;			
				this.IfDelete = false;
			}
			GameObject.Find("GameCenter").GetComponent<Group_GameCenterScript>().ifclick = false;
			Debug.Log(GetComponent<Common_CardInfo>().cardInfo.copyType);

			GameObject Scoll = GameObject.Find("Canvas/Scrollbar_left");
			//Scoll.GetComponent<Scrollbar>().value = 0;			
			
		}	
	}
	
	public void OnPointerEnter(PointerEventData eventData){
		
		if(GameObject.Find("GameCenter").GetComponent<Group_GameCenterScript>().ifclick)
		{
			return ;
		}		
		//draw big card
		Vector3 localposition = this.transform.position;
		int bigId = GetComponent<Common_CardInfo>().cardInfo.id;
		int bigType = GetComponent<Common_CardInfo>().cardInfo.copyType;		
		bigCard=CardSet_DataBase.GetCard(bigId,bigType,true);
		bigCard.transform.SetParent(GameObject.Find("Canvas").transform);
		if(localposition.y>400){
			BigCardPosition.y = localposition.y - 250f;
		}
		else{
			BigCardPosition.y = localposition.y + 300f;
		}
		
		if(localposition.x>=500){
			BigCardPosition.x = localposition.x - 150f;
		}
		else{
			BigCardPosition.x = localposition.x + 150f;
		}
		bigCard.transform.position = BigCardPosition;
		
		if(placeholder!=null)
		 {
			this.transform.SetParent(parentToReturnTo);
			this.transform.SetSiblingIndex( placeholder.transform.GetSiblingIndex() );
			GetComponent<CanvasGroup>().blocksRaycasts = true;	
			Destroy(placeholder);
			return;
		}		
		// if(GetComponent<Common_CardInfo>().cardInfo.copyType == 1)
		// {
			// placeholder = new GameObject();
		// }			
		// else
		// {		
			// placeholder = CardSet_DataBase.GetCard(GetComponent<Common_CardInfo>().cardInfo.id,
			// GetComponent<Common_CardInfo>().cardInfo.copyType);
		// }
		// placeholder.transform.SetParent( this.transform.parent );
		// LayoutElement le = placeholder.AddComponent<LayoutElement>();
		// le.preferredWidth = this.GetComponent<LayoutElement>().preferredWidth;
		// le.preferredHeight = this.GetComponent<LayoutElement>().preferredHeight;
		// le.flexibleWidth = 0;
		// le.flexibleHeight = 0;	
		// holderindex = this.transform.GetSiblingIndex();
		// placeholder.transform.SetSiblingIndex(holderindex);
		// parentToReturnTo = this.transform.parent;
		// this.transform.SetParent(this.transform.parent.parent);
		// float up = 40f;	
		// this.transform.position = localposition;
	}
	
	public void OnPointerExit(PointerEventData eventData){
		
		//Debug.Log("Mouse Leave Card");
		if(bigCard!=null)
			Destroy(bigCard);
		if(!GameObject.Find("GameCenter").GetComponent<Group_GameCenterScript>().ifclick && placeholder!=null)
		{
			this.transform.SetParent(parentToReturnTo);
			this.transform.SetSiblingIndex( placeholder.transform.GetSiblingIndex() );
			GetComponent<CanvasGroup>().blocksRaycasts = true;
		
			Destroy(placeholder);
		}
	}
	

}
