using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class CardMove : MonoBehaviour {
	
	public AnimationCurve scaleCurve;
	public AnimationCurve positionCurve;
	public AnimationCurve attackPath;
	public AnimationCurve waggle;
	public AnimationCurve deathCurve;
	public Sprite __cardBack;
	public Sprite __cardFront;
	public GameObject cardBack;
	public GameObject textBlood;
	public GameObject Image_ball;
	public GameObject Heal_ball;
	public GameObject Damage_ball;
	
	GameObject placeholder_2;
	float duration = 0.5f;
	
	public void flyAndFlip(int id =0)
	{
        StartCoroutine(moveAndFlip(id));
	}
	
	IEnumerator moveAndFlip(int id)
	{
		Vector3 handPosition;
		int count;
		if(id == 0)
			count = GameObject.Find("Canvas/Hand").transform.childCount;
		else
			count = GameObject.Find("Canvas/Hand_op").transform.childCount;
		if(count>0)
		{
			if(id==0)
				handPosition = GameObject.Find("Canvas/Hand").transform.GetChild(count-1).position;
			else
				handPosition = GameObject.Find("Canvas/Hand_op").transform.GetChild(count-1).position;
			handPosition.x+=110f;
		}
		else
		{
			if(id == 0)handPosition = new Vector3(512f,50f,0f);
			else handPosition = new Vector3(512f,720f,0f);//可能坐标参数需要调整
		}
			
		
		GameObject CardBack = Instantiate(cardBack);
		float time = 0f;
		Vector3 de;
		if(id == 0)
			de = new Vector3(1200f,280f,0f);
		else
			de = new Vector3(1200f,480f,0f);//可能坐标参数需要调整
		CardBack.transform.position = de;
		CardBack.transform.SetParent(GameObject.Find("Canvas").transform);
		
		while(time<1.2f)
		{
			float x = de.x - (de.x - handPosition.x)*positionCurve.Evaluate(time/4);
            time += Time.deltaTime / duration;

            Vector3 local = CardBack.transform.position;
            local.x = x;
            CardBack.transform.position = local;

            yield return new WaitForFixedUpdate();
		}
		if(id == 0){
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
			while(time<3.3f)
			{
				Vector3 location = handPosition - positionCurve.Evaluate((time-0.3f)/3)*(handPosition - new Vector3(handPosition.x,de.y,0f));
				time += Time.deltaTime / duration;
				this.transform.position = location;
				yield return new WaitForFixedUpdate();
			}
		}
		if(id == 1){
				this.transform.SetParent(GameObject.Find("Canvas").transform);
				this.transform.position = CardBack.transform.position;
				
				for(int i=0; i<this.transform.childCount; i++){
					Color col;
					
					if(this.transform.GetChild(i).GetComponent<Image>()!=null)
					{
						col = this.transform.GetChild(i).GetComponent<Image>().color;
						col.a = 0f;
						this.transform.GetChild(i).GetComponent<Image>().color = col;
					}
					else
					{
						col = this.transform.GetChild(i).GetComponent<Text>().color;
						col.a = 0f;
						this.transform.GetChild(i).GetComponent<Text>().color = col;
					}
						
				}
				this.GetComponent<Image>().sprite = __cardBack;
				Destroy(CardBack);
				while(time<2.1f)
				{
					
					Vector3 location = handPosition - positionCurve.Evaluate((time-1.2f)/3+0.7f)*(handPosition - new Vector3(handPosition.x,de.y,0f));
					time += Time.deltaTime / duration;
					this.transform.position = location;
					yield return new WaitForFixedUpdate();
				}
		}
		if(id == 0)
			this.transform.SetParent(GameObject.Find("Canvas/Hand").transform);
		else
			this.transform.SetParent(GameObject.Find("Canvas/Hand_op").transform);
		GameObject.Find("GameCenter").GetComponent<GamePlayScene_GameCenterScript>().moveEnded=true;
	}
	
	public void cardAttack(GameObject start, GameObject end)
	{
		if(end.GetComponent<Draggerable>()!=null)
		{
			end.GetComponent<CardMove>().placeholder_2 = new GameObject();
			end.GetComponent<CardMove>().placeholder_2.transform.SetParent( end.transform.parent );
			LayoutElement le = end.GetComponent<CardMove>().placeholder_2.AddComponent<LayoutElement>();
			le.preferredWidth = end.GetComponent<LayoutElement>().preferredWidth;
			le.preferredHeight = end.GetComponent<LayoutElement>().preferredHeight;
			le.flexibleWidth = 0;
			le.flexibleHeight = 0;
			end.GetComponent<CardMove>().placeholder_2.transform.SetSiblingIndex(end.transform.GetSiblingIndex());
			end.GetComponent<LayoutElement>().ignoreLayout = true;
		}
		StartCoroutine(CardAttackMove(start,end));
	}
		
	IEnumerator CardAttackMove(GameObject start, GameObject end)
	{
		float time = 0f;//Debug.Log(start.ToString()+" 给我动起来 "+ end.ToString());
		Vector3 beginPosition;
		if(start.GetComponent<Draggerable>().placeholder != null)
			beginPosition = start.GetComponent<Draggerable>().placeholder.transform.position;
		else
			beginPosition = start.transform.position;
		GameObject placeholder = this.GetComponent<Draggerable>().placeholder;
		this.GetComponent<LayoutElement>().ignoreLayout = true;
		GameObject Ball = Instantiate(Damage_ball);
		int u = 0;
		while(time<2f)//移动攻击的动画
		{
			//placeholder = this.GetComponent<Draggerable>().placeholder;
			if(placeholder == null){
				//Debug.Log("mipa");
				placeholder = new GameObject();
				placeholder.transform.SetParent( this.transform.parent );
				LayoutElement le = placeholder.AddComponent<LayoutElement>();
				le.preferredWidth = this.GetComponent<LayoutElement>().preferredWidth;
				le.preferredHeight = this.GetComponent<LayoutElement>().preferredHeight;
				le.flexibleWidth = 0;
				le.flexibleHeight = 0;
		
				placeholder.transform.SetSiblingIndex(this.transform.GetSiblingIndex());
			}
			Vector3 location = beginPosition - (1f-attackPath.Evaluate(time/2))*(beginPosition - end.transform.position)*0.8f;
			time += Time.deltaTime / duration;
			this.transform.position = location;
			//Debug.Log("攻击" + this.transform.position);
			if(time > 0.8f && time < 1.3f)
			{
				if(u == 0)
				{
					Debug.Log("攻击特效！Mipa");
					Ball.transform.SetParent(GameObject.Find("Canvas").transform);
					Ball.transform.position = end.transform.position;
				}
				u++;
				if(time + Time.deltaTime / duration > 1.3f)
					Destroy(Ball);
			}
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
		while(time<3.6f)//旋转死亡的动画
		{
			float scale = deathCurve.Evaluate((time-2.6f)/0.6f);
			time += Time.deltaTime / duration;
			Vector3 rotation = this.transform.localEulerAngles; 
			rotation.z = 360f*scale; 
			if(this.GetComponent<Common_CardInfo>().cardInfo.hp <= end.GetComponent<Common_CardInfo>().cardInfo.atk)
				this.transform.localEulerAngles = rotation;
			Vector3 localScale = this.transform.localScale;
            localScale.x = 1f-scale;
			localScale.y = 1f-scale;
			if(this.GetComponent<Common_CardInfo>().cardInfo.hp <= end.GetComponent<Common_CardInfo>().cardInfo.atk)
				this.transform.localScale = localScale;
			if(end.GetComponent<Common_CardInfo>().cardInfo.hp <= this.GetComponent<Common_CardInfo>().cardInfo.atk)
			{
				end.transform.localScale = localScale;
				end.transform.localEulerAngles = rotation;
			}
			yield return new WaitForFixedUpdate();
		}
		this.GetComponent<LayoutElement>().ignoreLayout = false;
		
		if(placeholder != null)
			Destroy(placeholder);
		if(end.GetComponent<CardMove>().placeholder_2 != null)
		{
			Debug.Log("mipa");
			end.GetComponent<LayoutElement>().ignoreLayout = false;
			Destroy(end.GetComponent<CardMove>().placeholder_2);
		}
		OnAttackEvent(start,end);
		GameObject.Find("GameCenter").GetComponent<GamePlayScene_GameCenterScript>().moveEnded=true;
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
		//Debug.Log("BeforeMove");
		switch(type)
		{
			case 0:
				//Debug.Log("reallyMove?");
				input.CardUser.GetComponent<Common_CardInfo>().cardInfo.thisTrigger.thisResult.exec(input);//空动画
				break;
			case 1:
				StartCoroutine(battleMove(input,extra));
				break;
			case 2:
                //Debug.Log("BeforeSpell");
				StartCoroutine(SpellDamage(input,extra));
                //Debug.Log("afterSpell");
				break;
			case 3:
				StartCoroutine(Heal(input,extra));
				break;
		}
	}
		
	IEnumerator battleMove(Trigger.TriggerInput input, int damage)
	{
		if(input.CardTarget.GetComponent<Draggerable>()!=null)
		{
			input.CardTarget.GetComponent<CardMove>().placeholder_2 = new GameObject();
			input.CardTarget.GetComponent<CardMove>().placeholder_2.transform.SetParent( input.CardTarget.transform.parent );
			LayoutElement le = input.CardTarget.GetComponent<CardMove>().placeholder_2.AddComponent<LayoutElement>();
			le.preferredWidth = input.CardTarget.GetComponent<LayoutElement>().preferredWidth;
			le.preferredHeight = input.CardTarget.GetComponent<LayoutElement>().preferredHeight;
			le.flexibleWidth = 0;
			le.flexibleHeight = 0;
			input.CardTarget.GetComponent<CardMove>().placeholder_2.transform.SetSiblingIndex(input.CardTarget.transform.GetSiblingIndex());
			input.CardTarget.GetComponent<LayoutElement>().ignoreLayout = true;
		}
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
			c.a = deathCurve.Evaluate(time/0.8f);
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
		if(input.CardTarget.GetComponent<CardMove>().placeholder_2 != null)
		{
			Debug.Log("mipa");
			input.CardTarget.GetComponent<LayoutElement>().ignoreLayout = false;
			Destroy(input.CardTarget.GetComponent<CardMove>().placeholder_2);
		}
		input.CardUser.GetComponent<Common_CardInfo>().cardInfo.thisTrigger.thisResult.exec(input);
		GameObject.Find("GameCenter").GetComponent<GamePlayScene_GameCenterScript>().moveEnded=true;
	}
	IEnumerator SpellDamage(Trigger.TriggerInput input, int damage)
	{
		if(input.CardTarget.GetComponent<Draggerable>()!=null)
		{
			input.CardTarget.GetComponent<CardMove>().placeholder_2 = new GameObject();
			input.CardTarget.GetComponent<CardMove>().placeholder_2.transform.SetParent( input.CardTarget.transform.parent );
			LayoutElement le = input.CardTarget.GetComponent<CardMove>().placeholder_2.AddComponent<LayoutElement>();
			le.preferredWidth = input.CardTarget.GetComponent<LayoutElement>().preferredWidth;
			le.preferredHeight = input.CardTarget.GetComponent<LayoutElement>().preferredHeight;
			le.flexibleWidth = 0;
			le.flexibleHeight = 0;
			input.CardTarget.GetComponent<CardMove>().placeholder_2.transform.SetSiblingIndex(input.CardTarget.transform.GetSiblingIndex());
			input.CardTarget.GetComponent<LayoutElement>().ignoreLayout = true;
		}
		
		float time = 0f;
		GameObject Ball;
		if(GameObject.Find("Box_FBX/skill1").GetComponent<fireBall>().ball != null)
			Ball = GameObject.Find("Box_FBX/skill1").GetComponent<fireBall>().ball;
		else
		{
			Ball = Instantiate(Image_ball);
			Ball.transform.SetParent(GameObject.Find("Canvas").transform);
			if(input.CardUser.GetComponent<Common_CardInfo>().cardInfo.position >= 3)
			{
				if(input.CardUser.GetComponent<Common_CardInfo>().cardInfo.CardType == Common_CardInfo.BaseInfo.Hero)
					Ball.transform.position = new Vector3(680f,533f,0f);
				else
					Ball.transform.position = new Vector3(512f,533f,0f);
			
			}
			else{
				Ball.transform.position = new Vector3(512f,150f,0f);
			}
		}
		
			
		Vector3 ballStart = Ball.transform.position;
		while(time<1.8f)//旋转死亡的动画
		{
			float scale = deathCurve.Evaluate(time/1.8f);
			time += Time.deltaTime / duration;
			Vector3 location = ballStart - scale*(ballStart - input.CardTarget.transform.position);
			Ball.transform.position = location;
				
			yield return new WaitForFixedUpdate();
		}
		Destroy(Ball);
		if(GameObject.Find("Box_FBX/skill1").GetComponent<fireBall>().ball != null)
			Destroy(GameObject.Find("Box_FBX/skill1").GetComponent<fireBall>().ball);
		time = 0f;
		Vector3 delta = new Vector3(10f,0f,0f);
		Vector3 beginPosition = input.CardTarget.transform.position;
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
			c.a = deathCurve.Evaluate(time/0.8f);
			reduceBlood.GetComponent<Text>().color = c;
			yield return new WaitForFixedUpdate();
		}
		Destroy(reduceBlood);
		if(input.CardTarget.GetComponent<Common_CardInfo>().cardInfo.hp <= damage)
		while(time<1.8f)//旋转死亡的动画
		{
			float scale = deathCurve.Evaluate(time-0.8f);
			time += Time.deltaTime / duration;		
			Vector3 rotation = input.CardTarget.transform.localEulerAngles; 
			rotation.z = 360f*scale; 
			input.CardTarget.transform.localEulerAngles = rotation;		
			Vector3 localScale = input.CardTarget.transform.localScale;
			localScale.x = 1f-scale;
			localScale.y = 1f-scale;
			input.CardTarget.transform.localScale = localScale;
			yield return new WaitForFixedUpdate();
		}
		
		if(input.CardTarget.GetComponent<CardMove>().placeholder_2 != null)
		{
			Debug.Log("mipa");
			input.CardTarget.GetComponent<LayoutElement>().ignoreLayout = false;
			Destroy(input.CardTarget.GetComponent<CardMove>().placeholder_2);
		}
		input.CardUser.GetComponent<Common_CardInfo>().cardInfo.thisTrigger.thisResult.exec(input);
		input.CardUser.GetComponent<Common_CardInfo>().cardInfo.ifdelete=true;
		GameObject.Find("GameCenter").GetComponent<GamePlayScene_GameCenterScript>().moveEnded=true;
	}

	IEnumerator Heal(Trigger.TriggerInput input, int heal)
	{
		float time = 0f;
		GameObject Ball = Instantiate(Heal_ball);
		Ball.transform.SetParent(GameObject.Find("Canvas").transform);
		Ball.transform.position = input.CardTarget.transform.position;
		
		GameObject reduceBlood = Instantiate(textBlood);
		reduceBlood.transform.SetParent(GameObject.Find("Canvas").transform);
		reduceBlood.transform.position = input.CardTarget.transform.position;
		reduceBlood.GetComponent<Text>().text = ("+"+heal);
		Color c = reduceBlood.GetComponent<Text>().color;
		c.r = 0f;
		c.g = 255f;
		c.b = 255f;
		reduceBlood.GetComponent<Text>().color = c;
		
		while(time<0.48f)//旋转死亡的动画
		{
			time += Time.deltaTime / duration;
			reduceBlood.transform.position = reduceBlood.transform.position + new Vector3(0f,2f,0f);
			yield return new WaitForFixedUpdate();
		}
		Destroy(Ball);
		Destroy(reduceBlood);
		input.CardUser.GetComponent<Common_CardInfo>().cardInfo.thisTrigger.thisResult.exec(input);
		GameObject.Find("GameCenter").GetComponent<GamePlayScene_GameCenterScript>().moveEnded=true;
	}
}
