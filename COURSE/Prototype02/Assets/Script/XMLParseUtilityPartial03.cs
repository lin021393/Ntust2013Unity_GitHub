/*
@file XMLParseUtilityPartial03.cs
@brief XML關卡讀檔相關函式
@author NDark

@date 20130622 . file created and derieved from XMLParseLevelUtility.cs of kobayashi maru commander open source project.
*/
using UnityEngine;

using System.Xml;

public static class XMLParseUtilityPartial03
{

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
	
	// 分析位置 包含座標及位置物件
	public static bool ParsePosition( XmlNode _PositionNode , 
									  ref Vector3 _Pos )
	{
		if( "Position3D" == _PositionNode.Name )
		{
			// 絕對座標
			string strx = _PositionNode.Attributes[ "x" ].Value ;
			string stry = _PositionNode.Attributes[ "y" ].Value ;
			string strz = _PositionNode.Attributes[ "z" ].Value ;
			float x , y , z ;
			float.TryParse( strx , out x ) ;
			float.TryParse( stry , out y ) ;
			float.TryParse( strz , out z ) ;
			_Pos = new Vector3( x , y , z ) ;

			return true ;
		}
		return false ;
	}
	

	// 分析旋轉
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
			Debug.Log( "XMLParseUtilityPartial03:ParseQuaternion() x=" + x + "," + y + ",z" + z ) ;
			return true ;
		}		
		return false ;
	}		
	
	public static bool ParseUnitObject( XmlNode _UnitNode , 
										ref string _UnitName ,
										ref string _PrefabName ,
										ref Vector3 _InitPosition ,
										ref Quaternion _InitQuaternion )
	{
		if( "Unit" == _UnitNode.Name )
		{
			if( null != _UnitNode.Attributes[ "name" ] )
				_UnitName = _UnitNode.Attributes[ "name" ].Value ;
			if( null != _UnitNode.Attributes[ "prefabName" ] )
				_PrefabName = _UnitNode.Attributes[ "prefabName" ].Value ;
			
			if( true == _UnitNode.HasChildNodes )
			{
				for( int j = 0 ; j < _UnitNode.ChildNodes.Count ; ++j )
				{
					if( true == ParsePosition( _UnitNode.ChildNodes[ j ] , 
											  	ref _InitPosition ) )
					{
						// Debug.Log( "_InitPosition=" + _InitPosition ) ;
					}
					else if( true == ParseQuaternion( _UnitNode.ChildNodes[ j ] , 
													   ref _InitQuaternion ) )
					{
						
					}				
				}
			}
			return true ;
		}
		return false ;
	}
	
	public static bool ParseComponent( XmlNode _UnitNode , 
										ref string _UnitName ,
										ref string _ComponentName
									)
	{
		if( "Component" == _UnitNode.Name )
		{
			if( null != _UnitNode.Attributes[ "name" ] )
				_UnitName = _UnitNode.Attributes[ "name" ].Value ;
			if( null != _UnitNode.Attributes[ "componentName" ] )
				_ComponentName = _UnitNode.Attributes[ "componentName" ].Value ;
			
			return true ;
		}
		return false ;
	}		
}
