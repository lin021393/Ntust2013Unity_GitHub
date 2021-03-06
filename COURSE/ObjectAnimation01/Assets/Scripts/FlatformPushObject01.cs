/*
@file FlatformPushObject01.cs
@author NDark
@date 20130807 file started.
*/
using UnityEngine;
using System.Collections;

public class FlatformPushObject01 : MonoBehaviour 
{
	public float m_MoveSpeed = 5.0f ;
	CharacterController controller = null ;
	Vector3 m_PreviousPosOfPlatform = Vector3.zero ;
	Vector3 m_VelocityOfPlatform = Vector3.zero ;
	Transform activePlatform = null ;
	// Use this for initialization
	void Start () 
	{
		controller = this.GetComponent<CharacterController>() ;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if( null == controller )
			return ;
		
		
		if( false == controller.isGrounded )
		{
			controller.Move( Vector3.up * -1 ) ;
		}
		
		if( null != activePlatform )
		{
			m_VelocityOfPlatform = activePlatform.position - m_PreviousPosOfPlatform ;
			transform.position = transform.position + m_VelocityOfPlatform ;
			// controller.Move( m_VelocityOfPlatform ) ;
			m_PreviousPosOfPlatform = activePlatform.position ;
			// Debug.Log( m_VelocityOfPlatform ) ;
		}
		
		
		if( true == Input.GetKey( KeyCode.A )  )
		{
			controller.Move( new Vector3( -1 * m_MoveSpeed * Time.deltaTime , 0 , 0 ) ) ;
		}
		else if( true == Input.GetKey( KeyCode.D ) )
		{
			controller.Move( new Vector3( 1 * m_MoveSpeed * Time.deltaTime , 0 , 0 ) ) ;			
		}
		else if( true == Input.GetKey( KeyCode.S ) )
		{
			controller.Move( new Vector3( 0 , 0 , -1 * m_MoveSpeed * Time.deltaTime ) ) ;			
		}
		else if( true == Input.GetKey( KeyCode.W ) )
		{
			controller.Move( new Vector3( 0 , 0 , 1 * m_MoveSpeed * Time.deltaTime ) ) ;			
		}
				
	}
	
	void OnControllerColliderHit(ControllerColliderHit _hit) 
	{
		Debug.Log( "OnControllerColliderHit" ) ;
		activePlatform = _hit.collider.gameObject.transform ;
		m_PreviousPosOfPlatform = activePlatform.position ;
	}
}
