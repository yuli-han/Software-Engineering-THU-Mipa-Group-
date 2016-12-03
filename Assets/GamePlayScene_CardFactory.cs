using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GamePlayScene_CardFactory :MonoBehaviour{

	public GameObject Card;
	public Sprite[] picture;
	
	public GameObject CreateNewCard(int pictureIndex)
	{
		GameObject NewCard = Instantiate(Card);
		NewCard.transform.Find("photo").GetComponent<Image>().sprite = picture[pictureIndex];
		return(NewCard);
	}
}
