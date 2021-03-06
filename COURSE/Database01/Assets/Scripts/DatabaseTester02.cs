/*
@file DatabaseTester02.cs
@author NDark
@date 20130928 file started
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DatabaseTester02 : MonoBehaviour 
{
	public dbAccess m_DB = null ;
	public string m_DataBaseFileName = "TestDB2.sqdb" ;
	public string m_DataBaseTable1Name = "UnitProperty" ;
	public string [] m_ColumnLabels = { "ID" , "Name" , "HP" , "Att" , "Def" } ;
	public string [] m_ValuesType = { "text" , "text" , "text" , "text" , "text" } ;
	public float m_FixedGridWidth = 100 ;
	
	public string [] m_InputValues = { "" , "" , "" , "" , "" } ;
	
	ArrayList m_DataBaseContent = new ArrayList() ;
	
	// Use this for initialization
	void Start () 
	{
		if( m_ColumnLabels.Length != m_ValuesType.Length )
			return ;
		m_DB = new dbAccess() ;
		m_DB.OpenDB( m_DataBaseFileName ) ;
		Debug.Log( "m_DB.OpenDB done" ) ;
		
		try
		{
			m_DB.CreateTable( m_DataBaseTable1Name , m_ColumnLabels , m_ValuesType ) ;
			Debug.Log( "m_DB.CreateTable done" ) ;
		}
		catch
		{
			
		}
		
		RefreshData() ;
		Debug.Log( "m_DataBaseContent" + m_DataBaseContent.Count ) ;		

	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnDestroy()
	{
		Debug.Log( "OnDestroy()" ) ;
		m_DB.CloseDB() ;
	}
	
	Vector2 m_ScrollViewPos = Vector2.zero ;
	void OnGUI()
	{

		GUILayout.BeginHorizontal();
		for( int i = 0 ; i < m_ColumnLabels.Length ; ++i )
		{
			GUILayout.Label( m_ColumnLabels[ i ].ToString() , GUILayout.Width (m_FixedGridWidth) );
		}
		GUILayout.EndHorizontal();
		
		GUILayout.BeginHorizontal();
		for( int i = 0 ; i < m_InputValues.Length ; ++i )
		{
			m_InputValues[ i ] = GUILayout.TextField( m_InputValues[ i ] , GUILayout.Width (m_FixedGridWidth) );
		}
		GUILayout.EndHorizontal();		
		if( true == GUILayout.Button( "AppendData" ) )
		{
			AppendData() ;
		}
		
		if( true == GUILayout.Button( "RefreshData" ) )
		{
			RefreshData() ;
		}
		
        GUILayout.Label("Database Contents");
        m_ScrollViewPos = GUILayout.BeginScrollView( m_ScrollViewPos , GUILayout.Height(300));
	
        for( int j = 0 ; j < m_DataBaseContent.Count ; ++j )
		{
			ArrayList row = (ArrayList)m_DataBaseContent[ j ] ;
			GUILayout.BeginHorizontal();
			for( int i = 0 ; i < row.Count ; ++i )
			{
				GUILayout.Label( row[ i ].ToString() , GUILayout.Width (m_FixedGridWidth));
			}
			GUILayout.EndHorizontal();
		}
		GUILayout.EndScrollView() ;
		
		
	}
	
	private void AppendData()
	{
		ArrayList insertRow = new ArrayList() ;
		for( int i = 0 ; i < m_InputValues.Length ; ++i )
		{
			// Debug.Log( "m_InputValues[ i ]=" + m_InputValues[ i ] ) ;
			insertRow.Add( "\"" + m_InputValues[ i ] + "\"" );
		}
		m_DB.InsertInto( m_DataBaseTable1Name , insertRow ) ;
		RefreshData() ;
	}
	
	private void RefreshData()
	{
		m_DataBaseContent = m_DB.ReadFullTable( m_DataBaseTable1Name ) ;
	}	
}
