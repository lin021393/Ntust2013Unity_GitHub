/**
 * @file MainCharacterController06.cs
 * @author NDark
 * @date 20130622 . file started.
 */
using UnityEngine;
using System.Collections;

public class MainCharacterController06 : MonoBehaviour 
{
	public float m_MoveSpeed = 5.0f ;
	public float m_MouseMoveThreashold = 1.0f ;
	public float m_RotationSpeed = 1f ;
	public Vector3 m_MousePositionLast = Vector3.zero ;
	public Vector3 m_RotationUpVec = Vector3.up ;
	public GameObject m_MainCharacter = null ;
	public Camera m_CameraPtr = null ;
	
	

	// Use this for initialization
	void Start () 
	{
		m_MousePositionLast = Input.mousePosition ;	
		InitializeMainCharacterObjectPtr() ;
		
		// 沒設定才要初始化
		if( null == m_CameraPtr )
			InitializeCameraPtr() ;
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 rightVec = m_CameraPtr.transform.right ;
		rightVec.y = 0 ;
		Vector3 upVec = m_CameraPtr.transform.up ;
		upVec.y = 0 ;
		
		if( true == Input.GetKey( KeyCode.A )  )
		{
			this.gameObject.transform.Translate( -1 * rightVec * Time.deltaTime * m_MoveSpeed , Space.World ) ;
		}
		else if( true == Input.GetKey( KeyCode.D ) )
		{
			this.gameObject.transform.Translate( 1 * rightVec * Time.deltaTime * m_MoveSpeed , Space.World ) ;
		}
		
		m_MousePositionLast = Input.mousePosition ;
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
		m_CameraPtr = Camera.mainCamera ;
		
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
