/*
 * @file RotateAroundLocalX.cs
 * @author NDark
 * @date 20130712 . file started.
 */
using UnityEngine;
using System.Collections;

public class RotateAroundLocalX : MonoBehaviour 
{
	Vector3 m_Right = new Vector3( 10 , 0 ,0 ) ;
	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
		this.transform.Rotate( m_Right , 1 , Space.Self ) ;
	}
}
			
 