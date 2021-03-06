/**
@file MissleMoveAndRecycle02.cs
@author NDark
@date 20130801 . file started.
 */
using UnityEngine;

public class MissleMoveAndRecycle02 : MonoBehaviour 
{
	public Vector3 m_MoveDirection = Vector3.zero ;
	public float m_MoveSpeed = 3.0f ;
	public float m_BoundaryMax = 10.0f ;
	public float m_BoundaryMin = -10.0f ;
	public int m_Axis = 0 ;
	
	public void Setup( Vector3 _MoveDirection )
	{
		m_MoveDirection = _MoveDirection ;
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		MoveObject() ;
		DetectRecycle() ;
	}
	
	public void MoveObject()
	{
		Vector3 moveVec = m_MoveDirection * Time.deltaTime * m_MoveSpeed ;
		this.transform.Translate( moveVec , Space.World ) ;
	}
	
	public void DetectRecycle()
	{
		bool destroy = false ;
		Vector3 posNow = this.transform.position ;
		switch( m_Axis )
		{
		case 0 :
			if( posNow.x > m_BoundaryMax || 
				posNow.x < m_BoundaryMin )
			{
				destroy = true ;
			}
			break ;
		case 1 :
			if( posNow.y > m_BoundaryMax || 
				posNow.y < m_BoundaryMin )
			{
				destroy = true ;
			}			
			break ;
		case 2 :
			if( posNow.z > m_BoundaryMax || 
				posNow.z < m_BoundaryMin )
			{
				destroy = true ;
			}			
			break ;			
		}
		
		if( true == destroy )
		{
			GameObject.Destroy( this.gameObject ) ;
		}

	}
}
