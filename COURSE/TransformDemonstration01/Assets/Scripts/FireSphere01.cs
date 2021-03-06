/*
@file FireSphere01.cs
@author NDark
@date 20130831 . file started.
*/
using UnityEngine;

public class FireSphere01 : MonoBehaviour 
{
	
	public float m_Speed = 2.0f ;
	public GameObject m_Sphere = null ;
	public bool m_Move = false ;
	public Vector3 m_OrgPos = Vector3.zero ;
	// Use this for initialization
	void Start () 
	{
		m_Sphere = GameObject.Find( "Sphere" ) ;
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if( true == Input.GetKeyDown( KeyCode.Space ) &&
			false == m_Move )
		{
			m_Move = true ;
			m_OrgPos = this.gameObject.transform.position + this.gameObject.transform.up * 0.5f ;
		}
	
		if( true == m_Move &&
			null != m_Sphere )
		{
			m_Sphere.transform.Translate( this.gameObject.transform.up * m_Speed * Time.deltaTime , 
										  Space.World ) ;
			
			if( m_Sphere.transform.position.z >= 10 )
			{
				m_Move = false ;
				m_Sphere.transform.position = m_OrgPos ;
			}
		}		
		
		
		
	}
}
