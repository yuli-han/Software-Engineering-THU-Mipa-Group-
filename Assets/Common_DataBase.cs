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
                info.maxhp = 12;
                info.name = "Cute Wing";
                info.description = "战吼：秒杀所有队友，秒杀自己所有手牌";
				info.CardType=Common_CardInfo.BaseInfo.normalUnit;
                break;
            case 2:
                info.cost = 1;
                info.atk = 1;
                info.maxhp = 1;
                info.hp = 1;
                info.name = "Angry Bird";
                info.description = "激怒：杀死场上的所有死亡之翼";
				info.CardType=Common_CardInfo.BaseInfo.normalUnit;
                break;
			case 3:
                info.cost = 1;
                info.atk = 1;
                info.maxhp = 1;
                info.hp = 1;
                info.name = "Alexstrasza";
                info.description = "激怒：杀死场上的所有死亡之翼";
				info.CardType=Common_CardInfo.BaseInfo.normalUnit;
                break;
			case 4:
                info.cost = 1;
                info.atk = 1;
                info.maxhp = 1;
                info.hp = 1;
                info.name = "Illidan Stormrage";
                info.description = "激怒：杀死场上的所有死亡之翼";
				info.CardType=Common_CardInfo.BaseInfo.normalUnit;
                break;
			case 5:
                info.cost = 6;
                info.atk = 5;
                info.maxhp = 5;
                info.hp = 5;
                info.name = "Sylvanas";
                info.description = "亡语：获取MiPa";
				info.CardType=Common_CardInfo.BaseInfo.normalUnit;
                break;
			case 6:

				info.cost=4;
				info.name="水球术";
				info.description="对一个单位造成5点伤害/n 都觉得火球术超模，那么水球术总不超模了吧！";
				info.CardType=Common_CardInfo.BaseInfo.aimSpell;//这个标签指示了这张卡是一个指向法术卡
				info.thisTrigger=new Trigger.Trigger();
				info.thisTrigger.thisTarget.target=Trigger.TriggerTarget.Anyone;//在这里指明了这个卡可以攻击任何单位
				info.thisTrigger.thisResult = new TriggerExecSpace.DealDamage(5);

            break;
			case 7:
				info.cost=3;
				info.name="光明箭";
				info.description="对一个敌方单位造成4点伤害";
				info.CardType=Common_CardInfo.BaseInfo.aimSpell;//这个标签指示了这张卡是一个指向法术卡
				info.thisTrigger = new Trigger.Trigger();
				info.thisTrigger.thisTarget.target=Trigger.TriggerTarget.Enemy;//在这里则指示了只能攻击敌方单位
				info.thisTrigger.thisResult = new TriggerExecSpace.DealDamage(4);
            break;
        }
        info.id = cardId;
        nowItemId++;
        info.itemId = nowItemId;
		info.ifdelete = false;
        newCard.GetComponent<Common_CardInfo>().cardInfo = info;

        newCard.name = newCard.name + nowItemId;

        return newCard;
    }
}