using UnityEngine;
using System.Collections;

public class CardMove : MonoBehaviour {
	
	public AnimationCurve scaleCurve;
	public AnimationCurve positionCurve;
	public AnimationCurve attackPath;
	public GameObject cardBack;
	float duration = 0.5f;
	
	public void flyAndFlip()
	{
		StopCoroutine(moveAndFlip());
        StartCoroutine(moveAndFlip());
	}
	IEnumerator moveAndFlip()
	{
		Vector3 handPosition;
		int count = GameObject.Find("Canvas/Hand").transform.childCount;
		if(count>0)
		{
			handPosition = GameObject.Find("Canvas/Hand").transform.GetChild(count-1).position;
			handPosition.x+=110f;
		}
		else
			handPosition = new Vector3(512f,50f,0f);
		
		GameObject CardBack = Instantiate(cardBack);
		float time = 0f;
		CardBack.transform.position = new Vector3(1200f,380f,0f);
		CardBack.transform.SetParent(GameObject.Find("Canvas").transform);
		//飞到(520f,380f,0f)
		while(time<1.2f)
		{
			float x = 1200f - 680f*positionCurve.Evaluate(time/4);
            time += Time.deltaTime / duration;

            Vector3 local = CardBack.transform.position;
            local.x = x;
            CardBack.transform.position = local;

            yield return new WaitForFixedUpdate();
		}
		while(time<1.8f)
		{
			float scale = scaleCurve.Evaluate((time-0.3f)/3);
            time += Time.deltaTime / duration;

            Vector3 localScale = CardBack.transform.localScale;
            localScale.x = scale;
            CardBack.transform.localScale = localScale;

            yield return new WaitForFixedUpdate();
		}
		this.transform.SetParent(GameObject.Find("Canvas").transform);
		this.transform.position = CardBack.transform.position;
		this.transform.localScale = CardBack.transform.localScale;
		Destroy(CardBack);
		while(time<2.4f)
		{
			float scale = scaleCurve.Evaluate((time-0.3f)/3);
            time += Time.deltaTime / duration;

            Vector3 localScale = this.transform.localScale;
            localScale.x = scale;
            this.transform.localScale = localScale;

            yield return new WaitForFixedUpdate();
		}
		//(520,380,0)
		while(time<3.3f)
		{
			Vector3 location = handPosition - positionCurve.Evaluate((time-0.3f)/3)*(handPosition - new Vector3(520f,380f,0f));
			time += Time.deltaTime / duration;
			this.transform.position = location;
			yield return new WaitForFixedUpdate();
		}
		this.transform.SetParent(GameObject.Find("Canvas/Hand").transform);
	}
	
	IEnumerator CardAttckMove(Vector3 start, Vector3 end)
	{
		yield return new WaitForFixedUpdate();
	}
}
