using UnityEngine;
using System.Collections;
using System.IO;

public class MainScene_CreateCardGroup : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown(){
		int CardGroupNumber;
		string FileName;
		string[] CardGroupName;

		Debug.Log ("添加了一个新的卡组");
		FileName = "CardGroup.txt";
		StreamReader sr = new StreamReader(FileName, System.Text.Encoding.Default);

		CardGroupNumber = int.Parse(sr.ReadLine ());

		CardGroupNumber = CardGroupNumber + 1;

		MainScene_CardGroupManager.CardGroupNumber = CardGroupNumber;
		MainScene_CardGroupManager.CardGroupSum = MainScene_CardGroupManager.CardGroupSum+1;
		MainScene_CardGroupManager.CardGroupStatus[CardGroupNumber]=0;

		string line;
		int i = 0;
		int max = 0;
		int number;
		int length;
		line = sr.ReadLine ();
		while(i<MainScene_CardGroupManager.CardGroupNumber-1)
		{
			MainScene_CardGroupManager.CardGroupName [i] = line;
			length = line.Length;
			length = length - 4;
			line = line.Remove (length, length + 3);
			number = int.Parse (line);
			if (number > max)
				max = number;
			i++;
			line = sr.ReadLine ();
		}

		max = max + 1;
		FileName= max.ToString ();
		FileName=FileName+".txt";
		Debug.Log ("新卡组的名字是"+FileName);
		MainScene_CardGroupManager.CardGroupName [i]= FileName;
		MainScene_CardGroupManager.CardGroupStatus [i] = 0;
		sr.Close ();

		StreamWriter sw=new StreamWriter("CardGroup.txt",false);

		sw.WriteLine (CardGroupNumber);
		for (i = 0; i < MainScene_CardGroupManager.CardGroupSum; i++) {
				sw.WriteLine (MainScene_CardGroupManager.CardGroupName[i]);		
		}
		sw.Flush ();
		sw.Close ();

		FileStream newfs = new FileStream (FileName, FileMode.Create);
		newfs.Close ();


		//Common_CardGroupInfo.CardGroupName [Common_CardGroupInfo.CardGroupNumber-1] = FileName;

	}
}
