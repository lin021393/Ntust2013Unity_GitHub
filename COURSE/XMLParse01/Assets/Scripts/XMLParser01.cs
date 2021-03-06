/*
 * @file XMLParser01.cs
 * @author NDark
 * @date 20130622 . file started.
 */
using UnityEngine;
using System.Collections;
using System.Xml;

public class XMLParser01 : MonoBehaviour 
{
	public string m_LevelFilePath = "Common/Data/Level001" ;

	// Use this for initialization
	void Start () 
	{
		LoadLevelFile( m_LevelFilePath ) ;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	private void LoadLevelFile( string _LevelFilePath )
	{
		TextAsset textAsset = 
			(TextAsset) Resources.Load( _LevelFilePath ) ;
		
		XmlDocument doc = new XmlDocument() ;
		
		if( null != textAsset )
		{
			doc.LoadXml( textAsset.text ) ;	
			ParseLevel( doc ) ;
		}
		Debug.Log( "XMLParser01:LoadLevelFile() end." ) ;
	}
	
	private void ParseLevel( XmlDocument _Doc )
	{
		Debug.Log( "XMLParser01:ParseLevel() start." ) ;
		
		XmlNode rootNode = _Doc.FirstChild ;
		if( null == rootNode || 
			false == rootNode.HasChildNodes )
		{
			Debug.LogError( "XMLParser01:ParseLevel() null == rootNode || false == rootNode.HasChildNodes." ) ;
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
						Debug.Log( "XMLParser01:ParseLevel() unitName=" + unitName ) ;
						for( int j = 0 ; j < unitNode.ChildNodes.Count ; ++j )
						{
							if( "Position3D" == unitNode.ChildNodes[ j ].Name )
							{
								XMLParseUtilityPartial01.ParsePosition( 
									unitNode.ChildNodes[ j ] , 
									ref unitPos ) ;
								
								Debug.Log( "XMLParser01:ParseLevel() unitPos=" + 
									unitPos ) ;
							}
						}
												
					}
				}
			}
		}
		Debug.Log( "XMLParser01:ParseLevel() end." ) ;
	}
}
