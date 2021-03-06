/*
@file DestinationController01.cs
@author NDark
@date 20130808 file started.
*/
using UnityEngine;

public class DestinationController01 : MonoBehaviour 
{
	public GUITexture m_GUIDestination = null ;
	public bool m_IsShowDestination = false ;
	public Vector3 m_Destination3DPos = Vector3.zero ;
	public Vector3 m_Destination2DPos = Vector3.zero ;
	
	public void ShowDestination( bool _Enable , Vector3 _Pos3D ) 
	{
		EnableDestination( _Enable ) ;
		m_Destination3DPos = _Pos3D ;
		m_IsShowDestination = _Enable ;
	}
	
	// Use this for initialization
	void Start () 
	{
		if( null == m_GUIDestination )
		{
			InitGUIDestination() ; 				
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		UpdateDestination() ;
	}
	
	private void UpdateDestination()
	{
		if( null != m_GUIDestination && 
			true == m_IsShowDestination )
		{
			Vector3 viewportPos = 
				Camera.mainCamera.WorldToViewportPoint( m_Destination3DPos ) ;
			// Debug.Log( viewportPos ) ;
			m_Destination2DPos.Set( viewportPos.x , viewportPos.y , 0 ) ;
			m_GUIDestination.transform.position = m_Destination2DPos ;
		}
	}		
	
	private void InitGUIDestination()
	{
		GameObject guiDestination = GameObject.Find( "GUI_Destination" ) ;
		if( null != guiDestination )
		{
			m_GUIDestination = guiDestination.guiTexture ;
		}
	}
	
	private void EnableDestination( bool _Enable )
	{
		if( null == m_GUIDestination )
			return ;
		
		m_GUIDestination.enabled = _Enable ;
	}	
		
}
