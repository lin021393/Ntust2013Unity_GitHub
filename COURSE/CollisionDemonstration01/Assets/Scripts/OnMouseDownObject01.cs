/*
@file OnMouseDownObject01.cs
@author NDark
@date 20130727 . file started.
@date 20130804 
. add log.
. add code of find root object.

*/
using UnityEngine;

public class OnMouseDownObject01 : MonoBehaviour 
{
	public bool m_Fly = false ;
	public Vector3 m_FlyDirection = Vector3.up ;
	public float m_FlySpeed = 10.0f ;
	public Vector3 m_RotateAxis = Vector3.up ;
	public float m_RotationSpeed = 30.0f ;
	public Vector3 m_OrgPosition = Vector3.zero ;
	public Quaternion m_OrgRotation = Quaternion.identity ;
	Transform m_RootNodeTransform = null ;
	
	// Use this for initialization
	void Start () 
	{
		m_RootNodeTransform = this.gameObject.transform ;
		
		// find top root node transform		
		Transform parentTransform = m_RootNodeTransform.parent ;
		while( null != parentTransform )
		{
			m_RootNodeTransform = parentTransform ;
			parentTransform = m_RootNodeTransform.parent ;			
		}		
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		KeepFly() ;
		DetectAndReset() ;
	
	}
	
	void OnMouseDown() 
	{
		Debug.Log( "OnMouseDown" ) ;
		if( false == m_Fly )
		{

				
			m_RotateAxis = Random.onUnitSphere ;
			m_FlyDirection = Random.onUnitSphere ;
			m_FlyDirection.y = Mathf.Abs( m_FlyDirection.y ) ;
			m_OrgPosition = m_RootNodeTransform.position ;
			m_OrgRotation = m_RootNodeTransform.rotation ;
			m_Fly = true ;	
		}
	}
	
	private void KeepFly()
	{
		if( true == m_Fly )
		{
			m_RootNodeTransform.Translate( m_FlyDirection * m_FlySpeed * Time.deltaTime , Space.World ) ;
			m_RootNodeTransform.Rotate( m_RotateAxis , m_RotationSpeed * Time.deltaTime , Space.World ) ;
		}		
	}
	
	private void DetectAndReset()
	{
		Vector3 thisPosition = m_RootNodeTransform.position ;
		if( thisPosition.sqrMagnitude > 400 )
		{
			m_RootNodeTransform.position = m_OrgPosition ;
			m_RootNodeTransform.rotation = m_OrgRotation ;
			m_Fly = false ;
		}
	}
}
