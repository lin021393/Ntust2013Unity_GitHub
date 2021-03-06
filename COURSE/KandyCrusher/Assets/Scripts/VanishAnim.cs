/*
@file VanishAnim.cs
@author NDark
@date 20130828 file started.
*/
using UnityEngine;

public class VanishAnim : MonoBehaviour 
{
	public Vector3 m_InitScale = Vector3.zero ;
	public Vector3 m_TargetScale = Vector3.zero ;
	
	public float m_InterpolateNow = 0.0f ;
	public float m_AnimSec = 1.0f ;
	public float m_StartTime = 0.0f ;
	
	public bool m_Active = false ;
	
	public bool IsAnimEnd()
	{
		return ( Time.timeSinceLevelLoad - m_StartTime > m_AnimSec ) ;
	}
	// Use this for initialization
	void Start () 
	{
		m_InitScale = this.gameObject.transform.localScale ;
		m_StartTime = Time.timeSinceLevelLoad ;
		m_Active = true ;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if( true == m_Active )
		{
			Vector3 nextScale = Vector3.zero ;
				
			float elapsedSec = Time.timeSinceLevelLoad - m_StartTime  ;
			
			// timeNow      0   .... 1 
			// interpolate  0/1 .... 1/1			
			// go 
			m_InterpolateNow = ( elapsedSec ) / m_AnimSec ;
			
			nextScale = Vector3.Lerp( m_InitScale , m_TargetScale , m_InterpolateNow ) ;
			
			this.transform.localScale = nextScale ;
			
			if( elapsedSec > m_AnimSec )
			{
				m_Active = false ;
			}
		}
	}
	
}
 