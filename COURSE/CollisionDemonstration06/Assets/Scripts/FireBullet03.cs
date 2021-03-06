/**
@file FireBullet03.cs
@author NDark
@date 20130806 . file started.
*/
using UnityEngine;

public class FireBullet03 : MonoBehaviour 
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
	
	
	public float m_Force = 100 ;
	public float m_Mass = 0.1f ;
	public Rect m_BulletMassRect = new Rect( 100 , 200 , 100 , 300 ) ;
	public Rect m_ForceScrollViewRect = new Rect( 850 , 200 , 100 , 200 ) ;
	void OnGUI()
	{
		
		GUILayout.BeginArea( m_BulletMassRect ) ;
		if( true == GUILayout.Button( "Mass:0.1" ) )
		{
			m_Mass = 0.1f ;
		}
		if( true == GUILayout.Button( "Mass:1" ) )
		{
			m_Mass = 1 ;
		}
		if( true == GUILayout.Button( "Mass:10" ) )
		{
			m_Mass = 10 ;
		}		
		GUILayout.EndArea() ;
	
		
		GUILayout.BeginArea( m_ForceScrollViewRect ) ;
		GUILayout.Label( m_Force.ToString() ) ;	
		m_Force = GUILayout.VerticalSlider( m_Force , 1000 , 1 ) ;			
		GUILayout.EndArea() ;		
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
			Debug.LogError( "FireBullet03:FireBullet() Resources.Load failed." ) ;
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
					this.gameObject.transform.forward * m_Force ) ;
				
				missleObj.rigidbody.mass = m_Mass ;
				
				Debug.Log( "FireBullet03:FireBullet() succeed." ) ;
			}
		}
	}
	
}
