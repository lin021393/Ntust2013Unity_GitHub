/*
 * @file ObjectTrackball01.cs
 * @author NDark
 * @date 20130719 . file started.
 */
using UnityEngine;
using System.Collections;

public class ObjectTrackball01 : MonoBehaviour 
{
	public GameObject m_TestBox = null ;
	public Camera m_Camera = null ;
	public float m_RotateSpeed = 600 ;
	
	// Use this for initialization
	void Start () 
	{
		if( null == m_TestBox )
			InitializeTestBox() ;
		if( null == m_Camera )
			InitializeCamera() ;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if( null == m_TestBox )
			return ;
		if( true == Input.GetMouseButton( 0 ) ) 
		{
			float ydiff = Input.GetAxis( "Mouse Y" ) ;
			
			if( Mathf.Abs( ydiff ) > 0 )
			{
				m_TestBox.transform.Rotate( m_Camera.transform.right , 
											ydiff * m_RotateSpeed * Time.deltaTime , 
											Space.World ) ;
			}
			
			
			float xdiff = Input.GetAxis( "Mouse X" ) ;
			
			if( Mathf.Abs( xdiff ) > 0 )
			{
				
				m_TestBox.transform.Rotate( m_Camera.transform.up , 
											-1 * xdiff * m_RotateSpeed * Time.deltaTime , 
											Space.World ) ;
			}
		}		
	}
	
	private void InitializeTestBox()
	{
		m_TestBox = GameObject.Find( "TestBox" ) ;
		if( null == m_TestBox )
		{
			Debug.LogError( "ObjectTrackball01 :: InitializeTestBox(), null == m_TestBox" ) ;
		}
		else
		{
			Debug.Log( "ObjectTrackball01 :: InitializeTestBox(), end" ) ;
		}
	}
			
	private void InitializeCamera()
	{
		m_Camera = Camera.mainCamera ;
		if( null == m_Camera )
		{
			Debug.LogError( "ObjectTrackball01 :: InitializeCamera(), null == m_Camera" ) ;
		}
		else
		{
			Debug.Log( "ObjectTrackball01 :: InitializeCamera(), end" ) ;
		}
	}			
}
