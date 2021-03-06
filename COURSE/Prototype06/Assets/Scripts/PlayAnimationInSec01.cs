/*
@file PlayAnimationInSec01.cs
@author NDark
@date 20130823 file started.
*/
using UnityEngine;

public class PlayAnimationInSec01 : MonoBehaviour 
{
	public string m_AnimationName = "Anim1" ;
	public float m_StartSec = 0 ;
	public bool m_Played = false ;
	
	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
		if( false == m_Played )
		{
			if( Time.timeSinceLevelLoad > m_StartSec && 
				0 != m_AnimationName.Length &&
				null != this.animation )
			{
				Debug.Log( "this.animation.Play" ) ;
				this.animation.Play( m_AnimationName ) ;
				m_Played = true ;
			}
			
		}			
	}
}
