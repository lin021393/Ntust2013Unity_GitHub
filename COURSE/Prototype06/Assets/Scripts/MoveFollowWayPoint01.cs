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
	public Transform currentWayPoint = null ;
	public float m_WaitStartSec = 0 ;
	public float m_StartTime = 0 ;
	
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
			Component.Destroy( this ) ;
			break ;			
		}
	}
	
	private void InitializeWayPoints()
	{
		m_WayPoints = new List<Transform>() ;
		GameObject wayPointParent = GameObject.Find( "WayPointParent" ) ;
		if( null != wayPointParent )
		{
			for( int i = 0 ; i < 12 ; ++i )
			{
				string wayPointName = string.Format( "WayPoint{0:00}" , i ) ;
				Transform wayPointTransform = wayPointParent.transform.FindChild( wayPointName ) ;
				if( null != wayPointTransform )
				{
					m_WayPoints.Add( wayPointTransform ) ;
				}
			}
		}
	}
	
	private void FindNextWayPoint()
	{
		if( 0 == m_WayPoints.Count )
		{
			m_State = MoveState.End ;
		}
		else if( null == currentWayPoint )
		{
			currentWayPoint = m_WayPoints[ 0 ] ;
		}
		else
		{
			int index = m_WayPoints.IndexOf( currentWayPoint ) ;
			if( index == m_WayPoints.FindLastIndex() )
			{
				m_State = MoveState.End ;
			}
			else
			{
				currentWayPoint = m_WayPoints[ index + 1 ] ;
			}
		}
	}
	
	private void KeepMoving()
	{
		
	}
}
