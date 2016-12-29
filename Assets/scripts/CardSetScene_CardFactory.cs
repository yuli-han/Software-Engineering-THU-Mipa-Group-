using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CardSetScene_CardFactory : MonoBehaviour
{

    public GameObject Card;
    public GameObject BigCard;
    public Sprite[] picture;
    public Sprite[] frame;

    public GameObject CreateNewCard(int pictureIndex)
    {
        GameObject NewCard = Instantiate(Card);

        NewCard.transform.Find("Image").GetComponent<Image>().sprite = picture[pictureIndex];
        NewCard.transform.Find("frame").GetComponent<Image>().sprite = frame[pictureIndex];
        return (NewCard);
    }

    public GameObject CreateNewBigCard(int pictureIndex)
    {
        GameObject NewCard = Instantiate(BigCard);

        NewCard.GetComponent<Image>().sprite = picture[pictureIndex];
        NewCard.transform.Find("frame").GetComponent<Image>().sprite = frame[pictureIndex];
        return (NewCard);
    }
}
