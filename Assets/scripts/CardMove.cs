using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CardMove : MonoBehaviour {
	
	public AnimationCurve scaleCurve;
	public AnimationCurve positionCurve;
	public AnimationCurve attackPath;
	public AnimationCurve waggle;
	public AnimationCurve deathCurve;
	public GameObject cardBack;
	public GameObject textBlood;
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
	
	public void cardAttack(GameObject start, GameObject end)
	{
		StartCoroutine(CardAttackMove(start,end));
	}
		
	IEnumerator CardAttackMove(GameObject start, GameObject end)
	{
		float time = 0f;//Debug.Log(start.ToString()+" 给我动起来 "+ end.ToString());
		Vector3 beginPosition = start.GetComponent<Draggerable>().placeholder.transform.position;
		while(time<2f)//移动攻击的动画
		{
			Vector3 location = beginPosition - (1f-attackPath.Evaluate(time/2))*(beginPosition - end.transform.position)*0.8f;
			time += Time.deltaTime / duration;
			this.transform.position = location;
			
			yield return new WaitForFixedUpdate();
		}
		Vector3 delta = new Vector3(10f,0f,0f);
		Vector3 endPosition = end.transform.position;

		GameObject reduceBlood = Instantiate(textBlood);
		GameObject reduceBlood2 = Instantiate(textBlood);
		reduceBlood.transform.SetParent(GameObject.Find("Canvas").transform);
		reduceBlood2.transform.SetParent(GameObject.Find("Canvas").transform);
		reduceBlood.transform.position = start.transform.position;
		reduceBlood2.transform.position = end.transform.position;
		reduceBlood.GetComponent<Text>().text = "-"+end.GetComponent<Common_CardInfo>().cardInfo.atk.ToString();
		reduceBlood2.GetComponent<Text>().text = "-"+start.GetComponent<Common_CardInfo>().cardInfo.atk.ToString();
		
		while(time<2.6f)//宸﹀彸鎶栧姩鐨勫姩鐢诲姞鎺夎
		{
			Vector3 location = beginPosition - waggle.Evaluate((time-2f)/0.6f)*delta;
			Vector3 location2 = endPosition - waggle.Evaluate((time-2f)/0.6f)*delta;
			time+=Time.deltaTime / duration;
			start.transform.position = location;
			end.transform.position = location2;
			reduceBlood.transform.position = reduceBlood.transform.position + new Vector3(0f,2f,0f);
			Color c = reduceBlood.GetComponent<Text>().color;
			c.a = deathCurve.Evaluate((2.6f-time)/0.6f);
			reduceBlood.GetComponent<Text>().color = c;
			reduceBlood2.transform.position = reduceBlood2.transform.position + new Vector3(0f,2f,0f);
			reduceBlood2.GetComponent<Text>().color = c;
			yield return new WaitForFixedUpdate();
		}
		Destroy(reduceBlood);
		Destroy(reduceBlood2);
		if(this.GetComponent<Common_CardInfo>().cardInfo.hp <= end.GetComponent<Common_CardInfo>().cardInfo.atk)
		while(time<3.6f)//旋转死亡的动画
		{
			float scale = deathCurve.Evaluate((time-2.6f)/0.6f);
			//Debug.Log(time.ToString()+" 实验 " +scale.ToString());
			time += Time.deltaTime / duration;
			//Quaternion q = Quaternion.Euler(new Vector3(0f,0f,360f*scale));
			
			Vector3 rotation = this.transform.localEulerAngles; 
			rotation.z = 360f*scale; 
			this.transform.localEulerAngles = rotation;
			//this.transform.rotation = q;
			
			Vector3 localScale = this.transform.localScale;
            localScale.x = 1f-scale;
			localScale.y = 1f-scale;
			this.transform.localScale = localScale;
			if(end.GetComponent<Common_CardInfo>().cardInfo.hp <= this.GetComponent<Common_CardInfo>().cardInfo.atk)
			{
				end.transform.localScale = localScale;
				end.transform.localEulerAngles = rotation;
			}
			
			yield return new WaitForFixedUpdate();
		}
		OnAttackEvent(start,end);
	
	}
	public void OnAttackEvent(GameObject user,GameObject target)
	{
		//血量结算
		user.GetComponent<Common_CardInfo>().cardInfo.hp-=target.GetComponent<Common_CardInfo>().cardInfo.atk;
		target.GetComponent<Common_CardInfo>().cardInfo.hp-=user.GetComponent<Common_CardInfo>().cardInfo.atk;
	}
	
	
	//动画的分类
	public static readonly int battleDamage = 1;
	public static readonly int spellDamage = 2;
	public static readonly int battleHeal = 3;
	public static readonly int spellHeal = 4;
	public static readonly int drawCard = 5;
	
	public void Move(Trigger.TriggerInput input,int type, int extra = 0)
	{
		switch(type)
		{
			case 1:
				StartCoroutine(battleMove(input,extra));
				break;
			case 2:
				break;
		}
		//input.CardUser.GetComponent<Common_CardInfo>().cardInfo.thisTrigger.thisResult.exec(input);
	}
		
	IEnumerator battleMove(Trigger.TriggerInput input, int damage)
	{
		Vector3 delta = new Vector3(10f,0f,0f);
		Vector3 beginPosition = input.CardTarget.transform.position;
		float time = 0f;
		GameObject reduceBlood = Instantiate(textBlood);
		reduceBlood.transform.SetParent(GameObject.Find("Canvas").transform);
		reduceBlood.transform.position = input.CardTarget.transform.position;
		reduceBlood.GetComponent<Text>().text = "-"+damage.ToString();
		while(time<0.8f)//宸﹀彸鎶栧姩鐨勫姩鐢诲姞鎺夎
		{
			Vector3 location = beginPosition - waggle.Evaluate(time/0.8f)*delta;
			time+=Time.deltaTime / duration;
			input.CardTarget.transform.position = location;
			reduceBlood.transform.position = reduceBlood.transform.position + new Vector3(0f,2f,0f);
			Color c = reduceBlood.GetComponent<Text>().color;
			c.a = 255f*deathCurve.Evaluate(time/0.8f);
			reduceBlood.GetComponent<Text>().color = c;
			yield return new WaitForFixedUpdate();
		}
		Destroy(reduceBlood);
		if(input.CardTarget.GetComponent<Common_CardInfo>().cardInfo.hp <= damage)
		while(time<1.8f)//旋转死亡的动画
		{
			float scale = deathCurve.Evaluate(time-0.8f);
			//Debug.Log(time.ToString()+" 实验 " +scale.ToString());
			time += Time.deltaTime / duration;
			//Quaternion q = Quaternion.Euler(new Vector3(0f,0f,360f*scale));
		
			Vector3 rotation = input.CardTarget.transform.localEulerAngles; 
			rotation.z = 360f*scale; 
			input.CardTarget.transform.localEulerAngles = rotation;		
			Vector3 localScale = input.CardTarget.transform.localScale;
			localScale.x = 1f-scale;
			localScale.y = 1f-scale;
			input.CardTarget.transform.localScale = localScale;
		
			yield return new WaitForFixedUpdate();
		}
		input.CardUser.GetComponent<Common_CardInfo>().cardInfo.thisTrigger.thisResult.exec(input);
	}
	/*IEnumerator SpellMove(Trigger.TriggerInput input, int damage)
	{
		
	}*/	
}
