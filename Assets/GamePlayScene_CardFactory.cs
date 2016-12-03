using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GamePlayScene_CardFactory :MonoBehaviour{

	public GameObject Card;
	public Sprite[] picture;
	
	public GameObject CreateNewCard(int cost,int atk,int hp,string name,string description,int pictureIndex)
	{
		GameObject NewCard = Instantiate(Card);
		NewCard.transform.FindChild("Card Name").GetComponent<Text>().text=name;
		NewCard.transform.Find("Description").GetComponent<Text>().text=description;
		NewCard.transform.Find("Cost").GetComponent<Text>().text=cost.ToString();
		NewCard.transform.Find("Attack").GetComponent<Text>().text=atk.ToString();
		NewCard.transform.Find("Life").GetComponent<Text>().text=hp.ToString();
		NewCard.transform.Find("photo").GetComponent<Image>().sprite = picture[pictureIndex];
		
		return(NewCard);
	}
}
