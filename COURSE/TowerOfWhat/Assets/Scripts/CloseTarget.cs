/*
@file CloseTarget.cs
@author NDark
@date 20130828 file started.
*/
using UnityEngine;

public class CloseTarget : MonoBehaviour 
{
	public Vector3 m_InitPos = Vector3.zero ;
	public Vector3 m_TargetPos = Vector3.zero ;
	
	public float m_InterpolateNow = 0.0f ;
	public float m_AnimSec = 0.25f ;
	public float m_StartTime = 0.0f ;
	
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
		
		// timeNow      0   .... 1 
		// interpolate  0/1 .... 1/1			
		// go 
		m_InterpolateNow = ( elapsedSec ) / m_AnimSec ;
		
		nextPos = Vector3.Lerp( m_InitPos , m_TargetPos , m_InterpolateNow ) ;
		
		this.transform.position = nextPos ;
		
		if( elapsedSec > m_AnimSec )
		{
			Suicide() ;
		}
	}
	
	private void Suicide()
	{
		Component.Destroy( this ) ;
	}
}
 