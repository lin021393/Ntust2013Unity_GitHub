/*
@file KandyCrusherManager.cs
@author NDark
@date 20130906 file started.
@date 20130910 by NDark . comment.
@date 20131011 by NDark 
. add class method SetState()
. fix an error of not check drop after fill a new block at CheckFill()
*/
// #define DEBUG
using UnityEngine;
using System.Collections;
using System.Collections.Generic ;

public class KandyCrusherManager : MonoBehaviour 
{
	
	// 定義尋找的方向
	public enum Direction
	{
		Up = 0 ,
		Down = 1 ,
		Right = 2 ,
		Left = 3 ,
	}
	
	public enum KandyCrusherState
	{
		UnActive ,
		CheckDrop , 			// 檢查是否要掉落
		CheckDropingFinished ,	// 檢查掉落中是否已經結束
		CheckDestroy ,  		// 檢查是否要摧毀
		Destroying ,			// 檢查摧毀動畫結束與否
		WaitingPress , 			// 檢查按下
		WaitingRelease ,		// 檢查放開
		Swap, 					// 檢查交換是否結束
	}
	
	public KandyCrusherState m_State = KandyCrusherState.UnActive ;
	
	// 定位板
	public List<GameObject> m_EmptyBoards = new List<GameObject>() ;
	
	// 真正掉落的unit
	public Dictionary<int , GameObject> m_Units = new Dictionary<int, GameObject>() ;
	
	public int m_WidthNum = 1 ;
	public int m_HeightNum = 1 ;
	public int m_Iter = 0 ;
	
	public GameObject m_UnitCollector = null ;
	
	// 按下跟放開的索引
	public int m_PressIndex = -1 ;
	public int m_ReleaseIndex = -1 ;
	
	public float m_InitDropAnimSec = 0 ; // 掉落時間(一段)
	public float m_CheckFillClockTime = 0 ; // 檢查新增新單位的周期時刻
	
	// 按下
	public void InputPress( int _i , int _j ) 
	{
		if( KandyCrusherState.WaitingPress != m_State )
			return ;
		// Debug.Log( "InputPress()" + _i + "," + _j ) ;
		
		m_PressIndex = _j * m_WidthNum + _i ;
		
		SetState( KandyCrusherState.WaitingRelease ) ;
	}
	
	// 放開
	public void InputRelease( int _i , int _j ) 
	{
		if( KandyCrusherState.WaitingRelease != m_State )
			return ;		
		
		m_ReleaseIndex = _j * m_WidthNum + _i ;
		
		int DiffInIndex = Mathf.Abs( m_PressIndex - m_ReleaseIndex ) ;
		// Debug.Log( "InputRelease()" + _i + "," + _j ) ;
			
		if( 1 == DiffInIndex ||		
				 m_WidthNum == DiffInIndex ) // 只准左右交換,上下交換
		{
			// 加交換動畫
			AddSwap( m_PressIndex , m_ReleaseIndex ) ;
			SetState( KandyCrusherState.Swap );
		}
		else
		{
			SetState( KandyCrusherState.WaitingPress ); // 其他都返回
		}
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
		case KandyCrusherState.UnActive :
			SetState( KandyCrusherState.CheckDrop ) ;
			break ;
			
		case KandyCrusherState.CheckDrop :
			CheckDrop() ;
			break ;
			
		case KandyCrusherState.CheckDropingFinished :
			CheckDropingFinished() ;
			break ;
			
		case KandyCrusherState.CheckDestroy :
			// 檢查是否有需要消滅
			CheckDestroy() ;
			break ;	
			
		case KandyCrusherState.Destroying :
			// 消滅動畫檢查結束,回到 CheckDrop
			CheckDestroyEnd() ;
			break ;	
			
		case KandyCrusherState.WaitingPress :
			// 等待按下
			break ;	
		case KandyCrusherState.WaitingRelease :
			// 等待放開 
			break ;			
			
		case KandyCrusherState.Swap :
			// 等待交換動畫結束
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
		SetState( KandyCrusherState.CheckDropingFinished ) ;
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
			SetState( KandyCrusherState.CheckDestroy );
		}
		else
		{
			// 還沒結束 回去檢查有沒有多餘的掉落空間
			SetState( KandyCrusherState.CheckDrop );
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
				SetState( KandyCrusherState.CheckDrop ) ;
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
			SetState( KandyCrusherState.Destroying );
		}
		else
		{
			SetState( KandyCrusherState.WaitingPress );
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
			SetState( KandyCrusherState.CheckDrop );
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
	}
	
	// 檢查交換結束與否
	private void CheckSwapEnd()
	{
		if( false == m_Units.ContainsKey( m_PressIndex ) || 
			false == m_Units.ContainsKey( m_ReleaseIndex )  )
			return ;
		
		GameObject obj1 = m_Units[ m_PressIndex ] ;
		GameObject obj2 = m_Units[ m_ReleaseIndex ] ;
		if( null != obj1.GetComponent<CloseTarget>() ||
			null != obj2.GetComponent<CloseTarget>() )
			return ;
		
		SetState( KandyCrusherState.CheckDrop ) ;
	}	
	
	private void SetState( KandyCrusherState _Set )
	{
		if( _Set == m_State )
		{
			return ;
		}
// #if DEBUG		
		Debug.Log( "SetState() " + _Set.ToString() ) ;
// #endif		
		m_State = _Set ;
	}
}
