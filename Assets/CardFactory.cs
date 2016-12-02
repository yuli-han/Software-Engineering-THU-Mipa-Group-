using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CardFactory :MonoBehaviour{

	public GameObject Card;
	
	public GameObject CreateNewCard(int cost,int atk,int hp,string name,string description,string pictureLocation)
	{
		GameObject NewCard = Instantiate(Card);
		NewCard.transform.FindChild("Card Name").GetComponent<Text>().text=name;
		NewCard.transform.Find("Description").GetComponent<Text>().text=description;
		NewCard.transform.Find("Cost").GetComponent<Text>().text=cost.ToString();
		NewCard.transform.Find("Attack").GetComponent<Text>().text=atk.ToString();
		NewCard.transform.Find("Life").GetComponent<Text>().text=hp.ToString();
		NewCard.transform.Find("Image").GetComponent<Image>().overrideSprite = Resources.Load(pictureLocation, typeof(Sprite)) as Sprite;
		
		return(NewCard);
	}
}
