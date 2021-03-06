/**
@file FireBullet02.cs
@author NDark
@date 20130806 . file started.
*/
using UnityEngine;

public class FireBullet02 : MonoBehaviour 
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
			Debug.LogError( "FireBullet02:FireBullet() Resources.Load failed." ) ;
		}
		else 
		{
			Vector3 initPos = this.gameObject.transform.position + 
							  this.gameObject.transform.forward ;
			Quaternion initRotation = this.gameObject.transform.rotation ;
			GameObject missleObj = 
				(GameObject) GameObject.Instantiate( prefabObj , 
													 initPos , 
													 initRotation ) ;
			
			if( null != missleObj )
			{
				missleObj.name = "MissleObj" + iterator.ToString() ;
				++iterator ;
				missleObj.rigidbody.AddForce( 
					this.gameObject.transform.forward * 100 ) ;
				
				Debug.Log( "FireBullet02:FireBullet() succeed." ) ;
			}
		}
	}
	
}
