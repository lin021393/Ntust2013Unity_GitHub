/*
@file MouseOver_ResizeObj.cs
@author NDark
@date 20130825 file started and derived from GUI_ResizeTexture
*/
using UnityEngine;

public class MouseOver_ResizeObj : MonoBehaviour 
{
	public bool m_Active = false ;
	public Vector3 m_InitScale = Vector3.zero ;
	public Vector3 m_TargetScale = new Vector3( 2 , 2 , 2 ) ;
	public float m_ScaleSpeed = 0.1f ;
	public float m_Threashold = 0.01f ;
	public Vector3 m_ScaleNow = Vector3.zero ;
		
	// Use this for initialization
	void Start () 
	{
		m_InitScale = this.gameObject.transform.localScale ;
		m_ScaleNow = m_InitScale ;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if( true == m_Active )
		{
			ResizeObject() ;
		}
	}
	
	void OnMouseEnter()
	{
		m_Active = true ;
	}

	void OnMouseExit()
	{
		m_Active = false ;
		this.transform.localScale = m_InitScale ;
	}
	
	protected void ResizeObject()
	{
		float positiveScaleSpeed = m_ScaleSpeed ;
		if( m_ScaleSpeed < 0 )
		{
			positiveScaleSpeed = -1 * m_ScaleSpeed ;
			m_ScaleNow = Vector3.Lerp( m_ScaleNow , 
									   m_InitScale , 
									   positiveScaleSpeed * Time.deltaTime ) ;
			if( Vector3.Distance( m_ScaleNow , m_InitScale ) < m_Threashold )
			{
				m_ScaleSpeed *= -1 ;
			}
		}
		else
		{
			m_ScaleNow = Vector3.Lerp( m_ScaleNow , 
									   m_TargetScale , 
									   positiveScaleSpeed * Time.deltaTime ) ;
			if( Vector3.Distance( m_ScaleNow , m_TargetScale ) < m_Threashold )
			{
				m_ScaleSpeed *= -1 ;
			}
		}
		
		this.transform.localScale = m_ScaleNow ;
	}
	
}
