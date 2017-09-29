using UnityEngine;
using System.Collections;

public class MouseIcon : MonoBehaviour {
	public Texture2D cursorTexture; 
	public Texture2D DefaultCursor;
	
	private CursorMode cursorMode = CursorMode.Auto;  
	private Vector2 hotSpot = Vector2.zero;  
	void OnMouseDown()   
	{  
		Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
		//Debug.Log("鼠标已按下");
	}  
	void OnMouseUp()
	{
		Cursor.SetCursor(DefaultCursor, hotSpot, cursorMode);
	}
	
}
