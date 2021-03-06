/*
 * @file RotateByAngular03.cs
 * @author NDark
 * @date 20130719 . file started.
 */
using UnityEngine;
using System.Collections;

public class RotateByAngular03 : MonoBehaviour 
{
	public Vector3 m_ArroundPoint = Vector3.zero ;
	public Vector3 m_RotateAxis = Vector3.up ;
	public float m_Degree = 10.0f ;

	// Use this for initialization
	void Start () 
	{
		this.transform.RotateAround( m_ArroundPoint , m_RotateAxis , m_Degree ) ;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
