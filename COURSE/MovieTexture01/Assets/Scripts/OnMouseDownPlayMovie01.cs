/*
@file OnMouseDownPlayMovie01.cs
@author NDark
@date 20130830 file started.
*/
using UnityEngine;


public class OnMouseDownPlayMovie01 : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnMouseDown()
	{
		if( null == this.audio )
		{
			Debug.LogError( "No audio source" ) ;
			return ;
		}
		
		MovieTexture mt = (MovieTexture) renderer.material.mainTexture ;
		if( false == mt.isPlaying )
		{
			Debug.Log( "StartPlay" ) ;
			mt.Play() ;
			this.audio.Play() ;
		}
		else
		{
			Debug.Log( "Pause" ) ;
			mt.Pause() ;
			this.audio.Pause() ;
		}
	}
}
