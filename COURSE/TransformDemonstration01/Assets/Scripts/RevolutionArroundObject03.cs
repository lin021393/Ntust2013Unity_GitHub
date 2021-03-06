/**
 * @file RevolutionArroundObject03.cs
 * @author NDark
 * 
 * Demonstration of RotateAround().
 * 
 * @date 20130712 . file started.
 */
using UnityEngine;
using System.Collections;

public class RevolutionArroundObject03 : MonoBehaviour 
{
	public float m_RotateSpeed = 30.0f ;
	public GameObject m_CenterTarget = null ;
	
	// Use this for initialization
	void Start () 
	{
		if( null == m_CenterTarget )
		{
			IntializeCenterTarget() ;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if( null == m_CenterTarget )
			return ;
		
		this.transform.RotateAround( m_CenterTarget.transform.position , 
									 Vector3.up , 
									 m_RotateSpeed * Time.deltaTime ) ;
		
	}
	
	private void IntializeCenterTarget()
	{
		m_CenterTarget = GameObject.Find( "Crab" ) ;
		if( null == m_CenterTarget )
		{
			Debug.LogError( "RevolutionArroundObject03::IntializeCenterTarget() null == m_CenterTarget" ) ;
		}
		else
		{
			Debug.Log( "RevolutionArroundObject03::IntializeCenterTarget() end." ) ;
		}		
	}
}
