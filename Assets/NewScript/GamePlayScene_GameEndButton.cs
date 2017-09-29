using UnityEngine;
using System.Collections;

public class GamePlayScene_GameEndButton : MonoBehaviour {

    void OnMouseDown()
    {
        GameObject.Find("GameCenter").GetComponent<GamePlayScene_GameCenterScript>().makeGameEnd();
    }

}
