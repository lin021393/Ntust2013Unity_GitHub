/**
@file ShakeObject01.cs
@author NDark
@date 20130815 . file started.
*/
using UnityEngine;

public class ShakeObject01 : MonoBehaviour 
{
	public enum ShakeState
	{
		UnActive ,
		InShake ,
	}
	
	ShakeState m_State = ShakeState.UnActive ;
	public Vector3 m_OrgPosition = Vector3.zero ;
	public float m_ShakeStartTime = 0.0f ;
	public float m_ShakeEndTime = 0.0f ;
	public float m_ShakeElapsedSec = 1.0f ;
	public float m_ShakeRange = 1.0f ;
	
	public void StartShake()
	{
		if( m_State == ShakeState.InShake )
			return ;
		m_ShakeStartTime = Time.timeSinceLevelLoad ;
		m_ShakeEndTime = m_ShakeStartTime + m_ShakeElapsedSec ;
		m_State = ShakeState.InShake ;
		m_OrgPosition = this.gameObject.transform.position ;
	}
	
	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
		switch( m_State )
		{
		case ShakeState.UnActive :
			break ;
		case ShakeState.InShake :
			KeepShaking() ;
			CheckShakeEnd() ;
			break ;			
		}
	}
	
	private void KeepShaking()
	{
		Vector3 position = this.gameObject.transform.position ;
		this.gameObject.transform.position = position + Random.insideUnitSphere * m_ShakeRange ;
	}
	
	private void CheckShakeEnd()
	{
		if( Time.timeSinceLevelLoad > m_ShakeEndTime )
		{
			this.gameObject.transform.position = m_OrgPosition ;
			m_State = ShakeState.UnActive ;
		}
	}
}
