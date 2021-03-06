/*
@file TowerOfWhatManager.cs
@author NDark
@date 20131011 by NDark . file started and copy from TowerOfWhatManager.cs
@date 20131101 by NDark
. add class method CheckDragMove()

*/
// #define DEBUG
using UnityEngine;
using System.Collections;
using System.Collections.Generic ;

public class TowerOfWhatManager : MonoBehaviour 
{
	
	// 定義尋找的方向
	public enum Direction
	{
		Up = 0 ,
		Down = 1 ,
		Right = 2 ,
		Left = 3 ,
	}
	
	// 狀態只有順序修改
	public enum TowerOfWhatState
	{
		UnActive ,
		CheckDrop , 			// 檢查是否要掉落
		CheckDropingFinished ,	// 檢查掉落中是否已經結束
		CheckDestroy ,  		// 檢查是否要摧毀
		Destroying ,			// 檢查摧毀動畫結束與否
		WaitingPress , 			// 檢查按下,按下之後就會切換到 WaitingRelease
		WaitingRelease ,		// 檢查滑鼠拖拉移動,交換,直到放開為止.開始檢查掉落 CheckDrop
		Swap, 					// 檢查交換是否結束,就回到繼續檢查 WaitingRelease
	}
	
	public TowerOfWhatState m_State = TowerOfWhatState.UnActive ;
	
	// 定位板
	public List<GameObject> m_EmptyBoards = new List<GameObject>() ;
	
	// 真正掉落的unit
	public Dictionary<int , GameObject> m_Units = new Dictionary<int, GameObject>() ;
	
	public int m_WidthNum = 1 ;
	public int m_HeightNum = 1 ;
	public int m_Iter = 0 ;
	
	public GameObject m_UnitCollector = null ;
	
	public int m_CurrentIndex = -1 ; // 目前移動中的索引,每次交換完就更新為 m_MoveToIndex
	public int m_MoveToIndex = -1 ; // 正在交換的下一個索引
	
	public float m_InitDropAnimSec = 0 ; // 掉落時間(一段)
	public float m_CheckFillClockTime = 0 ; // 檢查新增新單位的周期時刻
	
	// 按下
	public void InputPress( int _i , int _j ) 
	{
		if( TowerOfWhatState.WaitingPress != m_State )
			return ;
		// Debug.Log( "InputPress()" + _i + "," + _j ) ;
		
		m_CurrentIndex = _j * m_WidthNum + _i ;
		
		SetState( TowerOfWhatState.WaitingRelease ) ;
	}
	
	// 放開
	public void InputRelease( int _i , int _j ) 
	{
		if( TowerOfWhatState.WaitingRelease != m_State )
			return ;	
		
		// 單純就只是
		// 結束移動
		SetState( TowerOfWhatState.CheckDrop );
	}	
	
	// Use this for initialization
	void Start () 
	{
		if( null == m_UnitCollector )
		{
			m_UnitCollector = GameObject.Find( "UnitCollector" ) ;
		}
		
		InitializeAllUnits() ;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		switch( m_State )
		{
		case TowerOfWhatState.UnActive :
			SetState( TowerOfWhatState.CheckDrop ) ;
			break ;
			
		case TowerOfWhatState.CheckDrop :
			CheckDrop() ;
			break ;
			
		case TowerOfWhatState.CheckDropingFinished :
			CheckDropingFinished() ;
			break ;
			
		case TowerOfWhatState.CheckDestroy :
			// 檢查是否有需要消滅
			CheckDestroy() ;
			break ;	
			
		case TowerOfWhatState.Destroying :
			// 消滅動畫檢查結束,回到 CheckDrop
			CheckDestroyEnd() ;
			break ;	
			
		case TowerOfWhatState.WaitingPress :
			// 等待按下
			break ;	
		case TowerOfWhatState.WaitingRelease :
			
			// 在滑鼠還是按下的時候檢查拖拉,並切換到Swap
			CheckDragMove() ;
			
			// 等待放開 
			break ;			
			
		case TowerOfWhatState.Swap :
			// 等待交換動畫結束,回到 WaitingRelease
			CheckSwapEnd() ; 
			break ;				
		}
	
	}
	
	void InitializeAllUnits()
	{
		for( int j = 0 ; j < m_HeightNum ; ++j )
		{
			for( int i = 0 ; i < m_WidthNum ; ++i )
			{
				GameObject newObj = GenerateUnit( i , j ) ;
				
				if( null != newObj )
				{
					m_Units.Add( j * m_WidthNum + i , newObj ) ;
				}
			}
		}
	}
	
	GameObject GenerateUnit( int _i , int _j )
	{
		GameObject ret = null ;
		
		int index = Random.Range( 1 , 5 ) ;
		string prefabName = string.Format( "AlienUnit{0:00}" , index ) ;
		Object prefab = Resources.Load( prefabName ) ;
		if( null == prefab )
			Debug.LogError( "null == prefab" + prefabName ) ;
		else
		{
			ret = (GameObject) GameObject.Instantiate( prefab ) ;
			ret.name = "Unit" + m_Iter.ToString() ;
			++m_Iter ;
			
			// 設定unit data
			UnitData script = ret.AddComponent<UnitData>() ;
			script.m_IndexI = _i ;
			script.m_IndexJ = _j ;
			script.m_UnitType = index ;
			
			// 設定parent
			if( null != m_UnitCollector )
			{
				ret.transform.parent = m_UnitCollector.transform ;
			}
	
			// 設定位置
			// Debug.Log( _j * m_WidthNum + _i ) ;
			ret.transform.position = m_EmptyBoards[ _j * m_WidthNum + _i ].transform.position ;
		}
		return ret ;
	}
	
	private void CheckDrop()
	{
		HashSet<int> dropIndices = new HashSet<int>() ;
		
		int currentIndex = 0 ;
		int downIndex = 0 ;
		
		// 先檢查哪些需要掉(下面空掉的)
		// 由下而上
		Dictionary<int , GameObject>.Enumerator iU = m_Units.GetEnumerator() ;
		while( iU.MoveNext() )
		{
			currentIndex = iU.Current.Key ;
			downIndex = currentIndex - m_WidthNum ;// 越下面index越少
			// Debug.Log( "CheckDrop() downIndex=" + downIndex ) ;
			if( true == IsValidIndex( downIndex ) &&
				false == m_Units.ContainsKey( downIndex ) )
			{
				// drop index
				dropIndices.Add( currentIndex ) ;
#if DEBUG
				Debug.Log( "CheckDrop() dropIndices.Add=" + currentIndex ) ;
#endif				
			}
		}
		
		// 再檢查要掉到哪裡(裝上動畫
		foreach( int index in dropIndices )
		{
			GameObject dropObj = m_Units[ index ] ;
			// 修改index
			UnitData unitData = dropObj.GetComponent<UnitData>() ;
			if( null == unitData )
				break ;
			
			if( false == IsValidIndex( index - m_WidthNum ) )
				continue;
			downIndex = index ;
			int dropCount = 0 ;
			while( true == IsValidIndex( downIndex - m_WidthNum ) &&
				   false == m_Units.ContainsKey( downIndex - m_WidthNum ) )
			{
				downIndex = downIndex - m_WidthNum ;
				++dropCount ;
			}
			
			// 掉落動畫 設定目標
			CloseTarget moveAnim = dropObj.GetComponent<CloseTarget>() ;
			if( null == moveAnim )
			{
				moveAnim = dropObj.AddComponent<CloseTarget>() ;
			}
			
#if DEBUG			
			Debug.Log( "index=" + index ) ;
			Debug.Log( "dropTo=" + downIndex ) ;
#endif			
			unitData.m_IndexJ = downIndex / m_WidthNum ;
			m_Units.Remove( index ) ;
			m_Units.Add( downIndex , dropObj ) ;
			
			moveAnim.m_TargetPos = m_EmptyBoards[ downIndex ].transform.position ;			
			
			m_InitDropAnimSec = moveAnim.m_AnimSec ;
			
			moveAnim.m_AnimSec = dropCount * moveAnim.m_AnimSec ;
			
		}
		
		// 開始倒數重新填滿的時間(一格的掉落長度)
		if( dropIndices.Count > 0 )
		{
			m_CheckFillClockTime = Time.timeSinceLevelLoad + m_InitDropAnimSec ;
		}
		
		// 一定檢查
		SetState( TowerOfWhatState.CheckDropingFinished ) ;
	}
	
	// 檢查掉落結束了沒有, 回到 CheckDrop
	private void CheckDropingFinished()
	{
		bool isFinished = true ;
		
		// 就是檢查所有的動畫是否都自我毀滅了
		Dictionary<int , GameObject>.Enumerator iU = m_Units.GetEnumerator() ;
		while( iU.MoveNext() )
		{
			GameObject unit = iU.Current.Value ;
			CloseTarget script = unit.GetComponent<CloseTarget>() ;
			if( null != script )// 有一個單位還在掉
			{
				isFinished = false ;
			}
		}
		
		if( true == isFinished )
		{
			// 已經結束,檢查毀滅
			// Debug.Log( "CheckDropingFinished() end droping" ) ;
			SetState( TowerOfWhatState.CheckDestroy );
		}
		else
		{
			// 還沒結束 回去檢查有沒有多餘的掉落空間
			SetState( TowerOfWhatState.CheckDrop );
		}
		
		// 因為此函式釋放在 CheckDropFinish 有可能會往下CheckDestroy 
		// 所以 如果有填新的,強制轉回CheckDrop	
		CheckFill() ;
	}
	
	// 檢查填滿最上面的空位，記得週期性的檢查，不要一次把一連串的方塊生在同一個地方
	private void CheckFill() 
	{
		int fillCount = 0 ;
		if( Time.timeSinceLevelLoad > m_CheckFillClockTime )
		{
			fillCount = FillTopUnits() ;// 檢查要不要補
			if( fillCount > 0 )
			{
				// 因為此函式釋放在 CheckDropFinish 有可能會往下CheckDestroy 
				// 所以 如果有填新的,強制轉回CheckDrop
				SetState( TowerOfWhatState.CheckDrop ) ;
				m_CheckFillClockTime = Time.timeSinceLevelLoad + m_InitDropAnimSec ;
			}
		}
	}
	
	
	private void CheckDestroy()
	{
		// Debug.Log( "CheckDestroy()" + m_EmptyBoards.Count ) ;
		
		HashSet<int> oneTimedestroyList = new HashSet<int>() ;
		HashSet<int> totalDestroyList = new HashSet<int>() ;
		
		// 每一個掃過一次
		// 檢查往右及往上
		Dictionary<int , GameObject>.Enumerator iU = m_Units.GetEnumerator() ;
		while( iU.MoveNext() )
		{
			GameObject unit = iU.Current.Value ;
			UnitData unitData = unit.GetComponent<UnitData>() ;
			if( null != unitData )
			{
				// 往右
				oneTimedestroyList.Clear() ;
				
				// Debug.Log( "isChecked.Count= " + isChecked.Count ) ;
				ContinueFindDestroy( unitData.m_IndexI ,
									 unitData.m_IndexJ ,
									 unitData.m_UnitType ,
									 Direction.Right ,
									 ref oneTimedestroyList ) ;
			
				if( oneTimedestroyList.Count > 2 )
				{
					foreach( int i in oneTimedestroyList )
					{
						totalDestroyList.Add( i ) ;
					}
				}
				
				// 往上
				oneTimedestroyList.Clear() ;
				
				ContinueFindDestroy( unitData.m_IndexI ,
									 unitData.m_IndexJ ,
									 unitData.m_UnitType ,
									 Direction.Up ,
									 ref oneTimedestroyList ) ;				
				
				if( oneTimedestroyList.Count > 2 )
				{
					foreach( int i in oneTimedestroyList )
					{
						totalDestroyList.Add( i ) ;
					}
				}				
			}
		}
		
		// 新增 清除動畫 
		if( totalDestroyList.Count > 0 )
		{
			foreach( int i in totalDestroyList )
			{
#if DEBUG				
				Debug.Log( "CheckDestroy() totalDestroyList " + i ) ;
#endif			
				GameObject obj = m_Units[ i ] ;
				obj.AddComponent<VanishAnim>() ; 
			}
			SetState( TowerOfWhatState.Destroying );
		}
		else
		{
			SetState( TowerOfWhatState.WaitingPress );
		}
	}
	
	void ContinueFindDestroy( int _i , 
							  int _j , 
							  int _Type , 
							  Direction _FindingDirection ,
							  ref HashSet<int> _DestroyList ) 
	{
#if DEBUG			
		Debug.Log( "RecursiveFindDestroy() " + _i + "," + _j + "," + _Type ) ;
#endif
		
		if( false == IsValidIndex( _i , _j ) )
		{
#if DEBUG				
			Debug.Log( "false == IsValidIndex" ) ;
#endif			
			return ;
		}
		
		int index = _j * m_WidthNum + _i ;
		
		if( false == m_Units.ContainsKey( index ) )
		{
#if DEBUG						
			Debug.Log( "false == m_Units.ContainsKey" ) ;
#endif
			return ;
		}
		
		GameObject unit = m_Units[ index ] ;
		UnitData unitData = unit.GetComponent<UnitData>() ;
		if( null == unitData )
			return ;
		if( unitData.m_UnitType != _Type )
		{
#if DEBUG			
			Debug.Log( "unitData.m_UnitType != _Type" + unitData.m_UnitType ) ;
#endif			
			return ;
		}
#if DEBUG		
		Debug.Log( "_DestroyList.Add " + index ) ;
#endif		
		_DestroyList.Add( index ) ;
		
		// 只有往右跟往上
		if( _FindingDirection == Direction.Right )
			ContinueFindDestroy( _i + 1 , _j , _Type , _FindingDirection  , ref _DestroyList ) ;
		// else if( _FindingDirection == Direction.Left )
		// 	ContinueFindDestroy( _i - 1 , _j , _Type , _FindingDirection , ref _DestroyList ) ;
		else if( _FindingDirection == Direction.Up )
			ContinueFindDestroy( _i , _j + 1 , _Type , _FindingDirection , ref _DestroyList ) ;
		// else if( _FindingDirection == Direction.Down )
		// 	ContinueFindDestroy( _i , _j - 1 , _Type , _FindingDirection , ref _DestroyList ) ;
	}
	
	private bool IsValidIndex( int _index )
	{
		if( _index < 0 || _index >= m_HeightNum * m_WidthNum )
			return false ;
		return true ;
	}
	private bool IsValidIndex( int _i , int _j )
	{
		if( _i < 0 || 
			_i >= m_WidthNum ||
			_j < 0 || 
			_j >= m_HeightNum )
			return false ;
		return true ;
	}

	// 檢查摧毀結束與否
	private void CheckDestroyEnd()
	{
		bool animEnd = true ;
		List<int> destroyList = new List<int>() ;
		Dictionary<int , GameObject>.Enumerator iU = m_Units.GetEnumerator() ;
		while( iU.MoveNext() )
		{
			VanishAnim animScript = iU.Current.Value.GetComponent<VanishAnim>() ;
			if( null != animScript )
			{
				if( false == animScript.IsAnimEnd() )
				{
					animEnd = false ;
				}
				else
				{
					destroyList.Add( iU.Current.Key ) ;
				}
			}
		}
		
		// 真正移除物件以及 m_Units[] 指標
		if( true == animEnd )
		{
			foreach( int i in destroyList )
			{
				GameObject obj = m_Units[ i ] ;
				GameObject.Destroy(obj) ;
				m_Units.Remove( i ) ;
			}
			SetState( TowerOfWhatState.CheckDrop );
		}
	}
	
	private int FillTopUnits()
	{
		int count = 0 ;
		// 填補最上層的單位
		int j = m_HeightNum - 1 ;
		for( int i = 0 ; i < m_WidthNum ; ++i )
		{
			int index = j * m_WidthNum + i ;
			if( false == m_Units.ContainsKey( index ) )
			{
#if DEBUG							
				Debug.Log( "m_Units.Add " + index ) ;
#endif				
				m_Units.Add( index, GenerateUnit( i , j ) );
				++count ;
			}
		}		
		
		return count ;
	}
	
	// 交換動畫
	private void AddSwap( int _index1 , int _index2 )
	{

		if( false == m_Units.ContainsKey( _index1 ) || 
			false == m_Units.ContainsKey( _index2 ) ||
			_index1 == _index2 )
			return ;

		
		GameObject obj1 = m_Units[ _index1 ] ;
		UnitData unitDat1 = obj1.GetComponent<UnitData>() ;
		GameObject obj2 = m_Units[ _index2 ] ;
		UnitData unitDat2 = obj2.GetComponent<UnitData>() ;
		
		if( null == unitDat1 || 
			null == unitDat2 )
			return ;
		
#if DEBUG		
		Debug.Log( "AddSwap() _index1 " + _index1  ) ;
		Debug.Log( "AddSwap() _index2 " + _index2  ) ;
#endif		
		// 交換 ( i , j )
		int tmp ;
		tmp = unitDat1.m_IndexI ;
		unitDat1.m_IndexI = unitDat2.m_IndexI ;
		unitDat2.m_IndexI = tmp ;
	
		tmp = unitDat1.m_IndexJ ;
		unitDat1.m_IndexJ = unitDat2.m_IndexJ ;
		unitDat2.m_IndexJ = tmp ;
				
		// 交換 m_Units[] 容器
		m_Units[ _index1 ] = obj2 ;
		m_Units[ _index2 ] = obj1 ;
		
		// add animation
		CloseTarget animScript = obj1.AddComponent<CloseTarget>() ;
		animScript.m_TargetPos = obj2.transform.position ;
		
		animScript = obj2.AddComponent<CloseTarget>() ;
		animScript.m_TargetPos = obj1.transform.position ;
		
		// 交換必轉移到此狀態
		m_State = TowerOfWhatState.Swap ;
	}
	
	// 檢查交換結束與否
	private void CheckSwapEnd()
	{
		if( false == m_Units.ContainsKey( m_CurrentIndex ) || 
			false == m_Units.ContainsKey( m_MoveToIndex )  )
			return ;
		
		GameObject obj1 = m_Units[ m_CurrentIndex ] ;
		GameObject obj2 = m_Units[ m_MoveToIndex ] ;
		
		if( null != obj1.GetComponent<CloseTarget>() ||
			null != obj2.GetComponent<CloseTarget>() )
			return ;
		
		// 交換結束更新目前追蹤的索引
		m_CurrentIndex = m_MoveToIndex ;
		
		// 轉完回到繼續檢查有無需要再轉
		SetState( TowerOfWhatState.WaitingRelease ) ;
	}	
	
	private void SetState( TowerOfWhatState _Set )
	{
		if( _Set == m_State )
		{
			return ;
		}
#if DEBUG		
		Debug.Log( "SetState() " + _Set.ToString() ) ;
#endif		
		m_State = _Set ;
	}

	// 檢查拖拉移動
	private void CheckDragMove()
	{
		if( TowerOfWhatState.WaitingRelease != m_State )
			return ;
		
		if( true == Input.GetMouseButton( 0 ) )
		{
			// 持續按下 
			// 檢查是否有切換 
			Ray ray = Camera.mainCamera.ScreenPointToRay( Input.mousePosition ) ;
			RaycastHit hitInfo ;
			if( true == Physics.Raycast( ray , out hitInfo ) )
			{
				GameObject detectObj = hitInfo.collider.gameObject ;
				UnitData unitData = detectObj.GetComponent<UnitData>() ;
				if( null == unitData )
				{
					return ;
				}
				
				// Debug.Log( "CheckDragMove()" + detectObj.name ) ;
				int tmpIndex = IJToIndex( unitData.m_IndexI , unitData.m_IndexJ ) ;
				int DiffInIndex = Mathf.Abs( m_CurrentIndex - tmpIndex ) ;
		
				if( tmpIndex != m_CurrentIndex && // 索引不同才交換
					( 1 == DiffInIndex || 
					  m_WidthNum == DiffInIndex ) // 只准左右交換,上下交換
					)
				{
					// Swap
					m_MoveToIndex = tmpIndex ;
					AddSwap( m_CurrentIndex , m_MoveToIndex ) ;
				}
			}
		}		
	}
	
	private int IJToIndex( int _i , int _j )
	{
		return _j * m_WidthNum + _i ;
	}
}
