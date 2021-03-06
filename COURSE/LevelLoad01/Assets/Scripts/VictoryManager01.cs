/*
@file VictoryManager01.cs
@author NDark
@date 20130812 file started.
*/
using UnityEngine;

public class VictoryManager01 : MonoBehaviour 
{
	public enum GameState
	{
		UnActive ,
		InActive ,
		OnVictory ,
		InVictory ,
		OnLose ,
		InLose ,
		End ,
	}
	
	public GameState m_State = GameState.UnActive ;
	public GameObject m_MainCharacter = null ;
	public string m_EndLevelString = "" ;
	public float m_WaitStartTime = 0.0f ;
	public float m_WaitElapsedSec = 5.0f ;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		switch( m_State )
		{
		case GameState.UnActive :
			InitializeMainCharacter() ;
			break ;
		case GameState.InActive :
			if( null == m_MainCharacter )
			{
				m_State = GameState.OnLose ;
			}
			break ;
		case GameState.OnVictory :
			break ;	
		case GameState.InVictory :
			break ;	
		case GameState.OnLose :
			StopSystem() ;
			ShowLosePicture() ;
			m_WaitStartTime = Time.timeSinceLevelLoad ;
			m_State = GameState.InLose ;
			break ;	
		case GameState.InLose :
			if( Time.timeSinceLevelLoad > m_WaitStartTime + m_WaitElapsedSec )
				m_State = GameState.End ;
			
			break ;
		case GameState.End :
			Application.LoadLevel( m_EndLevelString ) ;
			break ;			
		}
	
	}

	private void InitializeMainCharacter()
	{
		m_MainCharacter = GameObject.FindGameObjectWithTag( "Player" ) ;
		m_State = GameState.InActive ;
	}
	
	private void ShowLosePicture() 
	{
		ShowGUITexture.Show( "YouLose" , true , true , true ) ;
	}
	
	private void StopSystem() 
	{
		this.gameObject.GetComponent<MainUpdate03>().m_PauseThisFrame = true ;
	}
}
