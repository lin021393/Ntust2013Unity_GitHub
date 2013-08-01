/**
@file OnCollideAddScriptOnObj01.cs
@author NDark
@date 20130801 file started.
*/
using UnityEngine;

public class OnCollideAddScriptOnObj01 : MonoBehaviour 
{
	public GameObject m_TargetObj = null ;
	
	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{	
	}
	
	void OnCollisionEnter( Collision other )
	{
		if( null != m_TargetObj )
		{
			if( null == m_TargetObj.GetComponent<GUITextureShake01>() )
			{
				m_TargetObj.AddComponent<GUITextureShake01>() ;
			}
			if( null == m_TargetObj.GetComponent<GUITextureColor01>() )
			{
				m_TargetObj.AddComponent<GUITextureColor01>() ;
			}			
		}
	}
	
}
