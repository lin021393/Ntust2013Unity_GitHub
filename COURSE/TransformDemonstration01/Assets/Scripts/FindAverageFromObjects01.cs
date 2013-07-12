/*
 * @file FindAverageFromObjects01.cs
 * @author NDark
 * @date 20130712 . file started.
 */
using UnityEngine;
using System.Collections;

public class FindAverageFromObjects01 : MonoBehaviour 
{
	GameObject [] m_Balls ;
	// Use this for initialization
	void Start () 
	{
		InitializedBalls() ;
	}
	
	// Update is called once per frame
	void Update () 
	{
		int count = 0 ;
		Vector3 sumPos = Vector3.zero ;
		for( int i = 0 ; i < m_Balls.Length ; ++i )
		{
			if( null != m_Balls[ i ] )
			{
				sumPos += m_Balls[ i ].transform.position ;
				++count ;
			}
		}
		
		this.transform.position = ( sumPos / count ) ;
	}
	
	private void InitializedBalls()
	{
		m_Balls = new GameObject[10] ;
		for( int i = 0 ; i < m_Balls.Length ; ++i )
		{
			m_Balls[ i ] = GameObject.Find( "Ball" + i.ToString() ) ;
		}
	}
}
