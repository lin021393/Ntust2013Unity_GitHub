/*
 * @file RotationAmbiguous01.cs
 * @author NDark
 * @date 20130712 . file started.
 */
using UnityEngine;
using System.Collections;

public class RotationAmbiguous01 : MonoBehaviour 
{

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI()
	{
		if( true == GUILayout.Button( "ReStart" ) )
		{
			Debug.Log( "ReStart()" ) ;
			GameObject testBox01 = GameObject.Find( "TestBox01" ) ;
			if( null != testBox01 )
			{
				Debug.Log( "null != testBox01()" ) ;
				RotateByAngular01 rotateByAngular01 = testBox01.GetComponent<RotateByAngular01>() ;
				if( null != rotateByAngular01 )
				{
					rotateByAngular01.ReStart() ;
					Debug.Log( "rotateByAngular01.ReStart" ) ;
				}
			}
			
			GameObject testBox02 = GameObject.Find( "TestBox02" ) ;
			if( null != testBox02 )
			{
				RotateByAngular02 rotateByAngular02 = testBox02.GetComponent<RotateByAngular02>() ;
				if( null != rotateByAngular02 )
				{
					rotateByAngular02.ReStart() ;
				}
			}			
		}
	}
}
