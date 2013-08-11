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
@file GUI_LoadTextFile.cs
@author NDark

讀取資源路徑檔並且透過 GUI_TextParagraph 顯示

# 檔案路徑 m_Filepath = "ResourceLocations.txt" ;
# 讀入之後使用\n來分段，並且設定給 GUI_TextParagraph

@date 20130811 . file started and derieved from GUI_LoadResourceLocations of Kobayashi Maru Commander

*/
using UnityEngine;

public class GUI_LoadTextFile : MonoBehaviour 
{
	
	public string m_Filepath = "" ;
	
	// Use this for initialization
	void Start () 
	{
		GUI_TextParagraph textParagraph = this.gameObject.GetComponent<GUI_TextParagraph>() ;
		if( null == textParagraph )
			return ;
		
		string content = LoadResourceLocation() ;	
		if( 0 != content.Length )
		{
			string [] strVec = content.Split( '\n' ) ;
			textParagraph.m_StrArray = strVec ;
			textParagraph.CreateParagraph() ;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
	}
	
	private string LoadResourceLocation()
	{
		return LoadToString( m_Filepath ) ;
	}
	
	public static string LoadToString( string _Filepath )
	{
		string ret = "" ;
		if( 0 == _Filepath.Length )
			return ret ;
		
		TextAsset textAsset = (TextAsset) Resources.Load( _Filepath ) ;
		if( null != textAsset )
		{
			ret = textAsset.text ;
		}
		return ret;
	}			
}
