/**
 * @file MissleMoveAndRecycle01.cs
 * @author NDark
 * @date 20130622 . file started.
 */
using UnityEngine;
using System.Collections;

public class MissleMoveAndRecycle01 : MonoBehaviour 
{
	public bool m_Active = false ;
	public Vector3 m_MoveDirection = Vector3.zero ;
	public float m_MoveSpeed = 3.0f ;
	public float m_ZBoundaryMax = 10.0f ;
	public float m_ZBoundaryMin = -10.0f ;
	
	public void Setup( bool _Active , Vector3 _MoveDirection )
	{
		m_Active = _Active ;
		m_MoveDirection = _MoveDirection ;
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if( true == m_Active )
		{
			MoveObject() ;
			DetectRecycle() ;
		}
	}
	
	public void MoveObject()
	{
		Vector3 moveVec = m_MoveDirection * Time.deltaTime * m_MoveSpeed ;
		this.gameObject.transform.Translate( moveVec ) ;
	}
	
	public void DetectRecycle()
	{
		Vector3 posNow = this.transform.position ;
		if( posNow.z > m_ZBoundaryMax || 
			posNow.z < m_ZBoundaryMin )
		{
			GameObject.Destroy( this.gameObject ) ;
		}
	}
}
