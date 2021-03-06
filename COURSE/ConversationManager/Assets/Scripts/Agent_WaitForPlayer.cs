/**
 * @file Agent_WaitForPlayer.cs
 * @author NDark
 * @date 20140405 . file started.
 */
using UnityEngine;

public class Agent_WaitForPlayer : AgentBase 
{
	private string oName = "SignalObj" ;
	private string aName = "SignalObj" ;
	private string aCategory = "CHARACTER_SignalObj" ;
	
	private float m_CloseDistance = 0.02f ;
	private GameObject m_MainCharacter = null ;
	private GameObject m_Anchor22 = null ;
	
	private GameObject m_GameObject = null ;
	
	// Use this for initialization
	void Start () 
	{
		this.AgentName = aName ;
		
		AgentStart() ;
		
		// SignalObj
		InfoDataCenter infoDataCenter = GlobalSingleton.GetInfoDataCenter() ;
		infoDataCenter.WriteProperty( aCategory , "OBJECT_NAME" , oName ) ;
		infoDataCenter.WriteProperty( aCategory , "STATE" , "Condition" ) ;
		infoDataCenter.WriteProperty( aCategory , "ASSIGNMENT" , "WaitForMainCharacter" ) ;
		
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		AgentUpdate() ;
	}
	
	protected override void DoCondition()
	{
		Conditon_DoWaitForPlayer() ;
	}
	
	protected override void DoAction()
	{
		Action_DoWaitForPlayer() ;
	}
	
	
	private void Conditon_DoWaitForPlayer()
	{
		InfoDataCenter infoDataCenter = GlobalSingleton.GetInfoDataCenter() ;
		if( null == m_GameObject )
		{
			string currentObjectName = infoDataCenter.ReadProperty( aCategory , "OBJECT_NAME" ) ;
			m_GameObject = GameObject.Find( currentObjectName ) ;
		}
		if( null == m_MainCharacter )
		{
			m_MainCharacter = GameObject.FindGameObjectWithTag( "Player" ) ;
		}
		if( null == m_Anchor22 )
		{
			m_Anchor22 = GameObject.Find( "Anchor22" ) ;
		}
		
		string assignmentStr = infoDataCenter.ReadProperty( aCategory , "ASSIGNMENT" ) ;
		
		if( "WaitForMainCharacter" == assignmentStr )
		{
			Vector3 mainCharObjPos = m_MainCharacter.transform.position ;
			Vector3 anchor22ObjPos = m_Anchor22.transform.position ;
			Vector3 distanceVec = mainCharObjPos - anchor22ObjPos ;
			distanceVec.z = 0 ;
			if( distanceVec.magnitude < m_CloseDistance )
			{
				// Debug.Log( "distanceVec < threashold" ) ;
				
				infoDataCenter.WriteProperty( aCategory , "ASSIGNMENT" , "ChangePicture" ) ;
				infoDataCenter.WriteProperty( aCategory , "TARGET_PICTURE" , "SignalRed" ) ;
				WriteAgentState( AgentState.Action ) ;
			}
		}
		else if( "Wait" == assignmentStr )
		{
		}
	}
	
	private void Action_DoWaitForPlayer()
	{
		InfoDataCenter infoDataCenter = GlobalSingleton.GetInfoDataCenter() ;
		if( null == m_GameObject ||
		   null == m_MainCharacter ||
		   null == m_Anchor22 )
		{
			Debug.Log( "null == m_GameObject") ;
			return;
		}
		
		string assignmentStr = infoDataCenter.ReadProperty( aCategory , "ASSIGNMENT" ) ;
		if( "WaitForMainCharacter" == assignmentStr )
		{
		}
		else if( "ChangePicture" == assignmentStr )
		{
			string targetPicture = infoDataCenter.ReadProperty( aCategory , "TARGET_PICTURE" ) ;
			
			Texture targetTexture = ResourceLoad.LoadTexture( targetPicture ) ;
			if( null != m_GameObject.renderer )
			{
				// Debug.Log( "ChangePicture" ) ;
				m_GameObject.renderer.material.mainTexture = targetTexture ;
			}
			infoDataCenter.WriteProperty( aCategory , "ASSIGNMENT" , "Wait" ) ;
		}
	}

	
}
