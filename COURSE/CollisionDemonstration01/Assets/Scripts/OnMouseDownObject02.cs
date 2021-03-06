/*
@file OnMouseDownObject02.cs
@author NDark
@date 20130804 . file started.
*/
using UnityEngine;

public class OnMouseDownObject02 : MonoBehaviour 
{
	public bool m_Fly = false ;
	public Vector3 m_FlyDirection = Vector3.up ;
	public float m_FlySpeed = 10.0f ;
	public Vector3 m_RotateAxis = Vector3.up ;
	public float m_RotationSpeed = 30.0f ;
	public Vector3 m_OrgPosition = Vector3.zero ;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		KeepFly() ;
		DetectAndReset() ;
	
	}
	
	void OnMouseDown() 
	{
		if( false == m_Fly )
		{
			m_RotateAxis = Random.onUnitSphere ;
			m_FlyDirection = Random.onUnitSphere ;
			m_FlyDirection.y = Mathf.Abs( m_FlyDirection.y ) ;
			m_FlyDirection.z = 0 ;
			m_OrgPosition = this.transform.position ;
			m_Fly = true ;	
		}
	}
	
	private void KeepFly()
	{
		Transform thisTransform = this.gameObject.transform ;
		if( true == m_Fly )
		{
			thisTransform.Translate( m_FlyDirection * m_FlySpeed * Time.deltaTime , Space.World ) ;
			thisTransform.Rotate( m_RotateAxis , m_RotationSpeed * Time.deltaTime , Space.World ) ;
		}		
	}
	
	private void DetectAndReset()
	{
		Vector3 thisPosition = this.gameObject.transform.position ;
		if( thisPosition.sqrMagnitude > 400 )
		{
			GameObject.Destroy( this.gameObject ) ;
		}
	}
}
