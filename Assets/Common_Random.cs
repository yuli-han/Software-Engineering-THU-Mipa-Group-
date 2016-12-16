using System;

public class Common_Random
{
	static Random thisRandom;
	public static void init(){thisRandom=new Random();}
	public static void init(int input){thisRandom=new Random(input);}
	public static int random(int a,int b){return thisRandom.Next(a,b);}
}