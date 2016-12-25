using UnityEngine;
using System.Collections;

public class fireBall : MonoBehaviour {
	
	public bool ifUse;
	public int common_cost;
	public int id;
	// Use this for initialization
	void Start () {
		ifUse = false;
		common_cost = 2;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void OnMouseDown()
	{
		if(ifUse)
			return;
		if(common_cost > GameObject.Find("GameCenter").GetComponent<GamePlayScene_GameCenterScript>().nowcost){
			Debug.Log("费用太高了");
			return;		
		}
		if(GameObject.Find("GameCenter").GetComponent<GamePlayScene_GameCenterScript>().ifclick)
			return;
		if(GameObject.Find("GameCenter").GetComponent<GamePlayScene_GameCenterScript>().ifsuspend)
		{
			return;
		}
		Debug.Log("请选择打击目标！");
		GameObject.Find("GameCenter").GetComponent<GamePlayScene_GameCenterScript>().ifsuspend = true;
		GameObject.Find("GameCenter").GetComponent<GamePlayScene_GameCenterScript>().suspend = GameObject.Find("Hero");		
	}
}
