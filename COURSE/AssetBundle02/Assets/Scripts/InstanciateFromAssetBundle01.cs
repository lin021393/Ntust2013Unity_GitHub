/*
@file InstanciateFromAssetBundle01.cs
@author NDark
@date 20130821 file started.
*/
using UnityEngine;

public class InstanciateFromAssetBundle01 : MonoBehaviour 
{
	public bool m_LoadFromLocal = true ;
	public string m_Unity3DURL = "" ;
	public string m_LocalURLTag = "file:///" ;
	public string m_LocalDirectory = "D:/Workspace/Project/Ntust2013Unity/COURSE/AssetBundle02/";
	public string m_LocalAssetBundleName = "BuildSelectedAssets_Cube.unity3d";
	
	
	public string m_PrefabName = "Cube" ;
	// Use this for initialization
	void Start () 
	{
		if( true == m_LoadFromLocal )
		{
			m_Unity3DURL = m_LocalURLTag + m_LocalDirectory + m_LocalAssetBundleName ;
		}
		else
		{
			// use m_Unity3DURL as parameter
		}
		Debug.Log( m_Unity3DURL ) ;
	
		if( 0 == m_Unity3DURL.Length )
			return ;	
	
		WWW download = WWW.LoadFromCacheOrDownload( m_Unity3DURL , 0 ) ;
		if( null == download )
		{
			Debug.LogError( "null == download" ) ;
		}
		else
		{
			
            AssetBundle assetBundle = null ;
			
			// Try mark this line
			assetBundle = download.assetBundle;
			// assetBundle.mainAsset
			if( null == assetBundle )
			{
				Debug.Log( "null == assetBundle" ) ;
			}
			else
			{
				Debug.Log( "null != assetBundle" ) ;
					
				Object prefab = assetBundle.Load( m_PrefabName , typeof( GameObject ) ) ;
				if( null == prefab )
				{
					Debug.Log( "null == prefab" ) ;
				}
				else
				{
					GameObject obj = (GameObject) GameObject.Instantiate( prefab ) ;
					obj.name = "NewCube" ;
				}
			}	
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
