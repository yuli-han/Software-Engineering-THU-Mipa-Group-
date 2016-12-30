using UnityEngine;
using System.Collections;

[RequireComponent(typeof(GamePlayScene_CardFactory))]
[RequireComponent(typeof(CardSetScene_CardFactory))]
public class GamePlayScene_CardFactoryAutoUpdate : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<GamePlayScene_CardFactory>().frame = GetComponent<CardSetScene_CardFactory>().frame;
        GetComponent<GamePlayScene_CardFactory>().picture = GetComponent<CardSetScene_CardFactory>().picture;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
