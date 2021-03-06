/*
@file BillboardToGameObject01.cs
@author NDark
@date 20130811 file started.
*/
using UnityEngine;

public class BillboardToGameObject01 : MonoBehaviour 
{
	public Vector3 m_FaceTargetPos = Vector3.zero ;
	public Camera m_FaceCamera = null ;
	
	public enum TheSameAxis
	{
		no ,
		x ,
		y ,
		z ,
	}
	
	public TheSameAxis m_Axis = TheSameAxis.no ;
	// Use this for initialization
	void Start () 
	{
		if( null == m_FaceCamera )
		{
			InitFaceCamera() ;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 thisPos = this.gameObject.transform.position ;
		m_FaceTargetPos = m_FaceCamera.gameObject.transform.position ;
		Vector3 toTargetCamera = m_FaceTargetPos - thisPos  ;
		
		switch( m_Axis )
		{
		case TheSameAxis.x :
			toTargetCamera.x = 0 ; 
			break ;
		case TheSameAxis.y :
			toTargetCamera.y = 0 ; 
			break ;
		case TheSameAxis.z :
			toTargetCamera.z = 0 ; 
			break ;			
		}

		this.gameObject.transform.rotation = Quaternion.LookRotation( toTargetCamera , m_FaceCamera.gameObject.transform.up );
	}
	
	private void InitFaceCamera() 
	{
		m_FaceCamera = Camera.mainCamera ;
	}
}
