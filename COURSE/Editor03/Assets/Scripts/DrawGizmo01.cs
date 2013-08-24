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
	}
}
