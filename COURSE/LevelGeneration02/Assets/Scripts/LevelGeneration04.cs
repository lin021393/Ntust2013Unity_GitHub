/*
@file LevelGeneration04.cs
@author NDark
@date 20130622 . file created.
@date 20130729 . comment and error log.
*/
using UnityEngine;
using System.Xml ; // for XmlDocument

public class LevelGeneration04 : MonoBehaviour 
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
		Debug.Log( "LevelGeneration04:InitializeLevel() start." ) ;
		
		InitializeChessBoard() ;
		Debug.Log( "LevelGeneration04:InitializeLevel() InitializeChessBoard() done." ) ;
		
		LoadLevelFile( m_LevelFilePath ) ;
		Debug.Log( "LevelGeneration04:InitializeLevel() LoadLevelFile() done." ) ;
		
		Debug.Log( "LevelGeneration04:InitializeLevel() end." ) ;
	}
	
	private void LoadLevelFile( string _LevelFilePath )
	{
		XmlDocument doc = new XmlDocument() ;
		TextAsset textAsset = 
			(TextAsset) Resources.Load( _LevelFilePath ) ;
		if( null == textAsset )
		{
			Debug.LogError( "LevelGeneration04:LoadLevelFile() Resources.Load failed." ) ;
		}
		else
		{
			doc.LoadXml( textAsset.text ) ;	
			ParseLevel( doc ) ;
		}
		Debug.Log( "LevelGeneration04:LoadLevelFile() end." ) ;
	}
	
	private void ParseLevel( XmlDocument _Doc )
	{
		Debug.Log( "LevelGeneration04:ParseLevel() start." ) ;
		XmlNode rootNode = _Doc.FirstChild ;
		if( null == rootNode || 
			false == rootNode.HasChildNodes )
		{
			Debug.LogError( "LevelGeneration04:ParseLevel() null == rootNode || false == rootNode.HasChildNodes." ) ;
			return ;
		}

		if( true == rootNode.HasChildNodes )
		{
			for( int i = 0 ; i < rootNode.ChildNodes.Count ; ++i )
			{
				if( "Unit" == rootNode.ChildNodes[ i ].Name )
				{
					XmlNode unitNode = rootNode.ChildNodes[ i ] ;
					
					if( null != unitNode.Attributes[ "name" ] )
					{
						string unitName = unitNode.Attributes[ "name" ].Value ;
						Vector3 unitPos = Vector3.zero ;
						Quaternion orientation = Quaternion.identity ;
						Debug.Log( "LevelGeneration04:ParseLevel() unitName=" + unitName ) ;
						
						for( int j = 0 ; j < unitNode.ChildNodes.Count ; ++j )
						{
							if( "Position3D" == unitNode.ChildNodes[ j ].Name )
							{
								XMLParseUtilityPartial01.ParsePosition( 
									unitNode.ChildNodes[ j ] , 
									ref unitPos ) ;
								
								Debug.Log( "LevelGeneration04:ParseLevel() unitPos=" + unitPos ) ;
							}
							else if( "OrientationEuler" == unitNode.ChildNodes[ j ].Name )
							{
								XMLParseUtilityPartial01.ParseQuaternion( 
									unitNode.ChildNodes[ j ] , 
									ref orientation ) ;
								
								Debug.Log( "LevelGeneration04:ParseLevel() orientation=" + orientation ) ;
							}							
						}
						
						GameObject addObj = InitializeChessPawn( unitPos , 
																 orientation ) ;
						if( null != addObj )
						{
							addObj.name = unitName ;
							Debug.Log( "LevelGeneration04:ParseLevel() addObj.name=" + addObj.name ) ;
						}												
					}
				}
			}
		}
		Debug.Log( "LevelGeneration04:ParseLevel() end." ) ;
	}
	
	private void InitializeChessBoard() 
	{
		Object chessBoardPrefab = Resources.Load( "Common/Prefabs/ChessBoard" ) ;
		if( null == chessBoardPrefab )
		{
			Debug.LogError( "LevelGeneration04:InitializeChessBoard() Resources.Load ChessBoard failed." ) ;
		}
		else
		{
			GameObject chessBoardObj = 
				(GameObject) GameObject.Instantiate( chessBoardPrefab ) ;
			
			if( null != chessBoardObj )
			{
				chessBoardObj.name = "ChessBoard" ;
				chessBoardObj.transform.position = new Vector3( 5 , 0 , 5 ) ;
			}
		}
	}

	private GameObject InitializeChessPawn( Vector3 _Pos , Quaternion _Rotation ) 
	{
		GameObject ret = null ;
		Object chessPawnPrefab = 
			Resources.Load( "Common/Prefabs/ChessPawn02" ) ;
		
		if( null == chessPawnPrefab )
		{
			Debug.LogError( "LevelGeneration04:InitializeChessPawn() Resources.Load ChessPawn02 failed." ) ;
		}		
		else
		{
			GameObject chessPawnObj = 
				(GameObject) GameObject.Instantiate( chessPawnPrefab ) ;
			
			if( null != chessPawnObj )
			{
				chessPawnObj.transform.position = _Pos  ;
				chessPawnObj.transform.rotation = _Rotation ;
				ret = chessPawnObj ;
			}
		}
		return ret;
	}	
}
