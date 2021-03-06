/*
@file MovableRangeManager01.cs
@author NDark
@date 20130811 file started.
*/
using UnityEngine;
using System.Collections.Generic ;

public class MovableRangeManager01 : MonoBehaviour 
{
	public List<GameObject> m_MovableRangeObjects = new List<GameObject>() ;
	public string m_MovableRangePrefabName = "Common/Prefabs/MovableRangeObj" ;
	
	public void CreateMovableObjects( GameObject _UnitObj , int _OrgI , int _OrgJ )
	{
		if( null == _UnitObj )
			return ;
		
		ChessUnitData01 unitData = _UnitObj.GetComponent<ChessUnitData01>() ;
		if( null == unitData )
			return ;
		
		// clear all
		ClearMovableRangeObj() ;
		
		if( "Pawn" == unitData.m_ChessType )
		{
			if( "Enemy" == unitData.m_Role )
			{
				CreateMovableRangeObj( _OrgI , _OrgJ - 1 ) ;
				CreateMovableRangeObj( _OrgI , _OrgJ - 2 ) ;
			}
			else 
			{
				CreateMovableRangeObj( _OrgI , _OrgJ + 1 ) ;
				CreateMovableRangeObj( _OrgI , _OrgJ + 2 ) ;				
			}
		}
	}
	
	public void ClearMovableRangeObj()
	{
		foreach( GameObject _obj in m_MovableRangeObjects )
		{
			GameObject.Destroy( _obj ) ;
			Debug.Log( "GameObject.Destroy()" + _obj.name ) ;
		}
		
		m_MovableRangeObjects.Clear() ;
	}
	

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

				
	private void CreateMovableRangeObj( int _i , int _j )
	{
		Vector3 pos = new Vector3( 0.65f + _i * 1.25f , 
								   0.51f , 
								   0.65f + _j * 1.25f ) ;
		
		Object prefab = Resources.Load( m_MovableRangePrefabName ) ;
		if( null == prefab )
			return ;
		GameObject movableObj = (GameObject) Instantiate( prefab ) ;
		if( null != movableObj )
		{
			movableObj.name = "MovableRangeObject" + _i.ToString() + _j.ToString() ;
			movableObj.transform.position = pos ;
			m_MovableRangeObjects.Add( movableObj ) ;
		}
			
	}
}
