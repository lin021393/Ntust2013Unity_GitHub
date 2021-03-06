/*
 * @file BounceAlongLocalX.cs
 * @author NDark
 * 
 * This Object will bounce from -3 to 3 in local x axis.
 * 
 * @date 20130712 . file started.
 * @date 20130713 by NDark . rename class member m_SpeedVec to m_SpeedVecNow.
 */
using UnityEngine;
using System.Collections;

public class BounceAlongLocalX : MonoBehaviour 
{
	Vector3 m_SpeedVecNow = new Vector3( 1 , 0 , 0 ) ;
	
	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
		this.gameObject.transform.Translate( m_SpeedVecNow * Time.deltaTime , 
											 Space.Self ) ;
		
		if( this.gameObject.transform.position.x > 3 )
		{
			m_SpeedVecNow = new Vector3( -1 , 0 , 0 ) ;
		}
		else if( this.gameObject.transform.position.x < -3 )
		{
			m_SpeedVecNow = new Vector3( 1 , 0 , 0 ) ;
		}
	}
}
			
 