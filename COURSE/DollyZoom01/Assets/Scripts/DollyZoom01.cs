/*
@file DollyZoom01.cs
@author NDark
@date 20130815 file started and derived from http://docs.unity3d.com/Documentation/Manual/DollyZoom.html
*/
using UnityEngine;

public class DollyZoom01 : MonoBehaviour 
{
	public GameObject m_TargetObject = null ;
	private Transform m_TargetTransform = null ;
	
	public Camera m_Camera = null ;
	private Transform m_CameraTransform = null ;
	public float m_InitHeightOfViewAtDist = 0.0f ;
	public bool m_Enable = false ;
	public float m_MoveSpeed = 5.0f ;
	
	// Use this for initialization
	void Start () 
	{
		if( null == m_Camera )
		{
			m_Camera = Camera.mainCamera ;
		}
		m_CameraTransform = m_Camera.gameObject.transform ;
		
		m_TargetTransform = m_TargetObject.transform ;
		
		SetupDollyZoom() ;	
	}
	
	// Update is called once per frame
	void Update () 
	{
		float currentDistance = 0.0f ;
		if( true == m_Enable )
		{
			currentDistance = 
				Vector3.Distance( m_CameraTransform.position , 
								  m_TargetTransform.position ) ;
			

			
			m_Camera.fieldOfView = 
				FOVForHeightAndDistance( m_InitHeightOfViewAtDist ,
										 currentDistance ) ;
			// Debug.Log( "camera.fieldOfView=" + camera.fieldOfView ) ;
		}
		

	
		float moveForward = Input.GetAxis("Vertical") ;
		if( 0.0f != moveForward )
		{
			if( currentDistance < 1.0f &&
				moveForward > 0.0f  )
				return ;		
			
			m_CameraTransform.Translate( 
				moveForward * m_CameraTransform.forward * Time.deltaTime * m_MoveSpeed , 
				Space.World ) ;
		}
	}
	
	private void SetupDollyZoom()
	{
		float currentDistance = 
			Vector3.Distance( m_CameraTransform.position , 
							  m_TargetTransform.position ) ;
		
		
		m_InitHeightOfViewAtDist = FrustumHeightAtDistance( currentDistance ) ;
		// Debug.Log( "initHeightAtDist=" + initHeightAtDist ) ;
		m_Enable = true ;
	}
	
	private void StopDollyZoom()
	{
		m_Enable = false ;
	}
	
	private float FrustumHeightAtDistance( float _Distance )
	{
		float ret = 2.0f * _Distance * Mathf.Tan( m_Camera.fieldOfView * 0.5f * Mathf.Deg2Rad ) ;
		return ret ;
	}
	
	private float FOVForHeightAndDistance( float _Height , float _Distance )
	{
		float ret = 2.0f * Mathf.Atan( _Height * 0.5f / _Distance ) * Mathf.Rad2Deg ;
		return ret ;
	}	
}
