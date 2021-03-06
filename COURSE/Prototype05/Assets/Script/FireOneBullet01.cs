/*
@file FireOneBullet01.cs
@author NDark
@date 20130823 file started.
*/
using UnityEngine;

public class FireOneBullet01 : MonoBehaviour 
{
	public enum FireState
	{
		Ready ,
		Fire , 
		Reload ,
	}	
	
	public FireState m_FireState = FireState.Ready ;
	public float m_FireTime = 0.0f ;
	public float m_ReloadTime = 1.0f ;
	public string m_BulletPrefabName = "Common/Prefabs/AlientBullet" ;
	static public int iterator = 0 ;
	
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		CheckFireReload() ;
	
	}
	
	private void CheckFireReload()
	{
		switch( m_FireState )
		{
		case FireState.Ready :
			m_FireState = FireState.Fire ;
			break ;
			
		case FireState.Fire :
			FireBullet() ;
			m_FireState = FireState.Reload ;
			m_FireTime = Time.timeSinceLevelLoad ;
			break ;
			
		case FireState.Reload :
			if( Time.timeSinceLevelLoad - m_FireTime > 
									m_ReloadTime )
				m_FireState = FireState.Ready ;
			break ;			
		}				
	}
	
	private void FireBullet()
	{
		// 因為怪物的方向相反,所以發射方向相反
		Object prefabObj = Resources.Load( m_BulletPrefabName ) ;
		if( null != prefabObj )
		{
			GameObject missleObj = (GameObject) GameObject.Instantiate( prefabObj ) ;
			if( null != missleObj )
			{
				
				missleObj.name = "AlienBulletObj" + iterator.ToString() ;
				missleObj.transform.position = this.gameObject.transform.position - this.gameObject.transform.forward ;
				missleObj.transform.rotation = this.gameObject.transform.rotation ;
				++iterator ;
				
				MissleMoveAndRecycle01 missle = missleObj.GetComponent<MissleMoveAndRecycle01>() ;
				if( null != missle )
				{
					missle.Setup( true , -1 * this.gameObject.transform.forward ) ;
				}
				Debug.Log( "FireOneBullet01:FireBullet() succeed." ) ;
			}
		}
	}	
}