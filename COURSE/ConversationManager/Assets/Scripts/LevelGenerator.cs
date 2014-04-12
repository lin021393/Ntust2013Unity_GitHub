/*
@file LevelGenerator.cs
@author NDark
@date 20140329 file started.
@date 20140412 by NDark
. add class member m_UnitDataTemplateTable
. add class member m_UnitDataTemplateFilepath
. add class method LoadUnitDataTemplate()
. 在 LoadLevel() 中 加入 UnitDataTemplate 的屬性

*/
#define USE_XML
using UnityEngine;
using System.Collections.Generic;
using System.Xml;

public class LevelGenerator : MonoBehaviour 
{
	public string LevelFilepath 
	{
		get { return m_LevelFilepath ; } 
		set { m_LevelFilepath = value ;}
	}
	public string m_LevelFilepath = "" ;

	public string UnitDataTemplateFilepath 
	{
		get { return m_UnitDataTemplateFilepath ; } 
		set { m_UnitDataTemplateFilepath = value ;}
	}
	public string m_UnitDataTemplateFilepath = "" ;

	public Dictionary<string,UnitDataParam> m_UnitDataTemplateTable = new Dictionary<string, UnitDataParam>() ;

	// Use this for initialization
	void Start () 
	{
		RetrieveLevelFilepath () ;
		LoadUnitDataTemplate( m_UnitDataTemplateFilepath ) ;
		LoadLevel( m_LevelFilepath ) ;
		GenerateObjectFromLevelData() ;
	}
	
	// Update is called once per frame
	void Update () 
	{
	}

	private void RetrieveLevelFilepath()
	{

	}

	public void LoadUnitDataTemplate( string _UnitDataTemplateFilepath )
	{
		Debug.Log( "_UnitDataTemplateFilepath=" + _UnitDataTemplateFilepath ) ;
		if( 0 == _UnitDataTemplateFilepath.Length )
		{
			// warning
			Debug.LogWarning( "_UnitDataTemplateFilepath=" + _UnitDataTemplateFilepath ) ;
			return ;
		}

		TextAsset ta = Resources.Load<TextAsset>( _UnitDataTemplateFilepath );
		if( null == ta )
		{
			Debug.LogError( "LoadUnitDataTemplate() null == ta" ) ;
			return ;
		}

		#if USE_XML
		XmlDocument doc = new XmlDocument() ;
		doc.LoadXml( ta.text ) ;
		XmlNode root = doc.FirstChild ;
		
		if( null == root )
		{
			Debug.LogError( "LoadUnitDataTemplate() : null == root" ) ;
			return ;
		}
		
		// string levelname = root.Attributes[ "name" ].Value ;
		if( false == root.HasChildNodes )
		{
			Debug.Log( "LoadUnitDataTemplate() : false == root.HasChildNodes" ) ;
			return ;			
		}
		#endif // USE_XML



		#if USE_XML
		for( int i = 0 ; i < root.ChildNodes.Count ; ++i )
		{
			XmlNode unitNode = root.ChildNodes[ i ] ;
		#endif // USE_XML

			string unitDataTemplateName = "";
			UnitDataParam unitData = new UnitDataParam() ;

			if( -1 != unitNode.Name.IndexOf( "comment" ) )
			{
				// comment
			}
			else if( true == ParseUtility.ParseUnitDataTemplateData( unitNode,
			                                                        ref unitDataTemplateName ,
			                                                        ref unitData ) )
			{
				Debug.Log( "m_UnitDataTemplateTable.Add " ) ;
				Debug.Log( "unitData.standardParameters.Count=" + unitData.standardParameters.Count ) ;
				m_UnitDataTemplateTable.Add( unitDataTemplateName , unitData ) ;
			}

		#if USE_XML
		}
		#endif // USE_XML		

	}
	public void LoadLevel( string _LevelFilepath )
	{
		Debug.Log( "_LevelFilepath=" + _LevelFilepath ) ;
		if( 0 == _LevelFilepath.Length )
		{
			// warning
			Debug.LogWarning( "_LevelFilepath=" + _LevelFilepath ) ;
			return ;
		}

		TextAsset ta = Resources.Load<TextAsset>( _LevelFilepath );
		if( null == ta )
		{
			Debug.LogError( "LoadLevel() null == ta" ) ;
			return ;
		}
#if USE_XML
		XmlDocument doc = new XmlDocument() ;
		doc.LoadXml( ta.text ) ;
		XmlNode root = doc.FirstChild ;

		if( null == root )
		{
			Debug.LogError( "LoadLevel() : null == root" ) ;
			return ;
		}
		
		// string levelname = root.Attributes[ "name" ].Value ;
		if( false == root.HasChildNodes )
		{
			Debug.Log( "LoadLevel() : false == root.HasChildNodes" ) ;
			return ;			
		}
#endif // USE_XML

#if USE_XML
		for( int i = 0 ; i < root.ChildNodes.Count ; ++i )
		{
			XmlNode unitNode = root.ChildNodes[ i ] ;
#endif // USE_XML
			string unitName = "";
			string prefabTemplateName = "";
			string unitDataTemplateName = "";

			Vector3 position = Vector3.zero ;
			PosAnchor posAnchor = new PosAnchor() ;
			Quaternion orientation = new Quaternion() ;
			string textureName = "";
			Dictionary<string,StandardParameter> standardParamMap = new Dictionary<string, StandardParameter>();

			// Debug.Log( "LoadLevel() : unitNode.Name=" + unitNode.Name ) ;
			
			if( -1 != unitNode.Name.IndexOf( "comment" ) )
			{
				// comment
			}


			else if( true == ParseUtility.ParseUnit( unitNode,
			                                        ref unitName ,
				                                                ref prefabTemplateName , 
			                                        ref unitDataTemplateName ,
			                                        ref posAnchor , 
				                                                ref orientation ,
			                                        ref standardParamMap ) )
			{
				GameObject obj = PrefabInstantiate.CreateByInit( prefabTemplateName , 
				                                                unitName , 
				                                                posAnchor.GetPosition() , 
				                                                orientation ) ;

				UnitDataParam unitData = null ;
				if( 0 != unitDataTemplateName.Length &&
				   true == m_UnitDataTemplateTable.ContainsKey( unitDataTemplateName ) )
				{
					Debug.Log( "unitDataTemplateName=" + unitDataTemplateName ) ;
					unitData = obj.AddComponent<UnitDataParam>() ;
					unitData.Import( m_UnitDataTemplateTable[ unitDataTemplateName ] ) ;
				}
				unitData.ImportStandardParameter( standardParamMap ) ;

				foreach( string key in unitData.standardParameters.Keys )
				{
					Debug.Log( "key=" + key ) ;
				}
			}
			else if( true == ParseUtility.ParseStaticObject( unitNode,
			                                                 ref unitName ,
			                                                 ref prefabTemplateName , 
			                                                 ref position , 
			                                                 ref orientation  ) )
			{
				/*GameObject obj =*/ PrefabInstantiate.CreateByInit( prefabTemplateName , 
				                                                unitName , 
				                                                position , 
				                                                orientation ) ;
			}
			else if( true == ParseUtility.ParseMapZoneObject( unitNode,
		                                                     ref unitName ,
		                                                     ref prefabTemplateName , 
		                                                     ref position , 
		                                                     ref orientation , 
			                                                 ref textureName ) )
			{
				GameObject obj = PrefabInstantiate.CreateByInit( prefabTemplateName , 
				                                                unitName , 
				                                                position , 
				                                                orientation ) ;

				if( 0 != textureName.Length )
				{
					obj.renderer.material = new Material( obj.renderer.material ) ;
					Texture tex = ResourceLoad.LoadTexture( textureName ) ;
					if( null == tex )
					{

					}
					else
					{
						obj.renderer.material.mainTexture = tex ;
					}
				}
			}
#if USE_XML
		}
#endif // USE_XML			

	}

	private void GenerateObjectFromLevelData()
	{

	}

}
