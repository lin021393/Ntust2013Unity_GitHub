/**
@file OnCollideAddBrokenGlass01.cs
@author NDark
@date 20130815 . file started.
*/
using UnityEngine;

public class OnCollideAddBrokenGlass01 : MonoBehaviour 
{
	public string m_PrefabName = "BrokenGlass" ;
	public int m_Iter = 0 ;
	public float m_EndTime = 0.0f ;
	public float m_ElapsedSec = 1.0f ;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter( Collider other )
	{
		if( Time.timeSinceLevelLoad > m_EndTime )
		{
			Debug.Log( "OnTriggerEnter" + other.collider.name ) ;
			CreateBrokenGlass() ;
			m_EndTime = Time.timeSinceLevelLoad + m_ElapsedSec ;
		}
	}	
	
	private void CreateBrokenGlass()
	{
		Object prefab = Resources.Load( m_PrefabName ) ;
		if( null != prefab )
		{
			GameObject obj = (GameObject) GameObject.Instantiate( prefab ) ;
			if( null != obj )
			{
				obj.name = m_PrefabName + m_Iter.ToString() ;
				obj.transform.position = new Vector3( 
					Random.Range( 0.0f , 1.0f ) ,
					Random.Range( 0.0f , 1.0f ) , 
					0 );
			}
		}
	}
		
		
}
