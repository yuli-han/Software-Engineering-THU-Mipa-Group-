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
            if (lifetime >= 2f)
            {
                transform.position = lastPos;
                transform.localEulerAngles = lastAngle + new Vector3(0, 0, 180f);
                MoveType = 0;
                return;
            }
            transform.position = lastPos + new Vector3(0, 196f*lifetime-98f*lifetime*lifetime, 0);
            transform.localEulerAngles = lastAngle + new Vector3(0, 0, 7.5f * 360f * lifetime / 2.0f);
        }
        else if (MoveType == ToReturn)
        {
            lifetime = lifetime + Time.deltaTime;
            if (lifetime >= 2f)
            {
                transform.position = lastPos;
                transform.localEulerAngles = lastAngle + new Vector3(0, 0, 180f);
                MoveType = 0;
                return;
            }
            transform.localEulerAngles = lastAngle + new Vector3(0, 0, 180f * lifetime / 2.0f);
        }
	}
}
