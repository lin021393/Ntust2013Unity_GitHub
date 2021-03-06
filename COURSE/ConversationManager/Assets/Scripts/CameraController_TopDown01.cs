/**
 * @file CameraController_TopDown01.cs
 * @author NDark
 * @date 20130601 . file started.
* @date 20130707 . fix an error of wrong up vector of camera.
* @date 20130713 . remove class method UpdateCameraPosNow()
 */
using UnityEngine;
using System.Collections;

public class CameraController_TopDown01 : MonoBehaviour 
{
	// the position of camera is always on the top of camera.
	public Vector3 m_DistanceVec = new Vector3( 0 , 10 , 0 ) ;
	public Camera m_CameraPtr = null ;
	public GameObject m_MainCharacterObjectPtr = null ;
	
	// Use this for initialization
	void Start () 
	{
		// 沒設定才要初始化
		if( null == m_CameraPtr )
			InitializeCameraPtr() ;
		
		// 沒設定才要初始化
		if( null == m_MainCharacterObjectPtr )
			InitializeMainCharacterObjectPtr() ;		
	}
	
	// Update is called once per frame
	void Update () 
	{
		TryUpdateCamera() ;
	}
	
	private void InitializeCameraPtr()
	{
		m_CameraPtr = Camera.main ;
		
		if( null == m_CameraPtr )
		{
			Debug.LogError( "CameraController_TopDown01:InitializeCameraPtr() null == m_CameraPtr" ) ;
		}
		else
		{
			Debug.Log( "CameraController_TopDown01:InitializeCameraPtr() end." ) ;
		}
	}
	
	private void InitializeMainCharacterObjectPtr()
	{
		m_MainCharacterObjectPtr = GameObject.FindGameObjectWithTag( "Player" ) ;
		
		if( null == m_MainCharacterObjectPtr )
		{
			Debug.LogError( "CameraController_TopDown01:InitializeMainCharacterObjectPtr() null == m_MainCharacterObjectPtr" ) ;
		}
		else
		{
			Debug.Log( "CameraController_TopDown01:InitializeMainCharacterObjectPtr() end." ) ;
		}
	}	
	
	private void TryUpdateCamera() 
	{
		if( null == m_CameraPtr || null == m_MainCharacterObjectPtr )
		{
			Debug.Log( "CameraController_TopDown01:TryUpdateCamera() null == m_CameraPtr || null == m_MainCharacterObjectPtr." ) ;
			return ;
		}
		
		m_CameraPtr.transform.position = m_MainCharacterObjectPtr.transform.position + m_DistanceVec ;
		m_CameraPtr.transform.LookAt( m_MainCharacterObjectPtr.transform.position , 
									  Vector3.forward  ) ;// the up vector is toward the up ( 0 , 0 , 1 ) of the map.
	}	
}
