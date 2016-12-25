using UnityEngine;
using System.Collections;
using System.IO;

public class MainScene_DeleteCardGroup : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown(){
		int CardGroupSum =  MainScene_CardGroupManager.CardGroupSum;
		int CanDelete = 0;
		string FileName;
		for (int i = 0; i < 3; i++) {
			if (MainScene_CardGroupManager.IsClick [i] == 1) {
				CanDelete = i + 1;
			}
		}
		if (CanDelete > 0) {
			MainScene_CardGroupManager.IsClick [CanDelete - 1] = 0;
			int CardNumber;
			if (CanDelete == 1) {
				FileName = MainScene_CardGroup1.CardGroupName;
				CardNumber = MainScene_CardGroup1.CardNumber;
			} else if (CanDelete == 2) {
				FileName = MainScene_CardGroup2.CardGroupName;
				CardNumber = MainScene_CardGroup2.CardNumber;
			} else {
				FileName = MainScene_CardGroup3.CardGroupName;
				CardNumber = MainScene_CardGroup3.CardNumber;
			}
			File.Delete (FileName);
			Debug.Log ("删除了卡组" + FileName);
			MainScene_CardGroupManager.CardGroupStatus [CardNumber] = -1; //标记，该卡组被删除
			MainScene_CardGroupManager.CardGroupName [CardNumber] = null;
			MainScene_CardGroupManager.CardGroupNumber = MainScene_CardGroupManager.CardGroupNumber - 1;  // 卡组数量减一
			StreamWriter sw = new StreamWriter ("CardGroup.txt", false);

			sw.WriteLine (MainScene_CardGroupManager.CardGroupNumber);       //重写CardGroup.txt文件
			for (int i = 0; i < MainScene_CardGroupManager.CardGroupSum; i++) {
				if (MainScene_CardGroupManager.CardGroupName [i] != null)
					sw.WriteLine (MainScene_CardGroupManager.CardGroupName [i]);		
			}

			sw.Flush ();
			sw.Close ();

		} else {
			Debug.Log ("没有选中卡组，无法实现删除！");
		}
	}
}
