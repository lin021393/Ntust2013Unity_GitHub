/*
@file OnMouseSelection01.cs
@author NDark
@date 20130804 file started.
*/
using UnityEngine;

public class OnMouseSelection01 : MonoBehaviour 
{
	static SelectionSystem01 m_SelectSystem = null ;

	// Use this for initialization
	void Start () 
	{	
		if( null == m_SelectSystem )
		{
			GameObject singleton = GameObject.Find( "GlobalSingleton" ) ;
			m_SelectSystem = singleton.GetComponent<SelectionSystem01>() ;
		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnMouseDown()
	{
		if( m_SelectSystem.m_SelectObject == this.gameObject )
		{
			m_SelectSystem.Select( false , null ) ;
		}
		else
		{
			m_SelectSystem.Select( true , this.gameObject ) ;
		}
	}
	
}
