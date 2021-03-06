/*
 * @file TransformTransform01.cs
 * @author NDark
 * 
 * Demonstration of transform.TransformPoint()
 * 
 * @date 20130712 . file started.
 */
using UnityEngine;
using System.Collections;

public class TransformTransform01 : MonoBehaviour 
{
	public Vector3 m_LocalPoint = new Vector3( 0 , 0 , -2 ) ;
	public GameObject m_BallObject = null ;
	
	// Use this for initialization
	void Start () 
	{
		if( null == m_BallObject )
		{
			InitBallObj() ;
		}
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if( null == m_BallObject )
			return ;
		
		Vector3 worldPoint = this.transform.TransformPoint( m_LocalPoint ) ;
		m_BallObject.transform.position = worldPoint ;
	
	}
	
	private void InitBallObj()
	{
		m_BallObject = GameObject.Find( "Ball" ) ;
		if( null == m_BallObject )
		{
			Debug.LogError( "TransformTransform01::InitBallObj() null == m_BallObject" ) ;
		}
		else
		{
			Debug.Log( "TransformTransform01::InitBallObj() end." ) ;
		}
	}
}
