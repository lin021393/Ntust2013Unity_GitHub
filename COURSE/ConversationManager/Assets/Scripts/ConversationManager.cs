/**
 * @file ConversationManager.cs
 * @author NDark
 * @date20140308 file started.
 */
using UnityEngine;
using System.Collections.Generic ;

public enum ConversationManagerState
{
	UnActive = 0 ,
	Starting ,
	WaitEnd ,
	WaitForInput , 
	Closing ,
	Close ,
} ;

public class ConversationManager : MonoBehaviour 
{
	public ConversationGUISystem m_ConversationGUISystemSharePointer = null ;
	public ConversationManagerState State
	{
		get { return m_State ; }
	}
	private ConversationManagerState m_State = ConversationManagerState.UnActive ;
	private int m_CurrentStoryUID = 0 ;
	private int m_CurrentTakeUID = 0 ;
	private float m_StartTime = 0.0f ;
	private bool m_MouseIsDown = false ;

	public List<Story> Stories
	{ 
		get { return m_Stories ; } 
		set { m_Stories = value ; } 
	}
	private List<Story> m_Stories = new List<Story>() ;

	public List<Take> Takes
	{ 
		get { return m_Takes ; } 
		set { m_Takes = value ; } 
	}
	private List<Take> m_Takes = new List<Take>() ;

	private List<int> m_StartingQueue = new List<int>() ;
	
	public void ActiveConversation( int _StoryUID )
	{
		m_StartingQueue.Add( _StoryUID ) ;
	}
	
	// Use this for initialization
	void Start () 
	{
		m_ConversationGUISystemSharePointer = this.gameObject.GetComponent<ConversationGUISystem>() ;
		if( null == m_ConversationGUISystemSharePointer )
		{
			Debug.LogError( "null == m_ConversationGUISystemSharePointer" ) ;
		}

		ShowDialogUI( false ) ;
	}
	
	// Update is called once per frame
	void Update () 
	{
		switch( m_State )
		{
		case ConversationManagerState.UnActive :
			CheckQueue() ;
			break ;
		case ConversationManagerState.Starting :
			ShowDialogUI( true ) ;
			m_State = ConversationManagerState.WaitEnd ;
			break ;
		case ConversationManagerState.WaitEnd :
			WaitEnd() ;
			break ;
		case ConversationManagerState.WaitForInput :
			if( true == CheckIfPress() )
			{
				PlayNext() ;

			}
			break ;
		case ConversationManagerState.Closing :
			ShowDialogUI( false ) ;
			m_State = ConversationManagerState.Close ;
			break ;
		case ConversationManagerState.Close :
			m_State = ConversationManagerState.UnActive ;
			break ;			
		}
	
	}
	
	private bool CheckIfPress()
	{
		bool ret = false ;
		if( false == m_MouseIsDown &&
		    true == Input.GetMouseButtonDown( 0 ) )
		{
			m_MouseIsDown = true ;
			ret = true ;
		}
		else if( true == m_MouseIsDown && 
		         false == Input.GetMouseButtonDown( 0 ))
		{
			m_MouseIsDown = false ;
		}
		return ret ; 
	}
	
	private void PlayNext()
	{
		Story story = GetStory( m_CurrentStoryUID ) ;
		if( m_CurrentTakeUID == story.EndTakeUID )
		{
			// no more next
			// Debug.Log( "m_CurrentTakeUID == story.EndTakeUID" ) ;
			m_State = ConversationManagerState.Closing ;
		}
		else
		{
			int takeIndex = GetTakeIndex( m_CurrentTakeUID ) ;
			++takeIndex ;
			if( takeIndex >= m_Takes.Count )
			{
				// no more next
				// Debug.Log( "takeIndex >= m_Takes.Count" ) ;
				m_State = ConversationManagerState.Closing ;
			}
			else
			{
				m_CurrentTakeUID = m_Takes[ takeIndex ].UID ;

				ShowDialogUI( true ) ;

				m_State = ConversationManagerState.WaitEnd ;
			}
		}
	}	
	
	private void ShowDialogUI( bool _Show )
	{

		if( false == _Show )
		{
			// Debug.Log( "ShowDialogUI=" + _Show ) ;
			m_ConversationGUISystemSharePointer.ShowDialog( false ) ;
			m_ConversationGUISystemSharePointer.ShowPotrait1( false ) ;
			m_ConversationGUISystemSharePointer.ShowPotrait2( false ) ;
		}
		else
		{
			int takeIndex = GetTakeIndex( m_CurrentTakeUID ) ;
			if( takeIndex >= m_Takes.Count )
			{
				// send warning
				Debug.LogError( "null == take" ) ;
				return ;
			}

			Take take = m_Takes[ takeIndex ] ;
			if( null == take )
			{
				// send warning
				Debug.LogError( "null == take" ) ;
				return ;
			}
			else
			{
				// potrait
				if( take.Potraits.Count <= 0 )
				{
					m_ConversationGUISystemSharePointer.ShowPotrait1( false ) ;
					m_ConversationGUISystemSharePointer.ShowPotrait2( false ) ;
				}
				else if( 1 == take.Potraits.Count )
				{
					string p1 = take.Potraits[ 0 ] ;
					m_ConversationGUISystemSharePointer.SetPotrait1( p1 ) ;
					m_ConversationGUISystemSharePointer.ShowPotrait1( true ) ;
				}
				else if( 2 == take.Potraits.Count )
				{
					string p1 = take.Potraits[ 0 ] ;
					if( 0 == p1.Length )
					{
						m_ConversationGUISystemSharePointer.ShowPotrait1( false ) ;
					}
					else
					{
						m_ConversationGUISystemSharePointer.SetPotrait1( p1 ) ;
						m_ConversationGUISystemSharePointer.ShowPotrait1( true ) ;
					}

					string p2 = take.Potraits[ 1 ] ;
					if( 0 == p2.Length )
					{
						m_ConversationGUISystemSharePointer.ShowPotrait2( false ) ;
					}
					else
					{
						m_ConversationGUISystemSharePointer.SetPotrait2( p2 ) ;
						m_ConversationGUISystemSharePointer.ShowPotrait2( true ) ;
					}
				}

				// content
				// Debug.Log( "take.Contents.Count=" + take.Contents.Count ) ;
				if( take.Contents.Count <= 0 )
				{
					m_ConversationGUISystemSharePointer.ShowDialog( false ) ;
				}
				else if( 1 == take.Contents.Count )
				{
					string [] spliter = { ":" } ;
					string contentStr = take.Contents[ 0 ] ;
					string [] strVec = contentStr.Split( spliter , System.StringSplitOptions.None ) ;
					// Debug.Log( "strVec[0]=" + strVec[0] ) ;
					if( strVec.Length >= 2 )
					{
						m_ConversationGUISystemSharePointer.SetContent( strVec[ 0 ] , strVec[ 1 ] ) ;
						m_ConversationGUISystemSharePointer.ShowDialog( true ) ;
					}
				}
				else if( 2 == take.Contents.Count )
				{
					string [] spliter = { ":" } ;
					string content2Str = take.Contents[ 1 ] ;
					string [] strVec = content2Str.Split( spliter , System.StringSplitOptions.None ) ;
					// Debug.Log( "strVec[0]=" + strVec[0] ) ;
					// Debug.Log( "strVec[1]=" + strVec[1] ) ;
					if( strVec.Length >= 2 )
					{
						m_ConversationGUISystemSharePointer.SetContent( strVec[ 0 ] , strVec[ 1 ] ) ;
						m_ConversationGUISystemSharePointer.ShowDialog( true ) ;
					}
				}

				m_StartTime = Time.time ;
			}


		}
	}
	
	private void CheckQueue()
	{
		if( ConversationManagerState.UnActive != m_State )
			return ;

		if( m_StartingQueue.Count > 0 )
		{
			int retreiveStoryUID = m_StartingQueue[ 0 ] ;
			m_StartingQueue.RemoveAt( 0 ) ;


			Story s = GetStory( retreiveStoryUID ) ;
			if( null != s )
			{
				m_CurrentStoryUID = retreiveStoryUID ;
				m_CurrentTakeUID = s.StartTakeUID ;
				m_State = ConversationManagerState.Starting ;
			}

		}
	}

	private Story GetStory( int _UID )
	{
		foreach( Story s in m_Stories )
		{
			if( _UID == s.UID )
			{
				return s ;
			}
		}
		return null ;
	}

	private int GetTakeIndex( int _TakeUID )
	{
		for( int i = 0 ; i < m_Takes.Count ; ++i )
		{
			if( _TakeUID == m_Takes[ i ].UID )
			{
				return i ;
			}
		}
		return -1 ;
	}

	private void WaitEnd()
	{
		int index = GetTakeIndex( m_CurrentTakeUID ) ;
		if( index >= m_Takes.Count )
		{
			// warning
			return ;
		}

		Take currentTake = m_Takes[ index ] ;
		if( 0 == currentTake.WaitingTime )
		{
			m_State = ConversationManagerState.WaitForInput ;
		}
		else
		{
			float diffTime = Time.time - m_StartTime ;
			if( diffTime >= currentTake.WaitingTime )
			{
				PlayNext() ;
			}
		}

	}
}

