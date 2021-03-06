/**
@file OnCollideAddScriptOnObj02.cs
@author NDark
@date 20130815 . file started.
*/
using UnityEngine;

public class OnCollideAddScriptOnObj02 : MonoBehaviour 
{
	public GameObject m_TargetObj = null ;
	public string m_AddScriptName = "" ;
	public string m_SendMessage = "" ;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter( Collider other )
	{
		// Debug.Log( "OnTriggerEnter" + other.collider.name ) ;
		
		if( null != m_TargetObj &&
			0 != m_AddScriptName.Length )
		{
			Component script = m_TargetObj.GetComponent( m_AddScriptName ) ;
			if( null == script )
			 	script = m_TargetObj.AddComponent( m_AddScriptName ) ;
			
			if( 0 != m_SendMessage.Length )
			{
				script.SendMessage( m_SendMessage ) ;
			}
		}
		
	}	
	
}
