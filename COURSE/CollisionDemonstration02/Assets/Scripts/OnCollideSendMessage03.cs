/**
@file OnCollideSendMessage03.cs
@author NDark
@date 20130731 file started.
*/
using UnityEngine;

public class OnCollideSendMessage03 : MonoBehaviour 
{
	public AudioClip m_AlertAudio = null ;
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
			m_MessageQueueManagerPtr.AddMessage( "Alert!!! Intruder Alert!!!" ) ;
		}
		
		AddComponentToAlienUnits() ;
		
		PlayAlertAudio() ;
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
	
	private void AddComponentToAlienUnits()
	{
		string objName = "" ;
		GameObject alienUnit = null ;
		for( int i = 0 ; i < 3 ; ++i )
		{
			objName = string.Format( "AlienUnit{0:00}" , i+1 ) ;
			alienUnit = GameObject.Find( objName ) ;
			alienUnit.AddComponent( "FollowTheMainCharacter01" ) ;
		}
	}
	
	private void PlayAlertAudio()
	{
		if( null != m_AlertAudio )
		{
			this.audio.PlayOneShot( m_AlertAudio ) ;
		}
	}
}
