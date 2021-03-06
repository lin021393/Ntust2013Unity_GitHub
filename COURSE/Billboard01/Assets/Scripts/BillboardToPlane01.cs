/*
@file BillboardToPlane01.cs
@author NDark
@date 20130809 file started.
*/
using UnityEngine;

public class BillboardToPlane01 : MonoBehaviour 
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
		
	
		
		// 因為plane的正面是朝向+y軸
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
			
		/*
		You can determine the direction of the result vector using the "left hand rule".
		http://docs.unity3d.com/Documentation/ScriptReference/Vector3.Cross.html
		 */
		Vector3 trueUpOfBillboard = 
			Vector3.Cross( m_FaceCamera.gameObject.transform.right , 
						   toTargetCamera ) ;
		
		this.gameObject.transform.rotation = 
			Quaternion.LookRotation( trueUpOfBillboard , 
									 toTargetCamera );
	}
	
	private void InitFaceCamera() 
	{
		m_FaceCamera = Camera.mainCamera ;
	}
}
