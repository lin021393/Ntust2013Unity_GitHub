/*
@file UpgradeAndEndAnimation.cs
@author NDark
@date 20130828 file started.
*/
using UnityEngine;

public class UpgradeAndEndAnimation : MonoBehaviour 
{
	static TripleTaoManager s_TripleTaoManager = null ;
	
	public float m_AnimSec = 1.0f ;
	public float m_StartTime = 0.0f ;
	public UnitType m_UpGradeTo = UnitType.grass ;
	
	// Use this for initialization
	void Start () 
	{
		if( null == s_TripleTaoManager )
		{
			GameObject obj = GameObject.Find( "GlobalSingleton" ) ;
			if( null != obj )
			{
				s_TripleTaoManager = obj.GetComponent<TripleTaoManager>() ;
			}
		}
		
		m_StartTime = Time.timeSinceLevelLoad ;
	}
	
	// Update is called once per frame
	void Update () 
	{
		float elapsedSec = Time.timeSinceLevelLoad - m_StartTime  ;
		if( elapsedSec > m_AnimSec )
		{
			Upgrade() ;
			TellTrippleTaoManagerEndCollectAnimation() ;
			Suicide() ;
		}
	}
	
	private void Upgrade()
	{
		UnitData unitData = this.gameObject.GetComponent<UnitData>() ;
		if( null != unitData )
		{
			unitData.Upgrade( m_UpGradeTo ) ;
		}
	}
	
	private void TellTrippleTaoManagerEndCollectAnimation()
	{
		if( null != s_TripleTaoManager )
		{
			s_TripleTaoManager.m_CollectAnimation = false ;
		}
	}	
	
	private void Suicide()
	{
		Component.Destroy( this ) ;
	}		
}
 