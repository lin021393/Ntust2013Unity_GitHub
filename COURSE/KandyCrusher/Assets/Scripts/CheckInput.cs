/*
@file CheckInput.cs
@author NDark
@date 20130908 file started.
*/
using UnityEngine;

public class CheckInput : MonoBehaviour 
{
	static KandyCrusherManager s_Manager = null ;
	public UnitData m_UnitData = null ;
	public bool m_PressDown = false ;
		
	// Use this for initialization
	void Start () 
	{
		if( null == s_Manager )
		{
			GameObject obj = GameObject.Find( "GlobalSingleton" ) ;
			s_Manager = obj.GetComponent<KandyCrusherManager>() ;
		}
	
		m_UnitData = this.gameObject.GetComponent<UnitData>() ;
		m_PressDown = false ;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if( true == m_PressDown )
		{
			if( false == Input.GetMouseButton( 0 ) )
			{
				// 放開 檢查是哪一個 
				Ray ray = Camera.mainCamera.ScreenPointToRay( Input.mousePosition ) ;
				RaycastHit hitInfo ;
				if( true == Physics.Raycast( ray , out hitInfo ) )
				{
					GameObject detectObj = hitInfo.collider.gameObject ;
					UnitData unitData = detectObj.GetComponent<UnitData>() ;
					if( null != unitData )
					{
						// Debug.Log( "mouse up()" + this.gameObject.name ) ;
						s_Manager.InputRelease( unitData.m_IndexI , unitData.m_IndexJ ) ;
					}
				}
				m_PressDown = false ;
			}
		}
	
	}
	
	void OnMouseDown()
	{
		if( null != s_Manager &&
			null != m_UnitData ) 
		{
			// Debug.Log( "OnMouseDown()" + this.gameObject.name ) ;
			s_Manager.InputPress( m_UnitData.m_IndexI , m_UnitData.m_IndexJ ) ;
			m_PressDown = true ;
		}
	}
	
	
	void OnMouseUp()
	{
		// No you can't do this in different object.
//		if( null != s_Manager &&
//			null != m_UnitData ) 
//		{
//			Debug.Log( this.gameObject.name ) ;
//			s_Manager.InputRelease( m_UnitData.m_IndexI , m_UnitData.m_IndexJ ) ;
//		}
	}	
}
