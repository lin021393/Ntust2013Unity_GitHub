/*
@file DrivingCar01.cs
@author NDark
@date 20131005 file started.
*/
using UnityEngine;

public class DrivingCar01 : MonoBehaviour 
{
	public WheelCollider m_WheelCollderFR = null ;
	public WheelCollider m_WheelCollderFL = null ;
	public GameObject m_WheelModelFR = null ;
	public GameObject m_WheelModelFL = null ;
	public GameObject m_WheelModelBR = null ;
	public GameObject m_WheelModelBL = null ;

	private Animation m_WheelFRAnimation = null ;
	private Animation m_WheelFLAnimation = null ;
	private Animation m_WheelBRAnimation = null ;
	private Animation m_WheelBLAnimation = null ;

	Quaternion m_OrgRotationWheelRF = Quaternion.identity ;
	Quaternion m_OrgRotationWheelLF = Quaternion.identity ;
	Quaternion m_RotationRight = Quaternion.identity ;
	Quaternion m_RotationLeft = Quaternion.identity ;
	// Use this for initialization
	void Start () 
	{
		if(	null != m_WheelModelFR &&
			null != m_WheelModelFL )
		{
			m_OrgRotationWheelRF = m_WheelModelFR.transform.localRotation ;
			m_OrgRotationWheelLF = m_WheelModelFL.transform.localRotation ;
			m_RotationRight = m_OrgRotationWheelRF * Quaternion.AngleAxis( 20 , Vector3.right ) ;
			m_RotationLeft = m_OrgRotationWheelLF * Quaternion.AngleAxis( -20 , Vector3.right ) ;

			m_WheelFRAnimation = m_WheelModelFR.GetComponentInChildren<Animation>() ;
			m_WheelFLAnimation = m_WheelModelFL.GetComponentInChildren<Animation>() ;
		}

		if(	null != m_WheelModelBR &&
		   null != m_WheelModelBL )
		{
			m_WheelBRAnimation = m_WheelModelBR.GetComponentInChildren<Animation>() ;
			m_WheelBLAnimation = m_WheelModelBL.GetComponentInChildren<Animation>() ;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if( null != m_WheelCollderFR &&
			null != m_WheelCollderFL &&
			null != m_WheelModelFR &&
			null != m_WheelModelFL )
		{
			float updownValue = Input.GetAxis("Vertical") ;

			m_WheelCollderFR.motorTorque = 260 * updownValue ;
			m_WheelCollderFL.motorTorque = 260 * updownValue ;

			float speed = this.gameObject.rigidbody.velocity.magnitude ;

			RunWheelAnimation( speed ) ;

			float leftRightValue = Input.GetAxis("Horizontal") ;
			m_WheelCollderFR.steerAngle = 20 * leftRightValue ;
			m_WheelCollderFL.steerAngle = 20 * leftRightValue ;

			RotateInModel( leftRightValue ) ;

		
		}

	
	}

	private void RunWheelAnimation( float _Speed )
	{
		if( null != m_WheelFRAnimation 
		   && null != m_WheelFLAnimation 
		   && null != m_WheelBRAnimation 
		   && null != m_WheelBLAnimation )
		{
			if( Mathf.Abs( _Speed ) > 0.1f )
			{
				m_WheelFRAnimation.Play() ;
				m_WheelFLAnimation.Play() ;
				m_WheelBRAnimation.Play() ;
				m_WheelBLAnimation.Play() ;
			}
			else
			{
				m_WheelFRAnimation.Stop() ;
				m_WheelFLAnimation.Stop() ;
				m_WheelBRAnimation.Stop() ;
				m_WheelBLAnimation.Stop() ;
			}
		}

	}

	private void RotateInModel( float _LeftRightValue )
	{
		if( _LeftRightValue > 0.1f )
		{
			m_WheelModelFR.transform.localRotation = m_RotationRight ;
			m_WheelModelFL.transform.localRotation = m_RotationRight ;
		}
		else if( _LeftRightValue < -0.1f )
		{
			m_WheelModelFR.transform.localRotation = m_RotationLeft ;
			m_WheelModelFL.transform.localRotation = m_RotationLeft ;
		}
		else
		{
			m_WheelModelFR.transform.localRotation = m_OrgRotationWheelRF ;
			m_WheelModelFL.transform.localRotation = m_OrgRotationWheelLF ;
		}

	}
}
