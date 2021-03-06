/**
@file CameraController_CameraField04.cs
@author NDark
@date 20130815 . file started.
*/
using UnityEngine;

public class CameraController_CameraField04 : MonoBehaviour 
{
	public Camera m_CameraPtr = null ;
	public GameObject m_MainCharacterPtr = null ;
	
	public Vector3 m_WorldUp = new Vector3( 0 , 1 , 0 ) ;

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
		
		// 依照 m_DistanceVec 更新 Camera
		TryUpdateCamera() ;
	}
	
	private void InitializeCameraPtr()
	{
		m_CameraPtr = Camera.mainCamera ;
		
		if( null == m_CameraPtr )
		{
			Debug.LogError( "CameraController_CameraField04:InitializeCameraPtr() null == m_CameraPtr" ) ;
		}
		else
		{
			Debug.Log( "CameraController_CameraField04:InitializeCameraPtr() end." ) ;
		}
	}
	
	private void InitializeMainCharacterObjectPtr()
	{
		
		m_MainCharacterPtr = GameObject.FindGameObjectWithTag( "Player" ) ;
		
		
		if( null == m_MainCharacterPtr )
		{
			Debug.LogError( "CameraController_CameraField04:InitializeMainCharacterObjectPtr() null == m_MainCharacterPtr" ) ;
		}
		else
		{
			Debug.Log( "CameraController_CameraField04:InitializeMainCharacterObjectPtr() end." ) ;
		}
	}
	
	private void TryUpdateCamera() 
	{
		if( null == m_CameraPtr || null == m_MainCharacterPtr )
		{
			return ;
		}
		
		m_CameraPtr.transform.LookAt( m_MainCharacterPtr.transform.position , 
									  m_WorldUp ) ;	
		
	}
}
