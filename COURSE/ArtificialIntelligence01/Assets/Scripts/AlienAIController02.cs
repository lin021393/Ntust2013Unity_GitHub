/**
@file AlienAIController02.cs
@author NDark
@date 20130820 . file started.
*/
using UnityEngine;

public class AlienAIController02 : MonoBehaviour 
{
	public GameObject m_MainCharacter = null ;
	string m_LastScriptName = "" ;

	// Use this for initialization
	void Start () 
	{
		if( null == m_MainCharacter )
		{
			InitializeMainCharacterObjectPtr() ;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 mainCharacterForward = m_MainCharacter.transform.forward ;
		Vector3 toMainCharacter = 
			m_MainCharacter.transform.position - this.gameObject.transform.position ;
		
		// Debug.Log( toMainCharacter.magnitude ) ;
		float angle = Vector3.Angle( mainCharacterForward , toMainCharacter ) ;
		if( angle < 90 )
		{
			if( "FollowTheMainCharacter02" != m_LastScriptName )
			{
				DisablePreviousScript() ;
				FollowTheMainCharacter02 script = this.gameObject.GetComponent<FollowTheMainCharacter02>() ;			
				if( null == script )
				{
					script = this.gameObject.AddComponent<FollowTheMainCharacter02>() ;
					script.m_MainCharacter = this.m_MainCharacter ;
				}
				script.enabled = true ;
				m_LastScriptName = "FollowTheMainCharacter02" ;
			}
		}
		else if( toMainCharacter.magnitude < 5 )
		{
			if( "AwayFromTheMainCharacter01" != m_LastScriptName )
			{
				DisablePreviousScript() ;
				AwayFromTheMainCharacter01 script = this.gameObject.GetComponent<AwayFromTheMainCharacter01>() ;			
				if( null == script )
				{
					script = this.gameObject.AddComponent<AwayFromTheMainCharacter01>() ;
					
					script.m_MainCharacter = this.m_MainCharacter ;
				}
				script.enabled = true ;
				m_LastScriptName = "AwayFromTheMainCharacter01" ;
			}
		}
		else
		{
			if( "RotateAroundLocalX" != m_LastScriptName )
			{
				DisablePreviousScript() ;
				RotateAroundLocalX script = this.gameObject.GetComponent<RotateAroundLocalX>() ;			
				if( null == script )
				{
					script = this.gameObject.AddComponent<RotateAroundLocalX>() ;
					script.m_Right = new Vector3( 0 , 1 , 0 ) ;
				}
				script.enabled = true ;
				m_LastScriptName = "RotateAroundLocalX" ;
			}			
		}
	}

	private void InitializeMainCharacterObjectPtr()
	{
		m_MainCharacter = GameObject.FindGameObjectWithTag( "Player" ) ;
		
		if( null == m_MainCharacter )
		{
			Debug.LogError( "FollowTheMainCharacter02:InitializeMainCharacterObjectPtr() null == m_MainCharacter" ) ;
		}
		else
		{
			Debug.Log( "FollowTheMainCharacter02:InitializeMainCharacterObjectPtr() end." ) ;
		}
	}
	
	private void DisablePreviousScript()
	{
		if( 0 != m_LastScriptName.Length )
		{
			MonoBehaviour script = (MonoBehaviour)this.gameObject.GetComponent( m_LastScriptName ) ;
			if( null != script )
				script.enabled = false ;
		}
	}
}
