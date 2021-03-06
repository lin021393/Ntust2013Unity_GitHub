/*
@file JSonParser01.cs
@author NDark
@date 20130623 . file started.
@date 20130730 by NDark . comment and format.
*/
using UnityEngine;

public class JSonParser01 : MonoBehaviour 
{
	public string m_LevelFilePath = "Common/Data/Level007" ;

	// Use this for initialization
	void Start () 
	{
		LoadLevelFile( m_LevelFilePath ) ;
	}
	
	// Update is called once per frame
	void Update () 
	{
	}
	
	private void LoadLevelFile( string _LevelFilePath )
	{
		Debug.Log( "JSonParser01:LoadLevelFile() start." ) ;	
		
		TextAsset textAsset = (TextAsset) Resources.Load( _LevelFilePath ) ;
		// Be careful the end of line!!!
		
		if( null != textAsset )
		{
			JSONObject jsObj = new JSONObject( textAsset.text );
			
			ParseLevel( jsObj ) ;
			
			Debug.Log( "JSonParser01:LoadLevelFile() ParseLevel() completed." ) ;	
		}
		Debug.Log( "JSonParser01:LoadLevelFile() end." ) ;
	}
	
	private void ParseLevel( JSONObject _JsonObj )
	{
		if( null == _JsonObj )
		{
			Debug.LogError( "JSonParser01::ParseLevel() null == _JsonObj" ) ;
			return ;
		}

		JSONObject nameNode = _JsonObj.GetField( "name" ) ;
		if( null != nameNode )
		{
			Debug.Log( "JSonParser01::ParseLevel() nameNode.str=" + nameNode.str ) ;
		}
		
		JSONObject unitsArray = _JsonObj.GetField( "units" ) ;
		
		if( null != unitsArray && 
			unitsArray.type == JSONObject.Type.ARRAY )
		{
			Debug.Log( "JSonParser01::ParseLevel() unitsArray.type == JSONObject.Type.ARRAY" ) ;
			
			foreach( JSONObject unitNode in unitsArray.list )
			{
				if( true == unitNode.HasField( "name" )  )
				{
					string unitName = unitNode.GetField( "name" ).str ;
					Debug.Log( "JSonParser01::ParseLevel() unitName=" + unitName ) ;
				}
				
				if( true == unitNode.HasField( "position3D" )  )
				{
					JSONObject positione3DNode = unitNode.GetField( "position3D" ) ;
					
					if( true == positione3DNode.HasField( "x" ) &&
						true == positione3DNode.HasField( "y" ) &&
						true == positione3DNode.HasField( "z" ) )
					{
						Debug.Log( "JSonParser01::ParseLevel() x=" + 
									positione3DNode.GetField("x").str + 
							",y=" + positione3DNode.GetField("y").str + 
							",z=" + positione3DNode.GetField("z").str ) ;
					}
				}
			}// end of foreach
		}		
		
		Debug.Log( "JSonParser01:ParseLevel() end." ) ;
	}
}
