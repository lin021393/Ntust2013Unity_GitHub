/*
@file RayHitAddScriptOnObj01.cs
@author NDark
@date 20140803 file started.
*/
using UnityEngine;

public class RayHitAddScriptOnObj01 : MonoBehaviour 
{
	static GameObject m_GUIParent = null ;

	// Use this for initialization
	void Start () 
	{
		if( null == m_GUIParent )
		{
			m_GUIParent = GameObject.Find( "GUIParent" ) ;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		Transform thisTransform = this.transform ;
		Ray ray = new Ray( thisTransform.position , 
						   thisTransform.forward ) ;
		RaycastHit hitInfo ;
		if( true == Physics.Raycast( ray , out hitInfo ) )
		{
			// Debug.Log( hitInfo.collider.gameObject.name ) ;
			string guiObjName = "GUI_" + hitInfo.collider.gameObject.name ;
			if( null != m_GUIParent )
			{
				Transform trans = m_GUIParent.transform.FindChild( guiObjName ) ;
				if( null != trans )
				{
					AddScripts( trans.gameObject ) ;
					
					m_GUIParent.audio.Play() ;
				}
			}
			Component.Destroy( this ) ;
		}
	}
	
	private void AddScripts( GameObject _TargetObj )
	{
		if( null != _TargetObj )
		{
			if( null == _TargetObj.GetComponent<GUITextureShake01>() )
			{
				_TargetObj.AddComponent<GUITextureShake01>() ;
			}
			if( null == _TargetObj.GetComponent<GUITextureColor01>() )
			{
				_TargetObj.AddComponent<GUITextureColor01>() ;
			}			
		}		
	}
}
