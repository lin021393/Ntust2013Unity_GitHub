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
@file LoadLevelInSec.cs
@author NDark

一定時間後讀取關卡

@date 20130811 file started and deriived from LoadLevelInSec of Kobayashi Maru Commander Open Source Project
*/
using UnityEngine;

public class LoadLevelInSec : MonoBehaviour 
{
	public string m_LevelString = "" ;
	
	public float m_StartTime = 0.0f ;
	public float m_EndTime = 0.0f ;
	public float m_CountDownSec = 3.0f ;
	
	// Use this for initialization
	void Start () 
	{
		m_StartTime = Time.timeSinceLevelLoad ;
		m_EndTime = m_StartTime + m_CountDownSec ;
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if( 0 != m_LevelString.Length &&
			Time.timeSinceLevelLoad > m_EndTime )
		{
			// Remember to add scene to Build Setting
			Application.LoadLevel( m_LevelString ) ;
		}
	}
}
