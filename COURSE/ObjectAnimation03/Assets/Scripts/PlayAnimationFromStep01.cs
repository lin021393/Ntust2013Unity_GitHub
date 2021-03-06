/*
@file PlayAnimationFromStep01.cs
@author NDark
@date 20131005 file started.
*/
using UnityEngine;
using System.Collections;

public class PlayAnimationFromStep01 : MonoBehaviour 
{
	public string m_AnimationName = "" ;
	public float m_StartTime = 0.0f ;
	
	public int m_StartIndex = 0 ;
	public int m_TotalNum = 14 ;
	public float m_TotalTime = 2.33f ;
	// Use this for initialization
	void Start () 
	{
		// PlayAnim( true ) ;
	}
	
	public void PlayAnim( bool positive )
	{
		if( null != this.animation[ m_AnimationName ] &&
			false == this.animation.IsPlaying( m_AnimationName ) )
		{
			AnimationState animState = this.animation[ m_AnimationName ] ;
			animState.time = m_TotalTime * m_StartIndex / (float)m_TotalNum ;
			animState.speed = ( true == positive ) ? 1 : -1 ;
//			Debug.Log( "m_TotalTime"  + m_TotalTime ) ;
//			Debug.Log( "m_StartIndex"  + m_StartIndex ) ;
//			Debug.Log( "m_TotalNum"  + m_TotalNum ) ;
			this.animation.Play() ;
		}
	}
	
	public void StopAnim()
	{
		this.animation.Stop( m_AnimationName ) ;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	
	}
}
