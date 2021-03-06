/**
@file CameraFieldManager02.cs
@author NDark
@date 20130602 . file started.
@date 20130719 by NDark 
. rename class member m_LiftFieldObjects to m_FieldProbeObjects.
. rename class member m_CameraPositionObjects to m_CameraPoseObjects.
 */
using UnityEngine;
using System.Collections;

public class CameraFieldManager02 : MonoBehaviour 
{
	public GameObject[] m_FieldProbeObjects = null ;
	public GameObject[] m_CameraPoseObjects = null ;
	
	public GameObject GetLightFieldPoseGameObject( Vector3 _TestPosition )
	{
		Vector3 distVec = Vector3.zero ;
		GameObject ret = null ;
		float minSqrMagnitude = 9999999.999999f ;
		for( int i = 0 ; i < m_FieldProbeObjects.Length ; ++i )
		{
			distVec = m_FieldProbeObjects[ i ].transform.position - _TestPosition ;
			if( distVec.sqrMagnitude < minSqrMagnitude )
			{
				ret = m_CameraPoseObjects[ i ] ;
				minSqrMagnitude = distVec.sqrMagnitude ;
			}
		}
		return ret ;
	}
	
	// Use this for initialization
	void Start () 
	{
		InitilaizeCameraFieldObjects() ;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	private void InitilaizeCameraFieldObjects()
	{
		m_FieldProbeObjects = new GameObject[2] ;
		m_FieldProbeObjects[ 0 ] = GameObject.Find( "CameraFieldProbe0" ) ;
		m_FieldProbeObjects[ 1 ] = GameObject.Find( "CameraFieldProbe1" ) ;		
		
		m_CameraPoseObjects = new GameObject[2] ;
		m_CameraPoseObjects[ 0 ] = GameObject.Find( "CameraFieldPosition0" ) ;
		m_CameraPoseObjects[ 1 ] = GameObject.Find( "CameraFieldPosition1" ) ;				
	}
	
}
