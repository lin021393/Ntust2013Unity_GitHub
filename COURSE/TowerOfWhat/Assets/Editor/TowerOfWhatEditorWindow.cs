/*
@file TowerOfWhatEditorWindow.cs
@author NDark
@date 20131011 by NDark . file started and copy from KandyCrusherEditorWindow.cs
*/
using UnityEngine ;
using UnityEditor ; // add this for editor
using System.Collections;
using System.Collections.Generic;

public class TowerOfWhatEditorWindow : EditorWindow 
{
	[MenuItem ("KandyCrusher/Manager Window 1")]
    static void ShowWindow () 
	{
        EditorWindow.GetWindow<TowerOfWhatEditorWindow>() ;
    }
	
	// GameObject GlobalSingleton
	static public GameObject m_GlobalSingleton = null ;
	
	// GameObject EmptyBoardsParent
	static public GameObject m_EmptyBoardsParent = null ;
	const string EMPTY_BOARD_PARENT_NAME = "EmptyBoardParent" ;
	
	// each empty board with collider
	static public List<GameObject> m_EmptyBoards = new List<GameObject>() ;
	
	// start position of first empty board
	static public Vector3 m_StartPos = Vector3.zero ;
	
	// space of 2 stage board
	static public Vector3 m_SpaceBetweenBoards = new Vector3( 1.1f , 0 , 1.1f )  ;
	static public int m_WidthNum = 1 ;
	static public int m_VisibleHeightNum = 1 ;
	static public int m_TotalHeightNum = 1 ;
		
	
	void OnGUI()
	{
		// stage board layout
		m_StartPos = EditorGUILayout.Vector3Field( "StartPos" , 
												   m_StartPos ) ;
		m_SpaceBetweenBoards = EditorGUILayout.Vector3Field( "Space between board" , 
															 m_SpaceBetweenBoards ) ;
		m_WidthNum = EditorGUILayout.IntField( "Width Num" , 
											   m_WidthNum ) ;
		m_VisibleHeightNum= EditorGUILayout.IntField( "Visible Height Num" , 
											   m_VisibleHeightNum ) ;
		m_TotalHeightNum= EditorGUILayout.IntField( "Total Height Num" , 
											   m_TotalHeightNum ) ;		
		if( m_VisibleHeightNum >= m_TotalHeightNum )
		{
			m_TotalHeightNum = m_VisibleHeightNum ;
		}
		
		if( true == GUILayout.Button( "RecreateEmptyBoards" ) )
		{
			RecreateEmptyBoards() ;
			
		}
		if( true == GUILayout.Button( "InvisibleAllEmptyBoards" ) )
		{
			InvisibleAllEmptyBoards() ;
		}
		
	}
	
	// 重新創造定位的版子
	private void RecreateEmptyBoards()
	{
		// destroy old empty boards
		ClearAllEmptyBoards() ;
		
		// create empty boards' parent
		m_EmptyBoardsParent = GameObject.Find( EMPTY_BOARD_PARENT_NAME ) ;
		if( null == m_EmptyBoardsParent )
		{
			m_EmptyBoardsParent = new GameObject( EMPTY_BOARD_PARENT_NAME ) ;
		}
		
		const string prefabName = "EmptyUnit" ;
		Object prefab = Resources.Load( prefabName ) ;
		if( null == prefab )
		{
			Debug.LogError( "null == prefab, prefabName=" + prefabName ) ;
			return ;
		}		
		
		// create empty boards by i and j
		for( int j = 0 ; j < m_TotalHeightNum ; ++j )// 全部創造
		{
			for( int i = 0 ; i < m_WidthNum ; ++i )
			{
				GameObject obj = (GameObject) GameObject.Instantiate( prefab ) ;
				obj.name = "EmptyUnit:" + i + "," + j ;
				
				// start pos + ( i * width , j * height )
				obj.transform.position = 
					new Vector3( m_StartPos.x + i * m_SpaceBetweenBoards.x ,
								 m_StartPos.y , 
								 m_StartPos.z + j * m_SpaceBetweenBoards.z );
				
				// setup Camera pos by the center board
				// only execute once
				if( i == m_WidthNum / 2 &&
					j == m_VisibleHeightNum / 2 ) // 注意只檢查看得到的高度
				{
					Vector3 shiftVec = Vector3.zero ;
					
					// even or odd ( shift half board )
					if( 0 == m_WidthNum % 2 )// 只檢查寬度
					{
						shiftVec.x = m_SpaceBetweenBoards.x / 2.0f ;
						
					}
					if( 0 == m_VisibleHeightNum % 2 )// 只檢查寬度
					{
						shiftVec.z = m_SpaceBetweenBoards.z / 2.0f ;
					}					
					
					// center board pos - shift
					Camera.mainCamera.transform.position = 
						new Vector3( obj.transform.position.x - shiftVec.x ,
								 	 obj.transform.position.y + 10 ,
									 obj.transform.position.z - shiftVec.z ) ;	
					
					if( m_WidthNum > m_VisibleHeightNum )
						Camera.mainCamera.orthographicSize = m_WidthNum * m_SpaceBetweenBoards.x / 2.0f ;
					else 
						Camera.mainCamera.orthographicSize = m_VisibleHeightNum * m_SpaceBetweenBoards.z / 2.0f ;					
				}
				
				// 掛上unit data
				UnitData unitData = obj.AddComponent<UnitData>() ;
				unitData.m_IndexI = i ;
				unitData.m_IndexJ = j ;
				
				obj.AddComponent<CheckInput>() ;

				
				// parent is m_EmptyBoardsParent
				obj.transform.parent = m_EmptyBoardsParent.transform ;	
				
				// add to list
				m_EmptyBoards.Add( obj ) ;

			}// end of for i
		}// end of for j
		
		// set width, height, and preserver index to TripleTaoManager
		if( null == m_GlobalSingleton )
		{
			m_GlobalSingleton = GameObject.Find( "GlobalSingleton" ) ;
		}
		
		if( null != m_GlobalSingleton )
		{
			TowerOfWhatManager manager = m_GlobalSingleton.GetComponent<TowerOfWhatManager>() ;
			if( null == manager )
			{
				Debug.LogError( "null == manager" ) ;
			}
			else
			{
				Debug.Log( "manager.m_WidthNum = m_WidthNum" ) ;
				manager.m_WidthNum = m_WidthNum ;
				manager.m_HeightNum = m_TotalHeightNum ;// 指定的是全部高度
				manager.m_EmptyBoards = m_EmptyBoards ;// 將定位點設定過去
			}
		}
	}
	
	private void ClearAllEmptyBoards()
	{
		foreach( GameObject obj in m_EmptyBoards )
		{
			// can't use Destroy()
			GameObject.DestroyImmediate( obj ) ;
		}
		m_EmptyBoards.Clear() ;
		
		
		// in case this window show up first time
		m_EmptyBoardsParent = GameObject.Find( EMPTY_BOARD_PARENT_NAME ) ;
		if( null != m_EmptyBoardsParent )
		{
			GameObject.DestroyImmediate( m_EmptyBoardsParent ) ;
		}
	}
	
	
	
	private void InvisibleAllEmptyBoards()
	{
		foreach( GameObject obj in m_EmptyBoards )
		{
			obj.renderer.enabled = false ;
		}
	}	
	
}
