/**
 * @file FollowTheMainCharacter01.cs
 * @author NDark
 * @date 20130630 . file started.
 */
using UnityEngine;
using System.Collections;

public class FollowTheMainCharacter01 : MonoBehaviour 
{
	public float m_Speed = 1.0f ;
	GameObject m_MainCharacter = null ;

	// Use this for initialization
	void Start () 
	{
		InitializeMainCharacterObjectPtr() ;
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
			Debug.LogError( "MainCharacterController03:InitializeMainCharacterObjectPtr() null == m_MainCharacter" ) ;
		}
		else
		{
			Debug.Log( "MainCharacterController03:InitializeMainCharacterObjectPtr() end." ) ;
		}
	}	
	
	private void FindTheVectorToMainCharacterAndGoToIt()
	{
		if( null == m_MainCharacter )
			return ;
		
		Vector3 toMainCharacter = 
			m_MainCharacter.transform.position - this.transform.position ;
		toMainCharacter.Normalize() ;
		Vector3 moveVec = toMainCharacter * ( m_Speed * Time.deltaTime ) ;
		this.transform.Translate( moveVec ) ;
	}
	

}
