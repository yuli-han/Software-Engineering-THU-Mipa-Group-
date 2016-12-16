using System.Random;

public class Common_Random
{
	static Random thisRandom;
	public static init(){thisRandom=new Random();}
	public static init(int input){thisRandom=new Random(input);}
	public static int random(int a,int b){return thisRandom.next(a,b);}
}