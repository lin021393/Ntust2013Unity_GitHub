/*
@file CreateStaticObject.cs
@author NDark
@date 20130825 file started.
*/
using UnityEngine;

public class CreateStaticObject : MonoBehaviour 
{
	public string m_StaticObjName = "StaticVersionText" ;
	public string m_PrefabName = "Common/Prefabs/StaticVersionText" ;
	// Use this for initialization
	void Start () 
	{
		GameObject staicObj = GameObject.Find( m_StaticObjName ) ;
		if( null == staicObj )
		{
			Debug.Log( "CreateStaticObject() staicObj do not exist , create it." ) ;
			Object prefab = Resources.Load( m_PrefabName ) ;
			if( null == prefab )
			{
				Debug.LogError( "CreateStaticObject() null == prefab." ) ;
			}
			else
			{
				staicObj = (GameObject) GameObject.Instantiate( prefab ) ; 
				staicObj.name = m_StaticObjName ;
			}
		}
		else
		{
			Debug.Log( "CreateStaticObject() staicObj do exist , do nothing." ) ;
		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
