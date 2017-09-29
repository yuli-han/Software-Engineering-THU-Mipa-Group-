using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class IfDead : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		GameObject myPanal = GameObject.Find("Canvas/Field");
		GameObject opPanal = GameObject.Find("Canvas/Field_op");
		GameObject handPanal = GameObject.Find("Canvas/Hand");
		GameObject opHandPanal = GameObject.Find("Canvas/Hand_op");
		for(int i=0; i<myPanal.transform.childCount; i++){
			if(myPanal.transform.GetChild(i).GetComponent<Common_CardInfo>()== null)
				continue;
			if(myPanal.transform.GetChild(i).GetComponent<Common_CardInfo>().cardInfo.CardType >= Common_CardInfo.BaseInfo.aimSpell)
				return;
			if(myPanal.transform.GetChild(i).GetComponent<Common_CardInfo>().cardInfo.hp<=0){
				Destroy(myPanal.transform.GetChild(i).gameObject);
			}
		}
		
		for(int i=0; i<opPanal.transform.childCount; i++){
			if(opPanal.transform.GetChild(i).GetComponent<Common_CardInfo>()== null)
				continue;
			if(opPanal.transform.GetChild(i).GetComponent<Common_CardInfo>().cardInfo.CardType >= Common_CardInfo.BaseInfo.aimSpell)
				continue;
			if(opPanal.transform.GetChild(i).GetComponent<Common_CardInfo>().cardInfo.hp<=0){
				Destroy(opPanal.transform.GetChild(i).gameObject);
			}
		}
		
		for(int i=0; i<handPanal.transform.childCount; i++){
			if(handPanal.transform.GetChild(i).GetComponent<Common_CardInfo>()== null)
				continue;
			if(handPanal.transform.GetChild(i).GetComponent<Common_CardInfo>().cardInfo.CardType >= Common_CardInfo.BaseInfo.aimSpell){
				if(handPanal.transform.GetChild(i).GetComponent<Common_CardInfo>().cardInfo.ifdelete)
					Destroy(handPanal.transform.GetChild(i).gameObject);
			}
		}
		for(int i=0; i<opHandPanal.transform.childCount; i++){
			if(opHandPanal.transform.GetChild(i).GetComponent<Common_CardInfo>()== null)
				continue;
			if(opHandPanal.transform.GetChild(i).GetComponent<Common_CardInfo>().cardInfo.CardType >= Common_CardInfo.BaseInfo.aimSpell){
				if(opHandPanal.transform.GetChild(i).GetComponent<Common_CardInfo>().cardInfo.ifdelete)
					Destroy(opHandPanal.transform.GetChild(i).gameObject);
			}
		}

        if ((GameObject.Find("Hero").GetComponent<Common_CardInfo>().cardInfo.hp <= 0) ||
            (GameObject.Find("Hero_op").GetComponent<Common_CardInfo>().cardInfo.hp <= 0))
        {
            GameObject.Find("GameCenter").GetComponent<GamePlayScene_GameCenterScript>().makeGameEnd();
        }
	}
}
