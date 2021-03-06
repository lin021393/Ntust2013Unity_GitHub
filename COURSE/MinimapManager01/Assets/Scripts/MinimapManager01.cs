/**
@file MinimapManager01.cs
@author NDark
@date 20130609 . file started.
@date 20130714 by NDark . comment and format.
@date 20130721 by NDark . add comment.
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MinimapManager01 : MonoBehaviour 
{
	// 單位與小地圖物件的組合(pair)
	[System.Serializable]
	public class ObjectPair
	{
		public GameObject UnitObj = null ;
		public GameObject MiniMapObj = null ;
		
		public ObjectPair()
		{
		}
		
		public ObjectPair( GameObject _UnitObj , 
						   GameObject _MiniMapObj )
		{
			UnitObj = _UnitObj ;
			MiniMapObj = _MiniMapObj ;
		}
	}
		
	/* 
	 小地圖物件都存在容器中
	 依照物件名稱收集的所有小地圖物件
	 */
	public Dictionary< string , ObjectPair > m_MiniMapPairs = new Dictionary<string, ObjectPair>() ;
	private GameObject m_TrafficSignalParent = null ;	
	
	
	// Use this for initialization
	void Start () 
	{
		InitializTrafficSignalParent() ;
		InitializPairs() ;
	}
	
	// Update is called once per frame
	void Update () 
	{
		UpdatePos() ;			
	}
	
	private void InitializTrafficSignalParent()
	{
		// the parent of objects at minimap
		m_TrafficSignalParent = GameObject.Find( "TrafficSignalParent" ) ;
		
		if( null == m_TrafficSignalParent )
		{
			Debug.LogError( "MinimapManager01:InitializTrafficSignalParent() null == m_TrafficSignalParent" ) ;
		}		
		else
		{
			Debug.Log( "MinimapManager01:InitializTrafficSignalParent() end" ) ;
		}
	}
	
	private void InitializPairs()
	{
		// the parent of object at real map
		GameObject realMapParentObj = GameObject.Find( "TrafficLightParent" ) ;
		if( null == realMapParentObj )
		{
			Debug.LogError( "MinimapManager01:InitializPairs() null == realMapParentObj" ) ;
			return ;
		}
		
		ObjectPair newPair = null ;
		Transform trans = null ;
		GameObject realObj = null ;
		string trafficLightName = "";
		for( int i = 0 ; i < 3 ; ++i )
		{
			// use this will result in TrafficLight0 , TrafficLight1 , TrafficLight2
			// trafficLightName = "TrafficLight" + i.ToString() ;
			
			// use this will result in TrafficLight00 , TrafficLight01 , TrafficLight02
			trafficLightName = string.Format( "TrafficLight{0:00}" , i ) ;
			
			
			// find child at realmap
			trans = realMapParentObj.transform.FindChild( trafficLightName ) ;
			if( null != trans )
			{
				realObj = trans.gameObject ;
				
				Object prefabObj = Resources.Load( "Prefabs/TrafficSignal" ) ;
				if( null != prefabObj )
				{
					GameObject newSingalObject = (GameObject) GameObject.Instantiate( prefabObj ) ;
					
					newSingalObject.name = "TrafficSignal_" + trafficLightName ;
					
					// collect this object under parent
					if( null != m_TrafficSignalParent )
					{
						newSingalObject.transform.parent = m_TrafficSignalParent.transform ;
					}
					
					// add in dictionary
					newPair = new ObjectPair() ;
					newPair.UnitObj = realObj ;
					newPair.MiniMapObj = newSingalObject ;
					m_MiniMapPairs.Add( trafficLightName , newPair ) ;
				}
			}
		}
		
	}
	
	private void UpdatePos()
	{
		Dictionary<string,ObjectPair>.Enumerator ePair = m_MiniMapPairs.GetEnumerator() ;
		while( ePair.MoveNext() )
		{
			// string unitName = eInMinimap.Current.Key ;
			GameObject unitObj = ePair.Current.Value.UnitObj ;
			GameObject miniMapObj = ePair.Current.Value.MiniMapObj ;
			if( null != unitObj && 
				null != miniMapObj )
			{
				float originalMinimapY = miniMapObj.transform.position.y ;
				miniMapObj.transform.position = new Vector3(
					unitObj.transform.position.x ,
					originalMinimapY ,					 
					unitObj.transform.position.z ) ;
			}
		}
	}	
}
