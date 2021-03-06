/*
IMPORTANT: READ BEFORE DOWNLOADING, COPYING, INSTALLING OR USING. 

By downloading, copying, installing or using the software you agree to this license.
If you do not agree to this license, do not download, install, copy or use the software.

    License Agreement For Kobayashi Maru Commander Open Source

Copyright (C) 2013, Chih-Jen Teng(NDark) and Koguyue Entertainment, 
all rights reserved. Third party copyrights are property of their respective owners. 

Redistribution and use in source and binary forms, with or without modification,
are permitted provided that the following conditions are met:

  * Redistribution's of source code must retain the above copyright notice,
    this list of conditions and the following disclaimer.

  * Redistribution's in binary form must reproduce the above copyright notice,
    this list of conditions and the following disclaimer in the documentation
    and/or other materials provided with the distribution.

  * The name of Koguyue or all authors may not be used to endorse or promote products
    derived from this software without specific prior written permission.

This software is provided by the copyright holders and contributors "as is" and
any express or implied warranties, including, but not limited to, the implied
warranties of merchantability and fitness for a particular purpose are disclaimed.
In no event shall the Koguyue or all authors or contributors be liable for any direct,
indirect, incidental, special, exemplary, or consequential damages
(including, but not limited to, procurement of substitute goods or services;
loss of use, data, or profits; or business interruption) however caused
and on any theory of liability, whether in contract, strict liability,
or tort (including negligence or otherwise) arising in any way out of
the use of this software, even if advised of the possibility of such damage.  
*/
/*
@date ConditionEvent.cs
@author NDark

條件事件

# ParseForChildren() 會分析XML內的子節點，決定條件的類型

@date 20130118 file created and derived from ConditionEvent of Kobayashi Maru Commander Open Source Project
*/
#define DEBUG
using UnityEngine;
using System.Collections.Generic;
using System.Xml;

[System.Serializable]
public class ConditionEvent
{
	public ConditionEvent()
	{
	}
	
	public ConditionEvent( ConditionEvent _src )
	{
	}
	
	public void AddCondition( Condition _Condition )
	{
		m_Conditions.Add( _Condition ) ;
	}

	public bool ParseXML( XmlNode _Node )
	{
		for( int i = 0 ; i < _Node.ChildNodes.Count ; ++i )
		{
			if( "Condition" == _Node.ChildNodes[ i ].Name )
			{
				ParseXMLCondition( _Node.ChildNodes[ i ] ) ;
			}
		}
		
		ParseXMLEvent( _Node ) ;
		return true ;
	}

	public virtual bool ParseXMLEvent( XmlNode _Node )
	{
		return false ;
	}
	
	public bool ParseXMLCondition( XmlNode _Node )
	{
#if DEBUG				
		Debug.Log( "ConditionEvent::ParseXMLCondition() _Node.Name=" + _Node.Name ) ;
#endif	

		if( null != _Node.Attributes[ "ConditionName" ] )
		{
			string ConditionName = _Node.Attributes[ "ConditionName" ].Value ;
			Condition addCondition = ConditionFactory.GetByString( ConditionName ) ;
			if( null == addCondition )
				Debug.Log( "ConditionEvent::ParseXMLCondition() null == addCondition , ConditionName=" + ConditionName ) ;
			else
			{
#if DEBUG				
				Debug.Log( "ConditionEvent::ParseXMLCondition() , ConditionName=" + ConditionName ) ;
#endif					
				if( true == addCondition.ParseXML( _Node ) )
				{
					AddCondition( addCondition ) ;
					return true ;
				}
			}
		}
		return false ;
	}
	
	public virtual void Update()
	{
		if( true == m_HasTriggered ||
			m_Conditions.Count == 0 )
			return ;

		List<Condition>.Enumerator eList = m_Conditions.GetEnumerator() ;
		while( eList.MoveNext() )
		{
			if( false == eList.Current.IsTrue() )
			{
				return ;
			}
		}
#if DEBUG				
		Debug.Log( "ConditionEvent::Update() DoEvent" ) ;
#endif
		DoEvent() ;			
		m_HasTriggered = true ;
	}
	
	public virtual void DoEvent()
	{
	}
	
	protected bool m_HasTriggered = false ;
	protected List<Condition> m_Conditions = new List<Condition>() ;
}

[System.Serializable]
public class ConditionEvent_PlayAudio : ConditionEvent
{
	static GameObject sGlobalSingleton = null ;
	
	public override bool ParseXMLEvent( XmlNode _Node )
	{
		if( null != _Node.Attributes[ "AudioName" ] )
		{
			m_AudioName = _Node.Attributes[ "AudioName" ].Value ;
			return true ;
		}
		return false ;
	}
	
	public override void DoEvent()
	{
#if DEBUG				
		Debug.Log( "ConditionEvent_PlayAudio::DoEvent()" ) ;
#endif		
		if( null == sGlobalSingleton )
		{
			sGlobalSingleton = GameObject.Find( "GlobalSingleton" ) ;
		}
		if( null != sGlobalSingleton &&
			null != sGlobalSingleton.audio )
		{
#if DEBUG				
		Debug.Log( "ConditionEvent_PlayAudio::DoEvent() Resources.Load" ) ;
#endif
			AudioClip audioClip = (AudioClip)Resources.Load( m_AudioName ) ;
			if( null != audioClip )
			{
				sGlobalSingleton.audio.PlayOneShot( audioClip ) ;
			}
		}
	}
	
	private string m_AudioName = "" ;
	
}


[System.Serializable]
public class ConditionEvent_ShowAMessage : ConditionEvent
{
	static GameObject sGlobalSingleton = null ;
	
	public override bool ParseXMLEvent( XmlNode _Node )
	{
		if( null != _Node.Attributes[ "MessageContent" ] )
		{
			m_MessageContent = _Node.Attributes[ "MessageContent" ].Value ;
			return true ;
		}
		return false ;
	}
	
	public override void DoEvent()
	{
#if DEBUG				
		Debug.Log( "ConditionEvent_ShowAMessage::DoEvent()" ) ;
#endif		
		if( null == sGlobalSingleton )
		{
			sGlobalSingleton = GameObject.Find( "GlobalSingleton" ) ;
		}
		if( null != sGlobalSingleton &&
			null != sGlobalSingleton.audio )
		{
#if DEBUG				
		Debug.Log( "ConditionEvent_ShowAMessage::DoEvent()" ) ;
#endif
			MessageQueueManager01 messageQueueManager = sGlobalSingleton.GetComponent<MessageQueueManager01>() ;
			if( null != messageQueueManager )
			{
				messageQueueManager.AddMessage( m_MessageContent ) ;
			}
		}
	}
	
	private string m_MessageContent = "" ;
	
}



[System.Serializable]
public class ConditionEvent_ShowAGUIObj : ConditionEvent
{
	public override bool ParseXMLEvent( XmlNode _Node )
	{
		if( null != _Node.Attributes[ "GUIObjName" ] )
		{
			m_GUIObjName = _Node.Attributes[ "GUIObjName" ].Value ;
			return true ;
		}
		return false ;
	}
	
	public override void DoEvent()
	{
#if DEBUG				
		Debug.Log( "ConditionEvent_ShowAGUIObj::DoEvent()" ) ;
#endif		
		ShowGUITexture.Show( m_GUIObjName , true , true , true ) ;
	}
	
	private string m_GUIObjName = "" ;
	
}




[System.Serializable]
public class ConditionEvent_AddScriptOnGameObject : ConditionEvent
{
	public override bool ParseXMLEvent( XmlNode _Node )
	{
		if( null != _Node.Attributes[ "GameObjectName" ] && 
			null != _Node.Attributes[ "ScriptName" ] &&
			null != _Node.Attributes[ "SendMessageName" ] )
		{
			m_GameObjectName = _Node.Attributes[ "GameObjectName" ].Value ;
			m_ScriptName = _Node.Attributes[ "ScriptName" ].Value ;
			m_SendMessageName = _Node.Attributes[ "SendMessageName" ].Value ;
			return true ;
		}
		return false ;
	}
	
	public override void DoEvent()
	{
#if DEBUG				
		Debug.Log( "ConditionEvent_AddScriptOnGameObject::DoEvent()" ) ;
#endif		
		GameObject obj = GameObject.Find( m_GameObjectName ) ;
		if( null != obj &&
			0 != m_ScriptName.Length )
		{
			Component script = obj.AddComponent( m_ScriptName ) ;
			if( null != script &&
				0 != m_SendMessageName.Length )
			{
				script.SendMessage( m_SendMessageName ) ;
			}
		}
	}
	
	private string m_GameObjectName = "" ;
	private string m_ScriptName = "" ;
	private string m_SendMessageName = "" ;
	
}


[System.Serializable]
public class ConditionEvent_SendMessageToGameObject : ConditionEvent
{
	public override bool ParseXMLEvent( XmlNode _Node )
	{
		if( null != _Node.Attributes[ "GameObjectName" ] && 
			null != _Node.Attributes[ "SendMessageName" ] )
		{
			m_GameObjectName = _Node.Attributes[ "GameObjectName" ].Value ;
			m_SendMessageName = _Node.Attributes[ "SendMessageName" ].Value ;
			return true ;
		}
		return false ;
	}
	
	public override void DoEvent()
	{
#if DEBUG				
		Debug.Log( "ConditionEvent_SendMessageToGameObject::DoEvent()" ) ;
#endif		
		GameObject obj = GameObject.Find( m_GameObjectName ) ;
		if( null != obj &&
			0 != m_SendMessageName.Length )
		{
			obj.SendMessage( m_SendMessageName ) ;
		}
	}
	
	private string m_GameObjectName = "" ;
	private string m_SendMessageName = "" ;
	
}


[System.Serializable]
public class ConditionEvent_EnableScriptToGameObject : ConditionEvent
{
	public override bool ParseXMLEvent( XmlNode _Node )
	{
		if( null != _Node.Attributes[ "GameObjectName" ] && 
			null != _Node.Attributes[ "ScriptName" ] &&
			null != _Node.Attributes[ "Enable" ] )
		{
			m_GameObjectName = _Node.Attributes[ "GameObjectName" ].Value ;
			m_ScriptName = _Node.Attributes[ "ScriptName" ].Value ;
			m_Enable = ( _Node.Attributes[ "Enable" ].Value == "true" ) ;
			return true ;
		}
		return false ;
	}
	
	public override void DoEvent()
	{
#if DEBUG				
		Debug.Log( "ConditionEvent_EnableScriptToGameObject::DoEvent()" ) ;
#endif		
		GameObject obj = GameObject.Find( m_GameObjectName ) ;
		if( null != obj &&
			0 != m_ScriptName.Length )
		{
			MonoBehaviour script = (MonoBehaviour) obj.GetComponent( m_ScriptName ) ;
			if( null != script )
			{
				
				Debug.Log( "script.enabled = m_Enable" + m_Enable ) ;
				script.enabled = m_Enable ;
			}
		}
	}
	
	private string m_GameObjectName = "" ;
	private string m_ScriptName = "" ;
	private bool m_Enable = true ;
	
}



[System.Serializable]
public class ConditionEvent_MoveGameObject : ConditionEvent
{
	public override bool ParseXMLEvent( XmlNode _Node )
	{
		if( null != _Node.Attributes[ "GameObjectName" ] )
		{
			m_GameObjectName = _Node.Attributes[ "GameObjectName" ].Value ;
			
			for( int i = 0 ; i < _Node.ChildNodes.Count ; ++i )
			{
				if( "Position3D" == _Node.ChildNodes[ i ].Name )
				{
					if( true == XMLParseUtilityPartial03.ParsePosition( _Node.ChildNodes[ i ] , ref m_Position ) )
					{
					}
				}
				if( "OrientationEuler" == _Node.ChildNodes[ i ].Name )
				{
					if( true == XMLParseUtilityPartial03.ParseQuaternion( _Node.ChildNodes[ i ] , ref m_Rotation ) )
					{
					}
				}				
			}
			
			
			return true ;
		}
		return false ;
	}
	
	public override void DoEvent()
	{
#if DEBUG				
		Debug.Log( "ConditionEvent_MoveGameObject::DoEvent()" + m_GameObjectName ) ;
#endif		
		GameObject obj = GameObject.Find( m_GameObjectName ) ;
		if( null != obj )
		{
			obj.transform.position = m_Position ;
			obj.transform.rotation = m_Rotation ;
		}
	}
	
	private string m_GameObjectName = "" ;
	private Vector3 m_Position = Vector3.zero ;
	private Quaternion m_Rotation = Quaternion.identity ;
	
}



[System.Serializable]
public class ConditionEvent_CreateGameObject : ConditionEvent
{
	static GameObject sGlobalSingleton = null ;
	
	public override bool ParseXMLEvent( XmlNode _Node )
	{
		if( true == XMLParseUtilityPartial03.ParseUnitObject( _Node ,
			ref m_GameObjectName ,
			ref m_PrefabName ,
			ref m_Position ,
			ref m_Rotation ) )
		{
			
		}
			
		return true ;
	}
	
	public override void DoEvent()
	{
#if DEBUG				
		Debug.Log( "ConditionEvent_CreateGameObject::DoEvent()" + m_GameObjectName ) ;
#endif		
		if( null == sGlobalSingleton )
			sGlobalSingleton = GameObject.Find( "GlobalSingleton" ) ;
		
		if( null != sGlobalSingleton )
		{
			Debug.Log( "null != sGlobalSingleton" ) ;
			LevelGeneration08 levelGeneration = sGlobalSingleton.GetComponent<LevelGeneration08>() ;
			if( null != levelGeneration )
			{
				Debug.Log( "levelGeneration.InstaniateObject" ) ;
				levelGeneration.InstaniateObject( m_GameObjectName , m_PrefabName , m_Position , m_Rotation ) ;
			}
		}
	}
	
	private string m_GameObjectName = "" ;
	private string m_PrefabName = "" ;
	private Vector3 m_Position = Vector3.zero ;
	private Quaternion m_Rotation = Quaternion.identity ;
	
}



[System.Serializable]
public class ConditionEvent_PlayAnimation : ConditionEvent
{
	public override bool ParseXMLEvent( XmlNode _Node )
	{
		if( null != _Node.Attributes[ "GameObjectName" ] && 
			null != _Node.Attributes[ "AnimationName" ] )
		{
			m_GameObjectName = _Node.Attributes[ "GameObjectName" ].Value ;
			m_AnimationName = _Node.Attributes[ "AnimationName" ].Value ;
			return true ;
		}
		return false ;
	}
	
	public override void DoEvent()
	{
#if DEBUG				
		Debug.Log( "ConditionEvent_EnableScriptToGameObject::DoEvent()" ) ;
#endif		
		GameObject obj = GameObject.Find( m_GameObjectName ) ;
		if( null != obj &&
			null != obj.animation &&
			0 != m_AnimationName.Length )
		{
			obj.animation.Play( m_AnimationName ) ;
		}
	}
	
	private string m_GameObjectName = "" ;
	private string m_AnimationName = "" ;
	
}
