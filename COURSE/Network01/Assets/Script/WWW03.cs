/*
@file WWW03.cs
@author NDark
@date 20130822 file started.
*/
using UnityEngine;
using System.Collections ;

public class WWW03 : MonoBehaviour 
{
	public string m_URL = "http://kobayashi.gadgetapp.net/xz1KqPi-1024.jpg" ;
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
		if( null != m_WWWObj )
		{
			m_ProgressNow = m_WWWObj.progress ;
		}
	
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
	
	public Rect m_ProgressBarRect = new Rect( 200 , 20 , 600 , 50 ) ;
	public float m_ProgressNow = 0 ;
	void OnGUI()
	{
		Debug.Log( m_ProgressNow ) ;
		GUI.HorizontalSlider( m_ProgressBarRect ,
							  m_ProgressNow * 100 , 
							  0 , 100 ) ;
	}
}
