/**
@file CameraFieldManager01.cs
@author NDark
@date 20130602 . file started.
@date 20130719
. rename class member m_LiftFieldObjects to m_FieldProbeObjects.
. rename class method GetLightFieldVec() to GetFieldVec()

 */
using UnityEngine;
using System.Collections;

public class CameraFieldManager01 : MonoBehaviour 
{
	public GameObject[] m_FieldProbeObjects ;
	
	public Vector3 GetFieldVec( Vector3 _TestPosition )
	{
		Vector3 distVec = Vector3.zero ;
		Vector3 ret = Vector3.zero ;
		
		// 找到最近的 m_FieldProbeObjects[ i ]
		float minSqrMagnitude = 9999999.999999f ;
		for( int i = 0 ; i < m_FieldProbeObjects.Length ; ++i )
		{
			distVec = m_FieldProbeObjects[ i ].transform.position - _TestPosition ;
			if( distVec.sqrMagnitude < minSqrMagnitude )
			{
				// 紀錄最近的 foward 向量
				ret = m_FieldProbeObjects[ i ].transform.forward ;
				
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
		Debug.Log( "CameraFieldManager01::InitilaizeCameraFieldObjects() end." ) ;
	}
	
}
