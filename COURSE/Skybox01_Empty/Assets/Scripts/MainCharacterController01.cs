/**
@file MainCharacterController01.cs
@author NDark
@date 20130601
@date 20130712 by NDark 
. remove class member m_MouseMoveThreashold.
. remove class member m_MousePositionLast
. remove local variable m_Update
@date 20130730 by NDark
. add checking of m_EyeObjectPtr at Update()

 */
using UnityEngine;
using System.Collections;

public class MainCharacterController01 : MonoBehaviour 
{
	public float m_MoveSpeed = 5.0f ;
	public float m_RotationSpeed = 50.0f ;
	public GameObject m_EyeObjectPtr = null ;

	// Use this for initialization
	void Start () 
	{
		InitializeMainCharacterEyeObjectPtr() ;
	}
	
	// Update is called once per frame
	void Update () 
	{
		Transform thisTransform = this.gameObject.transform ;
		if( true == Input.GetKey( KeyCode.A )  )
		{
			thisTransform.Translate( -1 * m_MoveSpeed * Time.deltaTime , 0 , 0 , Space.Self ) ;
		}
		else if( true == Input.GetKey( KeyCode.D ) )
		{
			thisTransform.Translate( 1 * m_MoveSpeed * Time.deltaTime , 0 , 0 , Space.Self ) ;
		}
		else if( true == Input.GetKey( KeyCode.S ) )
		{
			thisTransform.Translate( 0 , 0 , -1 * m_MoveSpeed * Time.deltaTime , Space.Self ) ;
		}
		else if( true == Input.GetKey( KeyCode.W ) )
		{
			this.gameObject.transform.Translate( 0 , 0 , 1 * m_MoveSpeed * Time.deltaTime , Space.Self ) ;
		}
		
		float mouseDiff = Input.GetAxis( "Mouse X" ) ;
		if( Mathf.Abs( mouseDiff ) > 0 ) 
		{
			thisTransform.Rotate( Vector3.up , mouseDiff * m_RotationSpeed * Time.deltaTime , Space.Self ) ;
		}
		
		mouseDiff = Input.GetAxis( "Mouse Y" ) ;
		if( Mathf.Abs( mouseDiff ) > 0 )
		{
			if( null != m_EyeObjectPtr )
				m_EyeObjectPtr.transform.Rotate( Vector3.right , -1 * mouseDiff * m_RotationSpeed * Time.deltaTime , Space.Self ) ;
			else
				thisTransform.transform.Rotate( Vector3.right , -1 * mouseDiff * m_RotationSpeed * Time.deltaTime , Space.Self ) ;
		}		
		
	}
	
	private void InitializeMainCharacterEyeObjectPtr()
	{
		
		GameObject mainCharacter = this.gameObject ;
		if( null != mainCharacter )
		{
			Transform trans = mainCharacter.transform.FindChild( "MainCharacterEye" ) ;
			if( null != trans )
			{
				m_EyeObjectPtr = trans.gameObject ;
			}
		}
		
		if( null == m_EyeObjectPtr )
		{
			Debug.LogWarning( "MainCharacterController01:InitializeMainCharacterEyeObjectPtr() null == m_EyeObjectPtr" ) ;
		}
		else
		{
			Debug.Log( "MainCharacterController01:InitializeMainCharacterEyeObjectPtr() end." ) ;
		}
	}	
}
