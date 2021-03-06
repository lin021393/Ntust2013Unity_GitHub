/*
@file LevelGeneration05.cs
@author NDark
@date 20130622 . file created.
@date 20130729 . comment and error log.
*/
using UnityEngine;
using System.Xml ;// for XmlDocument

public class LevelGeneration05 : MonoBehaviour 
{
	public string m_LevelFilePath = "Common/Data/Level006" ;

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
		Debug.Log( "LevelGeneration05:InitializeLevel() start." ) ;
		
		LoadLevelFile( m_LevelFilePath ) ;
		Debug.Log( "LevelGeneration05:InitializeLevel() LoadLevelFile() done." ) ;
		
		Debug.Log( "LevelGeneration05:InitializeLevel() end." ) ;
	}
	
	private void LoadLevelFile( string _LevelFilePath )
	{
		XmlDocument doc = new XmlDocument() ;
		TextAsset textAsset = 
			(TextAsset) Resources.Load( _LevelFilePath ) ;
		if( null == textAsset )
		{
			Debug.LogError( "LevelGeneration05:LoadLevelFile() Resources.Load failed." ) ;
		}
		else 
		{
			doc.LoadXml( textAsset.text ) ;	
			ParseLevel( doc ) ;
		}
		Debug.Log( "LevelGeneration05:LoadLevelFile() end." ) ;
	}
	
	private void ParseLevel( XmlDocument _Doc )
	{
		Debug.Log( "LevelGeneration05:ParseLevel() start." ) ;
		XmlNode rootNode = _Doc.FirstChild ;
		if( null == rootNode || 
			false == rootNode.HasChildNodes )
		{
			Debug.LogError( "LevelGeneration05:ParseLevel() null == rootNode || false == rootNode.HasChildNodes." ) ;
			return ;
		}

		if( true == rootNode.HasChildNodes )
		{
			string unitName = "" ;
			string prefabName = "" ;
			Vector3 unitPos = Vector3.zero ;
			Quaternion unitOrientation = Quaternion.identity ;
			
			for( int i = 0 ; i < rootNode.ChildNodes.Count ; ++i )
			{
				if( "Unit" == rootNode.ChildNodes[ i ].Name )
				{
					if( true == 
						XMLParseUtilityPartial02.ParseUnitObject( rootNode.ChildNodes[ i ] ,
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
			}// end of for
		}
		Debug.Log( "LevelGeneration05:ParseLevel() end." ) ;
	}
	
	private GameObject InstaniateObject( string _ObjName , 
										 string _PrefabName , 
										 Vector3 _Pos , 
										 Quaternion _Rotation ) 
	{
		GameObject ret = null ;
		Object chessPawnPrefab = Resources.Load( _PrefabName ) ;
		if( null == chessPawnPrefab )
		{
			Debug.LogError( "LevelGeneration05:InstaniateObject() Resources.Load failed _PrefabName=" + _PrefabName ) ;
		}
		else 
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
