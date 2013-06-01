/**
 * @file CameraController_CameraRoutes01.cs
 * @author NDark
 * @date 20130601 . file started.
 */
using UnityEngine;
using System.Collections;

public enum CameraRoutesState
{
	UnActive = 0 ,
	MoveToTarget ,
	CheckNextTarget ,
	Pause ,	
	Closed ,
}

public class CameraController_CameraRoutes01 : MonoBehaviour 
{
	public int m_RouteIndex = 0 ; 
	public Transform[] m_Routes ;
	public CameraRoutesState m_CameraState = CameraRoutesState.UnActive ;
	public Camera m_CameraPtr = null ;
	public float m_CloseDistanceThreasholdSquare = 1.0f ;
	public float m_SlerpSpeed = 1.0f ;
	
	public void StartMove()
	{
		if( m_RouteIndex < m_Routes.Length )
		{
			m_CameraState = CameraRoutesState.MoveToTarget ;
		}
	}

	public void StopMove()
	{
		m_CameraState = CameraRoutesState.Pause ;
	}

	public void SetupRoutes( Transform [] _Routes )
	{
		m_Routes = _Routes ;
		m_RouteIndex = 0 ;
	}
		
	// Use this for initialization
	void Start () 
	{
		// 沒設定才要初始化
		if( null == m_CameraPtr )
			InitializeCameraPtr() ;
	}
	
	void OnGUI()
	{
		if( true == GUILayout.Button( "ReStart" ) )
		{
			Transform [] routes = new Transform[ 3 ] ;
			GameObject obj = null ;
			for( int i = 0 ; i < 3 ; ++i )
			{
				obj = GameObject.Find( "CameraRoute" + i.ToString() ) ;
				if( null != obj )
				{
					routes[ i ] = obj.transform ;
				}
			}			
			SetupRoutes( routes ) ;
			StartMove() ;			
		}
		if( true == GUILayout.Button( "Pause/Resume" ) )
		{
			if( m_CameraState != CameraRoutesState.MoveToTarget )
				StartMove() ;
			else
				StopMove() ;
		}		
	}
	
	// Update is called once per frame
	void Update () 
	{
		switch( m_CameraState )
		{
		case CameraRoutesState.UnActive :
			break ;
		case CameraRoutesState.MoveToTarget :
			MoveToTarget() ;
			break ;			
		case CameraRoutesState.CheckNextTarget :
			CheckNextTarget() ;
			break ;			
		case CameraRoutesState.Pause :
			break ;
		case CameraRoutesState.Closed :
			break ;
		}

	}
	
	private void MoveToTarget()
	{
		if( m_RouteIndex >= m_Routes.Length ) 
		{
			Debug.LogError( "CameraController_CameraRoutes01:MoveToTarget() m_RouteIndex >= m_Routes.Length" ) ;
			return ;
		}
		
		Vector3 TargetPos = m_Routes[ m_RouteIndex ].position ;
		Vector3 CurrentPos = m_CameraPtr.transform.position ;
		Vector3 DistanceVecNow = TargetPos - CurrentPos ;
		
		if( DistanceVecNow.sqrMagnitude < m_CloseDistanceThreasholdSquare )
		{
			m_CameraPtr.transform.position = m_Routes[ m_RouteIndex ].position ;
			m_CameraPtr.transform.rotation = m_Routes[ m_RouteIndex ].rotation ;
			m_CameraState = CameraRoutesState.CheckNextTarget ;
		}
		else
		{
			m_CameraPtr.transform.rotation = Quaternion.Slerp( m_CameraPtr.transform.rotation , 
				m_Routes[ m_RouteIndex ].transform.rotation , Time.deltaTime * m_SlerpSpeed ) ;
			m_CameraPtr.transform.position = Vector3.Slerp( m_CameraPtr.transform.position , 
				m_Routes[ m_RouteIndex ].transform.position , Time.deltaTime * m_SlerpSpeed ) ;			
		}
	}
	
	private void CheckNextTarget()
	{
		++m_RouteIndex ;
		if( m_RouteIndex >= m_Routes.Length ) 
		{
			m_CameraState = CameraRoutesState.Closed ;
		}
		else
		{
			m_CameraState = CameraRoutesState.MoveToTarget ;
		}
		
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
