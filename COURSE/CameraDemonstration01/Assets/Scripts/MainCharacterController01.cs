/**
 * @file MainCharacterController01.cs
 * @author NDark
 * @date 20130601
 */
using UnityEngine;
using System.Collections;

public class MainCharacterController01 : MonoBehaviour 
{
	public float m_MoveSpeed = 5.0f ;
	public float m_MouseMoveThreashold = 1.0f ;
	public float m_RotationSpeed = 1f ;
	public Vector3 m_MousePositionLast = Vector3.zero ;
	public GameObject m_EyeObjectPtr = null ;
	public CameraController_FirstPersonShooting01 m_CameraController01 = null ;
	
	

	// Use this for initialization
	void Start () 
	{
		m_MousePositionLast = Input.mousePosition ;	
		InitializeMainCharacterEyeObjectPtr() ;
		InitializeCameraControllerPtr() ;
	}
	
	// Update is called once per frame
	void Update () 
	{
		bool m_Update = false ;
		if( true == Input.GetKey( KeyCode.A )  )
		{
			this.gameObject.transform.Translate( -1 * m_MoveSpeed * Time.deltaTime , 0 , 0 , Space.Self ) ;
			m_Update = true ;
		}
		else if( true == Input.GetKey( KeyCode.D ) )
		{
			this.gameObject.transform.Translate( 1 * m_MoveSpeed * Time.deltaTime , 0 , 0 , Space.Self ) ;
			m_Update = true ;
		}
		else if( true == Input.GetKey( KeyCode.S ) )
		{
			this.gameObject.transform.Translate( 0 , 0 , -1 * m_MoveSpeed * Time.deltaTime , Space.Self ) ;
			m_Update = true ;
		}
		else if( true == Input.GetKey( KeyCode.W ) )
		{
			this.gameObject.transform.Translate( 0 , 0 , 1 * m_MoveSpeed * Time.deltaTime , Space.Self ) ;
			m_Update = true ;
		}
		
		float mouseDiff = Input.GetAxis( "Mouse X" ) ;
		if( Mathf.Abs( mouseDiff ) > m_MouseMoveThreashold ) 
		{
			this.gameObject.transform.RotateAroundLocal( Vector3.up , mouseDiff * m_RotationSpeed * Time.deltaTime ) ;
			m_Update = true ;
		}
		
		mouseDiff = Input.GetAxis( "Mouse Y" ) ;
		if( Mathf.Abs( mouseDiff ) > m_MouseMoveThreashold )
		{
			m_EyeObjectPtr.transform.RotateAroundLocal( Vector3.right , -1 * mouseDiff * m_RotationSpeed * Time.deltaTime ) ;			
			m_Update = true ;			
		}		
		
		if( true == m_Update )
		{
			if( null != m_CameraController01 )
			{
				m_CameraController01.UpdateCameraPosNow() ;
			}
		}
		
		m_MousePositionLast = Input.mousePosition ;
	}
	
	private void InitializeCameraControllerPtr()
	{
		m_CameraController01 = this.gameObject.GetComponent<CameraController_FirstPersonShooting01>() ;
		
		if( null == m_CameraController01 )
		{
			Debug.LogError( "MainCharacterController01:InitializeCameraControllerPtr() null == m_CameraController01" ) ;
		}
		else
		{
			Debug.Log( "MainCharacterController01:InitializeCameraControllerPtr() end." ) ;
		}		
	}
	
	private void InitializeMainCharacterEyeObjectPtr()
	{
		
		GameObject mainCharacter = this.gameObject ;
		if( null != mainCharacter )
		{
			Transform trans = mainCharacter.transform.FindChild( "MainCharacterEye" ) ;
			if( null != trans )
			{
				m_EyeObjectPtr = trans.gameObject ;
			}
		}
		
		if( null == m_EyeObjectPtr )
		{
			Debug.LogError( "MainCharacterController01:InitializeMainCharacterEyeObjectPtr() null == m_EyeObjectPtr" ) ;
		}
		else
		{
			Debug.Log( "MainCharacterController01:InitializeMainCharacterEyeObjectPtr() end." ) ;
		}
	}	
}
