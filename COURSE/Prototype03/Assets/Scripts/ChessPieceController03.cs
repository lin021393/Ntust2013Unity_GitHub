/*
@file ChessPieceController03.cs
@author NDark
@date 20130811 . file started and derived from ChessPieceController02
*/
using UnityEngine;

public class ChessPieceController03 : MonoBehaviour 
{
	public string m_ChessPieceMovementStr = "ChessPieceMovement02" ;
	SelectionSystem03 m_SelectSystem = null ;

	// Use this for initialization
	void Start () 
	{
		if( null == m_SelectSystem )
		{
			m_SelectSystem = this.gameObject.GetComponent<SelectionSystem03>() ;
		}
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		if( true == Input.GetMouseButtonDown( 0 ) )
		{
			Ray ray = Camera.mainCamera.ScreenPointToRay( Input.mousePosition ) ;
			RaycastHit hitInfo ;
			if( true == Physics.Raycast( ray , out hitInfo ) ) 
			{
				// Debug.Log( hitInfo.collider.gameObject ) ;
				Judge( hitInfo.collider.gameObject , hitInfo.point ) ;				
			}
		}
	
	}
	
	private void Judge( GameObject _Obj , Vector3 _Point )
	{
		if( null == m_SelectSystem || 
			false == m_SelectSystem.m_IsSelect ||
			null == m_SelectSystem.m_SelectObject )
			return ;
		
		GameObject selectObject = m_SelectSystem.m_SelectObject ;
		if( -1 != _Obj.name.IndexOf( "ChessBoard" ) )
		{
			
			int originalI = 0 ;
			int originalJ = 0 ;
			
			Vector3 centerPosOrg = Vector3.zero ;// 用不到
			if( false == GetClosestIndex( selectObject.transform.position , 
										  ref centerPosOrg , 
										  ref originalI , 
										  ref originalJ ) )
			{
				return ;
			}		
			
			int destinationI = 0 ;
			int destinationJ = 0 ;
			
			// 計算出正確格線中心點
			Vector3 centerPos = Vector3.zero ;
			if( false == GetClosestIndex( _Point , 
										  ref centerPos , 
										  ref destinationI , 
										  ref destinationJ ) )
			{
				return ;
			}

	
			
			// @todo we may not need to re-add it. modify ChessPieceMovement01 as well.
			// 判斷可不可行
			if( true == ClearToMove( selectObject , 
									 _Point , 
									 originalI , 
									 originalJ , 
								     destinationI , 
									 destinationJ  ) )
			{
				Debug.Log( "true == ClearToMove" ) ;
				Component moveScript = null ;
				moveScript = selectObject.GetComponent( m_ChessPieceMovementStr ) ;
				if( null == moveScript ) 
				{
					moveScript = selectObject.AddComponent( m_ChessPieceMovementStr ) ;
					Debug.Log( "AddComponent" ) ;
				}
				
				if( null != moveScript )
				{	
					Debug.Log( "null != moveScript" ) ;
					moveScript.SendMessage( "Setup" , centerPos ) ;
				}

			}
			
		}
		else if( -1 != _Obj.name.IndexOf("ChessPawn") )
		{
			Debug.Log( _Obj.name ) ;
		}
	}
	
	private bool GetClosestIndex( Vector3 _ClickPoint , 
								  ref Vector3 _ReturnCenterPos , 
								  ref int _IndexI , 
								  ref int _IndexJ )
	{
		bool ret = false ;
		
		float distanceOfABlock = 1.25f;// 每格閒距
		float startX = 0.65f ;
		float startY = 0.65f ;
		
		float minSquare = 999999.0f ;
		Vector3 centerPoint = Vector3.zero ;
		for( int j = 0 ; j < 8 ; ++j )
		{
			for( int i = 0 ; i < 8 ; ++i )
			{
				centerPoint = new Vector3( startX + i * distanceOfABlock , 
								   		   1.0f , 
								   		   startY + j * distanceOfABlock ) ;
				Vector3 diffVec = centerPoint - _ClickPoint ;
				if( diffVec.sqrMagnitude < minSquare )
				{
					minSquare = diffVec.sqrMagnitude ;
					_ReturnCenterPos = centerPoint ;
					_IndexI = i ;
					_IndexJ = j ;
					ret = true ;
				}
				
			}
		}
		return ret ;
	}
	
	private bool ClearToMove( GameObject _Obj , 
							  Vector3 _DesinationCenterPos , 
							  int _OriginalIndexI , 
							  int _OriginalIndexJ ,
							  int _DesinationIndexI , 
							  int _DesinationIndexJ )
	{
		bool ret = false ;
		
		ChessUnitData01 unitData = _Obj.GetComponent<ChessUnitData01>() ;
		if( null == unitData )
			return ret ;
		if( "Pawn" == unitData.m_ChessType )
		{
			if( "Enemy" == unitData.m_Role )
			{
				// 只能往下
				if( _OriginalIndexI == _DesinationIndexI &&
					( _DesinationIndexI == _OriginalIndexJ - 1 || 
					  _DesinationIndexI == _OriginalIndexJ - 2 ) )
				{
					ret = true ;
				}
			}
			else
			{
				// 只能往上
				if( _OriginalIndexI == _DesinationIndexI &&
					( _DesinationIndexI == _OriginalIndexJ + 1 || 
					  _DesinationIndexI == _OriginalIndexJ + 2 ) 
					)
				{
					ret = true ;
				}
			}
		}
		
		return ret ;
	}
}
