/*
 * @file RotateAroundLocalX.cs
 * @author NDark
 * 
 * Demonstration of transform.Rotate()
 * 
 * @date 20130712 . file started.
 */
using UnityEngine;
using System.Collections;

public class RotateAroundLocalX : MonoBehaviour 
{
	Vector3 m_Right = new Vector3( 1 , 0 , 0 ) ;

	void Awake() 
	{
		// this.transform.Translate( 1 , 0 , 0 ) ;
	}	
	
	// Use this for initialization
	void Start () 
	{
		// this.transform.Translate( 0 , 1 , 0 ) ;
	}
	
	// Update is called once per frame
	void Update () 
	{
		this.transform.Rotate( m_Right , 
							   1 , 
							   Space.Self ) ;
	}
}
			
 