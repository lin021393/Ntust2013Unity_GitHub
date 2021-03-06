/*
@file PlayAnimationPingpong01.cs
@author NDark
@date 20130913 file started.
*/
using UnityEngine;

public class PlayAnimationPingpong01 : MonoBehaviour 
{
	public string m_AnimationStr = "RunForwardNoGun" ;
	public float m_Speed = 0.1f ;
	
	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
		CheckAndPlayAnimation() ;
	}
	
	void CheckAndPlayAnimation()
	{
		if( false == this.animation.IsPlaying( m_AnimationStr ) &&
			null != this.animation[ m_AnimationStr ] )
		{
			if( m_Speed > 0 )
				this.animation[ m_AnimationStr ].time = 0 ;
			else
				this.animation[ m_AnimationStr ].time = 
					this.animation[ m_AnimationStr ].length ;
			
			this.animation[ m_AnimationStr ].speed = m_Speed ;
			this.animation[ m_AnimationStr ].wrapMode = WrapMode.Once ;
			
			this.animation.Play( m_AnimationStr , PlayMode.StopAll ) ;
			Debug.Log( "this.animation[ m_AnimationStr ].length = " + this.animation[ m_AnimationStr ].length );
			m_Speed *= -1 ;
		}
	}
}
