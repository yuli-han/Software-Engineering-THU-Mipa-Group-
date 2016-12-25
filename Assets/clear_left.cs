using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class clear_left : MonoBehaviour, IDropHandler {

	public int size;
	private int inner_size;

	// Use this for initialization
	void Start () {
		size = 0;
		inner_size = 0;	
	}
	
	float min(float a, float b)
	{
		if(a<b)return a;
		else return b;
	}
	
	// Update is called once per frame
	void Update () {
		GameObject MyCanvas = GameObject.Find("Canvas/Panel_left_scoll");
		for(int i = 0;i < MyCanvas.transform.childCount;i++)
		{
			Transform a = MyCanvas.transform.GetChild(i);
			if(a.GetComponent<Draggerable_edit>())
			{
				bool D = a.GetComponent<Draggerable_edit>().IfDelete;
				if(D == true)
				{
					Destroy(MyCanvas.transform.GetChild(i).gameObject);
					return;
				}
			}
		}
		
		//GameObject.Find("Canvas/Scrollbar_left").GetComponent<Scrollbar>().value = 0;
		
		if(size >= inner_size+1)
		{
			GameObject.Find("Canvas/Scrollbar_left").GetComponent<Scrollbar>().value = 0;
			inner_size = size;
		}
		if(size<= inner_size-1)
		{
			GameObject.Find("Canvas/Scrollbar_left").GetComponent<Scrollbar>().value = min(1f,1.2f*this.GetComponent<Scrollbar>().value);
			inner_size = size;
		}
		
	}
	
	public void OnDrop(PointerEventData eventData){
		//Debug.Log("OnDrop Panal");
		if(eventData.pointerDrag ==null)
			return;

		Draggerable_edit d = eventData.pointerDrag.GetComponent<Draggerable_edit>();
		if(d != null){
			int a = d.GetComponent<Common_CardInfo>().cardInfo.copyType;
			d.parentToReturnTo = GameObject.Find("Canvas/Panel_left_scoll/Panel_left").transform;
			d.GetComponent<Common_CardInfo>().cardInfo.copyType = 1;
			if(a == 0)
				GameObject.Find("Scrollbar_left").GetComponent<scrollBar>().size++;
			//StartCoroutine(wait());
		}
		
		
		
		
	}
	IEnumerator wait()
	{
		yield return new WaitForSeconds(0.1f);
		GameObject Scoll = GameObject.Find("Canvas/Scrollbar_left");
		Scoll.GetComponent<scrollBar>().test();
	}
	
	
}
