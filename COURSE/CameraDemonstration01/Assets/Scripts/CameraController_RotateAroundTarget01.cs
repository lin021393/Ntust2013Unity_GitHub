/*
 * @file CameraController_RotateAroundTarget01.cs
 * @author NDark
 * @date 20130601 . file started.
 */
using UnityEngine;
using System.Collections;

public class CameraController_RotateAroundTarget01 : MonoBehaviour 
{
	public float rotateSpeed = 30.0f ;
	GameObject m_MainCharacter = null ;
	public Camera m_CameraPtr = null ;
	// Use this for initialization
	void Start () 
	{
		m_MainCharacter = GameObject.FindGameObjectWithTag( "Player" ) ;
		// 沒設定才要初始化
		if( null == m_CameraPtr )
			InitializeCameraPtr() ;	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if( null == m_MainCharacter )
			return ;
		
		Vector3 target = m_MainCharacter.transform.position ;
		Vector3 toCamera = m_CameraPtr.transform.position - target ;
		toCamera = Quaternion.AngleAxis( rotateSpeed * Time.deltaTime , Vector3.up ) * toCamera ;
		Vector3 newPos = target + toCamera ;
		m_CameraPtr.transform.position = newPos ;
		m_CameraPtr.transform.LookAt( target , Vector3.up ) ;
	}
	
	private void InitializeCameraPtr()
	{
		m_CameraPtr = Camera.mainCamera ;
		
		if( null == m_CameraPtr )
		{
			Debug.LogError( "CameraController_CameraRoutes01:InitializeCameraPtr() null == m_CameraPtr" ) ;
		}
		else
		{
			Debug.Log( "CameraController_CameraRoutes01:InitializeCameraPtr() end." ) ;
		}
	}	
}
