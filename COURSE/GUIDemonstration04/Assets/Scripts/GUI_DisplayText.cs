/*
@file GUI_DisplayText.cs
@author NDark
@date 20130814 file started.
*/
using UnityEngine;

public class GUI_DisplayText : MonoBehaviour 
{
	public string m_DisplayString = "中文測試" ;

	// Use this for initialization
	void Start () 
	{
		GUIText guiText = this.gameObject.guiText ;
		if( null != guiText )
		{
			guiText.text = m_DisplayString ;
		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
