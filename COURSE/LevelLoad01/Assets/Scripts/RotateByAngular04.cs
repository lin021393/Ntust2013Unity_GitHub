/*
@file RotateByAngular04.cs
@author NDark
@date 20130813 . file started and derived from RotateByAngular02
*/
using UnityEngine;

public class RotateByAngular04 : MonoBehaviour 
{
	
	public Vector3 m_TargetAngle = Vector3.zero ;
	public Vector3 m_CurrentAngle = Vector3.zero ;
	public Quaternion m_InitRotation = Quaternion.identity ;
	public float m_AngleSpeed = 1.0f ;
	public bool m_Suicide = true ;
	
	public enum RotateByAngular04State
	{
		UnActive = 0 ,
		InActive,
		End ,
	}
	
	public RotateByAngular04State m_State = RotateByAngular04State.UnActive ;
	
	// Use this for initialization
	void Start () 
	{
		m_InitRotation = this.transform.rotation ;
		m_State = RotateByAngular04State.InActive ;
	}
	
	// Update is called once per frame
	void Update () 
	{
		switch( m_State )
		{
		case RotateByAngular04State.UnActive :
			break ; 
			
		case RotateByAngular04State.InActive :
			Quaternion now = Quaternion.Euler( m_CurrentAngle ) ;
			this.transform.rotation = m_InitRotation * now ;
			
			Vector3 DiffVec = m_TargetAngle - m_CurrentAngle ;
			Debug.Log( DiffVec ) ;
			if( Mathf.Abs( DiffVec.x ) > Mathf.Abs( m_AngleSpeed ) )
				m_CurrentAngle.x += m_AngleSpeed ;
			
			if( Mathf.Abs( DiffVec.y ) > Mathf.Abs( m_AngleSpeed ) )
				m_CurrentAngle.y += m_AngleSpeed ;
			
			if( Mathf.Abs( DiffVec.z ) > Mathf.Abs( m_AngleSpeed ) )
				m_CurrentAngle.z += m_AngleSpeed ;			
			
			Debug.Log( DiffVec.sqrMagnitude ) ;
			if( DiffVec.sqrMagnitude < 10.0f )
			{
				now = Quaternion.Euler( m_TargetAngle ) ;
				this.transform.rotation = m_InitRotation * now ;				
				m_State = RotateByAngular04State.End ;
			}
			
			break ; 
			
		case RotateByAngular04State.End :

			if( true == m_Suicide )
				Component.Destroy( this ) ;
			break ;
		}
		
	
	}
}
