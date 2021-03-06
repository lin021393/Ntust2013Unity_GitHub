/**
@file MinimapCameraController01.cs
@author NDark
@date 20130609 by NDark
*/
using UnityEngine;

public class MinimapCameraController01 : MonoBehaviour 
{
	public Camera m_MainCamera = null ;
	public Camera m_MinimapCamera = null ;
	
	// Use this for initialization
	void Start () 
	{
		InitializeMainCamera() ;
		InitializeMinimapCamera() ;
	}
	
	// Update is called once per frame
	void Update () 
	{
		UpdateMinimapCameraFromMainCamera() ;
	}
	
	private void InitializeMainCamera()
	{
		m_MainCamera = Camera.mainCamera ; 
		if( null == m_MainCamera )
		{
			Debug.LogError( "MinimapCameraController01:InitializeMainCamera() null == m_MainCamera" ) ;
		}		
		else
		{
			Debug.Log( "MinimapCameraController01:InitializeMainCamera() end" ) ;
		}			
	}
	
	private void InitializeMinimapCamera()
	{
		GameObject minimapCameraObj = GameObject.Find( "MiniMapCameraObj" ) ; 
		m_MinimapCamera = minimapCameraObj.camera ;
		if( null == m_MinimapCamera )
		{
			Debug.LogError( "MinimapCameraController01:InitializeMinimapCamera() null == m_MinimapCamera" ) ;
		}		
		else
		{
			Debug.Log( "MinimapCameraController01:InitializeMinimapCamera() end" ) ;
		}			
	}
	
	private void UpdateMinimapCameraFromMainCamera()
	{
		if( null == m_MainCamera || 
			null == m_MinimapCamera )
		{
			return ;
		}
		
		float originalY = m_MinimapCamera.transform.position.y ;
		m_MinimapCamera.transform.position = new Vector3( 
			m_MainCamera.transform.position.x ,
			originalY ,// keep original height
			m_MainCamera.transform.position.z ) ;
		
	}
}
