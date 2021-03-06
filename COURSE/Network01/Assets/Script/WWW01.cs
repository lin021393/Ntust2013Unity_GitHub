/*
@file WWW01.cs
@author NDark

reference
http://answers.unity3d.com/questions/11167/how-can-i-use-www-and-yield-in-c.html

@date 20130822 file started.
*/
using UnityEngine;
using System.Collections ;

public class WWW01 : MonoBehaviour 
{
	public string m_URL = "http://kowloonia.gadgetapp.net/test.txt" ;
	private WWW m_WWWObj ;

	// Use this for initialization
	void Start () 
	{
		m_WWWObj = new WWW( m_URL ) ;
		StartCoroutine( wwwCallbackFunction() ) ;
		Debug.Log( "GetWWW done" ) ;
	}
	
	// Update is called once per frame
	void Update () 
	{
	}
	
	public IEnumerator wwwCallbackFunction()
	{
		yield return m_WWWObj ;
		Debug.Log( "wwwCallbackFunction()" ) ;
		if( null != this.guiText )
		{
			// Debug.Log( "text:" + m_WWWObj.text ) ;
			this.guiText.text = m_WWWObj.text ;
		}
	}
}

