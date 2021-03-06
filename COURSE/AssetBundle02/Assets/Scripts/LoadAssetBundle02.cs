/*
@file LoadAssetBundle02.cs
@author NDark
@date 20130821 file started.
*/
using UnityEngine;

public class LoadAssetBundle02 : MonoBehaviour 
{
	public AssetBundle m_AssetBundle = null ;
	public bool m_LoadFromLocal = true ;
	public string m_Unity3DURL = "" ;
	public string m_LocalURLTag = "file:///" ;
	public string m_LocalDirectory = "D:\\NDark\\Ntust2013_Unity_GitTrunk\\COURSE\\AssetBundle02\\";
	public string m_LocalAssetBundleName = "BuildWithExplicitAssetNames.unity3d";
	
	
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
			
			// Try mark this line
			m_AssetBundle = download.assetBundle;
			
			if( null == m_AssetBundle )
			{
				Debug.Log( "null == assetBundle" ) ;
			}
			else
			{
				Debug.Log( "null != assetBundle" ) ;
				Object[] objs = m_AssetBundle.LoadAll() ;
				Debug.Log( "objNumbers" + objs.Length ) ;
				foreach( Object obj in objs )
				{
					Debug.Log( obj.name ) ;
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
