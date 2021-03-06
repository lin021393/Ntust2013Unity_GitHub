/*
@file DrawGizmo01.cs
@author NDark
@date 20130823 file started.
*/
using UnityEngine;

public class DrawGizmo01 : MonoBehaviour 
{
	public string m_IconName = "" ;
	public bool m_ScaleInScene = false ;
	public bool m_DrawLine = false ;
	public GameObject m_NextObj = null ;
	public GameObject m_PreviousObj = null ;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnDrawGizmos()
	{
		if( 0 != m_IconName.Length )
		{	
			Gizmos.DrawIcon( this.gameObject.transform.position , 
							 m_IconName , 
							 m_ScaleInScene ) ;
		}
		
		if( true == m_DrawLine )
		{
			if( null != m_PreviousObj )
			{
				Gizmos.DrawLine( this.gameObject.transform.position , 
								 m_PreviousObj.transform.position ) ;
			}
			
			if( null != m_NextObj )
			{
				Gizmos.DrawLine( this.gameObject.transform.position , 
								 m_NextObj.transform.position ) ;
			}
		}
	}
}
