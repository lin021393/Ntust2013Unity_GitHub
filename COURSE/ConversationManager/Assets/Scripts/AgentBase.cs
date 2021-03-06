/**
 * @file AgentBase.cs
 * @author NDark
 * @date 20140322 . file started.
 */
// #define TEST_AGENT
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum AgentState
{
	Condition,
	Action,
	Fighting,
}

public class AgentBase: MonoBehaviour 
{
	public string AgentName
	{
		get { return m_AgentName ; } 
		set { m_AgentName = value ; }
	}
	private string m_AgentName = "" ;
	
	public bool IsValid
	{
		get { return m_IsValid ; }
		set { m_IsValid = value ; }
	}
	private bool m_IsValid = true ;

	public AgentState State
	{
		get 
		{ 
			return FetchAgentState() ; 
		}
		set 
		{ 
			WriteAgentState( value ) ;
			m_AgentState = value ; 
		}
	}
	private AgentState m_AgentState = AgentState.Condition ;

	public ConditionBase Condition
	{
		get { return m_Condition ; }
		set 
		{ 
			m_Condition = value ; 
		}

	}
	private ConditionBase m_Condition = null ;
	
	public ActionBase Action
	{
		get { return m_Action ; }
		set { m_Action = value ; }
	}
	private ActionBase m_Action = null ;

	public void AssignConditionTable( Dictionary< string /*Chapter*/ , string > _Set )
	{
		m_ConditionTable = _Set ;
	}
	public void ImportConditionTable( Dictionary< string /*Chapter*/ , string > _Set )
	{
		Dictionary<string,string>.Enumerator i = _Set.GetEnumerator() ;
		while( i.MoveNext() )
		{
			AddConditionTable( i.Current.Key , i.Current.Value ) ;
		}
	}
	public void AddConditionTable( string _Key , string _ConditionLabel )
	{
		if( false == m_ConditionTable.ContainsKey( _Key ) )
		{
			m_ConditionTable.Add( _Key , _ConditionLabel ) ;
		}
		else
		{
			m_ConditionTable[ _Key ] = _ConditionLabel ;
		}
	}
	public void AddConditionTable( string _Chapter , string _Assignment , string _ConditionLabel )
	{
		string key = _Chapter + ":" + _Assignment ;
		AddConditionTable( key , _ConditionLabel ) ;
	}
	public void RemoveConditionTableEntry( string _Chapter , string _Assignment )
	{
		string key = _Chapter + ":" + _Assignment ;
		if( true == m_ConditionTable.ContainsKey( key ) )
		{
			m_ConditionTable.Remove( key ) ;
		}
	}
	public string GetConditionTableEntry( string _Chapter , string _Assignment )
	{
		string ret = "" ;
		string key = _Chapter + ":" + _Assignment ;
		if( true == m_ConditionTable.ContainsKey( key ) )
		{
			ret = m_ConditionTable[ key ] ;
		}
		return ret ;
	}
	private Dictionary< string /*Chapter:Assignment*/ , string /*ConditionLabel*/ > m_ConditionTable
		= new Dictionary<string, string>() ;




	// Use this for initialization
	void Start () 
	{
		AgentStart() ;
	}
	
	// Update is called once per frame
	void Update () 
	{
		AgentUpdate() ;
	}

	protected void AgentStart()
	{
		AgentManager agentManager = GlobalSingleton.GetAgentManager() ;
		agentManager.RegisterAgent( AgentName , this ) ;
	}

	protected void AgentUpdate()
	{
		if( false == m_IsValid )
			return  ;
		DoUpdate() ;
	}


	public virtual void DoUpdate()
	{
		FetchAgentState() ;
		switch( m_AgentState )
		{
		case AgentState.Condition :
			DoCondition() ;
			break ;
		case AgentState.Action :
			DoAction() ;
			break ;
		case AgentState.Fighting :
			break ;	
		}
	}

	private AgentState FetchAgentState()
	{
		// read from property
		InfoDataCenter infoDataCenter = GlobalSingleton.GetInfoDataCenter() ;
		string stateStr = infoDataCenter.ReadProperty( "CHARACTER_" + AgentName , "STATE" ) ;
		m_AgentState = AgentStateFromStr( stateStr ) ;
		return m_AgentState ;
	}

	protected void WriteAgentState( AgentState _Set )
	{
		// Set to property
		InfoDataCenter infoDataCenter = GlobalSingleton.GetInfoDataCenter() ;
		infoDataCenter.WriteProperty( "CHARACTER_" + AgentName , "STATE" , _Set.ToString() ) ;
		m_AgentState = _Set ;
	}

	protected virtual void DoCondition()
	{
		FetchCondition() ;
		if( null != m_Condition )
		{
			m_Condition.DoCondition() ;
		}

#if TEST_AGENT
		// Do Check if too far from target
		{
			Conditon_DoGoToTarget() ;
		}
#endif
	}

	protected virtual void DoAction()
	{

		if( null != m_Action )
		{
			m_Action.DoAction() ;
		}

#if TEST_AGENT
		// Do From GOTOTARGET Current Position to ( 5 , 0 , 0 )
		{
			Action_DoGoToTarget() ;
		}
#endif 
	}

	ConditionBase CreateCondition( string _ConditionLabel )
	{
		ConditionBase ret = null ;
		return ret ;
	}

	private void FetchCondition()
	{
		InfoDataCenter infoDataCenter = GlobalSingleton.GetInfoDataCenter() ;
		string chapterStr = infoDataCenter.ReadProperty( "CHARACTER_" + AgentName , "CHAPTER" ) ;
		string assignmentStr = infoDataCenter.ReadProperty( "CHARACTER_" + AgentName , "ASSIGNMENT" ) ;
		string conditionLabel = this.GetConditionTableEntry( chapterStr , assignmentStr ) ;
		if( 0 != conditionLabel.Length && 
		    null != m_Condition && 
		    conditionLabel != m_Condition.ToString() )
		{
			ConditionBase addConditon = CreateCondition( conditionLabel ) ;
			if( null != addConditon )
			{
				this.Condition = addConditon ;
			}
		}
	}

	public static AgentState AgentStateFromStr( string _Str )
	{
		if( _Str == AgentState.Condition.ToString() )
		{
			return AgentState.Condition ;
		}
		else if( _Str == AgentState.Action.ToString() )
		{
			return AgentState.Action ;
		}
		else // if( _Str == AgentState.Fighting.ToString() )
		{
			return AgentState.Fighting ;
		}
	}

	public static Vector3 Vector3FromFromStr( string _Str )
	{
		Vector3 ret = Vector3.zero ;
		string [] splitor = { "," } ;
		string [] strVec = _Str.Split( splitor , System.StringSplitOptions.RemoveEmptyEntries ) ;
		if( strVec.Length >= 3 )
		{
			float x = 0 ;
			float y = 0 ;
			float z = 0 ;
			if( true == float.TryParse( strVec[ 0 ] , out x ) )
			{
				ret.x = x ;
			}
			if( true == float.TryParse( strVec[ 1 ] , out y ) )
			{
				ret.y = y ;
			}
			if( true == float.TryParse( strVec[ 2 ] , out z ) )
			{
				ret.z = z ;
			}
		}
		return ret ;
	}

	private void Conditon_DoGoToTarget()
	{
		InfoDataCenter infoDataCenter = GlobalSingleton.GetInfoDataCenter() ;
		string currentObjectName = infoDataCenter.ReadProperty( "CHARACTER_TestObj1" , "OBJECT_NAME" ) ;
		GameObject currentObject = GameObject.Find( currentObjectName ) ;
		
		string assignmentStr = infoDataCenter.ReadProperty( "CHARACTER_TestObj1" , "ASSIGNMENT" ) ;

		if( "WaitSec" == assignmentStr )
		{
			// replace correct action object
			if( 0.0f == m_AlarmTime )
			{
				// Debug.Log( "0.0f == m_AlarmTime" ) ;
				string secStr = infoDataCenter.ReadProperty( "CHARACTER_TestObj1" , "WAITSEC"  ) ;
				// Debug.Log( "secStr=" + secStr ) ;
				
				float sec = 0 ;
				float.TryParse( secStr , out sec ) ;
				m_AlarmTime = Time.timeSinceLevelLoad + sec ;
				WriteAgentState( AgentState.Action ) ;
			}
			else 
			{
				Debug.Log( "ASSIGNMENT , GOTOTARGET" ) ;
				infoDataCenter.WriteProperty( "CHARACTER_TestObj1" , "ASSIGNMENT" , "GOTOTARGET" ) ;
				infoDataCenter.WriteProperty( "CHARACTER_TestObj1" , "TARGET_POSITION" , "5,0,0" ) ;
			}
		}
		else if( "GOTOTARGET" == assignmentStr )
		{
			Debug.Log( "GOTOTARGET" ) ;
			string targetPositionStr = infoDataCenter.ReadProperty( "CHARACTER_TestObj1" , "TARGET_POSITION" ) ;
			Vector3 targetPositon = Vector3FromFromStr( targetPositionStr ) ;
			Vector3 currentPosistion = currentObject.transform.position ;
			float distanceToTarget = Vector3.Distance( targetPositon , currentPosistion ) ;
			if( distanceToTarget > 1 )
			{
				// replace correct action object
				WriteAgentState( AgentState.Action ) ;
			}
		}
	}

	private void Action_DoGoToTarget()
	{
		InfoDataCenter infoDataCenter = GlobalSingleton.GetInfoDataCenter() ;
		string currentObjectName = infoDataCenter.ReadProperty( "CHARACTER_TestObj1" , "OBJECT_NAME" ) ;
		GameObject currentObject = GameObject.Find( currentObjectName ) ;
		if( null == currentObject )
		{
			Debug.Log( "null == currentObject") ;
			return;
		}

		string assignmentStr = infoDataCenter.ReadProperty( "CHARACTER_TestObj1" , "ASSIGNMENT" ) ;
		// Debug.Log( "assignmentStr=" + assignmentStr ) ;
		if( "WaitSec" == assignmentStr )
		{
			string secStr = infoDataCenter.ReadProperty( "CHARACTER_TestObj1" , "WAITSEC"  ) ;
			// Debug.Log( "secStr=" + secStr ) ;

			float sec = 0 ;
			float.TryParse( secStr , out sec ) ;
			// Debug.Log( "Time.timeSinceLevelLoad=" + Time.timeSinceLevelLoad ) ;
			if( Time.timeSinceLevelLoad > m_AlarmTime )
			{
				WriteAgentState( AgentState.Condition ) ;
			}
		}
		else if( "GOTOTARGET" == assignmentStr )
		{
			string targetPositionStr = infoDataCenter.ReadProperty( "CHARACTER_TestObj1" , "TARGET_POSITION" ) ;
			Vector3 targetPositon = Vector3FromFromStr( targetPositionStr ) ;
			Vector3 currentPosistion = currentObject.transform.position ;
			Vector3 distanceVec = targetPositon - currentPosistion ;
			float distanceToTarget = distanceVec.magnitude ;
			if( distanceToTarget > 1 )
			{
				// keep going
				Rigidbody2D r2d = currentObject.rigidbody2D ;
				if( null != r2d )
				{
					r2d.AddForce( distanceVec ) ;
				}
			}
			else
			{
				Rigidbody2D r2d = currentObject.rigidbody2D ;
				r2d.velocity = Vector2.zero ;
				WriteAgentState( AgentState.Condition ) ;
			}
		}
		return;
	}

	private float m_AlarmTime = 0.0f ;
}



















