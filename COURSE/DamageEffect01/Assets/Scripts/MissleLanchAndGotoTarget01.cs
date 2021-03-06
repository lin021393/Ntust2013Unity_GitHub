/**
@file MissleLanchAndGotoTarget01.cs
@author NDark
@date 20130804 . file started.
*/
using UnityEngine;

public class MissleLanchAndGotoTarget01 : MonoBehaviour 
{
	public enum MissleLanchAndGotoTarget01State 
	{
		UnActive ,
		Lanch ,
		GoToTarget ,
		Suicide ,
	}
	
	public MissleLanchAndGotoTarget01State m_State = MissleLanchAndGotoTarget01State.UnActive ;
	public float m_Speed = 1.0f ;
	public Vector3 m_InitialDirection = Vector3.zero ;
	public float m_StartTime = 0.0f ;
	public float m_LanchSec = 3.0f ;
	public float m_LanchEndTime = 0.0f ;
	public GameObject m_Target = null ;
	public float m_Threashold = 1.0f ;
	GameUnitData01 m_UnitData = null ;

	// Use this for initialization
	void Start () 
	{
		m_StartTime = Time.timeSinceLevelLoad ;
		m_LanchEndTime = m_StartTime + m_LanchSec ;
		m_UnitData = this.gameObject.GetComponent<GameUnitData01>() ;
	}
	
	// Update is called once per frame
	void Update () 
	{
		switch( m_State )
		{
		case MissleLanchAndGotoTarget01State.UnActive :
			m_State = MissleLanchAndGotoTarget01State.Lanch ;
			break ;
		case MissleLanchAndGotoTarget01State.Lanch :
			Lanching() ;
			if( Time.timeSinceLevelLoad > m_LanchEndTime )
			{
				this.m_State = MissleLanchAndGotoTarget01State.GoToTarget ;
			}
			break ;
		case MissleLanchAndGotoTarget01State.GoToTarget :
			DeteectAndGotoTarget() ;
			break ;			
		case MissleLanchAndGotoTarget01State.Suicide :
			/* 自殺，不能解決難題；求助，才是最好的路。求救請打1995 ( 要救救我 ) */
			GameObject.Destroy( this.gameObject ) ;
			break ;						
		}
	}
	
	private void Lanching()
	{
		if( null == m_Target )
			return ;
		
		Vector3 moveVec = m_InitialDirection * ( m_Speed * Time.deltaTime ) ;
		if( null != m_UnitData )
		{
			m_UnitData.m_Velocity += moveVec ;
		}
		else
		{
			this.transform.Translate( moveVec ) ;
		}
	}

	
	private void DeteectAndGotoTarget()
	{
		if( null == m_Target )
			return ;
		
		Vector3 toTarget = m_Target.transform.position - this.transform.position ;
		if( toTarget.sqrMagnitude < m_Threashold )
		{
			this.m_State = MissleLanchAndGotoTarget01State.Suicide ;
		}
		toTarget.Normalize() ;
		Vector3 moveVec = toTarget * ( m_Speed * Time.deltaTime ) ;
		if( null != m_UnitData )
		{
			m_UnitData.m_Velocity += moveVec ;
		}
		else
		{
			this.transform.Translate( moveVec ) ;
		}
	}
	

}
