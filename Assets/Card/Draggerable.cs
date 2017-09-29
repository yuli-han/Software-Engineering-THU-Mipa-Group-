using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;


public class Draggerable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler{
	
	public Transform parentToReturnTo = null;
	public Transform placeholderParent = null;
	//bool ifclick;
	public GameObject bigCard;
	
	public GameObject placeholder = null;
	public Texture2D dragTexture;
	public Texture2D DefaultCursor;
	private CursorMode cursorMode = CursorMode.Auto;  
	private Vector2 hotSpot = Vector2.zero;  
	
	GameObject attackTarget = null;
	
	Vector3 BigCardPosition;
	
	void Awake(){
		BigCardPosition.z = 0f;
	}
	
	
	public void OnBeginDrag(PointerEventData eventData){
		
		if(this.GetComponent<Common_CardInfo>().cardInfo.position > 2)
			return;
		if(GameObject.Find("GameCenter").GetComponent<GamePlayScene_GameCenterScript>().ifsuspend)
			return;
		if(this.GetComponent<Common_CardInfo>().cardInfo.cost > GameObject.Find("GameCenter").GetComponent<GamePlayScene_GameCenterScript>().nowcost){
			
			if(this.GetComponent<Common_CardInfo>().cardInfo.position == 1)
			{
				Debug.Log("费用太高了");
				return;
			}
					
		}		
		if(this.GetComponent<Common_CardInfo>().cardInfo.position == 2 && !this.GetComponent<Common_CardInfo>().cardInfo.attack)
		{
			Debug.Log("还不能攻击");
			return;	
		}
		if(!GameObject.Find("GameCenter").GetComponent<GamePlayScene_GameCenterScript>().ifclick)
		{
			//Debug.Log("OnBeginDrag");
			if(this.GetComponent<Common_CardInfo>().cardInfo.position == 2){
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
			}
		    
			//Debug.Log(this.GetComponent<Common_CardInfo>().cardInfo.position.ToString());
			
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
		
		Cursor.SetCursor(dragTexture, hotSpot, cursorMode);
		if(GameObject.Find("GameCenter").GetComponent<GamePlayScene_GameCenterScript>().ifsuspend)
			return;
		if(this.GetComponent<Common_CardInfo>().cardInfo.cost > GameObject.Find("GameCenter").GetComponent<GamePlayScene_GameCenterScript>().nowcost)
				if(this.GetComponent<Common_CardInfo>().cardInfo.position == 1)
					return;
		if(this.GetComponent<Common_CardInfo>().cardInfo.position == 2 && !this.GetComponent<Common_CardInfo>().cardInfo.attack)
			return;
		if(placeholder == null)
		{
			//Debug.Log(eventData.pointerDrag.name  + "OnDrag Fail");
			if(!GetComponent<CanvasGroup>().blocksRaycasts)
				GetComponent<CanvasGroup>().blocksRaycasts = true;
			return;
		}
		
		this.transform.position = eventData.position;
		
		if(placeholder.transform.parent != placeholderParent){
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
		
		placeholder.transform.SetSiblingIndex(newSiblingIndex);}
	}
	
	public void OnEndDrag(PointerEventData eventData){
		
		Cursor.SetCursor(DefaultCursor, hotSpot, cursorMode);
		
		if(GameObject.Find("GameCenter").GetComponent<GamePlayScene_GameCenterScript>().ifsuspend)
		{
			return;
		}
		if(GameObject.Find("GameCenter").GetComponent<GamePlayScene_GameCenterScript>().ifclick)
		{
			if(placeholder!=null)
			{	
				Debug.Log("卡片弹回");
				int ini = this.GetComponent<Common_CardInfo>().cardInfo.position;
				this.transform.SetParent(parentToReturnTo);
				this.transform.SetSiblingIndex( placeholder.transform.GetSiblingIndex() );
				GetComponent<CanvasGroup>().blocksRaycasts = true;
				
				this.GetComponent<Common_CardInfo>().cardInfo.position = this.transform.parent.GetComponent<canvas_position>().position;
				Debug.Log("原来我在" + ini + "上面");
				Debug.Log("现在我在" + this.GetComponent<Common_CardInfo>().cardInfo.position + "上面");
				Destroy(placeholder);
					
				if(ini == 1 && this.GetComponent<Common_CardInfo>().cardInfo.position == 2){
                    if (this.GetComponent<Common_CardInfo>().cardInfo.CardType == Common_CardInfo.BaseInfo.noaimSpell)
                    {
                        this.GetComponent<Common_CardInfo>().cardInfo.thisTrigger.thisResult.exec(new Trigger.TriggerInput(this.gameObject,null));
                        this.GetComponent<Common_CardInfo>().cardInfo.ifdelete = true;
                    }
					NetMessage outMSG=new NetMessage();
					outMSG.infoType=NetMessage.Summon;
					outMSG.addint1=this.GetComponent<Common_CardInfo>().cardInfo.itemId;
					outMSG.addint2=this.transform.GetSiblingIndex();
					Netlink.SendMessage(outMSG);
					GameObject.Find("GameCenter").GetComponent<GamePlayScene_GameCenterScript>().nowcost-=this.GetComponent<Common_CardInfo>().cardInfo.cost;
					if(this.GetComponent<Common_CardInfo>().cardInfo.CardType == Common_CardInfo.BaseInfo.aimBattleUnit)
					{
						if(Trigger.Trigger.IfHaveTarget(this.gameObject,this.GetComponent<Common_CardInfo>().cardInfo.thisTrigger.thisTarget))
						{
							Debug.Log("请点击战吼目标");
							GameObject.Find("GameCenter").GetComponent<GamePlayScene_GameCenterScript>().suspend = this.gameObject;
							GameObject.Find("GameCenter").GetComponent<GamePlayScene_GameCenterScript>().ifsuspend = true;
						}
					}
					else
						if(this.GetComponent<Common_CardInfo>().cardInfo.CardType == Common_CardInfo.BaseInfo.noaimBattleUnit)
						{
							Trigger.TriggerInput newInput = new Trigger.TriggerInput(this.gameObject,null);
							this.GetComponent<Common_CardInfo>().cardInfo.thisTrigger.exec(newInput);
							Netlink.SendMessage(NetMessage.TriggerExec,newInput);
						}
					
				}
				
			}
			GameObject.Find("GameCenter").GetComponent<GamePlayScene_GameCenterScript>().ifclick = false;
		}
	}
	
	
	public void OnPointerEnter(PointerEventData eventData){
		if(this.GetComponent<Common_CardInfo>().cardInfo.position == 4)
			return;
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
		Vector3 localposition = this.transform.position;
		int bigId = GetComponent<Common_CardInfo>().cardInfo.id;
		int bigType = GetComponent<Common_CardInfo>().cardInfo.copyType;
		bigCard=Common_DataBase.GetCard(bigId,bigType,true);
		bigCard.GetComponent<Common_CardInfo>().cardInfo = this.GetComponent<Common_CardInfo>().cardInfo;
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
		
		if(this.GetComponent<Common_CardInfo>().cardInfo.position >= 2)
			return;
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
		if(this.GetComponent<Common_CardInfo>().cardInfo.position == 1)
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
	

	
	public void SummonUnit(int siblingNum)
	{
		for(int i=0; i<this.transform.childCount; i++){
			Color col;
			if(this.transform.GetChild(i).GetComponent<Image>()!=null)
			{
				col = this.transform.GetChild(i).GetComponent<Image>().color;
				col.a = 1f;
				this.transform.GetChild(i).GetComponent<Image>().color = col;
			}
			else
			{
				col = this.transform.GetChild(i).GetComponent<Text>().color;
				col.a = 1f;
				this.transform.GetChild(i).GetComponent<Text>().color = col;
			}
		}			
		this.GetComponent<Image>().sprite = this.GetComponent<CardMove>().__cardFront;
		this.transform.SetParent(GameObject.Find("Canvas/Field_op").transform);
		this.transform.SetSiblingIndex(siblingNum);
		this.GetComponent<Common_CardInfo>().cardInfo.position = 3;
		GameObject.Find("GameCenter").GetComponent<GamePlayScene_GameCenterScript>().moveEnded=true;
	}
}
