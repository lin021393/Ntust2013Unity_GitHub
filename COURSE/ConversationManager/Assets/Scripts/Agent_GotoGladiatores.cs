/**
@file Agent_GotoGladiatores.cs
@author NDark
@date 20140330 . file started.
@date 20140501 by NDark
. add class member orgCloseDistance
. add code of calculation closeDistance by Time.timeScale at Update()

 */
using UnityEngine;

public class Agent_GotoGladiatores : AgentBase 
{
	private string oName = "MainCharacter" ;
	private string aName = "MainCharacter" ;
	private string aCategory = "CHARACTER_MainCharacter" ;

	private GameObject m_CarObject = null ;


	private GameObject m_GameObject = null ;
	private float carDistance = 0.07f ;
	private float orgBreakDistance = 0.01f ;
	private float breakDistance = 0.01f ;	
	private float orgCloseDistance = 0.001f ;
	private float closeDistance = 0.001f ;
	private int m_TargetObjectIndex = 0 ;
	private string []m_TargetObjectNames = 
	{
		"ChangePicture:DBLogo" ,
		"DeuscheMapAnchor01" ,
		"DeuscheMapAnchor02" ,
		"WaitSec:1" ,
		"HideObj:MapZoneObject00" ,
		"WaitSec:0.5" ,
		"ChangePicture:pedestrian" ,
		"WaitSec:1" ,
		"Anchor01" ,
		"ChangePicture:SBahn" ,
		"Anchor011" ,
		"Anchor02" ,
		"Anchor03" ,
		"Anchor04" ,
		"Anchor05" ,
		"Anchor06" ,
		"Anchor07" ,
		"ChangePicture:pedestrian" ,
		"Anchor08" ,
		"ChangePicture:SBahn" ,
		"Anchor09" ,
		"Anchor10" ,
		"Anchor11" ,
		"Anchor12" ,
		"Anchor13" ,
		"Anchor14" ,
		"Anchor15" ,
		"Anchor16" ,
		"Anchor17" ,
		"Anchor18" ,
		"Anchor19" ,
		"Anchor20" ,
		"Anchor21" ,
		"ChangePicture:pedestrian" ,
		"Anchor22" ,
		"WaitCar:CarObj" ,
		"Anchor23" ,
		"Anchor24" ,
		"Anchor25" ,
		"Anchor26" ,
		"WaitSec:5" ,
		"LoadScene:GladiatoresAd" ,
	} ;

	// Use this for initialization
	void Start () 
	{
		this.AgentName = aName ;

		AgentStart() ;

		// MainCharacter
		InfoDataCenter infoDataCenter = GlobalSingleton.GetInfoDataCenter() ;
		infoDataCenter.WriteProperty( aCategory , "OBJECT_NAME" , oName ) ;
		infoDataCenter.WriteProperty( aCategory , "STATE" , "Condition" ) ;
		infoDataCenter.WriteProperty( aCategory , "ASSIGNMENT" , "GoToTarget" ) ;

	}
	
	// Update is called once per frame
	void Update () 
	{
		AgentUpdate() ;


		closeDistance = Time.timeScale * orgCloseDistance ;
		breakDistance = Time.timeScale * orgBreakDistance ;
	}

	protected override void DoCondition()
	{
		Conditon_DoGoToGladiatores() ;
	}
	
	protected override void DoAction()
	{
		Action_DoGoToGladiatores() ;
	}

	
	private void Conditon_DoGoToGladiatores()
	{
		InfoDataCenter infoDataCenter = GlobalSingleton.GetInfoDataCenter() ;
		if( null == m_GameObject )
		{
			string currentObjectName = infoDataCenter.ReadProperty( aCategory , "OBJECT_NAME" ) ;
			m_GameObject = GameObject.Find( currentObjectName ) ;
		}
		
		string assignmentStr = infoDataCenter.ReadProperty( aCategory , "ASSIGNMENT" ) ;

		// Debug.Log( "assignmentStr" + assignmentStr ) ;
		
		if( "Wait" == assignmentStr )
		{
			// do nothing.
		}
		else if( "WaitSec" == assignmentStr )
		{

		}
		else if( "WaitCar" == assignmentStr )
		{
			string targetObjectNameStr = infoDataCenter.ReadProperty( aCategory , 
			                                                         "TARGET_OBJECT_NAME" ) ;

			if( null == m_CarObject )
			{
				m_CarObject = GameObject.Find( targetObjectNameStr ) ;
			}

			if( null != m_CarObject )
			{
				Vector3 targetPositon = m_CarObject.transform.position ;
				Vector3 currentPosistion = m_GameObject.transform.position ;
				Vector3 distanceVec = targetPositon - currentPosistion ;
				distanceVec.z = 0 ;
				// Debug.Log( "Conditon_DoGoToGladiatores():distanceVec.magnitude=" + distanceVec.magnitude ) ;
				if( distanceVec.magnitude < carDistance )
				{
					// replace correct action object
					infoDataCenter.WriteProperty( aCategory , "ASSIGNMENT" , "GoToTarget" ) ;
				}
			}
		}
		else if( "ChangePicture" == assignmentStr )
		{
			WriteAgentState( AgentState.Action ) ;
		}
		else if( "HideObj" == assignmentStr )
		{
			WriteAgentState( AgentState.Action ) ;
		}
		else if( "GoToTarget" == assignmentStr )
		{
			// Debug.Log( "Conditon_DoGoToGladiatores():GoToTarget" ) ;

			bool FindNextTargetObject = false ;

			string targetPositionStr = infoDataCenter.ReadProperty( aCategory , "TARGET_POSITION" ) ;
			string sourcePositionStr = infoDataCenter.ReadProperty( aCategory , "SOURCE_POSITION" ) ;
			// Debug.Log( "Conditon_DoGoToGladiatores():targetPositionStr" + targetPositionStr ) ;
			if( 0 == targetPositionStr.Length )
			{
				string targetObjectNameStr = infoDataCenter.ReadProperty( aCategory , "TARGET_OBJECT_NAME" ) ;
				if( 0 == targetObjectNameStr.Length )
				{
					FindNextTargetObject = true ;
				}
				else
				{
					GameObject obj = GameObject.Find( targetObjectNameStr ) ;
					if( null != obj )
					{
						targetPositionStr = string.Format( "{0},{1},{2}" , 
						                                  obj.transform.position.x , obj.transform.position.y , obj.transform.position.z ) ;
						sourcePositionStr = string.Format( "{0},{1},{2}" , 
						                                  m_GameObject.transform.position.x , m_GameObject.transform.position.y , m_GameObject.transform.position.z ) ;
						// Debug.Log( "Conditon_DoGoToGladiatores():targetPositionStr=" + targetPositionStr ) ;
						if( 0 == sourcePositionStr.Length )
						{
							infoDataCenter.WriteProperty( aCategory , "SOURCE_POSITION" , sourcePositionStr ) ;
						}
						infoDataCenter.WriteProperty( aCategory , "TARGET_POSITION" , targetPositionStr ) ;
					}
				}
			}

			// check distance
			if( 0 != targetPositionStr.Length )
			{
				Vector3 targetPositon = Vector3FromFromStr( targetPositionStr ) ;
				Vector3 currentPosistion = m_GameObject.transform.position ;
				targetPositon.z = currentPosistion.z ;
				float distanceToTarget = Vector3.Distance( targetPositon , currentPosistion ) ;
				if( distanceToTarget > closeDistance )
				{
					// replace correct action object
					WriteAgentState( AgentState.Action ) ;
				}
				else
				{
					FindNextTargetObject = true ;
				}
			}

			if( true == FindNextTargetObject )
			{
				// next target position
				if( m_TargetObjectIndex >= m_TargetObjectNames.Length )
				{
					Debug.Log( "Conditon_DoGoToGladiatores():m_TargetObjectIndex >= m_TargetObjectNames.Length" ) ;
					infoDataCenter.WriteProperty( aCategory , "ASSIGNMENT" , "Wait" ) ;
				}
				else 
				{
					string targetName = m_TargetObjectNames[ m_TargetObjectIndex ] ;

					// check special word
					if( 0 == targetName.IndexOf( "ChangePicture" ) )
					{
						// Debug.Log( "ChangePicture" ) ;
						int index = targetName.IndexOf( ":" ) ;
						string targetPicture = "" ;
						if( 0 != index )
						{
							targetPicture = targetName.Substring( index +  1 ) ;
						}
						infoDataCenter.WriteProperty( aCategory , "ASSIGNMENT" , "ChangePicture" ) ;
						infoDataCenter.WriteProperty( aCategory , "TARGET_PICTURE" , targetPicture ) ;
						WriteAgentState( AgentState.Action ) ;
					}
					else if( 0 == targetName.IndexOf( "LoadScene" ) )
					{
						int index = targetName.IndexOf( ":" ) ;
						string targetSceneStr = "" ;
						if( 0 != index )
						{
							targetSceneStr = targetName.Substring( index +  1 ) ;

						}
						infoDataCenter.WriteProperty( aCategory , "ASSIGNMENT" , "LoadScene" ) ;
						infoDataCenter.WriteProperty( aCategory , "TARGET_SCENE" , targetSceneStr ) ;
						WriteAgentState( AgentState.Action ) ;
					}
					else if( 0 == targetName.IndexOf( "WaitSec" ) )
					{
						int index = targetName.IndexOf( ":" ) ;
						string targetSecStr = "" ;
						if( 0 != index )
						{
							targetSecStr = targetName.Substring( index +  1 ) ;
							float elapsedSec = 0 ;
							float.TryParse( targetSecStr , out elapsedSec ) ;
							float stopTime = Time.timeSinceLevelLoad + elapsedSec ;
							targetSecStr = stopTime.ToString() ;
						}
						infoDataCenter.WriteProperty( aCategory , "ASSIGNMENT" , "WaitSec" ) ;
						infoDataCenter.WriteProperty( aCategory , "TARGET_TIME" , targetSecStr ) ;
						WriteAgentState( AgentState.Action ) ;
					}
					else if( 0 == targetName.IndexOf( "WaitCar" ) )
					{

						int index = targetName.IndexOf( ":" ) ;
						string targetCarStr = "" ;
						if( 0 != index )
						{
							targetCarStr = targetName.Substring( index +  1 ) ;
						}
						infoDataCenter.WriteProperty( aCategory , "ASSIGNMENT" , "WaitCar" ) ;
						infoDataCenter.WriteProperty( aCategory , "TARGET_OBJECT_NAME" , 
						                             targetCarStr ) ;
					}
					else if( 0 == targetName.IndexOf( "HideObj" ) )
					{
						int index = targetName.IndexOf( ":" ) ;
						string targetObjStr = "" ;
						if( 0 != index )
						{
							targetObjStr = targetName.Substring( index +  1 ) ;
						}
						infoDataCenter.WriteProperty( aCategory , "ASSIGNMENT" , "HideObj" ) ;
						infoDataCenter.WriteProperty( aCategory , "TARGET_OBJECT_NAME" , 
						                             targetObjStr ) ;
					}
					else
					{
						Debug.Log( "Conditon_DoGoToGladiatores():targetName=" + targetName ) ;
						infoDataCenter.WriteProperty( aCategory , "TARGET_OBJECT_NAME" , targetName ) ;
						if( 0 != targetPositionStr.Length )
						{
							infoDataCenter.WriteProperty( aCategory , "SOURCE_POSITION" , targetPositionStr ) ;// record TARGET_POSITION to source position
							Debug.Log( "Conditon_DoGoToGladiatores() SOURCE_POSITION=" + targetPositionStr ) ;
						}
						infoDataCenter.WriteProperty( aCategory , "TARGET_POSITION" , "" ) ;// clear TARGET_POSITION
					}

					++m_TargetObjectIndex ;	
				}
			}
		}
	}
	
	private void Action_DoGoToGladiatores()
	{
		InfoDataCenter infoDataCenter = GlobalSingleton.GetInfoDataCenter() ;
		if( null == m_GameObject )
		{
			Debug.Log( "null == m_GameObject") ;
			return;
		}

		string assignmentStr = infoDataCenter.ReadProperty( aCategory , "ASSIGNMENT" ) ;

		if( "Wait" == assignmentStr )
		{
			// do nothing.
		}
		else if( "LoadScene" == assignmentStr )
		{

			string targetSceneStr = infoDataCenter.ReadProperty( aCategory , "TARGET_SCENE" ) ;
			Application.LoadLevel( targetSceneStr ) ;

		}
		else if( "WaitSec" == assignmentStr )
		{
			// do nothing.
			string targetTimeStr = infoDataCenter.ReadProperty( aCategory , "TARGET_TIME" ) ;
			float targetTime = 0 ;
			float.TryParse( targetTimeStr , out targetTime ) ;
			if( Time.timeSinceLevelLoad > targetTime )
			{
				infoDataCenter.WriteProperty( aCategory , "ASSIGNMENT" , "GoToTarget" ) ;
				WriteAgentState( AgentState.Condition ) ;
			}
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

			infoDataCenter.WriteProperty( aCategory , "ASSIGNMENT" , "GoToTarget" ) ;
			WriteAgentState( AgentState.Condition ) ;
		}
		else if( "HideObj" == assignmentStr )
		{
			string targetObjName = infoDataCenter.ReadProperty( aCategory , "TARGET_OBJECT_NAME" ) ;
			GameObject obj = GameObject.Find( targetObjName ) ;
			if( null != obj &&
			   null != obj.renderer )
			{
				Debug.Log( "HideObj" ) ;
				obj.renderer.enabled = false ;
			}
			
			infoDataCenter.WriteProperty( aCategory , "ASSIGNMENT" , "GoToTarget" ) ;
			WriteAgentState( AgentState.Condition ) ;
		}
		else if( "GoToTarget" == assignmentStr )
		{

			string targetPositionStr = infoDataCenter.ReadProperty( aCategory , "TARGET_POSITION" ) ;
			string sourcePositionStr = infoDataCenter.ReadProperty( aCategory , "SOURCE_POSITION" ) ;
			Vector3 targetPositon = Vector3FromFromStr( targetPositionStr ) ;
			Vector3 sourcePosition = Vector3FromFromStr( sourcePositionStr ) ;

			Vector3 sourceToTargetVec = targetPositon - sourcePosition ;
			
			Vector3 currentPosistion = m_GameObject.transform.position ;
			Vector3 sourceToCurrentVec = currentPosistion - sourcePosition ;
			targetPositon.z = currentPosistion.z ;

			Vector3 distanceVec = targetPositon - currentPosistion ;
			float distanceToTarget = distanceVec.magnitude ;
//			Debug.Log( "Action_DoGoToGladiatores()::targetPositionStr=" + targetPositionStr ) ;
//			Debug.Log( "Action_DoGoToGladiatores()::currentPosistion=" + currentPosistion ) ;
//			Debug.Log( "Action_DoGoToGladiatores()::distanceVec=" + distanceVec ) ;

			Rigidbody2D r2d = m_GameObject.rigidbody2D ;
			if( 0 != sourcePositionStr.Length &&
			    0 != targetPositionStr.Length &&
				sourceToCurrentVec.magnitude > sourceToTargetVec.magnitude )
			{
				// stop right now
				r2d.velocity = Vector2.zero ;
				WriteAgentState( AgentState.Condition ) ;
			}
			else if( distanceToTarget > breakDistance )
			{
				// keep going
				
				if( null != r2d )
				{
					distanceVec.Normalize() ;
					if( r2d.velocity.magnitude < 0.1f )
						r2d.AddForce( distanceVec * 0.1f ) ;
				}
			}
			else if( distanceToTarget > closeDistance )
			{
				if( r2d.velocity.magnitude >= 0.01f )
				{
					r2d.velocity *= 0.99f ;// 減速
				}
				else 
				{
					distanceVec.Normalize() ;
					r2d.velocity = distanceVec * 0.01f ;
				}
			}
			else
			{
				r2d.velocity = Vector2.zero ;
				WriteAgentState( AgentState.Condition ) ;
			}
		}
	}

}
