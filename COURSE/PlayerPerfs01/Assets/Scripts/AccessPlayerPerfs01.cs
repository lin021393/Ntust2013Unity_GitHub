/*
@file AccessPlayerPerfs01.cs
@author NDark

http://docs.unity3d.com/Documentation/ScriptReference/PlayerPrefs.html

@date 20130623 . file started.
@date 20130729 . comment and formmat
@date 20130730 by NDark . rename class member m_TempStringVector to m_TempKeysVector

*/
using UnityEngine;
using System.Collections.Generic;

public class AccessPlayerPerfs01 : MonoBehaviour 
{
	// key -> value
	public Dictionary<string,string> m_Map = new Dictionary<string,string>() ;
	
	public Rect m_WindowLeft = new Rect( 100 , 100 , 200 , 600 ) ;
	
	private string [] m_TempKeysVector = null ; // initialize at InitilaizaeKeys()
	
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
	
	void OnGUI()
	{
		if( null == m_TempKeysVector )
			return ;
		
		GUILayout.BeginArea( m_WindowLeft , "" ) ;
			GUILayout.BeginHorizontal() ;
			
				// keys
				GUILayout.BeginVertical() ;
					Dictionary<string,string>.Enumerator iMap = m_Map.GetEnumerator() ;
					while( iMap.MoveNext() ) 
					{
						GUILayout.Label( iMap.Current.Key ) ;			
					}
				GUILayout.EndVertical() ;
			
				// values
				GUILayout.BeginVertical() ;
			
					string mapKey = "" ;
					string mapValue = "" ;
					string resultValue = "" ;			
					
					for( int i = 0 ; i < m_TempKeysVector.Length ; ++i )
					{
						mapKey = m_TempKeysVector[ i ] ;
						mapValue = m_Map[ mapKey ] ;
						resultValue = GUILayout.TextField( mapValue ) ;			
						if( resultValue != mapValue )
						{
							m_Map[ mapKey ] = resultValue ;
						}
					}
				
					// button
					if( true == GUILayout.Button( "Apply" ) )
					{
						SetPlayerPerfs() ;
					}
				GUILayout.EndVertical() ;
		
			GUILayout.EndHorizontal() ;
		GUILayout.EndArea() ;
	}
	
	private void InitilaizaeKeys()
	{
		m_Map.Clear() ;
		m_Map.Add( "PlayerName" , "" ) ;
		m_Map.Add( "PlayerRace" , "" ) ;
		m_Map.Add( "PlayerLv" , "" ) ;
		m_Map.Add( "PlayerScore" , "" ) ;
		
		m_TempKeysVector = new string[ m_Map.Count ] ;
	}
	
	// retrieve player perfs to m_Map[]
	private void RetrievePlayerPerfs()
	{
		Debug.Log( "AccessPlayerPerfs01::RetrievePlayerPerfs() start." ) ;						
		
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

				Debug.Log( "AccessPlayerPerfs01::RetrievePlayerPerfs() " + 
					"key=" + mapKey + " value=" + mapValue ) ;
			}
		}
	}
	
	// set m_Map[] to player perfs
	private void SetPlayerPerfs()
	{
		Dictionary<string,string>.Enumerator iMap = m_Map.GetEnumerator() ;
		while( iMap.MoveNext() ) 		
		{
			PlayerPrefs.SetString( iMap.Current.Key , iMap.Current.Value ) ;
			
			Debug.Log( "AccessPlayerPerfs01::SetPlayerPerfs() " + 
				"key=" + iMap.Current.Key + " value=" + iMap.Current.Value ) ;
		}
	}	
}

