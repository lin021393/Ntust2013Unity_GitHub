/*
@file LevelGeneration08.cs
@author NDark
@date 20130816 . file created.
 */
using UnityEngine;
using System.Xml ;

public class LevelGeneration08 : MonoBehaviour 
{
	public string m_LevelFilePath = "Common/Data/Level009" ;
	public EventManager01 m_EventManager = null ;
	
	public GameObject InstaniateObject( string _ObjName , 
										 string _PrefabName , 
										 Vector3 _Pos , 
										 Quaternion _Rotation ) 
	{
		Debug.Log( "_ObjName=" + _ObjName + " _PrefabName" + _PrefabName ) ;
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
	
	// Use this for initialization
	void Start () 
	{
		if( null == m_EventManager )
		{
			m_EventManager = this.gameObject.GetComponent<EventManager01>() ;
		}
		
		InitializeLevel() ;
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	private void InitializeLevel()
	{
		Debug.Log( "LevelGeneration08:InitializeLevel() start." ) ;
		
		LoadLevelFile( m_LevelFilePath ) ;
		Debug.Log( "LevelGeneration08:InitializeLevel() LoadLevelFile() done." ) ;
		
		Debug.Log( "LevelGeneration08:InitializeLevel() end." ) ;
	}
	
	private void LoadLevelFile( string _LevelFilePath )
	{
		XmlDocument doc = new XmlDocument() ;
		TextAsset textAsset = (TextAsset) Resources.Load( _LevelFilePath ) ;
		if( null != textAsset )
		{
			doc.LoadXml( textAsset.text ) ;	
			ParseLevel( doc ) ;
		}
		Debug.Log( "LevelGeneration08:LoadLevelFile() end." ) ;
	}
	
	private void ParseLevel( XmlDocument _Doc )
	{
		Debug.Log( "LevelGeneration08:ParseLevel() start." ) ;
		XmlNode rootNode = _Doc.FirstChild ;
		if( null == rootNode || false == rootNode.HasChildNodes )
		{
			Debug.LogError( "LevelGeneration08:ParseLevel() null == rootNode || false == rootNode.HasChildNodes." ) ;
			return ;
		}

		if( true == rootNode.HasChildNodes )
		{
			string unitName = "" ;
			string prefabName = "" ;
			string componentName = "" ;
			Vector3 unitPos = Vector3.zero ;
			Quaternion unitOrientation = Quaternion.identity ;
			for( int i = 0 ; i < rootNode.ChildNodes.Count ; ++i )
			{
				if( "Unit" == rootNode.ChildNodes[ i ].Name )
				{
					if( true == XMLParseUtilityPartial03.ParseUnitObject( rootNode.ChildNodes[ i ] ,
															  ref unitName , 
															  ref prefabName , 
															  ref unitPos , 
															  ref unitOrientation ) )
					{
						GameObject addObj = InstaniateObject( unitName , 
							prefabName , unitPos , unitOrientation ) ;
						if( null != addObj )
						{
							Debug.Log( "LevelGeneration08:ParseLevel() addObj." ) ;
						}																		
					}
				}
				else if( "Component" == rootNode.ChildNodes[ i ].Name )
				{
					if( true == XMLParseUtilityPartial03.ParseComponent( rootNode.ChildNodes[ i ] ,
															  ref unitName , 
															  ref componentName
															   ) )
					{
						Debug.Log( "LevelGeneration08:ParseLevel() add component:" + unitName + " " + componentName ) ;
						GameObject addObj = GameObject.Find( unitName ) ;
						if( null != addObj )
						{
							addObj.AddComponent( componentName ) ;
						}
						else
						{
							Debug.LogError( "LevelGeneration08:ParseLevel() null == addObj" ) ;
						}
					}					
				}
				else if( "ConditionEvent" == rootNode.ChildNodes[ i ].Name )
				{
					// Debug.Log( "ConditionEvent" ) ;
					
					XmlNode conditionEventNode = rootNode.ChildNodes[ i ] ;
					if( null != conditionEventNode.Attributes[ "ConditionEventName" ] )
					{
						string conditionEventName = conditionEventNode.Attributes[ "ConditionEventName" ].Value ;
						Debug.Log( "ConditionEventName=" + conditionEventName ) ;
						
						ConditionEvent conditionEvent = ConditionEventFactory.GetByString( conditionEventName ) ;
						if( true == conditionEvent.ParseXML( conditionEventNode ) )
						{
							m_EventManager.m_EventList.Add( conditionEvent ) ;
						}
						
					}
				}
			}
		}
		Debug.Log( "LevelGeneration08:ParseLevel() end." ) ;
	}
	

}
