/**
@file GUITextureShake01.cs
@author NDark
@date 20130801 file started.
*/
using UnityEngine;

public class GUITextureShake01 : MonoBehaviour 
{
	public float m_ElapsedSec = 1.0f ;
	
	public float m_StartTime = 0.0f ;
	public float m_EndTime = 0.0f ;
	
	public GUITexture m_TargetGUITexture = null ;
	public Rect m_OrgRect = new Rect() ;
	public float m_ShakeRange = 3 ; 
	// Use this for initialization
	void Start () 
	{
		m_StartTime = Time.timeSinceLevelLoad ;
		m_EndTime = m_StartTime + m_ElapsedSec ;
		m_TargetGUITexture = this.gameObject.guiTexture ;
		m_OrgRect = new Rect( m_TargetGUITexture.pixelInset ) ;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if( Time.timeSinceLevelLoad > m_EndTime )
		{
			EndShake() ;
			Component.Destroy( this ) ;
		}
		else
		{
			KeepShaking() ;
		}
	}
	
	private void EndShake()
	{
		m_TargetGUITexture.pixelInset = new Rect( m_OrgRect ) ;
	}
	
	private void KeepShaking()
	{
		m_TargetGUITexture.pixelInset = 
			new Rect( Random.Range( m_OrgRect.x - m_ShakeRange , m_OrgRect.x + m_ShakeRange ) , 
					  Random.Range( m_OrgRect.y - m_ShakeRange , m_OrgRect.y + m_ShakeRange ) ,
					  m_TargetGUITexture.pixelInset.width ,
					  m_TargetGUITexture.pixelInset.height ) ;
	}	
}
