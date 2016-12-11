using UnityEngine;
using System.Collections;

public class IfDead : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		GameObject myPanal = GameObject.Find("Canvas/Field");
		GameObject opPanal = GameObject.Find("Canvas/Field_op");
		for(int i=0; i<myPanal.transform.childCount; i++){
			if(myPanal.transform.GetChild(i).GetComponent<Common_CardInfo>()== null)
				continue;
			if(myPanal.transform.GetChild(i).GetComponent<Common_CardInfo>().cardInfo.hp<=0){
				Destroy(myPanal.transform.GetChild(i).gameObject);
			}
		}
		
		for(int i=0; i<opPanal.transform.childCount; i++){
			if(opPanal.transform.GetChild(i).GetComponent<Common_CardInfo>()== null)
				continue;
			if(opPanal.transform.GetChild(i).GetComponent<Common_CardInfo>().cardInfo.hp<=0){
				Destroy(opPanal.transform.GetChild(i).gameObject);
			}
		}
	}
}
