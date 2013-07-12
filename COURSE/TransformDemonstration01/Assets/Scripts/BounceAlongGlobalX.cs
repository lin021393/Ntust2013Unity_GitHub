/*
 * @file BounceAlongGlobalX.cs
 * @author NDark
 * @date 20130712 . file started.
 */
using UnityEngine;
using System.Collections;

public class BounceAlongGlobalX : MonoBehaviour 
{
	Vector3 m_SpeedVec = new Vector3( 1 , 0 , 0 ) ;
	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
		this.gameObject.transform.Translate( m_SpeedVec * Time.deltaTime , Space.World ) ;
		
		if( this.gameObject.transform.position.x > 3 )
		{
			m_SpeedVec = new Vector3( -1 , 0 , 0 ) ;
		}
		else if( this.gameObject.transform.position.x < -3 )
		{
			m_SpeedVec = new Vector3( 1 , 0 , 0 ) ;
		}
	}
}
			
 