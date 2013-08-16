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
		if( _Name == "ConditionEvent_PlayAudio" )
			return new ConditionEvent_PlayAudio() ;
		else
			return null ;
	}

}
