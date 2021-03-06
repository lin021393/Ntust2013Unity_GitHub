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
@file Condition.cs
@author NDark
@date 20130816 file started and derived from Kobayashi Maru Commander
*/
#define DEBUG
using UnityEngine;
using System.Collections.Generic;
using System.Xml;

/*
# 條件觸發 基本物件
# IsTrue() 是否可以觸發
# HasTriggered() 是否已經處發 
# ParseXML() 分析參數
*/
[System.Serializable]
public class Condition
{
	public virtual bool ParseXML( XmlNode _Node )
	{
		return false ;
	}
	public virtual bool IsTrue()
	{
		return false ;
	}
	public Condition()
	{
	}
	public Condition( Condition _src )
	{
	}
}

[System.Serializable]
public class Condition_Time : Condition
{
/*
	<Condition ConditionName="Condition_Time" 
		StartTime="126.0" />	
 */	
	public override bool ParseXML( XmlNode _Node )
	{
		if( null == _Node.Attributes[ "StartTime" ] )
			return false ;
		
		string StartTimeStr =_Node.Attributes[ "StartTime" ].Value ;
		float StartTime = 0.0f ;
		if( true == float.TryParse( StartTimeStr , out StartTime ) )
		{
#if DEBUG
			Debug.Log( "StartTime=" + StartTime ) ;
#endif
			m_CountDownTime = StartTime ;
			m_ChangeTime = Time.time ;
		}

#if DEBUG
			Debug.Log( "Condition_Time::ParseXML() end" ) ;
#endif		
		return true ;
	}
	
	public override bool IsTrue()
	{
		return IsCountDownToZero() ;
	}	

	private bool IsCountDownToZero()
	{
		return ElapsedFromLast() > m_CountDownTime ;
	}
	
	private float ElapsedFromLast()
	{
		float ret = 0.0f ;
		
		ret = Time.time - m_ChangeTime ;// reset time

		return ret;
	}	
	
	private float m_CountDownTime = 0.0f ;
	private float m_ChangeTime = 0.0f ;		
}

[System.Serializable]
public class Condition_Collision : Condition
{
/*
	<Condition ConditionName="Condition_Collision" 
		TriggerName="" 
		PositionStr="0,0,0"
		ScaleStr="1,1,1"
		CollisionPrefabName="Sphere"
		/>
		
 */	
	public void SetCollide( bool _Set )
	{
		m_IsCollide = _Set ;
	}
	
	public override bool ParseXML( XmlNode _Node )
	{
		if( null == _Node.Attributes[ "TriggerName" ] ||
			null == _Node.Attributes[ "PositionStr" ] ||
			null == _Node.Attributes[ "ScaleStr" ] ||
			null == _Node.Attributes[ "CollisionPrefabName" ] )
			return false ;
		
		string triggerName = _Node.Attributes[ "TriggerName" ].Value ;
		
		float x = 0 ;
		float y = 0 ;
		float z = 0 ;
		if( false == XMLParseUtilityPartial03.ParsePosition( 
			_Node.Attributes[ "PositionStr" ].Value , 
			ref x , ref y , ref z ) )
			return false ;
		
		Vector3 pos = new Vector3( x , y , z ) ;
		
		if( false == XMLParseUtilityPartial03.ParsePosition( 
			_Node.Attributes[ "ScaleStr" ].Value , 
			ref x , ref y , ref z ) )
			return false ;
		
		Vector3 scale = new Vector3( x , y , z ) ;
		
		string collisionPrefabName = 
			_Node.Attributes[ "CollisionPrefabName" ].Value ;
		
		Object prefab = Resources.Load( collisionPrefabName ) ;
		if( null != prefab )
		{
			GameObject obj = (GameObject) GameObject.Instantiate( prefab ) ;
			obj.transform.position = pos ;
			obj.transform.localScale = scale ;
			obj.name = triggerName ;
			
			Component script = obj.AddComponent( "OnTriggerEnterSendMessage" ) ;
			if( null != script )
			{
				script.SendMessage( "Setup" , this ) ;
			}
		}
		
		return true ;
	}
	
	public override bool IsTrue()
	{
		bool ret = m_IsCollide ;		
		return ret ;
	}

	private bool m_IsCollide = false ;
}

