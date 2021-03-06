/**
 * @file MainCharacterController07.cs
 * @author NDark
 * @date 20130612
 */
using UnityEngine;
using System.Collections;

public class MainCharacterController07 : MonoBehaviour 
{
	public float m_MoveSpeed = 5.0f ;
	public float m_MouseMoveThreashold = 1.0f ;
	public float m_RotationSpeed = 1f ;
	public Vector3 m_RotationUpVec = Vector3.up ;
	public Camera m_CameraPtr = null ;
	public bool m_EnableDetect = true ;
	

	// Use this for initialization
	void Start () 
	{
		// 沒設定才要初始化
		if( null == m_CameraPtr )
			InitializeCameraPtr() ;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if( true == m_EnableDetect )
			DetectInput() ;
		
	}
	
	private void DetectInput()
	{
		Vector3 rightVec = m_CameraPtr.transform.right ;
		rightVec.y = 0 ;
		Vector3 upVec = m_CameraPtr.transform.up ;
		upVec.y = 0 ;
		
		if( true == Input.GetKey( KeyCode.A )  )
		{
			this.gameObject.transform.Translate( -1 * rightVec * Time.deltaTime * m_MoveSpeed , Space.World ) ;
			this.gameObject.transform.rotation = Quaternion.LookRotation( -1 * rightVec , m_RotationUpVec ) ;		
		}
		else if( true == Input.GetKey( KeyCode.D ) )
		{
			this.gameObject.transform.Translate( 1 * rightVec * Time.deltaTime * m_MoveSpeed , Space.World ) ;
			this.gameObject.transform.rotation = Quaternion.LookRotation( 1 * rightVec , m_RotationUpVec ) ;		
		}
		else if( true == Input.GetKey( KeyCode.S ) )
		{
			this.gameObject.transform.Translate( -1 * upVec * Time.deltaTime * m_MoveSpeed , Space.World ) ;
			this.gameObject.transform.rotation = Quaternion.LookRotation( -1 * upVec , m_RotationUpVec ) ;		
		}
		else if( true == Input.GetKey( KeyCode.W ) )
		{
			this.gameObject.transform.Translate(  1 * upVec * Time.deltaTime * m_MoveSpeed , Space.World ) ;
			this.gameObject.transform.rotation = Quaternion.LookRotation( 1 * upVec , m_RotationUpVec ) ;		
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
