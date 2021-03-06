/*
@file ChessPieceMovement02.cs
@author NDark

將 m_TargetPos 改為 private 
並建立 Setup() 函式
mark [SerializeField]

@date 20130811 . file created and derieved from ChessPieceMovement01.
*/
using UnityEngine;

public class ChessPieceMovement02 : MonoBehaviour 
{
	public float m_ToTargetThreashold = 0.01f ; // 如何判定已經抵達
	public float m_MoveSpeed = 2.1f ;// 移動(內差)速度
	
	public void Setup( Vector3 _TargetPos )
	{
		if( true == this.enabled )
			return ;
		
		m_TargetPos = _TargetPos ;
		Debug.Log( "Setup" ) ;
		this.enabled = true ;
	}
	
	void Awake() 
	{
		Debug.Log( "Awake" ) ;
		m_TargetPos = this.gameObject.transform.position ;
		this.enabled = false ;
	}
	
	// Use this for initialization
	void Start () 
	{
		Debug.Log( "Start" ) ;
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
			Debug.Log( "distanceVecNow.magnitude < m_ToTargetThreashold" + distanceVecNow.magnitude ) ;
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
	
	// [SerializeField]
	private Vector3 m_TargetPos = Vector3.zero ; // 目前下一個目標位置
}
