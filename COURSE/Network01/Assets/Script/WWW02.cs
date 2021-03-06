/*
@file WWW02.cs
@author NDark
@date 20130822 file started.
*/
using UnityEngine;
using System.Collections ;

public class WWW02 : MonoBehaviour 
{
	public string m_URL = 
		"http://kobayashi.gadgetapp.net/xz1KqPi-1024.jpg" ;
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
		Debug.Log( m_WWWObj.texture.width + "," + 
				   m_WWWObj.texture.height ) ;
		if( null != this.guiTexture )
		{
			this.guiTexture.texture = m_WWWObj.texture ;
			this.guiTexture.pixelInset = 
				new Rect( -1 * m_WWWObj.texture.width / 2 ,
					   -1 * m_WWWObj.texture.height / 2 ,
					   m_WWWObj.texture.width ,
					   m_WWWObj.texture.height ) ;
		}
	}
}
