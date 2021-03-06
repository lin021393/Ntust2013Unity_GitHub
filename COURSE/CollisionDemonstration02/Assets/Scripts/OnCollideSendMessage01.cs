/**
 * @file OnCollideSendMessage01.cs
 * @author NDark
 * @date 20130612
 */
using UnityEngine;
using System.Collections;

public class OnCollideSendMessage01 : MonoBehaviour 
{
	MessageQueueManager01 m_MessageQueueManagerPtr = null ;

	// Use this for initialization
	void Start () {
		GameObject messageQueueManagerObj = GameObject.Find( "MessageQueueManagerObj" ) ;
		if( null != messageQueueManagerObj )
		{
			m_MessageQueueManagerPtr = messageQueueManagerObj.GetComponent<MessageQueueManager01>() ;
		}
		
		if( null == m_MessageQueueManagerPtr )
		{
			Debug.LogError( "OnCollideSendMessage01:Start() null == m_MessageQueueManagerPtr" ) ;
		}
		else
		{
			Debug.Log( "OnCollideSendMessage01:Start() end." ) ;
		}		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter( Collider other )
	{
		// Debug.Log( other.gameObject.name ) ;
		if( null == m_MessageQueueManagerPtr )
			return ;
		m_MessageQueueManagerPtr.AddMessage( other.gameObject.name + 
			" is collide on " + this.gameObject.name ) ;
	}
}
