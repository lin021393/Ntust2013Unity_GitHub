using UnityEngine;
using System.Collections;

public class load : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		Debug.Log( "load" ) ;
		StartCoroutine(LoadBundle());
	}
	
	
    public IEnumerator LoadBundle()
	{
		Debug.Log( "LoadBundle" ) ;
        using( WWW download = WWW.LoadFromCacheOrDownload( "Streamed-Level1.unity3d" , 0 ) )
		{
			Debug.Log( "using( WWW download" ) ;
            yield return download;
			
			Debug.Log( "yield return download completed" ) ;
            AssetBundle assetBundle = download.assetBundle;
        }
    }
		
	// Update is called once per frame
	void Update () {
	
	}
}
