/*
@file UpdateCursorPos01.cs
@author NDark
@date 20130808 file started.
*/
using UnityEngine;

public class UpdateCursorPos01 : MonoBehaviour 
{
	public GUITexture m_GUICursor = null ;
	public bool m_IsShowCursor = false ;
	public Vector3 m_CursorPos = Vector3.zero ;
	
	public void ShowCursor( bool _Select ) 
	{
		EnableCursor( _Select ) ;
		m_IsShowCursor = _Select ;
	}
	
	
	// Use this for initialization
	void Start () 
	{
		if( null == m_GUICursor )
		{
			InitGUICursor() ;
			ShowCursor( true ) ;
		}		
	}
	
	// Update is called once per frame
	void Update () 
	{
		UpdateCursorPos() ;
	}
	
	
	private void InitGUICursor()
	{
		GameObject guiCursorObj = GameObject.Find( "GUI_Cursor" ) ;
		if( null != guiCursorObj )
		{
			m_GUICursor = guiCursorObj.guiTexture ;
		}
	}
	
	
	private void EnableCursor( bool _Enable )
	{
		if( null == m_GUICursor )
			return ;
		
		m_GUICursor.enabled = _Enable ;
	}	
	
	private void UpdateCursorPos()
	{
		if( null != m_GUICursor && 
			true == m_IsShowCursor )
		{
			Vector3 viewportPos = 
				Camera.mainCamera.ScreenToViewportPoint( Input.mousePosition ) ;
			
			// Debug.Log( Input.mousePosition ) ;
			m_CursorPos.Set( viewportPos.x , viewportPos.y , 0 ) ;
			m_GUICursor.transform.position = m_CursorPos ;			
		}
	}
		
}
