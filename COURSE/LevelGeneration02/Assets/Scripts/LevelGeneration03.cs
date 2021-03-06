/*
@file LevelGeneration03.cs
@author NDark
@date 20130622 . file created.
@date 20130729 . comment and error log.
*/
using UnityEngine;

public class LevelGeneration03 : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
		InitializeLevel() ;
	}
	
	// Update is called once per frame
	void Update () 
	{
	}
	
	private void InitializeLevel()
	{
		Debug.Log( "LevelGeneration03:InitializeLevel() start." ) ;
		
		InitializeChessBoard() ;
		Debug.Log( "LevelGeneration03:InitializeLevel() InitializeChessBoard() done." ) ;
		
		InitializeChessPieces() ;
		Debug.Log( "LevelGeneration03:InitializeLevel() InitializeChessPieces() done." ) ;
		
		Debug.Log( "LevelGeneration03:InitializeLevel() end." ) ;
	}
	
	private void InitializeChessBoard() 
	{
		Object chessBoardPrefab = Resources.Load( "Common/Prefabs/ChessBoard" ) ;
		if( null == chessBoardPrefab )
		{
			Debug.LogError( "LevelGeneration03:InitializeChessBoard() Resources.Load ChessBoard failed." ) ;			
		}
		else
		{
			GameObject chessBoardObj = (GameObject) GameObject.Instantiate( chessBoardPrefab ) ;
			if( null != chessBoardObj )
			{
				chessBoardObj.name = "ChessBoard" ;
				chessBoardObj.transform.position = new Vector3( 5 , 0 , 5 ) ;
			}
		}
	}
	
	private void InitializeChessPieces() 
	{
		float distanceOfABlock = 1.25f;// 每格閒距
		float startX = 0.65f ;
		float startY = 0.65f ;
		
		int index = 0 ; 
		Vector3 pos = Vector3.zero ;
		Quaternion r180inY = Quaternion.AngleAxis( 180.0f , Vector3.up ) ;
		Quaternion rotation = Quaternion.identity ;
		for( int j = 0 ; j < 8 ; ++j )
		{
			// 0 1 2 3 4 5 6 7
			// P P         P P			
			if( j > 1 && j < 6 )
				continue ;
			
			if( j >= 4 )
				rotation = r180inY ; // 敵軍要轉向180度
			else
				rotation = Quaternion.identity ;// 我軍不轉
				
			for( int i = 0 ; i < 8 ; ++i )
			{
				index = j * 8 + i ;
				pos = new Vector3( startX + i * distanceOfABlock , 
								   1.0f , 
								   startY + j * distanceOfABlock ) ;
				
				InitializeChessPawn( index , pos , rotation ) ;
			}
		}
	}
	
	private void InitializeChessPawn( int index , Vector3 _Pos , Quaternion _Rotation ) 
	{
		Object chessPawnPrefab = Resources.Load( "Common/Prefabs/ChessPawn02" ) ;
		if( null == chessPawnPrefab )
		{
			Debug.LogError( "LevelGeneration03:InitializeChessPawn() Resources.Load ChessPawn02 failed." ) ;
		}
		else 
		{
			GameObject chessPawnObj = 
				(GameObject) GameObject.Instantiate( chessPawnPrefab ) ;
			
			if( null != chessPawnObj )
			{
				chessPawnObj.name = "ChessPawn"  + index.ToString() ;
				chessPawnObj.transform.position = _Pos  ;
				chessPawnObj.transform.rotation = _Rotation ;
			}
		}
	}	
}
