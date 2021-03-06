/*
@file ChessPieceMovement01.cs
@author NDark
@date 20130805 . file created.
@date 20130811 . replace destroy by enable at KeepMoving()
*/
using UnityEngine;

public class ChessPieceMovement01 : MonoBehaviour 
{

	public Vector3 m_TargetPos = Vector3.zero ; // 目前下一個目標位置
	public float m_ToTargetThreashold = 0.01f ; // 如何判定已經抵達
	public float m_MoveSpeed = 2.1f ;// 移動(內差)速度
	
	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
		KeepMoving() ;
	}
	

	private void KeepMoving()
	{
		Transform thisTransform = this.gameObject.transform ;
		Vector3 positionNow = thisTransform.position ;
		
		Vector3 distanceVecNow = m_TargetPos - positionNow ;
		if( distanceVecNow.magnitude < m_ToTargetThreashold ) 
		{
			// 立即抵達
			thisTransform.position = m_TargetPos ;
			this.enabled = false ;
		}
		else
		{
			// 繼續內差
			Vector3 nextPosition = Vector3.Lerp( positionNow , 
												 m_TargetPos , 
												 m_MoveSpeed * Time.deltaTime ) ;
			thisTransform.position = nextPosition ;
		}
	}
}
