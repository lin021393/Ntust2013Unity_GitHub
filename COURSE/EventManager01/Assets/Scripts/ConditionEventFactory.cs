/*
@file ConditionEventFactory.cs
@author NDark
@date 20130816 file started.
*/
using UnityEngine;

[System.Serializable]
public static class ConditionEventFactory
{
	public static ConditionEvent GetByString( string _Name )
	{
		if( "ConditionEvent_PlayAudio" == _Name )
			return new ConditionEvent_PlayAudio() ;
		else if( "ConditionEvent_ShowAMessage" == _Name )
			return new ConditionEvent_ShowAMessage() ;
		else if( "ConditionEvent_ShowAGUIObj" == _Name  )
			return new ConditionEvent_ShowAGUIObj() ;
		else if( "ConditionEvent_AddScriptOnGameObject" == _Name )
			return new ConditionEvent_AddScriptOnGameObject() ;
		else if( "ConditionEvent_SendMessageToGameObject" == _Name )
			return new ConditionEvent_SendMessageToGameObject() ;
		else if( "ConditionEvent_EnableScriptToGameObject" == _Name )
			return new ConditionEvent_EnableScriptToGameObject() ;		
		else if( "ConditionEvent_MoveGameObject" == _Name )
			return new ConditionEvent_MoveGameObject() ;
		else if( "ConditionEvent_CreateGameObject" == _Name )
			return new ConditionEvent_CreateGameObject() ;
		else if( "ConditionEvent_PlayAnimation" == _Name ) 
			return new ConditionEvent_PlayAnimation() ;
		else
			return null ;
	}

}
