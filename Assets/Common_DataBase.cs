using UnityEngine;
using System.Collections;

//此类的用途是用于获取卡片详细数据

public class Common_DataBase 
{
    static int nowItemId = 0;
    //需要为卡片的属性定一个类，并且将其作为卡片的成员。
    static public GameObject GetCard(int cardId, bool amplify = false)
    {
		GameObject newCard;
		if(amplify){
			newCard = GameObject.Find("GamePlayScene_CardFactory")
            .GetComponent<GamePlayScene_CardFactory>().CreateNewBigCard(cardId);
		}
		else{
			newCard = GameObject.Find("GamePlayScene_CardFactory")
            .GetComponent<GamePlayScene_CardFactory>().CreateNewCard(cardId);
		}
        
        Common_CardInfo.BaseInfo info=new Common_CardInfo.BaseInfo();
        switch (cardId)
        {
            case 1:
                info.cost = 10;
                info.atk = 12;
                info.hp = 12;
                info.name = "卖萌之翼";
                info.description = "战吼：秒杀所有队友，秒杀自己所有手牌";
                break;
            case 2:
                info.cost = 1;
                info.atk = 1;
                info.hp = 1;
                info.name = "愤怒的小鸡";
                info.description = "激怒：杀死场上的所有死亡之翼";
                break;
			case 3:
                info.cost = 1;
                info.atk = 1;
                info.hp = 1;
                info.name = "愤怒的小鸡";
                info.description = "激怒：杀死场上的所有死亡之翼";
                break;
			case 4:
                info.cost = 1;
                info.atk = 1;
                info.hp = 1;
                info.name = "愤怒的小鸡";
                info.description = "激怒：杀死场上的所有死亡之翼";
                break;
			case 5:
                info.cost = 6;
                info.atk = 5;
                info.hp = 5;
                info.name = "西尔瓦娜丝";
                info.description = "亡语：获取MiPa";
                break;
			
        }
        info.id = cardId;
        nowItemId++;
        info.itemId = nowItemId;
        newCard.GetComponent<Common_CardInfo>().cardInfo = info;

        newCard.name = newCard.name + nowItemId;

        return newCard;
    }
}