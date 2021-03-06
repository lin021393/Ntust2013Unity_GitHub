/*
@file SelectionSystem01.cs
@author NDark
@date 20130804 file started.
*/
using UnityEngine;

public class SelectionSystem01 : MonoBehaviour 
{
	public GUITexture m_GUISelection = null ;
	public bool m_IsSelect = false ;
	public GameObject m_SelectObject = null ;
	public Vector3 m_SelectionPos = Vector3.zero ;
	
	public void Select( bool _Select , GameObject _TargetObject ) 
	{
		// case 0 無到有
		if( _Select != m_IsSelect && false == m_IsSelect )
		{
			EnableSelect( true ) ;
		}
		else if( _Select != m_IsSelect && true == m_IsSelect )
		{
			// 取消點選
			EnableSelect( false ) ;
		}
		else
		{
			// 改變對象
		}
		m_IsSelect = _Select ;
		m_SelectObject = _TargetObject ;
	}
	
	// Use this for initialization
	void Start () 
	{
		if( null == m_GUISelection )
			InitGUISelection() ; 
	}
	
	// Update is called once per frame
	void Update () 
	{
		UpdateSelectionPos() ;	
	}
	
	private void UpdateSelectionPos()
	{
		float height = 0.0f ;
		if( null != m_GUISelection && 
			null != m_SelectObject )
		{
			Vector3 m_ViewportPos = 
				Camera.mainCamera.WorldToViewportPoint( 
					m_SelectObject.transform.position + 
					m_SelectObject.transform.up * height ) ;
			
			// Debug.Log( m_ViewportPos ) ;
			m_SelectionPos.Set( m_ViewportPos.x , m_ViewportPos.y , 0 ) ;
			m_GUISelection.transform.position = m_SelectionPos ;
		}
	}
	
	private void InitGUISelection()
	{
		GameObject guiSelectionObj = GameObject.Find( "GUI_Selection" ) ;
		if( null != guiSelectionObj )
		{
			m_GUISelection = guiSelectionObj.guiTexture ;
		}
	}
	
	private void EnableSelect( bool _Enable )
	{
		if( null == m_GUISelection )
			return ;
		
		m_GUISelection.enabled = _Enable ;
	}	
}
