/**
@file OnCollideSendMessage02.cs
@author NDark
@date 20130731 file started.
*/
using UnityEngine;

public class OnCollideSendMessage02 : MonoBehaviour 
{
	MessageQueueManager01 m_MessageQueueManagerPtr = null ;

	// Use this for initialization
	void Start () 
	{
		InitMessageQueueManager() ;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter( Collider other )
	{
		string reportLog = other.gameObject.name + " is OnTriggerEnter on " + this.gameObject.name ;
		Debug.Log( reportLog ) ;
		if( null != m_MessageQueueManagerPtr )
		{
			m_MessageQueueManagerPtr.AddMessage( reportLog ) ;
		}
	}
	
	void OnTriggerStay( Collider other )
	{
		string reportLog = other.gameObject.name + " is OnTriggerStay on " + this.gameObject.name ;
		Debug.Log( reportLog ) ;
		if( null != m_MessageQueueManagerPtr )
		{
			m_MessageQueueManagerPtr.AddMessage( reportLog ) ;
		}
	}
	
	void OnTriggerExit( Collider other )
	{
		string reportLog = other.gameObject.name + " is OnTriggerExit on " + this.gameObject.name ;
		Debug.Log( reportLog ) ;
		if( null != m_MessageQueueManagerPtr )
		{
			m_MessageQueueManagerPtr.AddMessage( reportLog ) ;
		}
	}
	
	void OnCollisionEnter( Collision other )
	{
		string reportLog = other.gameObject.name + " is OnCollisionEnter on " + this.gameObject.name ;
		Debug.Log( reportLog ) ;
		if( null != m_MessageQueueManagerPtr )
		{
			m_MessageQueueManagerPtr.AddMessage( reportLog ) ;
		}
	}
	
	void OnCollisionStay( Collision other )
	{
		string reportLog = other.gameObject.name + " is OnCollisionStay on " + this.gameObject.name ;
		Debug.Log( reportLog ) ;
		if( null != m_MessageQueueManagerPtr )
		{
			m_MessageQueueManagerPtr.AddMessage( reportLog ) ;
		}
	}
	
	void OnCollisionExit( Collision other )
	{
		string reportLog = other.gameObject.name + " is OnCollisionExit on " + this.gameObject.name ;
		Debug.Log( reportLog ) ;
		if( null != m_MessageQueueManagerPtr )
		{
			m_MessageQueueManagerPtr.AddMessage( reportLog ) ;
		}
	}
	
	private void InitMessageQueueManager()
	{
		GameObject messageQueueManagerObj = GameObject.Find( "MessageQueueManagerObj" ) ;
		if( null != messageQueueManagerObj )
		{
			m_MessageQueueManagerPtr = messageQueueManagerObj.GetComponent<MessageQueueManager01>() ;
		}
		
		if( null == m_MessageQueueManagerPtr )
		{
			Debug.LogError( "OnCollideSendMessage02:InitMessageQueueManager() null == m_MessageQueueManagerPtr" ) ;
		}
		else
		{
			Debug.Log( "OnCollideSendMessage02:InitMessageQueueManager() end." ) ;
		}		
	}
}
