using UnityEngine;
using System.Collections;

public class GUITextMoveInside1x1 : MonoBehaviour 
{
	public Vector3 m_MoveDirectionNow = new Vector3( 1 , 0 , 0 ) ;
	public float m_MoveSpeed = 0.1f ;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		KeepMoving() ;
		DetectBoundary() ;
	}
	
	private void KeepMoving()
	{
		Transform thisTransform = this.gameObject.transform ;
		thisTransform.Translate( 
			m_MoveDirectionNow * m_MoveSpeed * Time.deltaTime , Space.World ); 
		
		string displayText = 
			string.Format( "{0:0.00} , {1:0.00}" , 
				thisTransform.position.x , thisTransform.position.y ) ;
		
		GUIText guiText = this.guiText ;
		if( null != guiText )
		{
			this.guiText.text = displayText ;
		}
			
	}
	
	private void DetectBoundary()
	{
		Vector3 thisPosition = this.gameObject.transform.position ;
					
		if( thisPosition.x >= 1.0 || thisPosition.x <= 0.0 )
		{
			m_MoveDirectionNow.x = m_MoveDirectionNow.x * -1 ;
			m_MoveDirectionNow.y = Random.Range( -1.0f , 1.0f ) ;
		}
		if( thisPosition.y >= 1.0 || thisPosition.y <= 0.0 )
		{
			m_MoveDirectionNow.y = m_MoveDirectionNow.y * -1 ;
			m_MoveDirectionNow.x = Random.Range( -1.0f , 1.0f ) ;
		}		
	}
}
