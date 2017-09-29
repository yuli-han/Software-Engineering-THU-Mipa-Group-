using UnityEngine;
using System.Collections;
using System.IO;

public class MainScene_CardGroupManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		PageNumber = 1;
		string FileName;
		FileName = "CardGroup.txt";
		if (File.Exists(FileName)) {
			StreamReader sr = new StreamReader(FileName, System.Text.Encoding.Default);
			CardGroupNumber = int.Parse (sr.ReadLine ());
			string line;
			line = sr.ReadLine ();
			int i = 0;
			CardGroupName = new string[100];
			while (line != null) {
				CardGroupName [i] = line;
				i++;
				line = sr.ReadLine ();
			}
			CardGroupSum = CardGroupNumber;
			CardGroupStatus = new int[100];
			for (i = 0; i < CardGroupNumber; i++) {    //游戏开始的时候，所有卡组均处于未删除状态
				CardGroupStatus [i] = 0;
			}
			IsClick = new int[3];            // 游戏开始的时候，所有卡牌均未被点击
			for (i = 0; i < 3; i++)
				IsClick [i] = 0;
			sr.Close ();
		} else {
			FileStream newfs = new FileStream (FileName, FileMode.Create);
			StreamWriter sw = new StreamWriter (newfs);
			sw.WriteLine ("0");
			sw.Close ();
			newfs.Close ();
			CardGroupSum = 0;
			CardGroupNumber = 0;
			CardGroupName = new string[100];
			CardGroupStatus = new int[100]; 
			IsClick = new int[3];            // 游戏开始的时候，所有卡牌均未被点击
			for (int i = 0; i < 3; i++)
				IsClick [i] = 0;
		}
	}
	
	// Update is called once per frame
	void Update () {
	}

	public static int[] CardGroupStatus;  //用来记录卡组是否有被删除
	public static int PageNumber;        
	public static int CardGroupSum;
	public static int CardGroupNumber;
	public static string[] CardGroupName;
	public static int[] IsClick;
}