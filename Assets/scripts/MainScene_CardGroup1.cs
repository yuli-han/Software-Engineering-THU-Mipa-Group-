using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainScene_CardGroup1 : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	public Sprite CardBack;
	public Sprite Card;
	public static int CardNumber; //卡组编号
	public static string CardGroupName; // 卡组对应的文件名

	// Update is called once per frame
	void Update () {
		int PageNumber = MainScene_CardGroupManager.PageNumber;
		int CardNo = (PageNumber-1) * 3 + 1;
		int CardGroupNumber = MainScene_CardGroupManager.CardGroupNumber;

		if (CardNo > CardGroupNumber)
			GameObject.Find ("CardGroup1").GetComponent<SpriteRenderer> ().sprite = CardBack;
		else {
			GameObject.Find ("CardGroup1").GetComponent<SpriteRenderer> ().sprite = Card;
			int j=0;
			for (int i = 0; i < MainScene_CardGroupManager.CardGroupSum; i++) {
				if (MainScene_CardGroupManager.CardGroupStatus[i]!=-1)
					j++;
				if (j == CardNo) {
					CardNumber = i;
					break;
				}
			}
			CardGroupName = MainScene_CardGroupManager.CardGroupName [CardNumber];
			if (MainScene_CardGroupManager.IsClick [0] == 1) {
				Vector3 ClickPosition;
				ClickPosition.x = 2867.97f;
				ClickPosition.y = 2854.4f;
				ClickPosition.z = 905.055f;
				this.transform.position = ClickPosition;
			} else {
				Vector3 ClickPosition;
				ClickPosition.x = 2877.97f;
				ClickPosition.y = 2854.4f;
				ClickPosition.z = 905.055f;
				this.transform.position = ClickPosition;
			}
		}
	}

	void OnMouseDown(){
		int PageNumber = MainScene_CardGroupManager.PageNumber;
		int CardNo = (PageNumber-1) * 3 + 1;
		int CardGroupNumber = MainScene_CardGroupManager.CardGroupNumber;

		if (!(CardNo > CardGroupNumber)) {
			MainScene_CardGroupManager.IsClick [0] = 1;
			MainScene_CardGroupManager.IsClick [1] = 0;
			MainScene_CardGroupManager.IsClick [2] = 0;
			Debug.Log ("您选中了一个卡组"+CardGroupName);
		}

	}

	void OnPointerEnter(PointerEventData eventData)
	{
		Debug.Log ("Pointer in");
	}

	void OnPointerExit(PointerEventData eventData)
	{
		Debug.Log ("Pointer out");
	}

}
