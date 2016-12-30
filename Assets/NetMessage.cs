using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;

//NetMessage:网络数据传输用的接口。期待传输的数据类都应当以此为接口往复传输数据

public class NetMessage
{
	public int infoType;
	public int addint1;//两个附加int，用于额外信息。一般情况下，会对应
	public int addint2;
	public static readonly int Attack=1;
	public static readonly int DrawCard=2;
	public static readonly int Summon=3;
	public static readonly int SpellCard=4;
	public static readonly int TurnChange=5;
	public static readonly int TriggerExec=6;
    public static readonly int GameEnd = 7;

	public static NetMessage toMSG(int input,Trigger.TriggerInput inputTrigger)
	{
		NetMessage output=new NetMessage();
		output.infoType=input;
		if(inputTrigger==null)
		{
			output.addint1=0;
            output.addint2 = 0;
		}
		else
		{
			if(inputTrigger.CardUser==null)
                output.addint1 = 0;
            else output.addint1 = inputTrigger.CardUser.GetComponent<Common_CardInfo>().cardInfo.itemId;
			if(inputTrigger.CardTarget==null)
                output.addint2 = 0;
            else output.addint2 = inputTrigger.CardTarget.GetComponent<Common_CardInfo>().cardInfo.itemId;

		}
        return output;
	}

        public static NetMessage toMSG(string input)
        {
            NetMessage output = new NetMessage();
            output.infoType = int.Parse(input.Substring(0, input.IndexOf(" ")));
            input = input.Remove(0, input.IndexOf(" ")+1);
            output.addint1 = int.Parse(input.Substring(0, input.IndexOf(" ")));
            input = input.Remove(0, input.IndexOf(" ")+1);
            output.addint2 = int.Parse(input);

            return output;
        }
	public override string ToString()
	{
        	return ""+infoType+" "+addint1+" "+addint2;
	}
		

}