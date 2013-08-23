/*
@file ResizeScale01.cs
@author NDark
@date 20130823 file started.
*/
using UnityEngine;

public class ResizeScale01 : MonoBehaviour {
	
	public Vector3 m_ScaleVec = new Vector3( 1.0f , 1.0f , 1.0f ) ;
	// Use this for initialization
	void Start () 
	{
		m_ScaleVec = this.transform.localScale ;
	}
	
	// Update is called once per frame
	void Update () 
	{
		m_ScaleVec *= 1.01f ;
		this.transform.localScale = m_ScaleVec ;
	}
}
