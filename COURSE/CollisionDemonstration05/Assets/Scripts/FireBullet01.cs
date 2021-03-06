/**
@file FireBullet01.cs
@author NDark
@date 20130801 . file started.
*/
using UnityEngine;

public class FireBullet01 : MonoBehaviour 
{
	public float m_MoveSpeed = 5.0f ;
	public float m_MouseMoveThreashold = 1.0f ;
	public float m_RotationSpeed = 1f ;
	public Vector3 m_RotationUpVec = Vector3.up ;
	public int iterator = 0 ;
	public string m_BulletPrefabName = "Prefabs/Bullet01" ;
	
	public enum FireState
	{
		Ready ,
		Fire , 
		Reload ,
	}
	
	public FireState m_FireState = FireState.Ready ;
	public float m_FireTime = 0.0f ;
	public float m_ReloadTime = 1.0f ;
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
			if( true == Input.GetKey( KeyCode.Space ) )
			{
				m_FireState = FireState.Fire ;
			}			
			break ;
			
		case FireState.Fire :
			FireBullet() ;
			m_FireState = FireState.Reload ;
			m_FireTime = Time.timeSinceLevelLoad ;
			break ;
			
		case FireState.Reload :
			if( Time.timeSinceLevelLoad - m_FireTime > m_ReloadTime )
				m_FireState = FireState.Ready ;
			break ;			
		}				
	}
	
	private void FireBullet()
	{

		Object prefabObj = Resources.Load( m_BulletPrefabName ) ;
		if( null == prefabObj )
		{
			Debug.LogError( "FireBullet01:FireBullet() Resources.Load failed." ) ;
		}
		else 
		{
			GameObject missleObj = (GameObject) GameObject.Instantiate( prefabObj ) ;
			if( null != missleObj )
			{
				missleObj.name = "MissleObj" + iterator.ToString() ;
				missleObj.transform.position = this.gameObject.transform.position + this.gameObject.transform.forward ;
				missleObj.transform.rotation = this.gameObject.transform.rotation ;
				++iterator ;
				
				MissleMoveAndRecycle02 missle = missleObj.GetComponent<MissleMoveAndRecycle02>() ;
				if( null != missle )
				{
					missle.Setup( this.gameObject.transform.forward ) ;
				}
				Debug.Log( "FireBullet01:FireBullet() succeed." ) ;
			}
		}
	}
	
}
