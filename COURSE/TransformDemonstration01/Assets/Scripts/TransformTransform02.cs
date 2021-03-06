/*
@file TransformTransform02.cs
@author NDark

Demonstration of transform.TransformDirection()

@date 20130831 . file started.
 */
using UnityEngine;
using System.Collections.Generic ;

public class TransformTransform02 : MonoBehaviour 
{
	public Vector3 m_LocalDirection = new Vector3( 0 , 0 , 1 ) ;
	public float m_ScaleValue = 1 ;
	public List<GameObject> m_Followers = new List<GameObject>() ;
	
	// Use this for initialization
	void Start () 
	{
		InitFollowers() ;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if( true == Input.GetKeyDown( KeyCode.Space ) )
		{
			// wrong answer , it should be world
			// ControlFollower( m_LocalDirection ) ;
			
			
			// correct answer , world vector
			Vector3 worldDirection = 
				this.gameObject.transform.TransformDirection( m_LocalDirection ) ;
			ControlFollower( worldDirection ) ;
			
			
		}
	
		
	}
	
	private void ControlFollower( Vector3 _WorldDirection ) 
	{
		// follower assum it is worldDirection
		foreach( GameObject obj in m_Followers )
		{
			obj.transform.rotation = Quaternion.LookRotation( _WorldDirection ) ;
			// obj.transform.localRotation
		}
	}
	
	private void InitFollowers()
	{
		m_Followers.Clear() ;
		for( int i = 0 ; i < 10 ; ++i )
		{
			string objName = string.Format( "CrabFollower{0}" , i ) ;
			GameObject obj = GameObject.Find( objName ) ;
			if( null != obj )
			{
				m_Followers.Add( obj ) ;
			}
		}
	}
}
