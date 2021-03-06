/*
@file MouseOver_ShowInformation.cs
@author NDark
@date 20130825 file started.
*/
using UnityEngine;

public class MouseOver_ShowInformation : MonoBehaviour 
{
	public bool m_Active = false ;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnMouseEnter()
	{
		m_Active = true ;
	}

	void OnMouseExit()
	{
		m_Active = false ;
	}
		
	public Rect m_WinRec = new Rect( 686 , 189 , 300 , 500 ) ;
	public Rect m_PotraitRec = new Rect( 154 , 33 , 128 , 128 ) ;
	public Texture m_Texture = null;
	void OnGUI()
	{
		if( true == m_Active )
		{
			GUI.Window( 0 , m_WinRec , DrawWindow , "" ) ;
		}
	}
	
	void DrawWindow( int id )
	{
		GUILayout.BeginHorizontal() ;
		
		GUILayout.BeginVertical() ;
		GUILayout.Label ( "Name: " + this.gameObject.name ) ;
		GUILayout.Label ( "Country:" ) ;
		GUILayout.Label ( "Height:" ) ;
		GUILayout.Label ( "Weight:" ) ;
		GUILayout.EndVertical() ;
		
		if( null != m_Texture )
		{
			GUI.DrawTexture( m_PotraitRec , m_Texture ) ;
		}
			
		GUILayout.EndHorizontal() ;
		
	}
}
