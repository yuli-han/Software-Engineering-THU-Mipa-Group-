using UnityEngine;
using System.Collections;

public class GamePlayScene_SkillIconMove : MonoBehaviour {

    int MoveType;
    float lifetime;
	
	
    Vector3 lastPos;
    Vector3 lastAngle;

    public static readonly int ToUse = 1;//使用技能
    public static readonly int ToReturn = 2;//恢复原状

    public void StartMove(int movetype)//调用此函数，则开始旋转
    {
        MoveType = movetype;
        lifetime = 0f;
        lastPos = transform.position;
        lastAngle = transform.localEulerAngles;
        
    }
	// Update is called once per frame
	void Update () {
        if (MoveType == ToUse)
        {
            lifetime = lifetime + Time.deltaTime;
            if (lifetime >= 0.6f)
            {
                transform.position = lastPos+new Vector3(0,0.6f,0);
                transform.localEulerAngles = lastAngle + new Vector3(180f, 0, 0);
                MoveType = 0;
                return;
            }
            transform.position = lastPos + new Vector3(0, 7f*lifetime-10f*lifetime*lifetime, 0);
            transform.localEulerAngles = lastAngle + new Vector3(1.5f * 360f * lifetime / 0.6f,0,0);
        }
        else if (MoveType == ToReturn)
        {
            lifetime = lifetime + Time.deltaTime;
            if (lifetime >= 0.6f)
            {
                transform.position = lastPos-new Vector3(0,0.6f,0);
                transform.localEulerAngles = lastAngle + new Vector3(180f,0,0);
                MoveType = 0;
                return;
            }
            transform.position = lastPos + new Vector3(0, -1f*lifetime, 0);
            transform.localEulerAngles = lastAngle + new Vector3(180f * lifetime / 0.6f,0,0);
        }
	}
}
