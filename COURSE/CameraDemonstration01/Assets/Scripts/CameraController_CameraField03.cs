/**
 * @file CameraController_CameraField03.cs
 * @author NDark
 * @date 20130602 . file started.
* @date 20130712 . remove class method UpdateCameraPosNow()
 */
using UnityEngine;
using System.Collections;

public class CameraController_CameraField03 : MonoBehaviour 
{
	public Camera m_CameraPtr = null ;
	public GameObject m_MainCharacterPtr = null ;
	public CameraFieldManager02 m_CameraFieldManagerPtr = null ;

	public GameObject m_CameraPose = null ;	
	public Vector3 m_WorldUp = new Vector3( 0 , 1 , 0 ) ;
	public float m_InterpolateSpeed = 10.0f ;


	// Use this for initialization
	void Start () 
	{
		// 沒設定才要初始化
		if( null == m_CameraPtr )
			InitializeCameraPtr() ;
		
		// 沒設定才要初始化
		if( null == m_MainCharacterPtr )
			InitializeMainCharacterObjectPtr() ;
		
		// 取得攝影機場的管理器
		if( null == m_CameraFieldManagerPtr )
			InitializeCameraFieldManagerPtr() ;
	}
	
	// Update is called once per frame
	void Update () 
	{
		// 取得 m_DistanceVec
		DecideTheTargetPositionOfCamera() ;
		
		// 依照 m_DistanceVec 更新 Camera
		TryUpdateCamera() ;
	}

	private void DecideTheTargetPositionOfCamera()
	{
		if( null != m_CameraFieldManagerPtr )
		{
			// 傳入目前主角的位置
			m_CameraPose = m_CameraFieldManagerPtr.GetLightFieldPoseGameObject( this.gameObject.transform.position ) ;
		}
	}
	
	private void TryUpdateCamera() 
	{
		if( null == m_CameraPtr || null == m_MainCharacterPtr )
		{
			return ;
		}
		
		
		// 期待的目標位置 m_CameraPose
		
		// 內差得到下一個畫格的姿勢
		Vector3 actuallyMoveTarget = Vector3.Lerp( m_CameraPtr.transform.position , 
												   m_CameraPose.transform.position , 
												   m_InterpolateSpeed * Time.deltaTime ) ;
		Quaternion actuallyRotation = Quaternion.Lerp( m_CameraPtr.transform.rotation ,
												   m_CameraPose.transform.rotation , 
												   m_InterpolateSpeed * Time.deltaTime ) ;
		
		m_CameraPtr.transform.position = actuallyMoveTarget ;
		m_CameraPtr.transform.rotation = actuallyRotation ;
	}
	
	
	private void InitializeCameraPtr()
	{
		m_CameraPtr = Camera.mainCamera ;
		
		if( null == m_CameraPtr )
		{
			Debug.LogError( "CameraController_CameraField03:InitializeCameraPtr() null == m_CameraPtr" ) ;
		}
		else
		{
			Debug.Log( "CameraController_CameraField03:InitializeCameraPtr() end." ) ;
		}
	}
	
	private void InitializeMainCharacterObjectPtr()
	{
		
		m_MainCharacterPtr = GameObject.FindGameObjectWithTag( "Player" ) ;
		
		
		if( null == m_MainCharacterPtr )
		{
			Debug.LogError( "CameraController_CameraField03:InitializeMainCharacterObjectPtr() null == m_MainCharacterPtr" ) ;
		}
		else
		{
			Debug.Log( "CameraController_CameraField03:InitializeMainCharacterObjectPtr() end." ) ;
		}
	}
	
	private void InitializeCameraFieldManagerPtr()
	{
		m_CameraFieldManagerPtr = this.gameObject.GetComponent<CameraFieldManager02>() ;
		if( null == m_CameraFieldManagerPtr )
		{
			Debug.LogError( "CameraController_CameraField03:InitializeCameraFieldManagerPtr() null == m_CameraFieldManagerPtr" ) ;
		}
		else
		{
			Debug.Log( "CameraController_CameraField03:InitializeCameraFieldManagerPtr() end." ) ;
		}	
	}
		
}
