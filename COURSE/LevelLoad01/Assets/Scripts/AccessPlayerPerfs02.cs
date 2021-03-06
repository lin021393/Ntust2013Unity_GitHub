/*
@file AccessPlayerPerfs02.cs
@author NDark
@date 20130811 by NDark . file started and derived from AccessPlayerPerfs01
*/
using UnityEngine;
using System.Collections.Generic;

public class AccessPlayerPerfs02 : MonoBehaviour 
{
	// key -> value
	public Dictionary<string,string> m_Map = new Dictionary<string,string>() ;
	
	public Rect m_WindowLeft = new Rect( 100 , 100 , 200 , 600 ) ;
	
	private string [] m_TempKeysVector = null ; // initialize at InitilaizaeKeys()
	
	// set m_Map[] to player perfs
	public void SetPlayerPerfs()
	{
		Dictionary<string,string>.Enumerator iMap = m_Map.GetEnumerator() ;
		while( iMap.MoveNext() ) 		
		{
			PlayerPrefs.SetString( iMap.Current.Key , iMap.Current.Value ) ;
			
			Debug.Log( "AccessPlayerPerfs02::SetPlayerPerfs() " + 
				"key=" + iMap.Current.Key + " value=" + iMap.Current.Value ) ;
		}
	}	
	
	// Use this for initialization
	void Start () 
	{
		// 初始化要用來取值的已知陣列
		InitilaizaeKeys() ;
		
		// 依照已知陣列取值
		RetrievePlayerPerfs() ;
	}
	
	// Update is called once per frame
	void Update () 
	{
	}
	
	private void InitilaizaeKeys()
	{
		m_Map.Clear() ;
		m_Map.Add( "PlayerAccount" , "" ) ;
		m_Map.Add( "PlayerPassword" , "" ) ;
		
		m_TempKeysVector = new string[ m_Map.Count ] ;
	}
	
	// retrieve player perfs to m_Map[]
	private void RetrievePlayerPerfs()
	{
		Debug.Log( "AccessPlayerPerfs02::RetrievePlayerPerfs() start." ) ;						
		
		// m_Map.Keys[] -> m_TempKeysVector
		m_Map.Keys.CopyTo( m_TempKeysVector , 0 ) ;
		
		string mapKey = "" ;
		string mapValue = "" ;

		for( int i = 0 ; i < m_TempKeysVector.Length ; ++i )
		{
			mapKey = m_TempKeysVector[ i ] ;
			if( true == PlayerPrefs.HasKey( mapKey ) )
			{
				mapValue = PlayerPrefs.GetString( mapKey ) ;
				m_Map[ mapKey ] = mapValue ;// assign to m_Map[]

				Debug.Log( "AccessPlayerPerfs02::RetrievePlayerPerfs() " + 
					"key=" + mapKey + " value=" + mapValue ) ;
			}
		}
	}
	

}

