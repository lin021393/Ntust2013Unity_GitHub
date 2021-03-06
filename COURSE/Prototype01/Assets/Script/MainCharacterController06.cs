/**
@file MainCharacterController06.cs
@author NDark
@date 20130622 . file started.
@date 20130721 
. remove class member m_MouseMoveThreashold.
. remove class member m_MousePositionLast
. remove class member m_RotationUpVec
. fix an error of use wrong class member at InitializeMainCharacterObjectPtr()

 */
using UnityEngine;

public class MainCharacterController06 : MonoBehaviour 
{
	public float m_MoveSpeed = 5.0f ; // 移動速度
	
	public GameObject m_MainCharacter = null ;
	public Camera m_CameraPtr = null ;
	public int iterator = 0 ;
	

	// Use this for initialization
	void Start () 
	{
		if( null == m_MainCharacter )
			InitializeMainCharacterObjectPtr() ;
		
		// 沒設定才要初始化
		if( null == m_CameraPtr )
			InitializeCameraPtr() ;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if( null == m_MainCharacter || null == m_CameraPtr )
			return ;
		
		Vector3 rightVec = m_CameraPtr.transform.right ; // right of camera
		rightVec.y = 0 ;
		
		Transform mainCharacterTransform = m_MainCharacter.transform ;
		if( true == Input.GetKey( KeyCode.A )  )
		{
			// left
			mainCharacterTransform.Translate( -1 * rightVec * Time.deltaTime * m_MoveSpeed , 
											  Space.World ) ;
		}
		else if( true == Input.GetKey( KeyCode.D ) )
		{
			// right
			mainCharacterTransform.Translate( 1 * rightVec * Time.deltaTime * m_MoveSpeed , 
											  Space.World ) ;
		}
		
		// fire
		if( true == Input.GetKey( KeyCode.Space ) )
		{
			FireBullet() ;
		}
		
	}
	
	private void InitializeMainCharacterObjectPtr()
	{
		m_MainCharacter = GameObject.FindGameObjectWithTag( "Player" ) ;
		
		if( null == m_MainCharacter )
		{
			Debug.LogError( "MainCharacterController06:InitializeMainCharacterObjectPtr() null == m_MainCharacter" ) ;
		}
		else
		{
			Debug.Log( "MainCharacterController06:InitializeMainCharacterObjectPtr() end." ) ;
		}
	}	
	
	private void InitializeCameraPtr()
	{
		m_CameraPtr = Camera.mainCamera ;
		
		if( null == m_CameraPtr )
		{
			Debug.LogError( "MainCharacterController06:InitializeCameraPtr() null == m_CameraPtr" ) ;
		}
		else
		{
			Debug.Log( "MainCharacterController06:InitializeCameraPtr() end." ) ;
		}
	}	
	
	private void FireBullet()
	{
		if( null == m_MainCharacter )
			return ;
		
		Transform mainCharacterTransform = m_MainCharacter.transform ;
		
		Object prefabObj = Resources.Load( "Common/Prefabs/Missile01" ) ;
		if( null == prefabObj )
		{
			Debug.LogError( "MainCharacterController06:FireBullet() Resources.Load Missile01 failed" ) ;
		}		
		else
		{
			GameObject missleObj = (GameObject) GameObject.Instantiate( prefabObj ) ;
			if( null != missleObj )
			{
				missleObj.name = "MissleObj" + iterator.ToString() ;
				++iterator ;
				
				missleObj.transform.position = mainCharacterTransform.position + 
											   mainCharacterTransform.forward ;
				
				missleObj.transform.rotation = mainCharacterTransform.rotation ;
				
				
				
				// set up move direction 
				MissleMoveAndRecycle01 missle = missleObj.GetComponent<MissleMoveAndRecycle01>() ;
				if( null != missle )
				{
					missle.Setup( true , mainCharacterTransform.forward ) ;
				}
				
				Debug.Log( "LevelGeneration03:FireBullet() succeed, " + missleObj.name + " is fired.") ;
			}
		}		
	}
}
