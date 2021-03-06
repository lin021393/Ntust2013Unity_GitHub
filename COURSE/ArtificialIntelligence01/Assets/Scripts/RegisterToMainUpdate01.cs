/**
 * @file RegisterToMainUpdate01.cs
 * @author NDark
 * @date 20130630 . file started.
 */
using UnityEngine;
using System.Collections;

public class RegisterToMainUpdate01 : MonoBehaviour 
{
	static public GameObject sSingletonObject = null ;

	// Use this for initialization
	void Start () 
	{
		if( null == sSingletonObject )
		{
			sSingletonObject = GameObject.Find( "GlobalSingleton" ) ;
		}

		if( null != sSingletonObject )
		{		
			MainUpdate01 mainUpdate = 
				sSingletonObject.GetComponent<MainUpdate01>() ;
			if( null != mainUpdate )
			{
				mainUpdate.RegisterGameUnit( this.gameObject ) ;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
