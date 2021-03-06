/**
 * @file PauseController02.cs
 * @author NDark
 * @date 20130630 . file started.
 */
using UnityEngine;
using System.Collections;

public class PauseController02 : MonoBehaviour 
{

	public enum PauseState 
	{
		OnPlaying ,
		OnPause ,
	}
	
	public PauseState m_PauseState = PauseState.OnPlaying ;
	public GUIText m_PauseText = null ;
	public int m_ClickBounceFrames = 0 ;
	public GameObject m_MainCharacter = null ;
	
	
	// Use this for initialization
	void Start () 
	{
		if( null == m_PauseText )
			InitializePauseTextObj() ;
		if( null == m_MainCharacter )
			InitializeMainCharacterObjectPtr() ;		
	}
	
	// Update is called once per frame
	void Update () 
	{
		--m_ClickBounceFrames ;
		if( m_ClickBounceFrames < 0 )
			m_ClickBounceFrames = 0 ;
		if( m_ClickBounceFrames > 0 )
			return ;
		
		switch( m_PauseState )
		{
		case PauseState.OnPlaying :
			if( Input.GetKey( KeyCode.P ) )
			{
				// Debug.Log( "PauseController02:Update() show pause." ) ;
				// show pause
				ToPause() ;

			}
			break ;
		case PauseState.OnPause :
			if( Input.GetKey( KeyCode.P ) )
			{
				// Debug.Log( "PauseController02:Update() hide pause." ) ;
				// hide pause
				UnPause() ;
				
			}			
			break ;			
		}
	}

	private void InitializePauseTextObj()
	{
		GameObject pauseTextObj = GameObject.Find( "GUI_PauseTextObj" ) ;
		m_PauseText = pauseTextObj.guiText ;
		
		if( null == m_PauseText )
		{
			Debug.LogError( "PauseController02:InitializePauseTextObj() null == m_PauseText" ) ;
		}
		else
		{
			Debug.Log( "PauseController02:InitializePauseTextObj() end." ) ;
		}		
	}
	
	private void InitializeMainCharacterObjectPtr()
	{
		
		m_MainCharacter = GameObject.FindGameObjectWithTag( "Player" ) ;
		
		if( null == m_MainCharacter )
		{
			Debug.LogError( "PauseController02:InitializeMainCharacterObjectPtr() null == m_MainCharacter" ) ;
		}
		else
		{
			Debug.Log( "PauseController02:InitializeMainCharacterObjectPtr() end." ) ;
		}
	}	
	
	private void ToPause()
	{
		ShowPauseText() ;
		
		TellMainCharacterPause() ;
		
		m_PauseState = PauseState.OnPause ;
		m_ClickBounceFrames = 30 ;
		
		
	}
	
	private void UnPause()
	{
		
		HidePauseText() ;
		
		TellMainCharacterUnPause() ;
		
		m_PauseState = PauseState.OnPlaying ;
		m_ClickBounceFrames = 30 ;
	}
	
	
	private void ShowPauseText()
	{
		if( null == m_PauseText )
			return ;
		m_PauseText.enabled = true ;
	}
		
	private void HidePauseText()
	{
		if( null == m_PauseText )
			return ;
		m_PauseText.enabled = false ;		
	}
	
	
	private void TellMainCharacterPause()
	{
		if( null == m_MainCharacter )
			return ;
		MainCharacterController07 controller = m_MainCharacter.GetComponent<MainCharacterController07>() ;
		if( null == controller )
			return ;
		controller.m_EnableDetect = false ;
	}
	
	private void TellMainCharacterUnPause()
	{
		if( null == m_MainCharacter )
			return ;
		MainCharacterController07 controller = m_MainCharacter.GetComponent<MainCharacterController07>() ;
		if( null == controller )
			return ;
		controller.m_EnableDetect = true ;
	}
		
}
