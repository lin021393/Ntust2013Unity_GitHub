/**
 * @file CameraController_ThirdPersonShooting02.cs
 * @author NDark
 * @date 20130707 . file started.
 * @date 20130713 . remove class method UpdateCameraPosNow()
 */
using UnityEngine;
using System.Collections;

public class CameraController_ThirdPersonShooting02 : MonoBehaviour 
{
	public Vector3 m_DistanceVec = new Vector3( 0 , 2 , -10 ) ;
	public Camera m_CameraPtr = null ;
	public GameObject m_MainCharacterPtr = null ;
	public Vector3 m_WorldUp = new Vector3( 0 , 1 , 0 ) ;
	public float m_CameraFollowSpeed = 1.0f ;


	// Use this for initialization
	void Start () 
	{
		// 沒設定才要初始化
		if( null == m_CameraPtr )
			InitializeCameraPtr() ;
		
		// 沒設定才要初始化
		if( null == m_MainCharacterPtr )
			InitializeMainCharacterObjectPtr() ;
	}
	
	// Update is called once per frame
	void Update () 
	{
		TryUpdateCamera() ;
	}
	
	private void InitializeCameraPtr()
	{
		m_CameraPtr = Camera.mainCamera ;
		
		if( null == m_CameraPtr )
		{
			Debug.LogError( "CameraController_ThirdPersonShooting02:InitializeCameraPtr() null == m_CameraPtr" ) ;
		}
		else
		{
			Debug.Log( "CameraController_ThirdPersonShooting02:InitializeCameraPtr() end." ) ;
		}
	}
	
	private void InitializeMainCharacterObjectPtr()
	{
		
		m_MainCharacterPtr = GameObject.FindGameObjectWithTag( "Player" ) ;
		
		
		if( null == m_MainCharacterPtr )
		{
			Debug.LogError( "CameraController_ThirdPersonShooting02:InitializeMainCharacterObjectPtr() null == m_MainCharacterPtr" ) ;
		}
		else
		{
			Debug.Log( "CameraController_ThirdPersonShooting02:InitializeMainCharacterObjectPtr() end." ) ;
		}
	}
	
	private void TryUpdateCamera() 
	{
		if( null == m_CameraPtr || null == m_MainCharacterPtr )
		{
			return ;
		}
		Vector3 targetPosWorld = m_MainCharacterPtr.transform.TransformPoint( m_DistanceVec ) ;
		
		m_CameraPtr.transform.position = Vector3.Lerp( m_CameraPtr.transform.position ,
													   targetPosWorld , 
													   m_CameraFollowSpeed * Time.deltaTime ) ;
		m_CameraPtr.transform.LookAt( m_MainCharacterPtr.transform.position , m_WorldUp ) ;	
	}
}


