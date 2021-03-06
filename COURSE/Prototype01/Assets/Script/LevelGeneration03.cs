/*
@file LevelGeneration03.cs
@author NDark
@date 20130622 . file created.
@date 20130714 . fix an index error at InitializeAlienUnits()
@date 20130721 
. add coment and error log.
 */
using UnityEngine;

public class LevelGeneration03 : MonoBehaviour 
{
	public Vector3 m_FleetStartPos = new Vector3( -6 , 1 , 1 ) ;// 隊伍的基準點
	public float m_UnitSpaceInX = 1.5f ;// 橫向間距
	public float m_UnitSpaceInZ = 1.0f ;// 縱向間距
	
	public GameObject m_AlienCollector = null ;
	// Use this for initialization
	void Start () 
	{
		if( null == m_AlienCollector )
			InitializeAlienCollector() ;
		
		InitializeLevel() ;	
	}
	
	// Update is called once per frame
	void Update () 
	{
	}
	
	private void InitializeAlienCollector()
	{
		m_AlienCollector = GameObject.Find( "AlienCollector" ) ;
		
		if( null == m_AlienCollector )
		{
			Debug.LogError( "LevelGeneration03:InitializeAlienCollector() null == m_AlienCollector" ) ;
		}
		else
		{
			Debug.Log( "LevelGeneration03:InitializeAlienCollector() end." ) ;
		}		
	}
	
	private void InitializeLevel()
	{
		Debug.Log( "LevelGeneration03:InitializeLevel() start." ) ;
		
		InitializeBackgroundScene() ;
		
		InitializeMainCharacter() ;
		
		InitializeAlienUnits() ;
			
		Debug.Log( "LevelGeneration03:InitializeLevel() end." ) ;
	}
	
	private void InitializeBackgroundScene()
	{
		Object prefabObj = Resources.Load( "Common/Prefabs/SceneBackground001" ) ;
		if( null == prefabObj )
		{
			Debug.LogError( "LevelGeneration03:InitializeBackgroundScene() Resources.Load SceneBackground001 failed" ) ;
		}
		else 
		{
			GameObject sceneObj = (GameObject) GameObject.Instantiate( prefabObj ) ;
			if( null != sceneObj )
			{
				sceneObj.name = "SceneBackground001" ;
				Debug.Log( "LevelGeneration03:InitializeBackgroundScene() succeed." ) ;
			}
		}
	}
	
	private void InitializeMainCharacter()
	{
		Object prefabObj = Resources.Load( "Common/Prefabs/MainCharacter03" ) ;
		if( null == prefabObj )
		{
			Debug.LogError( "LevelGeneration03:InitializeMainCharacter() Resources.Load MainCharacter03 failed" ) ;
		}
		else 
		{
			GameObject sceneObj = (GameObject) GameObject.Instantiate( prefabObj ) ;
			if( null != sceneObj )
			{
				sceneObj.name = "MainCharacter" ;
				Debug.Log( "LevelGeneration03:InitializeMainCharacter() succeed." ) ;
			}
		}
	}
	
	private void InitializeAlienUnits()
	{
		Object prefabObj = Resources.Load( "Common/Prefabs/AlienUnit01" ) ;
		if( null == prefabObj )
		{
			Debug.LogError( "LevelGeneration03:InitializeAlienUnits() Resources.Load AlienUnit01 failed" ) ;
		}
		else 
		{
			int count = 0 ;
			for( int j = 0 ; j < 4 ; ++j )
			{
				for( int i = 0 ; i < 9 ; ++i )
				{
					int index = j * 9 + i ;
					GameObject sceneObj = (GameObject) GameObject.Instantiate( prefabObj ) ;
					if( null != sceneObj )
					{
						sceneObj.name = "AlienUnit" + index.ToString()  ;
						sceneObj.transform.position = new Vector3( i * m_UnitSpaceInX , 
																   0 , 
																   j * m_UnitSpaceInZ ) ;
						
						sceneObj.transform.position += m_FleetStartPos ;						
						if( null != m_AlienCollector )
						{
							sceneObj.transform.parent = m_AlienCollector.transform ;
						}
						++count ;
					}				
				}
			}
			Debug.Log( "LevelGeneration03:InitializeAlienUnits() count=" + count.ToString() ) ;
		}
	}		
}
