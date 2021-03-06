/*
@file OnGUI03.cs
@author NDark
@date 20130722 . file started.
*/
using UnityEngine;

public class OnGUI03 : MonoBehaviour 
{
	public class Piece
	{
		public Rect m_RectNow = new Rect() ;
		public Rect m_TextureCoordinate = new Rect() ;
	}
	
	
	// 初始所有piece的貼圖座標
	// 使用 CalculateTextureCoordinate() 來取得貼圖座標
	public int []m_RandomIndex = new int[16] ;
	public bool m_Randomize = true ;
	
	// 真正使用的貼圖
	public Texture m_SpriteTexture = null ;
	
	// 一片piece的大小
	public Rect m_RectSize = new Rect( 0 , 0 , 100 , 100 ) ;
	
	// 每片piece的原始座標
	public Rect []m_RectOrg = new Rect[16] ;
	
	// 真正使用的piece
	public Piece []m_Pieces = new Piece[16] ;
	
	// 目前pick的索引 -1 代表沒有
	public int m_PickIndex = -1 ;
	
	// Use this for initialization
	void Start () 
	{
		// 初始化 m_RandomIndex[] 
		InitializeRandomIndex() ;
		
		// 初始化每一piece
		InitializeRect() ;	
	}
	
	public Vector2 m_MouseClickInScreen = Vector2.zero ;
	// Update is called once per frame
	void Update () 
	{
		
		// Input.mousePosition 是 3D座標 , 左下角為 0 0  
		// 但畫在OnGUI內的 Rect 是螢幕座標 左上角為 0 0 				
		m_MouseClickInScreen.Set( Input.mousePosition.x , 
						 	      Screen.height - Input.mousePosition.y ) ;

		if( true == Input.GetMouseButtonDown( 0 ) )
		{
			// mouse click 的反應
			for( int j = 0 ; j < 16 ; ++j )
			{
				// 檢查點到哪一塊
				if( true == m_RectOrg[ j ].Contains( m_MouseClickInScreen ) )
				{
					// 目前已經撿起(m_PickIndex) , 又點到同一塊
					if( j == m_PickIndex )
					{
						// 放回去
						m_Pieces[ m_PickIndex ].m_RectNow = m_RectOrg[ m_PickIndex ] ;
						m_PickIndex = -1 ;
					}
					
					// 目前還未撿起
					else if( -1 == m_PickIndex )
					{
						// 撿起
						m_PickIndex = j ;
					}
					
					// 其他: 已經撿起,但是點到不同塊
					else
					{
						// 注意!!!
						// 我們這裡很奸詐,只是交換貼圖座標
						Rect tmp = new Rect( 
							m_Pieces[ m_PickIndex ].m_TextureCoordinate ) ;
						m_Pieces[ m_PickIndex ].m_TextureCoordinate = 
							m_Pieces[ j ].m_TextureCoordinate ;
						m_Pieces[ j ].m_TextureCoordinate = tmp ;
						
						// 然後偷放下
						// 所以其實都回到原位 piece本身並沒有交換
						// 這樣判斷比較簡單
						m_Pieces[ m_PickIndex ].m_RectNow = m_RectOrg[ m_PickIndex ] ;
						m_PickIndex = -1 ;
					}
					break ;// 只檢查判斷一次
				}
			}
		}
		
		// 隨著滑鼠座標 更新撿起的那一片
		if( -1 != m_PickIndex )
		{
			Rect oldRect = m_Pieces[ m_PickIndex ].m_RectNow ;
			m_Pieces[ m_PickIndex ].m_RectNow.Set( 
						  m_MouseClickInScreen.x - oldRect.width / 2, 
						  m_MouseClickInScreen.y - oldRect.height / 2 ,
						  oldRect.width ,
						  oldRect.height ) ;
		}		
	}
				
	void OnGUI()
	{
		if( null == m_SpriteTexture )
			return ;
		
		for( int j = 0 ; j < 16 ; ++j )
		{
			if( j == m_PickIndex )// 撿起的後畫
				continue ;
			
			GUI.DrawTextureWithTexCoords( m_Pieces[ j ].m_RectNow , 
				m_SpriteTexture , 
				m_Pieces[ j ].m_TextureCoordinate ) ;
		}
		
		// 畫出撿起的那一片
		if( -1 != m_PickIndex )
		{
			GUI.DrawTextureWithTexCoords( m_Pieces[ m_PickIndex ].m_RectNow , 
				m_SpriteTexture , 
				m_Pieces[ m_PickIndex ].m_TextureCoordinate ) ;
		}
		
	}
	
	// randomzie of m_RandomIndex[] from 0 ~ 15
	private void InitializeRandomIndex() 
	{
		// 0 1 2 3 ... 15
		for( int i = 0 ; i < 16 ; ++i )
		{
			m_RandomIndex[ i ] = i ;
		}
		
		if( true == m_Randomize )
		{
			// randomize
			for( int i = 0 ; i < 16 ; ++i )
			{
				int index1 = Random.Range( 0 , 15 ) ; 
				int index2 = Random.Range( 0 , 15 ) ; 
				
				// swap
				int tmp = m_RandomIndex[ index1 ] ;
				m_RandomIndex[ index1 ] = m_RandomIndex[ index2 ]  ;
				m_RandomIndex[ index2 ] = tmp ;
			}
		}
	}
	
	// 4 x 4
	// index is from 0 to 15
	// 計算 m_RectOrg
	private void InitializeRect()
	{
		int index = 0 ;
		for( int j = 0 ; j < 4 ; ++j )
		{
			for( int i = 0 ; i < 4 ; ++i )
			{
				index = j * 4 + i ;
				
				m_RectOrg[ index ] = new Rect() ;
				
				// 原點在左上角
				m_RectOrg[ index ].Set( 0 + i * m_RectSize.width ,
										0 + j * m_RectSize.height ,
										m_RectSize.width ,
										m_RectSize.height ) ;
				
				m_Pieces[ index ] = new Piece() ;
				m_Pieces[ index ].m_RectNow = new Rect( m_RectOrg[ index ] ) ;// copy
				m_Pieces[ index ].m_TextureCoordinate = 
					CalculateTextureCoordinate( m_RandomIndex[ index ] ) ;
			}			
		}
	}
	
	// 計算貼圖座標 ( 0~1 / 4 , 0~1 / 4)
	private Rect CalculateTextureCoordinate( int index )
	{
		int j = index / 4 ;
		int i = index % 4 ;

		// 原點在左下角
		Rect ret = new Rect( i / 4.0f , 
							 1.0f - (j+1) / 4.0f , 
							 1.0f / 4.0f , 
							 1.0f / 4.0f ) ;
		
		return ret ;
	}
}
