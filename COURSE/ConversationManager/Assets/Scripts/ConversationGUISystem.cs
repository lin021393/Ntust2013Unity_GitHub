/**
 * @file ConversationGUISystem.cs
 * @author NDark
 * @date20140309 file started.
 */
using UnityEngine;
using System.Collections.Generic ;

public class ConversationGUISystem : MonoBehaviour 
{
	private GameObject Dialog = null ;
	private GameObject Potrait1 = null ;
	private GameObject Potrait2 = null ;

	public Sprite []m_SpritePool ;
	public Dictionary<string,Sprite> m_SpriteMap = new Dictionary<string, Sprite>();

	public void ShowDialog( bool _Show ) 
	{
		if( null == Dialog )
			return ;

		SpriteRenderer sr = Dialog.GetComponent<SpriteRenderer>() ;
		if( null != sr )
		{
			sr.enabled = _Show ;
		}
	}

	public void ShowPotrait1( bool _Show ) 
	{
		if( null == Potrait1 )
			return ;

		SpriteRenderer sr = Potrait1.GetComponent<SpriteRenderer>() ;
		if( null != sr )
		{
			sr.enabled = _Show ;
		}
	}

	public void ShowPotrait2( bool _Show ) 
	{
		if( null == Potrait2 )
			return ;

		// Debug.Log( "ShowPotrait2::_Show=" + _Show ) ;

		SpriteRenderer sr = Potrait2.GetComponent<SpriteRenderer>() ;
		if( null != sr )
		{
			sr.enabled = _Show ;
		}
	}
	
	public void SetContent( string _SpriteLabel , string _Content ) 
	{
		if( null == Dialog )
			return ;
		
		SpriteRenderer sr = Dialog.GetComponent<SpriteRenderer>() ;
		if( null != sr )
		{
			Sprite changeSprite = null ;
			if( true == m_SpriteMap.TryGetValue( _SpriteLabel , out changeSprite ) )
			{
			}
			sr.sprite = changeSprite ;
		}

		// set text
	}

	public void SetPotrait1( string _SpriteLabel ) 
	{
		if( null == Potrait1 )
			return ;

		// Debug.Log( "SetPotrait1::_SpriteLabel=" + _SpriteLabel ) ;

		SpriteRenderer sr = Potrait1.GetComponent<SpriteRenderer>() ;
		if( null == sr )
		{
			Debug.LogError( "null == sr" ) ;
		}
		else
		{
			Sprite changeSprite = null ;
			if( true == m_SpriteMap.TryGetValue( _SpriteLabel , out changeSprite ) )
			{
			}
			sr.sprite = changeSprite ;
		}
	}

	public void SetPotrait2( string _SpriteLabel ) 
	{
		if( null == Potrait2 )
			return ;
		// Debug.Log( "SetPotrait2::_SpriteLabel=" + _SpriteLabel ) ;

		SpriteRenderer sr = Potrait2.GetComponent<SpriteRenderer>() ;
		if( null == sr )
		{
			Debug.LogError( "null == sr" ) ;
		}
		else
		{
			Sprite changeSprite = null ;
			if( true == m_SpriteMap.TryGetValue( _SpriteLabel , out changeSprite ) )
			{
			}
			sr.sprite = changeSprite ;
		}
	}

	void Awake()
	{
		// Debug.Log( "ConversationGUISystem::Awake" ) ;
		
		Dialog = GameObject.Find( "Dialog" ) ;
		if( null == Dialog )
		{
			Debug.LogError( "null == Dialog" ) ;
		}
		
		Potrait1 = GameObject.Find( "Potrait1" ) ;
		if( null == Potrait1 )
		{
			Debug.LogError( "null == Potrait1" ) ;
		}
		
		Potrait2 = GameObject.Find( "Potrait2" ) ;
		if( null == Potrait2 )
		{
			Debug.LogError( "null == Potrait2" ) ;
		}
		
		m_SpriteMap.Add( "ConversationManagerSprite_1" , m_SpritePool[ 0 ] ) ;
		m_SpriteMap.Add( "ConversationManagerSprite_2" , m_SpritePool[ 1 ] ) ;
		m_SpriteMap.Add( "ConversationManagerSprite_3" , m_SpritePool[ 2 ] ) ;
		m_SpriteMap.Add( "ConversationManagerSprite_4" , m_SpritePool[ 3 ] ) ;
	}
	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
