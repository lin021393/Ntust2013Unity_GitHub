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
@file XMLParseLevelUtility.cs
@brief XML關卡讀檔相關函式
@author NDark

@date 20140329 . file created and copy from Kobayashi Maru Commander Open Source Project
@date 20140412 by NDark
. add class method ParseUnitDataTemplateData()
. add argument of _UnitDataTemplateName at ParseUnit()
@date 20140413 by NDark
. add checking of _UnitNode at ParseUnitDataTemplateData()
. add code of ParseSkillZone() at ParseUnitDataTemplateData()
. add class method ParseSkill()

*/
using UnityEngine;
using System.Collections.Generic;
using System.Xml;

public static class XMLParseLevelUtility 
{
	/*
	 * parse 
	 * <xxx PosAnchor="x,y,z" />
	 * <xxx PosAnchor="objectName" />
	 */
	public static bool ParseAnchor( XmlNode _Node , 
									ref PosAnchor _ResultAnchor )
	{
		if( null == _Node.Attributes[ "PosAnchor" ] )
			return false ;
		string PosAnchorStr = _Node.Attributes[ "PosAnchor" ].Value ;
			
		return ParseAnchor( PosAnchorStr , 
							ref _ResultAnchor ) ;
	}		
	public static bool ParseAnchor( string _AnchorStr , 
									ref PosAnchor _ResultAnchor )
	{
		char [] seperator = {','} ;
		string[] strVec = _AnchorStr.Split( seperator ) ;
		if( strVec.Length > 2 )
		{
			float x = 0 ;
			float y = 0 ;
			float z = 0 ;
			ParsePosition( _AnchorStr , ref x , ref y , ref z ) ;
			_ResultAnchor.Setup( new Vector3( x , y , z ) ) ;
			return true ;
		}
		else
		{
			_ResultAnchor.Setup( _AnchorStr ) ;
			return true ;
		}		
	}	
	public static bool ParsePosition( string _PositionStr , 
									  ref float _x ,
									  ref float _y , 
									  ref float _z )
	{
		char [] seperator = {','} ;
		string[] strVec = _PositionStr.Split( seperator ) ;
		if( strVec.Length > 2 )
		{
			float.TryParse( strVec[0] , out _x ) ;
			float.TryParse( strVec[1] , out _y ) ;
			float.TryParse( strVec[2] , out _z ) ;
			return true ;
		}
		return false ;
	}
	
	/*
	 * 分析位置 包含座標及位置物件
	 * parse 
	 * <Position3D x="x" y="y" z="z" />
	 * <Position3D objectName="objectName" />
	 */
	public static bool ParsePosition( XmlNode _PositionNode , 
									  ref PosAnchor _PosAnchor )
	{
		if( "Position3D" == _PositionNode.Name )
		{
			if( null != _PositionNode.Attributes[ "objectName" ] )
			{
				string objectName = _PositionNode.Attributes[ "objectName" ].Value ;
				_PosAnchor.Setup( objectName ) ;
			}
			else
			{
				
				// 絕對座標
				string strx = _PositionNode.Attributes[ "x" ].Value ;
				string stry = _PositionNode.Attributes[ "y" ].Value ;
				string strz = _PositionNode.Attributes[ "z" ].Value ;
				float x , y , z ;
				float.TryParse( strx , out x ) ;
				float.TryParse( stry , out y ) ;
				float.TryParse( strz , out z ) ;
				Vector3 setPosition = new Vector3( x , y , z ) ;
				_PosAnchor.Setup( setPosition ) ;
			}			

			return true ;
		}
		return false ;
	}
	
	/*
	 * 分析旋轉
	 * parse 
	 * <OrientationEuler x="x" y="y" z="z" />
	 */
	public static bool ParseQuaternion( XmlNode _OrientationNode , 
						  				ref Quaternion _Orientation )
	{
		if( "OrientationEuler" == _OrientationNode.Name )
		{
			string strx = _OrientationNode.Attributes[ "x" ].Value ;
			string stry = _OrientationNode.Attributes[ "y" ].Value ;
			string strz = _OrientationNode.Attributes[ "z" ].Value ;
			float x , y , z ;
			float.TryParse( strx , out x ) ;
			float.TryParse( stry , out y ) ;
			float.TryParse( strz , out z ) ;									
			_Orientation = Quaternion.Euler( -x , -y , -z ) ;
			return true ;
		}		
		return false ;
	}	
	
	
	// 分析補充資料
	public static bool ParseSupplementalPair( XmlNode _Node , 
											  out string _Label , 
											  out string _Value )
	{
		_Label = "" ;
		_Value = "" ;
		bool ret = false ;
		if( "SupplementalPair" == _Node.Name &&
			null != _Node.Attributes[ "label" ] && 
			null != _Node.Attributes[ "value" ] )
		{
			_Label = _Node.Attributes[ "label" ].Value ;
			_Value = _Node.Attributes[ "value" ].Value ;
			return true ;
		}		
		return ret ;
	}
	
	public static bool ParseUnitAtt( XmlNode _UnitNode , 
									  ref string _UnitName , 
									  ref string _PrefabeName ,
	                                ref string _UnitDataTemplateName ,
									  ref PosAnchor _PosAnchor , 
									  ref Quaternion _Orientation ,
	                                ref Dictionary<string,StandardParameter> _StandardParamMap )
	{
		if( null != _UnitNode.Attributes[ "Name" ] )
			_UnitName = _UnitNode.Attributes[ "Name" ].Value ;
		
		if( null != _UnitNode.Attributes[ "PrefabName" ] )
			_PrefabeName = _UnitNode.Attributes[ "PrefabName" ].Value ;

		if( null != _UnitNode.Attributes[ "UnitDataTemplateName" ] )
			_UnitDataTemplateName = _UnitNode.Attributes[ "UnitDataTemplateName" ].Value ;

		
		if( true == _UnitNode.HasChildNodes )
		{
			for( int j = 0 ; j < _UnitNode.ChildNodes.Count ; ++j )
			{
				if( true == ParsePosition( _UnitNode.ChildNodes[ j ] , 
										   ref _PosAnchor ) )
				{
					
				}
				else if( true == ParseQuaternion( _UnitNode.ChildNodes[ j ] , 
																	   ref _Orientation ) )
				{
					
				}
				else if( true == ParseStandardParameterMap( _UnitNode.ChildNodes[ j ] , 
				                                           ref _StandardParamMap ) )
				{
					
				}
			}
		}
		return true ;
	}


	/* Parse Unit Init Data */
	public static bool ParseUnit( XmlNode _UnitNode , 
								  ref string _UnitName , 
	                             ref string _PrefabeName ,
	                             ref string _UnitDataTemplateName ,
	                             ref PosAnchor _PosAnchor , 
	                             ref Quaternion _Orientation ,
	                             ref Dictionary<string,StandardParameter> _StandardParamMap )
	{
		_UnitName = "" ;
		_PrefabeName = "" ;
		_PosAnchor = new PosAnchor() ;
		_Orientation = Quaternion.identity ;

		if( "Unit" != _UnitNode.Name )
		{
			// Debug.Log( "ParseUnitInitData() : Unit != _UnitNode.Name:" + _UnitNode.Name ) ;
			return false ;
		}
		
		return ParseUnitAtt( _UnitNode , 
								  ref _UnitName , 
		                    ref _PrefabeName ,
		                    ref _UnitDataTemplateName ,
								  ref _PosAnchor , 
								  ref _Orientation ,
		                    ref _StandardParamMap ) ;
	}
	
	

	public static bool ParseStandardParameterMap( XmlNode _StandardParameterMapNode ,
                                             ref Dictionary<string,StandardParameter> _StandardParamMap )
	{
		if( "StandardParameterMap" ==_StandardParameterMapNode.Name )
		{
			if( true == _StandardParameterMapNode.HasChildNodes )
			{
				for( int j = 0 ; j < _StandardParameterMapNode.ChildNodes.Count ; ++j )
				{
					string label = "" ;
					StandardParameter addStandardParameter = new StandardParameter() ;
					if( true == ParseStandardParameter( _StandardParameterMapNode.ChildNodes[ j ] , 
					                                   ref label ,
					                                   ref addStandardParameter ) )
					{
						// Debug.Log( "ParseStandardParameterMap() label=" + label ) ;
						_StandardParamMap.Add( label , addStandardParameter ) ;
					}
				}
			}
			return true ;
		}
		return false ;
	}

	/* 
	 * @brief Parse standard parameter from xml
	 */
	public static bool ParseStandardParameter( XmlNode _StandardParameterNode ,
											ref string _Name ,
											ref StandardParameter _Param )
	{
		if( null == _StandardParameterNode )
			return false ;
		
		_Name = _StandardParameterNode.Name ;
		// Debug.Log( _Name ) ;
		/*
			public float m_Now = 0.0f ;
			public float m_Max = 0.0f ;
			public float m_Min = 0.0f ;
			public float m_Std = 0.0f ;
			public float m_Init = 0.0f ;
		 */
		
		if( null != _StandardParameterNode.Attributes[ "min" ] )
		{
			float min = 0.0f ;
			float.TryParse( _StandardParameterNode.Attributes[ "min" ].Value , 
				out min ) ;
		    _Param.min = min ;
		}
		
		if( null != _StandardParameterNode.Attributes[ "max" ] )
		{
			float max ;
			float.TryParse( _StandardParameterNode.Attributes[ "max" ].Value , 
				out max ) ;
		    _Param.max = max ;
			_Param.m_Std = max ;
			_Param.m_Init = max ;
			_Param.now = max ;
		}

		if( null != _StandardParameterNode.Attributes[ "std" ] )
		{
			float std ;
			float.TryParse( _StandardParameterNode.Attributes[ "std" ].Value , 
				out std ) ;
		    _Param.m_Std = std ;
			_Param.now = std ;
		}
		
		if( null != _StandardParameterNode.Attributes[ "init" ] )
		{
			float init ;
			float.TryParse( _StandardParameterNode.Attributes[ "init" ].Value , 
				out init ) ;
		    _Param.m_Init = init ;
			_Param.now = init ;
		}
		
		return ( 0 != _Name.Length ) ;
	}
	
	/*
	<AddScriptOnObject objectName="GlobalSingleton" scriptName="ChallangeLevel01Manager" />	
	 */
	// 分析XML 新增script到指定物件
	public static bool ParseAddScriptOnObject( XmlNode _UnitNode , 
											 ref string _objectName , 
											 ref string _scriptName )
	{
		_objectName = "" ;
		_scriptName = "" ;
		if( "AddScriptOnObject" != _UnitNode.Name )
		{
			// Debug.Log( "ParseLoseCondition() : LoseCondition != _UnitNode.Name=" + _UnitNode.Name ) ;
			return false ;
		}
		
		if( null == _UnitNode.Attributes["objectName"] ||
		    null == _UnitNode.Attributes["scriptName"] )
			return false ;	
		_objectName = _UnitNode.Attributes["objectName"].Value ;
		_scriptName = _UnitNode.Attributes["scriptName"].Value ;
		return true ;
	}
	
	// 分析靜態物件
	private static void ParseStaticObjectAtt( XmlNode _UnitNode , 
	                                     ref string _UnitName ,
	                                     ref string _PrefabName ,
	                                     ref Vector3 _InitPosition ,
	                                     ref Quaternion _InitQuaternion )	
	{
		if( null != _UnitNode.Attributes[ "Name" ] )
			_UnitName = _UnitNode.Attributes[ "Name" ].Value ;
		if( null != _UnitNode.Attributes[ "PrefabName" ] )
			_PrefabName = _UnitNode.Attributes[ "PrefabName" ].Value ;
		
		if( true == _UnitNode.HasChildNodes )
		{
			for( int j = 0 ; j < _UnitNode.ChildNodes.Count ; ++j )
			{
				PosAnchor posAnchor = new PosAnchor() ;
				if( true == ParsePosition( _UnitNode.ChildNodes[ j ] , 
				                          ref posAnchor ) )
				{
					_InitPosition = posAnchor.GetPosition() ;
				}
				else if( true == ParseQuaternion( _UnitNode.ChildNodes[ j ] , 
				                                 ref _InitQuaternion ) )
				{
					
				}				
			}
		}
	}
	// 分析靜態物件
	public static bool CheckAndParseStaticObject( XmlNode _UnitNode , 
										ref string _UnitName ,
										ref string _PrefabName ,
										ref Vector3 _InitPosition ,
										ref Quaternion _InitQuaternion )
	{
		// Debug.Log( "ParseStaticObject()" ) ;
		if( "StaticObject" == _UnitNode.Name )
		{
			ParseStaticObjectAtt( _UnitNode , 
			                     ref _UnitName ,
			                     ref _PrefabName ,
			                     ref _InitPosition ,
			                     ref _InitQuaternion ) ;
//			Debug.Log( "ParseStaticObject()" + _UnitName ) ;
//			Debug.Log( "ParseStaticObject()" + _PrefabName ) ;
//			Debug.Log( "ParseStaticObject()" + _InitPosition ) ;
//			Debug.Log( "ParseStaticObject()" + _InitQuaternion ) ;
			return true ;
		}
		return false ;
	}

	// 分析靜態物件
	public static void ParseMapZoneObjectAtt( XmlNode _UnitNode , 
	                                              ref string _UnitName ,
	                                              ref string _PrefabName ,
	                                              ref Vector3 _InitPosition ,
	                                              ref Quaternion _InitQuaternion ,
	                                         ref string _TextureName )
	{
		ParseStaticObjectAtt( _UnitNode , 
		                     ref _UnitName ,
		                     ref _PrefabName ,
		                     ref _InitPosition ,
		                     ref _InitQuaternion ) ;
		if( null != _UnitNode.Attributes[ "TextureName" ] )
			_TextureName = _UnitNode.Attributes[ "TextureName" ].Value ;

	}
	// 分析靜態物件
	public static bool CheckAndParseMapZoneObject( XmlNode _UnitNode , 
	                                             ref string _UnitName ,
	                                             ref string _PrefabName ,
	                                             ref Vector3 _InitPosition ,
	                                             ref Quaternion _InitQuaternion ,
	                                              ref string _TextureName )
	{
		// Debug.Log( "ParseStaticObject()" ) ;
		if( "MapZoneObject" == _UnitNode.Name )
		{
			ParseMapZoneObjectAtt( _UnitNode , 
			                     ref _UnitName ,
			                      ref _PrefabName ,
			                      ref _InitPosition ,
			                      ref _InitQuaternion ,
			                      ref _TextureName ) ;
			return true ;
		}
		return false ;
	}

	
	// 分析XML 背景音樂設定
	public static bool ParseBackgroundMusic( XmlNode _UnitNode , 
											ref string _AudioPath )
	{
		if( "BackgroundMusicAudioPath" == _UnitNode.Name )
		{
			if( null != _UnitNode.Attributes["name"] )
			{
				_AudioPath = _UnitNode.Attributes["name"].Value ;
				return true ;
			}
		}
		return false ;
	}

	/* 
	 * @brief Parse UnitData from xml
	 */
	public static bool ParseUnitDataTemplateData( XmlNode _UnitNode , 
	                           ref string _UnitDataTemplateName ,
	                           ref UnitDataParam _Data )
	{
		// Debug.Log( _UnitNode.Name ) ;
		if( null == _UnitNode )
		{
			// Debug.LogWarning( "ParseUnitDataTemplateData(),null == _UnitNode" ) ;
			return false ;
		}

		if( null == _UnitNode.Attributes[ "UnitDataTemplateName" ] )
			return false ;
		
		_UnitDataTemplateName = _UnitNode.Attributes[ "UnitDataTemplateName" ].Value ;
		Dictionary<string,StandardParameter> standardParamMap = new Dictionary<string, StandardParameter>();
		Dictionary< string , Skill > skills = new Dictionary<string, Skill>() ;
		for( int i = 0 ; i < _UnitNode.ChildNodes.Count ; ++i )
		{
			if( 0 == _UnitNode.ChildNodes[ i ].Name.IndexOf( "#comment" ) )
			{
				// comment
			}
			else if( true == ParseSkillZone( _UnitNode.ChildNodes[ i ] , 
			                                ref skills ) )
			{
				_Data.ImportSkills( skills ) ;
			}
			else if( true == ParseStandardParameterMap( _UnitNode.ChildNodes[ i ] , 
				                                        ref standardParamMap ) )
			{
				_Data.ImportStandardParameter( standardParamMap ) ;
			}
		}
		return true ;
	}

	public static bool ParseSkill( XmlNode _node ,
	                               ref string _SkillName , 
	                              ref Skill _Skill )
	{
		if( "Skill" == _node.Name )
		{
			if( null != _node.Attributes[ "Name" ] )
			{
				_SkillName = _node.Attributes[ "Name" ].Value ;
			}

			if( null != _node.Attributes[ "DisplayString" ] )
			{
				_Skill.DisplayString = _node.Attributes[ "DisplayString" ].Value ;
			}
			return true ;
		}
		return false ;
	}

	public static bool ParseSkillZone( XmlNode _node ,
	                                  ref Dictionary< string , Skill > _Skills )
	{
		if( "SkillZone" == _node.Name )
		{
			foreach( XmlNode skillNode in _node.ChildNodes )
			{
				string skillname = "" ;
				Skill addSkill = new Skill() ;
				if( true == ParseSkill( skillNode , 
				                       ref skillname , 
				                       ref addSkill ) )
				{

					_Skills.Add( skillname , addSkill ) ;
				}
			}
			return true ;
		}
		return false ;
	}
}
