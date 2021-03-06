/*
@file RotateByAngular05.cs
@author NDark
@date 20130813 . file started and derived from RotateByAngular04
*/
using UnityEngine;

public class RotateByAngular05 : MonoBehaviour 
{
	
	public Vector3 m_TargetAngle = Vector3.zero ;
	public Quaternion m_TargetRotation = Quaternion.identity ;
	public Quaternion m_InitRotation = Quaternion.identity ;
	public float m_AngleSpeed = 1.0f ;
	public bool m_Suicide = true ;
	
	public enum RotateByAngular05State
	{
		UnActive = 0 ,
		InActive,
		End ,
	}
	
	public RotateByAngular05State m_State = RotateByAngular05State.UnActive ;
	
	
	// Use this for initialization
	void Start () 
	{
		m_InitRotation = this.gameObject.transform.rotation ;
		m_TargetRotation = m_InitRotation * Quaternion.Euler( m_TargetAngle ) ;
		m_State = RotateByAngular05State.InActive ;
	}
	
	// Update is called once per frame
	void Update () 
	{
		switch( m_State )
		{
		case RotateByAngular05State.UnActive :
			
			break ; 
			
		case RotateByAngular05State.InActive :
			
			Quaternion now = 
				Quaternion.Lerp( this.transform.rotation , 
								 m_TargetRotation , 
								 m_AngleSpeed * Time.deltaTime ) ;
			
			this.transform.rotation = now ;
			
			if( Quaternion.Angle( this.transform.rotation , 
								  m_TargetRotation ) < 1.0f )
			{
				this.transform.rotation = m_TargetRotation ;
				m_State = RotateByAngular05State.End ;
			}
			
			break ; 
			
		case RotateByAngular05State.End :

			if( true == m_Suicide )
				Component.Destroy( this ) ;
			break ;
		}
		
	
	}
}
