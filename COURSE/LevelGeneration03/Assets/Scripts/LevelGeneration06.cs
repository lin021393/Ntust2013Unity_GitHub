/*
 * @file LevelGeneration06.cs
 * @author NDark
 * @date 20130623 . file created.
 */
using UnityEngine;
using System.Collections;

public class LevelGeneration06 : MonoBehaviour 
{
	public string m_LevelFilePath = "Common/Data/Level008" ;

	// Use this for initialization
	void Start () 
	{
		InitializeLevel() ;
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	private void InitializeLevel()
	{
		Debug.Log( "LevelGeneration06:InitializeLevel() start." ) ;
		
		LoadLevelFile( m_LevelFilePath ) ;
		Debug.Log( "LevelGeneration06:InitializeLevel() LoadLevelFile() done." ) ;
		
		Debug.Log( "LevelGeneration06:InitializeLevel() end." ) ;
	}
	
	private void LoadLevelFile( string _LevelFilePath )
	{
		TextAsset textAsset = (TextAsset) Resources.Load( _LevelFilePath ) ;
		if( null != textAsset )
		{
			JSONObject jsObj = new JSONObject( textAsset.text );
			ParseLevel( jsObj ) ;
			Debug.Log( "LevelGeneration06:LoadLevelFile() ParseLevel()." ) ;	
		}

		Debug.Log( "LevelGeneration06:LoadLevelFile() end." ) ;
	}
	
	private void ParseLevel( JSONObject _JsonObj )
	{
		if( null == _JsonObj )
			return ;

		JSONObject nameNode = _JsonObj.GetField( "name" ) ;
		if( null != nameNode )
		{
			Debug.Log( "LevelGeneration06::ParseLevel() nameNode.str=" + nameNode.str ) ;
		}
		
		JSONObject unitsArray = _JsonObj.GetField( "units" ) ;
		
		if( null != unitsArray && 
			unitsArray.type == JSONObject.Type.ARRAY )
		{
			Debug.Log( "LevelGeneration06::ParseLevel() unitsArray.type == JSONObject.Type.ARRAY" ) ;
			
			string unitName = "" ;
			string prefabName = "" ;
			Vector3 unitPos = Vector3.zero ;
			Quaternion unitOrientation = Quaternion.identity ;			
			foreach( JSONObject unit in unitsArray.list )
			{
				if( true == JSonParseUtility01.ParseUnitObject( unit ,
														  ref unitName , 
														  ref prefabName , 
														  ref unitPos , 
														  ref unitOrientation ) )
				{
					GameObject addObj = InstaniateObject( unitName , 
						prefabName , 
						unitPos , 
						unitOrientation ) ;
					
					if( null != addObj )
					{
						Debug.Log( "LevelGeneration05:ParseLevel() addObj." ) ;
					}																		
				}			
			}
		}		
		
		Debug.Log( "LevelGeneration06:ParseLevel() end." ) ;
	}
	
	private GameObject InstaniateObject( string _ObjName , 
										 string _PrefabName , 
										 Vector3 _Pos , 
										 Quaternion _Rotation ) 
	{
		GameObject ret = null ;
		Object chessPawnPrefab = Resources.Load( _PrefabName ) ;
		if( null != chessPawnPrefab )
		{
			GameObject chessPawnObj = (GameObject) GameObject.Instantiate( chessPawnPrefab ) ;
			if( null != chessPawnObj )
			{
				chessPawnObj.transform.position = _Pos  ;
				chessPawnObj.transform.rotation = _Rotation ;
				chessPawnObj.name = _ObjName ;
				ret = chessPawnObj ;
			}
		}
		return ret;
	}	
}
