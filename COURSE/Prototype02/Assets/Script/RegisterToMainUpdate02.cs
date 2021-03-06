/**
 * @file RegisterToMainUpdate02.cs
 * @author NDark
 * @date 20130714 . file started.
 */
using UnityEngine;
using System.Collections;

public class RegisterToMainUpdate02 : MonoBehaviour 
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
			MainUpdate02 mainUpdate = sSingletonObject.GetComponent<MainUpdate02>() ;
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
