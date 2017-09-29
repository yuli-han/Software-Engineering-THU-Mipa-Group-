using System;

public class Common_Random
{
    public static bool inited = false;
	static Random thisRandom;
    public static void init() { thisRandom = new Random(); inited = true; }
    public static void init(int input) { thisRandom = new Random(input); inited = true; }
	public static int random(int a,int b){return thisRandom.Next(a,b);}
}