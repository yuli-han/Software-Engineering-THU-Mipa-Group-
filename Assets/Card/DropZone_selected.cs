using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class DropZone_selected : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler{

	public void Start(){
		for (int i = 0; i < Common_NowCardSet.Length; i++) {
			GameObject c=CardSet_DataBase.GetCard(Common_NowCardSet.CardSet[i]%5+1,0);
			c.transform.SetParent(GameObject.Find("Canvas/Panel_right_up_scoll/Panel_right_up").transform);
		}
	}

	public void Update(){
		//Debug.Log("Num: " + cardNum);
		saveCardGroup();

		GameObject MyCanvas = GameObject.Find("Canvas/Panel_left_scoll/Panel_left");
		for(int i = 0;i < MyCanvas.transform.childCount;i++)
		{
			Transform a = MyCanvas.transform.GetChild(i);
			if(a.GetComponent<Draggerable_edit>())
			{
				int type = a.GetComponent<Draggerable_edit>().GetComponent<Common_CardInfo>().cardInfo.copyType;
				if(type != 1)
				{
					a.GetComponent<Draggerable_edit>().GetComponent<Common_CardInfo>().cardInfo.copyType = 1;
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
			d.GetComponent<Common_CardInfo>().cardInfo.copyType = 1;
		}
		//Debug.Log(d.parentToReturnTo);
		GameObject Scoll = GameObject.Find("Canvas/Scrollbar_left");
		Scoll.GetComponent<Scrollbar>().value = 0;
	}
	
	
		//0.定义一组的卡牌数目
	public static int NGROUP = 10;
	public int cardNum = 0;
	public string groupName = "group_one";
	//保存卡组（方式：扫描左侧所有的卡片，首先判断总数是否正确（不对时返回值为当前卡片数），正确然后再保存为（什么形式？），正常返回-1）
	public int saveCardGroup(/*传入panel*/){
		
		//1.获得panel中的children个数

		GameObject cardGroupPanel = GameObject.Find("Canvas/Panel_left_scoll/Panel_left");
		cardNum = cardGroupPanel.transform.childCount;
		
		
		//2.判断是否符合数目，如果不符则输出结果
		if(cardNum != NGROUP)
		{
			return cardNum;
		}
		else
		{
			//3.写一个for循环将卡牌唯一信息输出到文件保存
			for(int i = 0;i < cardNum;i++)
			{
				
				
			}
			
			
			
			return -1;
		}

	}
	
	
}
