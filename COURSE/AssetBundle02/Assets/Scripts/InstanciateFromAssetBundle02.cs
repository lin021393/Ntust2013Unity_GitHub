using UnityEngine;
using System.Collections;

public class InstanciateFromAssetBundle02 : MonoBehaviour 
{
	public string m_PrefabName = "" ;
	public string m_ObjectName = "" ;
	
	LoadAssetBundle02 m_LoadAssetBundle = null ;
	// Use this for initialization
	void Start () 
	{
		m_LoadAssetBundle = this.gameObject.GetComponent<LoadAssetBundle02>() ;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI()
	{
		GUILayout.Label( "Prefab Name" ) ;
		m_PrefabName = GUILayout.TextArea( m_PrefabName ) ;
		
		GUILayout.Label( "Object Name" ) ;
		m_ObjectName = GUILayout.TextArea( m_ObjectName ) ;
		if( true == GUILayout.Button( "Instanciate" ) )
		{
			Object prefab = m_LoadAssetBundle.m_AssetBundle.Load( m_PrefabName , typeof( GameObject ) ) ;
			if( null == prefab )
			{
				Debug.Log( "null == prefab : " + m_PrefabName ) ;
				return ;
			}
			GameObject gameObj = (GameObject) GameObject.Instantiate( prefab ) ;
			
			if( null != gameObj )
			{
				gameObj.name = m_ObjectName ;				
				Debug.Log( "gameObj created : " + gameObj.name ) ;
			}
		}
	}
}
