/*
@file ScoreTextDisplay02.cs
@author NDark
@date 20130825 file started.
*/
using UnityEngine;

public class ScoreTextDisplay02 : MonoBehaviour 
{
	public bool m_Active = false ;
	public GUIText m_GUIText = null ;
	public float m_CurrentScore = 0.0f ;
	public int m_TargetScore = 0 ;
	public float m_ScoreSpeed = 10.0f ;
	
	public float m_ScorllingInSec = 0.0f ;
	public void SetScore( int _TargetScore )
	{
		if( _TargetScore == m_CurrentScore )
			return ;
		
		if( 0.0 != m_ScorllingInSec )
		{
			m_ScoreSpeed = ( _TargetScore - m_CurrentScore ) / m_ScorllingInSec ;
		}
		
		if( _TargetScore < m_CurrentScore && 
			m_ScoreSpeed > 0 )
			m_ScoreSpeed *= -1 ;
		else if( _TargetScore > m_CurrentScore && 
			m_ScoreSpeed < 0 )
			m_ScoreSpeed *= -1 ;
		m_TargetScore = _TargetScore ;
		m_Active = true ;
	}

	// Use this for initialization
	void Start () 
	{
		m_GUIText = this.gameObject.guiText ;
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if( null == m_GUIText )
			return ;
		if( true == m_Active )
		{
			UpdateScore() ;
		}
	
	}
	
	private void UpdateScore()
	{
		float tmpScore = m_CurrentScore + m_ScoreSpeed * Time.deltaTime ;
		if( m_ScoreSpeed > 0 && 
			tmpScore > m_TargetScore )
		{
			tmpScore = m_TargetScore ;
			m_Active = false ;
		}
		else if( m_ScoreSpeed < 0 && 
				 tmpScore < m_TargetScore )
		{
			tmpScore = m_TargetScore ;
			m_Active = false ;
		}
		else
		{
			
		}
		
		float m_PreviousScore = m_CurrentScore ;
		m_CurrentScore = tmpScore ;
		// Debug.Log( "tmpScore" + tmpScore ) ;
		// 在設定current score之前 判斷 真正更新
		string previousScoreStr = ((int)m_PreviousScore).ToString() ;
		string nextScoreStr = ((int)tmpScore).ToString() ;
		string targetScoreStr = ((int)m_TargetScore).ToString() ;
		
		int trueDigitLength = nextScoreStr.Length ;
		
		while( previousScoreStr.Length < targetScoreStr.Length )
		{
			previousScoreStr = "0" + previousScoreStr ;
		}
		
		while( nextScoreStr.Length < targetScoreStr.Length )
		{
			nextScoreStr = "0" + nextScoreStr ;
		}
		
		// Debug.Log( "nextScoreStr" + nextScoreStr ) ;
		
		char[] previousScoreChar = previousScoreStr.ToCharArray() ;
		char[] nextScoreChar = nextScoreStr.ToCharArray() ;
		char[] targetScoreChar = targetScoreStr.ToCharArray() ;
		
//		Debug.Log( "length" + previousScoreChar.Length +
//			" " + nextScoreChar.Length +
//			" " + targetScoreChar.Length ) ;
		
		int preserveDigit = 3 ;
		for( int i = 0 ; i < nextScoreChar.Length ; ++i )
		{
			char digitStrPrevious = previousScoreChar[ i ] ;
			char digitStrTarget = targetScoreChar[ i ] ;
			char digitStrNext = targetScoreChar[ i ] ;
//			Debug.Log( "digitStr1" + digitStr1 ) ;
//			Debug.Log( "digitStr2" + digitStr2 ) ;
			if( digitStrTarget == digitStrNext )
			{
				// 到位
			}
			else
			{
				// 尚未到位
				if( nextScoreChar.Length - trueDigitLength + i <= preserveDigit )
				{
				}
				else 
				{
					// 超過保留位 遞增
					
					int tmpInt = System.Convert.ToInt32( digitStrPrevious ) ;
					// Debug.Log( digitStrPrevious + " => " + tmpInt  ) ;
					tmpInt-=48 ;
					++tmpInt ;
					tmpInt = tmpInt % 10 ;
					char charInt = System.Convert.ToChar( tmpInt + 48 ) ;
					nextScoreChar[ i ] = charInt ;
					
				}
			}
			// Debug.Log( "nextScoreChar[ " + i.ToString() + " ]" + nextScoreChar[ i ] ) ;
		}
		
		
		string displayScoreStr = new string( nextScoreChar ) ;
		// Debug.Log( "displayScoreStr" + displayScoreStr + "." + displayScoreStr.Length );
		m_GUIText.text = displayScoreStr.ToString() ;
	}
}
