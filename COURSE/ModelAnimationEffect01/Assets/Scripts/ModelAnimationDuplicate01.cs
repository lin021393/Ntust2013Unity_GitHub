/*
@file ModelAnimationDuplicate01.cs
@author NDark
@date 20130927 file started
*/
using UnityEngine;
using System.Collections.Generic ;

public class ModelAnimationDuplicate01 : MonoBehaviour 
{
	// 產生的殘影數量
	public int m_ModelDuplicatedNum = 2 ;
	public List<GameObject> m_ModelDuplicated = new List<GameObject>() ; // 殘影的物件
	public Material m_DuplicatedMaterial = null ; // 提供給殘影的材質,請手動指定
	
	// 殘影撥放動畫的間隔(比例)	
	public float m_AnimationIntervalRatio = 0.1f ;
	
	// 儲存腳色經過位置的數量
	public int m_PositionQueueMaxNum = 30 ;
	
	// 儲存腳色經過位置的陣列
	public List<Vector3> m_PositionQueue = new List<Vector3>() ;
	
	// 殘影物件的在陣列上的間隔索引(絕對值)
	public int m_PositionIntervalNum = 10 ;
	
	// 目前正在播放的動畫名稱
	public string m_PlayAnimationString = "" ;
	
	// 目前正在播放的動畫速度
	public float m_AnimationSpeed = 0.1f ;
	
	// Use this for initialization
	void Start () 
	{
		// 產生殘影物件
		for( int i = 0 ; i < m_ModelDuplicatedNum ; ++i )
		{
			GameObject newObj = (GameObject) GameObject.Instantiate( 
				this.gameObject 
				/*, this.gameObject.transform.position + new Vector3( 1 , 0 , 0 ) , this.gameObject.transform.rotation */
				) ;
			
			// 移除殘影上的此腳本(否則會無限制創造殘影)
			ModelAnimationDuplicate01 removingScript = newObj.GetComponent<ModelAnimationDuplicate01>() ;
			Component.DestroyImmediate( removingScript ) ;
			
			// 增加殘影控制動畫的腳本
			newObj.AddComponent<PlayAnimationAtTime01>() ;
			
			
			newObj.name = this.gameObject.name + i.ToString() ;
			
			// 設定殘影的材質
			Renderer[] renderers = newObj.GetComponentsInChildren<Renderer>() ;
			for( int j = 0 ; j < renderers.Length ; ++j )
			{
				renderers[ j ].material = m_DuplicatedMaterial ;
			}
			
			m_ModelDuplicated.Add( newObj ) ;
		}

	}
	
	// Update is called once per frame
	void Update () 
	{
		if( null != this.animation )
		{
			// 播放動畫
			if( null != this.animation[ m_PlayAnimationString ] )
			{
				AnimationState animState = this.animation[ m_PlayAnimationString ] ;
				animState.speed = m_AnimationSpeed ;
				if( false == this.animation.IsPlaying( m_PlayAnimationString ) )
				{
					this.animation.Play( m_PlayAnimationString ) ;
				}
				
				// 更新殘影的動畫
				// 傳入目前的動畫時間以及殘影動畫的間隔時間
				UpdateDuplicatedModelAnimation( animState.time  , 
												animState.length * m_AnimationIntervalRatio ) ;
			}
		}
		
		// 更新殘影的位置
		UpdateDuplicatedModelPosition() ;
		
	}
	
	
	private void UpdateDuplicatedModelAnimation( float _currentTime , float _Interval )
	{
		// Debug.Log( "UpdateDuplicatedModelAnimation() " + _currentTime ) ;
		
		for( int i = 0 ; i < m_ModelDuplicated.Count ; ++i )
		{
			GameObject duplicatedObj = m_ModelDuplicated[ i ] ;
			if( null == duplicatedObj )
				continue ;
			
			// 播放殘影的動畫
			PlayAnimationAtTime01 script = duplicatedObj.GetComponent<PlayAnimationAtTime01>() ;
			if( null != script )
			{
				script.m_AnimationStr = m_PlayAnimationString ;
				
				// 計算殘影應該在的動畫時間
				float tryAnimTime = _currentTime - _Interval * ( i + 1 ) ;
				
				// Debug.Log( tryAnimTime ) ;
				if( tryAnimTime > 0 )
					script.m_AnimationTime = tryAnimTime ;
				else
					script.m_AnimationTime = 0 ;
				
				script.PlayAnimationAtSpecifiedTime() ;
			}
		}
	}
	
	private void UpdateDuplicatedModelPosition()
	{
		// 先將自己目前位置推到queue裡面
		m_PositionQueue.Add( this.transform.position ) ;
		if( m_PositionQueue.Count >= m_PositionQueueMaxNum )
		{
			m_PositionQueue.RemoveAt( 0 ) ;
		}
		
		for( int i = 0 ; i < m_ModelDuplicated.Count ; ++i )
		{
			GameObject duplicatedObj = m_ModelDuplicated[ i ] ;
			if( null == duplicatedObj )
				continue ;
			
			// 先計算殘影位置陣列的索引值
			int index = m_PositionQueue.Count - m_PositionIntervalNum * ( i + 1 ) ;
			// Debug.Log( index ) ;
			if( index >= 0 && index < m_PositionQueue.Count )
			{
				Vector3 duplicatedPos = duplicatedObj.transform.position ;
				Vector3 positionQueuePos = m_PositionQueue[ index ] ;
				
				duplicatedObj.transform.position = positionQueuePos ;
			}
			else
			{
				duplicatedObj.transform.position = m_PositionQueue[ 0 ] ;
			}

		}
		
	}
}
