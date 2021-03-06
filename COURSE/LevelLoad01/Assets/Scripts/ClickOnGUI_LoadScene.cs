/*
IMPORTANT: READ BEFORE DOWNLOADING, COPYING, INSTALLING OR USING. 

By downloading, copying, installing or using the software you agree to this license.
If you do not agree to this license, do not download, install, copy or use the software.

    License Agreement For Kobayashi Maru Commander Open Source

Copyright (C) 2013, Chih-Jen Teng(NDark) and Koguyue Entertainment, 
all rights reserved. Third party copyrights are property of their respective owners. 

Redistribution and use in source and binary forms, with or without modification,
are permitted provided that the following conditions are met:

  * Redistribution's of source code must retain the above copyright notice,
    this list of conditions and the following disclaimer.

  * Redistribution's in binary form must reproduce the above copyright notice,
    this list of conditions and the following disclaimer in the documentation
    and/or other materials provided with the distribution.

  * The name of Koguyue or all authors may not be used to endorse or promote products
    derived from this software without specific prior written permission.

This software is provided by the copyright holders and contributors "as is" and
any express or implied warranties, including, but not limited to, the implied
warranties of merchantability and fitness for a particular purpose are disclaimed.
In no event shall the Koguyue or all authors or contributors be liable for any direct,
indirect, incidental, special, exemplary, or consequential damages
(including, but not limited to, procurement of substitute goods or services;
loss of use, data, or profits; or business interruption) however caused
and on any theory of liability, whether in contract, strict liability,
or tort (including negligence or otherwise) arising in any way out of
the use of this software, even if advised of the possibility of such damage.  
*/
/*
@file ClickOnGUI_LoadScene.cs
@author NDark

點選就讀場景
# m_LevelString 指定場景名稱

@date 20121226 . file started.
*/
using UnityEngine;
using System.Collections ;

public class ClickOnGUI_LoadScene : MonoBehaviour 
{
	public string m_LevelString = "" ; // 
	public bool m_ResizeAnim = true ;
	public bool m_LoadLater = true ;
	public float m_LoadWaitSec = 3.0f ;
	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
	}
	
	void OnMouseDown()
	{
		// Debug.Log( "OnMouseDown" ) ;
		if( 0 != m_LevelString.Length )
		{
			if( true == m_ResizeAnim )
			{
				ResizeScale01 script = this.gameObject.GetComponent<ResizeScale01>() ;
				if( null == script )
				{
					this.gameObject.AddComponent< ResizeScale01 >() ;
				}
			}
			
			if( true == m_LoadLater )
			{
				// load later
				StartCoroutine( WaitAndLoadLevel( m_LoadWaitSec ) ) ;
			}
			else
			{
				// you can load right now 
				Application.LoadLevel( m_LevelString ) ;	
			}
		}
	}
	
	private IEnumerator WaitAndLoadLevel( float _Sec )
	{
        yield return new WaitForSeconds( _Sec ) ;
		Application.LoadLevel( m_LevelString ) ;
	}
}
