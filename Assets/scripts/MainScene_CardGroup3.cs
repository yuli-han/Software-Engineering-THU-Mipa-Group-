using UnityEngine;
using System.Collections;

public class MainScene_CardGroup3 : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	public Sprite CardBack;
	public Sprite Card;
	public static int CardNumber;
	public static string CardGroupName; // 卡组对应的文件名
	// Update is called once per frame
	void Update () {
		int PageNumber = MainScene_CardGroupManager.PageNumber;
		int CardNo = (PageNumber-1) * 3 + 3;
		int CardGroupNumber =  MainScene_CardGroupManager.CardGroupNumber;

		if (CardNo > CardGroupNumber)
			GameObject.Find ("CardGroup3").GetComponent<SpriteRenderer> ().sprite = CardBack;
		else {
			GameObject.Find ("CardGroup3").GetComponent<SpriteRenderer> ().sprite = Card;
			int j = 0;
			for (int i = 0; i < MainScene_CardGroupManager.CardGroupSum; i++) {
				if (MainScene_CardGroupManager.CardGroupStatus [i] != -1)
					j++;
				if (j == CardNo) {
					CardNumber = i;
					break;
				}
			}
			CardGroupName = MainScene_CardGroupManager.CardGroupName [CardNumber];
			if (MainScene_CardGroupManager.IsClick [2] == 1) {
				Vector3 ClickPosition;
				ClickPosition.x = 3f;
				ClickPosition.y = 0f;
				ClickPosition.z = 5.3f;
				this.transform.position = ClickPosition;
			} else {
				Vector3 ClickPosition;
				ClickPosition.x = 3.8f;
				ClickPosition.y = 0f;
				ClickPosition.z = 5.3f;
				this.transform.position = ClickPosition;
			}
		}
	}

	void OnMouseDown(){
		int PageNumber = MainScene_CardGroupManager.PageNumber;
		int CardNo = (PageNumber-1) * 3 + 3;
		int CardGroupNumber =  MainScene_CardGroupManager.CardGroupNumber;

		if (!(CardNo > CardGroupNumber)) {
			MainScene_CardGroupManager.IsClick [0] = 0;
			MainScene_CardGroupManager.IsClick [1] = 0;
			MainScene_CardGroupManager.IsClick [2] = 1;
			Debug.Log ("您选中了一个卡组"+CardGroupName);
		}
	}
}
