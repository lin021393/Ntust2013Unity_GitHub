/**
@file FollowTheMainCharacter03.cs
@author NDark
@date 20130820 . file started.
*/
using UnityEngine;

public class FollowTheMainCharacter03 : MonoBehaviour 
{
	public float m_Speed = 1.0f ;
	public GameObject m_MainCharacter = null ;
	public GameUnitData02 m_UnitData = null ;

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
			Debug.LogError( "FollowTheMainCharacter03:InitializeMainCharacterObjectPtr() null == m_MainCharacter" ) ;
		}
		else
		{
			Debug.Log( "FollowTheMainCharacter03:InitializeMainCharacterObjectPtr() end." ) ;
		}
	}
	
	private void InitializeUnitDataPtr()
	{
		
		m_UnitData = this.GetComponent<GameUnitData02>() ;
		
		if( null == m_UnitData )
		{
			Debug.LogError( "FollowTheMainCharacter03:InitializeUnitDataPtr() null == m_UnitData" ) ;
		}
		else
		{
			Debug.Log( "FollowTheMainCharacter03:InitializeUnitDataPtr() end." ) ;
		}
	}		
	
	private void FindTheVectorToMainCharacterAndGoToIt()
	{
		if( null == m_MainCharacter )
			return ;
		
		Vector3 toMainCharacter = m_MainCharacter.transform.position - this.transform.position ;
		toMainCharacter.Normalize() ;
		toMainCharacter.z = -1 ;// 強制往下
		Vector3 moveVec = toMainCharacter * ( m_Speed * Time.deltaTime ) ;
		
		if( null != m_UnitData )
		{
			m_UnitData.m_Velocity += moveVec ;
		}
		
	}
	

}
