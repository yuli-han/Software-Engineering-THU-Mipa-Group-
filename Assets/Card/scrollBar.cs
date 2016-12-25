using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class scrollBar : MonoBehaviour {
	
	public int size;
	private int inner_size;
	// Use this for initialization
	void Start () {
		size = 0;
		inner_size = 0;
	}
	
	// Update is called once per frame
	public void test () {
		if(size >= inner_size+1)
		{
			this.GetComponent<Scrollbar>().value = 0;
			inner_size = size;
		}
		if(size<= inner_size-1)
		{
			this.GetComponent<Scrollbar>().value = min(1f,1.2f*this.GetComponent<Scrollbar>().value);
			inner_size = size;
		}
	}
	float min(float a, float b)
	{
		if(a<b)return a;
		else return b;
	}
}
