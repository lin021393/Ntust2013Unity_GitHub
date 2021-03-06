/*
@file JSonParseUtility01.cs
@author NDark
@date 20130623 . file created.
*/
using UnityEngine;

public static class JSonParseUtility01
{

	// 分析位置 包含座標及位置物件
	public static bool ParsePosition( JSONObject _PositionNode , 
									  ref Vector3 _Pos )
	{
		if( true == _PositionNode.HasField( "x" ) &&
			true == _PositionNode.HasField( "y" ) &&
			true == _PositionNode.HasField( "z" ) )
		{
			string strx = _PositionNode.GetField("x").str ;
			string stry = _PositionNode.GetField("y").str ;
			string strz = _PositionNode.GetField("z").str ;
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
	public static bool ParseQuaternion( JSONObject _OrientationNode , 
						  				ref Quaternion _Orientation )
	{
		if( true == _OrientationNode.HasField( "x" ) &&
			true == _OrientationNode.HasField( "y" ) &&
			true == _OrientationNode.HasField( "z" ) )
		{		
			string strx = _OrientationNode.GetField("x").str ;
			string stry = _OrientationNode.GetField("y").str ;
			string strz = _OrientationNode.GetField("z").str ;
			float x , y , z ;
			float.TryParse( strx , out x ) ;
			float.TryParse( stry , out y ) ;
			float.TryParse( strz , out z ) ;									
			_Orientation = Quaternion.Euler( -x , -y , -z ) ;
			// Debug.Log( "XMLParseUtilityPartial02:ParseQuaternion() x=" + x + "," + y + ",z" + z ) ;
			return true ;
		}
		return false ;
	}		
	
	public static bool ParseUnitObject( JSONObject _UnitNode , 
										ref string _UnitName ,
										ref string _PrefabName ,
										ref Vector3 _InitPosition ,
										ref Quaternion _InitQuaternion )
	{
		if( true == _UnitNode.HasField( "name" ) && 
			true == _UnitNode.HasField( "prefabName" ) 
			)
		{
			
			_UnitName = _UnitNode.GetField( "name" ).str ;
			
			Debug.Log( "JSonParseUtility01:ParseUnitObject() _UnitName=" + _UnitName) ;
			
			_PrefabName = _UnitNode.GetField( "prefabName" ).str ;
			
			if( true == _UnitNode.HasField( "position3D" )  )
			{
				JSONObject positione3DNode = _UnitNode.GetField( "position3D" ) ;
				ParsePosition( positione3DNode , 
							   ref _InitPosition ) ;
			}
			
			if( true == _UnitNode.HasField( "orientationEuler" ) )
			{
				JSONObject orientationNode = _UnitNode.GetField( "orientationEuler" ) ;
				ParseQuaternion( orientationNode , 
								 ref _InitQuaternion ) ;					
			}	
			return true ;
		}
		return false ;
	}	
}
