/*
@file TryCloseTarget.cs
@author NDark
@date 20130827 file started.
*/
using UnityEngine;

public class TryCloseTarget : MonoBehaviour 
{
	public Vector3 m_InitPos = Vector3.zero ;
	public Vector3 m_TargetPos = Vector3.zero ;
	
	public float m_InterpolateNow = 0.0f ;
	public float m_AnimSec = 1.0f ;
	public float m_StartTime = 0.0f ;
	public bool m_Positive = true ;
	public float m_AnimScale = 0.1f ; // only move forward and back 10% 
	
	// Use this for initialization
	void Start () 
	{
		m_InitPos = this.gameObject.transform.position ;
		m_StartTime = Time.timeSinceLevelLoad ;
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 nextPos = Vector3.zero ;
			
		float elapsedSec = Time.timeSinceLevelLoad - m_StartTime  ;
		if( true == m_Positive )
		{
			// timeNow      0   .... 1 
			// interpolate  0/1 .... 1/1			
			// go 
			m_InterpolateNow = ( elapsedSec ) / m_AnimSec ;
		}
		else
		{
			// timeNow      0   .... 1 
			// interpolate  (1-0)/1 .... (1-1)/1			
			// back
			m_InterpolateNow = ( m_AnimSec - ( elapsedSec ) ) / m_AnimSec ;
		}
		
		nextPos = Vector3.Lerp( m_InitPos , 
								m_TargetPos , 
								m_InterpolateNow * m_AnimScale ) ;
		
		if( elapsedSec > m_AnimSec )
		{
			m_Positive = !m_Positive ;
			m_StartTime = Time.timeSinceLevelLoad ;
		}
		
		this.transform.position = nextPos ;
	}
}
 