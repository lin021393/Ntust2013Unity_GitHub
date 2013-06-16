/*
 * @file LevelGeneration01.cs
 * @author NDark
 * @date 20130616 . file created.
 */
using UnityEngine;
using System.Collections;

public class PawnController01 : MonoBehaviour 
{
	public enum MoveState
	{
		Steady = 0,
		InMove ,
	}
	public MoveState m_MoveState = MoveState.Steady ;
	public Vector3 m_TargetPos = Vector3.zero ;
	public float m_DistanceOfABlock = 1.25f;
	public float m_ToTargetThreashold = 0.01f ;
	public float m_MoveSpeed = 2.1f ;
	public Vector3 m_Boundary = new Vector3( 10.0f , 0.0f , 10.0f ) ;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		switch( m_MoveState )
		{
		case MoveState.Steady :
			CheckMove() ;
			break ;
		case MoveState.InMove :
			KeepMoving() ;
			break ;			
		}
		
		
	}
	
	private void CheckMove()
	{
		Vector3 targetPos = Vector3.zero ;
		float leftright = Input.GetAxis( "Horizontal" ) ;
		float updown = Input.GetAxis( "Vertical" ) ;		
		// Debug.Log( "PawnController01:CheckMove() leftright=" + leftright ) ;
		
		if( leftright > 0 )
		{
			targetPos = this.transform.position + new Vector3( m_DistanceOfABlock , 0 , 0 ) ;
		}
		else if( leftright < 0 )
		{
			targetPos = this.transform.position + new Vector3( -1 * m_DistanceOfABlock , 0 , 0 ) ;
		}
		else if( updown < 0 )
		{
			targetPos = this.transform.position + new Vector3( 0 , 0 , -1 * m_DistanceOfABlock ) ;
		}
		else if( updown > 0 )
		{
			targetPos = this.transform.position + new Vector3( 0 , 0 , 1 * m_DistanceOfABlock ) ;
		}	
		else
		{
			return ;
		}
		
		m_TargetPos = targetPos ;
		m_MoveState = MoveState.InMove ;
	}
	
	private void KeepMoving()
	{
		Vector3 distanceVec = this.transform.position - m_TargetPos ;
		if( distanceVec.magnitude < m_ToTargetThreashold ) 
		{
			this.transform.position = m_TargetPos ;
			m_MoveState = MoveState.Steady ;
		}
		else
		{
			this.transform.position = Vector3.Lerp( this.transform.position , m_TargetPos , Time.deltaTime * m_MoveSpeed ) ;
		}
	}
}
