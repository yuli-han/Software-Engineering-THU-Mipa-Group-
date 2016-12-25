using UnityEngine;
using System.Collections;

public class fireBall : MonoBehaviour {
	
	public bool ifUse;
	public int common_cost;
	public int id;
	public GameObject Image_ball;
	public GameObject ball;
	// Use this for initialization
	void Start () {
		ifUse = false;
		common_cost = 2;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(1))
		{
			if(ball != null)
			{
				Destroy(ball);
				GameObject.Find("GameCenter").GetComponent<GamePlayScene_GameCenterScript>().ifsuspend = false;
			}
		}
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
		ball = Instantiate(Image_ball);
		ball.transform.SetParent(GameObject.Find("Canvas").transform);
		ball.transform.position = new Vector3(680f,133f,0f);
		GameObject.Find("GameCenter").GetComponent<GamePlayScene_GameCenterScript>().ifsuspend = true;
		GameObject.Find("GameCenter").GetComponent<GamePlayScene_GameCenterScript>().suspend = GameObject.Find("Hero");		
	}
}
