/**
 * @file MainCharacterController03.cs
 * @author NDark
 * @date 20130601
 * @date 20130713 
 * . remove class member m_MousePositionLast.
 * . remove class member m_MouseMoveThreashold
 */
using UnityEngine;
using System.Collections;

public class MainCharacterController03 : MonoBehaviour 
{
	public float m_MoveSpeed = 5.0f ;
	
	
	public GameObject m_MainCharacter = null ;
	public Camera m_CameraPtr = null ;
	
	

	// Use this for initialization
	void Start () 
	{
		if( null == m_MainCharacter )
			InitializeMainCharacterObjectPtr() ;
		
		// 沒設定才要初始化
		if( null == m_CameraPtr )
			InitializeCameraPtr() ;
	}
	
	// Update is called once per frame
	void Update () 
	{
		// the right is the right of camera
		Vector3 rightVec = m_CameraPtr.transform.right ;
		rightVec.z = 0 ;// keep object at the plane
		
		// the up is the forward of camera
		Vector3 fowardVec = m_CameraPtr.transform.up ;
		fowardVec.z = 0 ;// keep object at the plane
		
		Transform thisTransform = this.gameObject.transform ;
		if( true == Input.GetKey( KeyCode.A )  )
		{
			thisTransform.Translate( -1 * rightVec * Time.deltaTime * m_MoveSpeed , 
									 Space.World ) ;
		}
		else if( true == Input.GetKey( KeyCode.D ) )
		{
			thisTransform.Translate( 1 * rightVec * Time.deltaTime * m_MoveSpeed , 
									 Space.World ) ;
		}
		else if( true == Input.GetKey( KeyCode.S ) )
		{
			thisTransform.Translate( -1 * fowardVec * Time.deltaTime * m_MoveSpeed , 
									 Space.World ) ;
		}
		else if( true == Input.GetKey( KeyCode.W ) )
		{
			this.gameObject.transform.Translate( 1 * fowardVec * Time.deltaTime * m_MoveSpeed , 
												 Space.World ) ;
		}
	}
	
	private void InitializeMainCharacterObjectPtr()
	{
		GameObject m_MainCharacter = GameObject.FindGameObjectWithTag( "Player" ) ;
		
		if( null == m_MainCharacter )
		{
			Debug.LogError( "MainCharacterController03:InitializeMainCharacterObjectPtr() null == m_MainCharacter" ) ;
		}
		else
		{
			Debug.Log( "MainCharacterController03:InitializeMainCharacterObjectPtr() end." ) ;
		}
	}	
	
	private void InitializeCameraPtr()
	{
		m_CameraPtr = Camera.main ;
		
		if( null == m_CameraPtr )
		{
			Debug.LogError( "CameraController_FirstPersonShooting01:InitializeCameraPtr() null == m_CameraPtr" ) ;
		}
		else
		{
			Debug.Log( "CameraController_FirstPersonShooting01:InitializeCameraPtr() end." ) ;
		}
	}	
}
