/**
@file AwayFromTheMainCharacter01.cs
@author NDark
@date 20130820 . file started.
 */
using UnityEngine;
using System.Collections;

public class AwayFromTheMainCharacter01 : MonoBehaviour 
{
	public float m_Speed = 1.0f ;
	public GameObject m_MainCharacter = null ;
	public GameUnitData01 m_UnitData = null ;

	// Use this for initialization
	void Start () 
	{
		if( null == m_MainCharacter )
		{
			InitializeMainCharacterObjectPtr() ;
		}
		
		if( null == m_UnitData )
		{
			InitializeUnitDataPtr() ;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		FindTheVectorToMainCharacterAndGoToIt() ;
	}
	
	private void InitializeMainCharacterObjectPtr()
	{
		
		m_MainCharacter = GameObject.FindGameObjectWithTag( "Player" ) ;
		
		if( null == m_MainCharacter )
		{
			Debug.LogError( "AwayFromTheMainCharacter01:InitializeMainCharacterObjectPtr() null == m_MainCharacter" ) ;
		}
		else
		{
			Debug.Log( "AwayFromTheMainCharacter01:InitializeMainCharacterObjectPtr() end." ) ;
		}
	}
	
	private void InitializeUnitDataPtr()
	{
		
		m_UnitData = this.GetComponent<GameUnitData01>() ;
		
		if( null == m_UnitData )
		{
			Debug.LogError( "AwayFromTheMainCharacter01:InitializeUnitDataPtr() null == m_UnitData" ) ;
		}
		else
		{
			Debug.Log( "AwayFromTheMainCharacter01:InitializeUnitDataPtr() end." ) ;
		}
	}		
	
	private void FindTheVectorToMainCharacterAndGoToIt()
	{
		if( null == m_MainCharacter )
			return ;
		
		Vector3 toMainCharacter = m_MainCharacter.transform.position - this.transform.position ;
		toMainCharacter.Normalize() ;
		Vector3 awayMainCharacter = toMainCharacter * -1 ;
		Vector3 moveVec = awayMainCharacter * ( m_Speed * Time.deltaTime ) ;
		if( null != m_UnitData )
		{
			m_UnitData.m_Velocity += moveVec ;
		}
		
	}
	

}
