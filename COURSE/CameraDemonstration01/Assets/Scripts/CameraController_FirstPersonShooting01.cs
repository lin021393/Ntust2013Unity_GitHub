/**
 * @file CameraController_FirstPersonShooting01.cs
 * @author NDark
 * @date 20130601 . file started.
* @date 20130713 . remove class method UpdateCameraPosNow()
 */
using UnityEngine;
using System.Collections;

public class CameraController_FirstPersonShooting01 : MonoBehaviour 
{
	public Camera m_CameraPtr = null ;
	public GameObject m_EyeObjectPtr = null ;

	// Use this for initialization
	void Start () 
	{
		// 沒設定才要初始化
		if( null == m_CameraPtr )
			InitializeCameraPtr() ;
		
		// 沒設定才要初始化
		if( null == m_EyeObjectPtr )
			InitializeMainCharacterEyeObjectPtr() ;
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
			Debug.LogError( "CameraController_FirstPersonShooting01:InitializeCameraPtr() null == m_CameraPtr" ) ;
		}
		else
		{
			Debug.Log( "CameraController_FirstPersonShooting01:InitializeCameraPtr() end." ) ;
		}
	}
	
	private void InitializeMainCharacterEyeObjectPtr()
	{
		
		GameObject mainCharacter = GameObject.FindGameObjectWithTag( "Player" ) ;
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
			Debug.LogError( "CameraController_FirstPersonShooting01:InitializeMainCharacterEyeObjectPtr() null == m_EyeObjectPtr" ) ;
		}
		else
		{
			Debug.Log( "CameraController_FirstPersonShooting01:InitializeMainCharacterEyeObjectPtr() end." ) ;
		}
	}
	
	private void TryUpdateCamera() 
	{
		if( null == m_CameraPtr || null == m_EyeObjectPtr )
		{
			return ;
		}
		
		m_CameraPtr.transform.position = m_EyeObjectPtr.transform.position ;
		m_CameraPtr.transform.rotation = m_EyeObjectPtr.transform.rotation ;
		
		
	}
}
