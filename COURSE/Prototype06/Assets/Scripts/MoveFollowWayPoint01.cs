/*
@file MoveFollowWayPoint01.cs
@author NDark
@date 20130824 file started.
*/
using UnityEngine;
using System.Collections.Generic;

public class MoveFollowWayPoint01 : MonoBehaviour 
{
	static List<Transform> m_WayPoints = null ;
	
	public enum MoveState
	{
		UnActive ,
		FindNextWayPoint ,
		KeepMoving ,
		End ,
	}
	public MoveState m_State = MoveState.UnActive ;
	
	public Transform m_CurrentWayPoint = null ;
	public float m_ReachThreashold = 0.3f ;
	public float m_Speed = 1 ;
	public float m_WaitStartSec = 1 ;
	private float m_StartTime = 0 ;
	
	// Use this for initialization
	void Start () 
	{
		if( null == m_WayPoints )
		{
			InitializeWayPoints() ;
		}
		m_StartTime = Time.timeSinceLevelLoad + m_WaitStartSec ;
	}
	
	// Update is called once per frame
	void Update () 
	{
		switch( m_State )
		{
		case MoveState.UnActive :
			if( Time.timeSinceLevelLoad > m_StartTime )
			{
				m_State = MoveState.FindNextWayPoint ;
			}
			break ;
		case MoveState.FindNextWayPoint :
			FindNextWayPoint() ;
			break ;
		case MoveState.KeepMoving :
			KeepMoving() ;
			break ;
		case MoveState.End :
			// Component.Destroy( this ) ;
			break ;			
		}
	}
	
	private void InitializeWayPoints()
	{
		Debug.Log( "InitializeWayPoints" ) ;
		
		m_WayPoints = new List<Transform>() ;
		GameObject wayPointParent = GameObject.Find( "WayPointParent" ) ;
		
		if( null != wayPointParent )
		{
			for( int i = 0 ; i < 15 ; ++i )
			{
				string wayPointName = string.Format( "WayPoint{0:00}" , i ) ;
				Transform wayPointTransform = wayPointParent.transform.FindChild( wayPointName ) ;
				if( null != wayPointTransform )
				{
					// Debug.Log( "m_WayPoints.Add" ) ;
					m_WayPoints.Add( wayPointTransform ) ;
				}
			}
		}
	}
	
	private void FindNextWayPoint()
	{
		if( 0 == m_WayPoints.Count )
		{
			// Debug.Log( "0 == m_WayPoints.Count" ) ;
			m_State = MoveState.End ;
		}
		else if( null == m_CurrentWayPoint )
		{
			m_CurrentWayPoint = m_WayPoints[ 0 ] ;
			m_State = MoveState.KeepMoving ;
		}
		else
		{
			int index = m_WayPoints.IndexOf( m_CurrentWayPoint ) ;
			// Debug.Log( index ) ;
			if( index >= m_WayPoints.Count - 1 )
			{
				// Debug.Log( "index >= m_WayPoints.Count - 1" ) ;
				m_State = MoveState.End ;
			}
			else
			{
				m_CurrentWayPoint = m_WayPoints[ index + 1 ] ;
				m_State = MoveState.KeepMoving ;
			}
		}
	}
	
	private void KeepMoving()
	{
		Vector3 nextPos = Vector3.Lerp( this.gameObject.transform.position , 
										m_CurrentWayPoint.position , 
										m_Speed * Time.deltaTime ) ;
		
		this.gameObject.transform.position = nextPos ;
		
		DetectReached() ;
	}
	
	private void DetectReached()
	{
		float distance = Vector3.Distance( this.gameObject.transform.position , 
										   m_CurrentWayPoint.position ) ;
		if( distance < m_ReachThreashold )
		{
			// Debug.Log( "distance < m_ReachThreashold" ) ;
			m_State = MoveState.FindNextWayPoint ;
		}
	}
}
