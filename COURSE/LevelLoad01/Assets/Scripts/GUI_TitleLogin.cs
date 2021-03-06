/*
@file GUI_TitleLogin.cs
@author NDark
@date 20130816 file started.
@date 20130819 . replace code by GUI.PasswordField at OnGUI()
*/
using UnityEngine;


public class GUI_TitleLogin : MonoBehaviour 
{
	AccessPlayerPerfs02 m_PlayerPerfsManager = null ;
	MessageQueueManager01 m_MessageQueueManager = null ;
	public string m_LevelString = "" ;
	
	// Use this for initialization
	void Start () 
	{
		if( null == m_PlayerPerfsManager )
		{
			m_PlayerPerfsManager = this.gameObject.GetComponent<AccessPlayerPerfs02>() ;
		}
		
		if( null == m_MessageQueueManager )
		{
			m_MessageQueueManager = this.gameObject.GetComponent<MessageQueueManager01>() ;
		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public Rect m_AccountRect = new Rect( 460 , 390 , 120 , 30 ) ;
	public string m_AccountString = "" ;
	
	public Rect m_PasswordRect = new Rect( 460 , 465 , 120 , 30 ) ;
	public string m_PasswordString = "" ;
	
	public Rect m_LoginButton = new Rect( 460 , 520 , 120 , 30 ) ;
	void OnGUI()
	{
		m_AccountString = GUI.TextField( m_AccountRect , m_AccountString ) ;
		
		m_PasswordString = GUI.PasswordField( m_PasswordRect , 
											  m_PasswordString , 
											  '*' ) ;

		if( true == GUI.Button( m_LoginButton , "" ) )
		{
			CheckLogin() ;
		}
	}
	
	private void CheckLogin()
	{
		if( null == m_PlayerPerfsManager ||
			null == m_MessageQueueManager )
			return ;
		
		if( true == m_PlayerPerfsManager.m_Map.ContainsKey( "PlayerAccount" ) &&
			true == m_PlayerPerfsManager.m_Map.ContainsKey( "PlayerPassword" ) )
		{
			bool passwordCorrect = true ;
			string account = m_PlayerPerfsManager.m_Map[ "PlayerAccount" ] ;
			
			if( 0 == m_AccountString.Length ||
				0 == m_PasswordString.Length )
			{
				m_MessageQueueManager.AddMessage( "Enter valid account and password, please~~." ) ;
			}
			else
			{
				if( 0 == account.Length )
				{
					// no user 
					Debug.Log( "NoUser Set One" ) ;
					// set it by current
					m_PlayerPerfsManager.m_Map[ "PlayerAccount" ] = m_AccountString ;
					m_PlayerPerfsManager.m_Map[ "PlayerPassword" ] = m_PasswordString ;
					m_PlayerPerfsManager.SetPlayerPerfs() ;
				}
				else
				{
					// check correct
					if( m_AccountString == m_PlayerPerfsManager.m_Map[ "PlayerAccount" ] &&
						m_PasswordString == m_PlayerPerfsManager.m_Map[ "PlayerPassword" ] )
					{
						Debug.Log( "Check Correct" ) ;
					}
					else
					{
						// show message wrong
						m_MessageQueueManager.AddMessage( "Your password is god damn wrong, HAHAHA!!!" ) ;
						passwordCorrect = false ;
					}
				}
			
				if( true == passwordCorrect )
					Application.LoadLevel( m_LevelString ) ;			
			}
		

		}
		
		
	}
	
}
