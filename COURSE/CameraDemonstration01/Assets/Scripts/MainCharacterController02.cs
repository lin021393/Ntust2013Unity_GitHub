/**
 * @file MainCharacterController02.cs
 * @author NDark
 * @date 20130601
 * @date 20130713 . remove class member m_MousePositionLast
 */
using UnityEngine;
using System.Collections;

public class MainCharacterController02 : MonoBehaviour 
{
	public float m_MoveSpeed = 5.0f ;	
	public GameObject m_MainCharacterObject = null ;
	Quaternion m_RotationToPositiveX = Quaternion.identity ;// 朝向右邊
	Quaternion m_RotationToMinusX = Quaternion.identity ;// 朝向左邊
	
	

	// Use this for initialization
	void Start () 
	{
		InitializeMainCharacterObjectPtr() ;
		
		// set face to ( 1 , 0 , 0 )
		m_RotationToPositiveX.SetLookRotation( new Vector3( 1 , 0 , 0 ) , new Vector3( 0 , 1 , 0 ) ) ;
		
		// set face to ( -1 , 0 , 0 )
		m_RotationToMinusX.SetLookRotation( new Vector3( -1 , 0 , 0 ) , new Vector3( 0 , 1 , 0 ) ) ;
			
	}
	
	// Update is called once per frame
	void Update () 
	{
		Transform thisTransform = this.gameObject.transform ;
		if( true == Input.GetKey( KeyCode.A )  )
		{
			thisTransform.Translate( -1 * m_MoveSpeed * Time.deltaTime , 0 , 0 , Space.World ) ;
			
			// face left ( -1 , 0 , 0 )
			thisTransform.rotation = m_RotationToMinusX ;
				
				
		}
		else if( true == Input.GetKey( KeyCode.D ) )
		{
			thisTransform.Translate( 1 * m_MoveSpeed * Time.deltaTime , 0 , 0 , Space.World ) ;
			
			// face right ( 1 , 0 , 0 )
			thisTransform.rotation = m_RotationToPositiveX ;
		}
	}
	
	private void InitializeMainCharacterObjectPtr()
	{
		
		m_MainCharacterObject = this.gameObject ;

		if( null == m_MainCharacterObject )
		{
			Debug.LogError( "MainCharacterController01:InitializeMainCharacterObjectPtr() null == m_MainCharacterObject" ) ;
		}
		else
		{
			Debug.Log( "MainCharacterController01:InitializeMainCharacterObjectPtr() end." ) ;
		}
	}	
}
