using UnityEngine;
using System.Collections;
using System.IO;

public class save_button : MonoBehaviour {

	public static int GROUP_SIZE = 5;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	
	
	void OnMouseDown()
	{
		Debug.Log("haha");
		int tempnum = GameObject.Find("Canvas/Panel_left_scoll/Panel_left").transform.childCount;
		print(tempnum);

		// if(tempnum != GROUP_SIZE)
		// {
			// Debug.Log("GROUP_SIZE is not correct");
		// }
		// else
		// {
			// string groupName = GameObject.Find("Canvas/Panel_left_scoll/Panel_left").transform.GetComponent<DropZone_selected>().groupName;
			// print(groupName);
			// FileStream fs = new FileStream(groupName+".txt",FileMode.Create);
			// StreamWriter sw = new StreamWriter(fs);
			// for(int i = 0;i < GROUP_SIZE;i++)
			// {
				// int tempcardid = GameObject.Find("Canvas/Panel_left_scoll/Panel_left").transform.GetChild(i).
				// GetComponent<Draggerable_edit>().GetComponent<Common_CardInfo>().cardInfo.id;
				// sw.WriteLine(tempcardid);
			// }
			// sw.Close();
		// }

		Common_NowCardSet.Length = tempnum;
		Common_NowCardSet.CardSet = new int[tempnum];
		for(int i = 0;i < tempnum;i++)
		{
			int tempcardid = GameObject.Find("Canvas/Panel_left_scoll/Panel_left").transform.GetChild(i).
			GetComponent<Draggerable_edit>().GetComponent<Common_CardInfo>().cardInfo.id;
			Common_NowCardSet.CardSet[i] = tempcardid;
		}
		
		Common_NowCardSet.Length = tempnum;
		Common_NowCardSet.SaveCardFile();

	}
	
}
