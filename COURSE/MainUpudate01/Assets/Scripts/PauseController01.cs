/**
 * @file PauseController01.cs
 * @author NDark
 * @date 20130630 . file started.
 */
using UnityEngine;
using System.Collections;

public class PauseController01 : MonoBehaviour 
{

	public enum PauseState 
	{
		OnPlaying ,
		OnPause ,
	}
	
	public PauseState m_PauseState = PauseState.OnPlaying ;
	public GUIText m_PauseText = null ;
	public int m_ClickBounceFrames = 0 ;
	
	
	// Use this for initialization
	void Start () 
	{
		if( null == m_PauseText )
			InitializePauseTextObj() ;
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
				// Debug.Log( "PauseController01:Update() show pause." ) ;
				// show pause
				ShowPauseText() ;
				m_PauseState = PauseState.OnPause ;
				m_ClickBounceFrames = 30 ;
			}
			break ;
		case PauseState.OnPause :
			if( Input.GetKey( KeyCode.P ) )
			{
				// Debug.Log( "PauseController01:Update() hide pause." ) ;
				// hide pause
				HidePauseText() ;
				m_PauseState = PauseState.OnPlaying ;
				m_ClickBounceFrames = 30 ;
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
			Debug.LogError( "PauseController01:InitializePauseTextObj() null == m_PauseText" ) ;
		}
		else
		{
			Debug.Log( "PauseController01:InitializePauseTextObj() end." ) ;
		}		
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
	
}
