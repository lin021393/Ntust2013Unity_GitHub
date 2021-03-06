/*
@file PlayAnimationByMainCharacterMove.cs
@author NDark
@date 20131005 by NDark
*/
using UnityEngine;

public class PlayAnimationByMainCharacterMove : MonoBehaviour 
{

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if( true == Input.GetKey( KeyCode.S ) 
			)
		{
			StartWheelAnim( false ) ;
		}
		else if( 
			true == Input.GetKey( KeyCode.W ) )
		{
			StartWheelAnim( true ) ;
		}			
		else
		{
			StopWheelAnim() ;
		}
	
	}

	private void StartWheelAnim( bool positive )
	{
		PlayAnimationFromStep01 [] scripts = this.gameObject.GetComponentsInChildren<PlayAnimationFromStep01>() ;
		foreach( PlayAnimationFromStep01 script in scripts )
		{
			script.PlayAnim( positive ) ;
		}
	}
	
	private void StopWheelAnim()
	{
		PlayAnimationFromStep01 [] scripts = this.gameObject.GetComponentsInChildren<PlayAnimationFromStep01>() ;
		foreach( PlayAnimationFromStep01 script in scripts )
		{
			script.StopAnim() ;
		}
	}
}
