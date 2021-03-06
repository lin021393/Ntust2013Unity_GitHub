/**
 * @file ConversationManagerLoader.cs
 * @author NDark
 * @date20140309 file started.
 */
#define DEBUG 
using UnityEngine;


public class ConversationManagerLoader : MonoBehaviour 
{
	ConversationManager m_ConversationManagerSharePointer = null ;

	// Use this for initialization
	void Start () 
	{
		m_ConversationManagerSharePointer = this.gameObject.GetComponent<ConversationManager>() ;
		if( null == m_ConversationManagerSharePointer )
		{
			Debug.LogError( "null == m_ConversationManagerSharePointer" ) ;
		}
	
		LoadStoryAndTakes() ;

#if DEBUG 
		if( null != m_ConversationManagerSharePointer )
		{
			m_ConversationManagerSharePointer.ActiveConversation( 1 ) ;
		}
#endif
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	private void LoadStoryAndTakes()
	{
		Story addStory = new Story() ;
		addStory.UID = 1 ;
		addStory.StartTakeUID = 1 ;
		addStory.EndTakeUID = 5 ;
		m_ConversationManagerSharePointer.Stories.Add( addStory ) ;

		Take addTake = new Take() ;
		addTake.WaitingTime = 5.0f ;
		addTake.UID = 1 ;
		addTake.Potraits.Add( "ConversationManagerSprite_1" ) ;
		m_ConversationManagerSharePointer.Takes.Add( addTake ) ;

		addTake = new Take() ;
		addTake.WaitingTime = 5.0f ;
		addTake.UID = 2 ;
		addTake.Potraits.Add( "ConversationManagerSprite_1" ) ;
		addTake.Contents.Add( "ConversationManagerSprite_3:Cowbay" ) ;
		m_ConversationManagerSharePointer.Takes.Add( addTake ) ;

		addTake = new Take() ;
		addTake.WaitingTime = 5.0f ;
		addTake.UID = 3 ;
		addTake.Potraits.Add( "ConversationManagerSprite_1" ) ;
		addTake.Potraits.Add( "ConversationManagerSprite_2" ) ;
		addTake.Contents.Add( "ConversationManagerSprite_3:Cowbay" ) ;
		m_ConversationManagerSharePointer.Takes.Add( addTake ) ;

		addTake = new Take() ;
		addTake.WaitingTime = 5.0f ;
		addTake.UID = 4 ;
		addTake.Potraits.Add( "ConversationManagerSprite_1" ) ;
		addTake.Potraits.Add( "ConversationManagerSprite_2" ) ;
		addTake.Contents.Add( "ConversationManagerSprite_4:WTF..." ) ;
		m_ConversationManagerSharePointer.Takes.Add( addTake ) ;

		addTake = new Take() ;
		addTake.WaitingTime = 5.0f ;
		addTake.UID = 5 ;
		addTake.Potraits.Add( "" ) ;
		addTake.Potraits.Add( "ConversationManagerSprite_2" ) ;
		addTake.Contents.Add( "ConversationManagerSprite_4:..." ) ;
		m_ConversationManagerSharePointer.Takes.Add( addTake ) ;

#if DEBUG 
		Debug.Log( "Stories.Count=" + m_ConversationManagerSharePointer.Stories.Count ) ;
		Debug.Log( "Takes.Count=" + m_ConversationManagerSharePointer.Takes.Count ) ;
#endif
	}
}
