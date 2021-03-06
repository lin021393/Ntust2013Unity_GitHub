/*
 * @file FindAverageFromObjects01.cs
 * @author NDark
 * @date 20130712 . file started.
 */
using UnityEngine;
using System.Collections;

public class FindAverageFromObjects01 : MonoBehaviour 
{
	public GameObject [] m_Balls = null ;// create at InitializedBalls()
	
	// Use this for initialization
	void Start () 
	{
		InitializedBalls() ;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if( null == m_Balls )
			return ;
		
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
		
		// average from sum of positions
		this.transform.position = ( sumPos / count ) ;
	}
	
	private void InitializedBalls()
	{
		// collect from Ball0 to Ball9, if them exist.
		m_Balls = new GameObject[10] ;
		for( int i = 0 ; i < m_Balls.Length ; ++i )
		{
			m_Balls[ i ] = GameObject.Find( "Ball" + i.ToString() ) ;
		}
	}
}
