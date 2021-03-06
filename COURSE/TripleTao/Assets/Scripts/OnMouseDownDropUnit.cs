/*
@file OnMouseDownDropUnit.cs
@author NDark
@date 20130826 file started
*/
using UnityEngine;

public class OnMouseDownDropUnit : MonoBehaviour 
{
	static TripleTaoManager s_Manager = null ;
	UnitData m_UnitData = null ;
	
	// Use this for initialization
	void Start () 
	{
		if( null == s_Manager )
		{
			GameObject globalSingleton = GameObject.Find( "GlobalSingleton" ) ;
			if( null != globalSingleton )
			{
				s_Manager = globalSingleton.GetComponent<TripleTaoManager>() ;
			}
		}
		
		if( null == m_UnitData )
		{
			m_UnitData = this.gameObject.GetComponent<UnitData>() ;
		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnMouseDown() 
	{
		Vector3 placePos = this.gameObject.transform.position ;
		placePos.y = 0.1f ;// a little bit up than position.
		
		if( null != s_Manager && 
			null != m_UnitData )
		{
			s_Manager.Drop( m_UnitData.m_IndexI , 
							m_UnitData.m_IndexJ , 
							placePos ) ;
		}
	}
}
