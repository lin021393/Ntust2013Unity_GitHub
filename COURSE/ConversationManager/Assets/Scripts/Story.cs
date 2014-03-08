/**
 * @file Story.cs
 * @author NDark
 * @date20140308 file started.
 */
using UnityEngine;
using System.Collections.Generic ;

public class Story
{
	public int UID
	{
		get { return m_UID ; }
		set { m_UID = value ; }
	}
	private int m_UID = 0 ;
	
	public int StartTakeUID
	{
		get { return m_StartTakeUID ; }
		set { m_StartTakeUID = value ; }
	}
	private int m_StartTakeUID = 0 ;
	
	public int EndTakeUID
	{
		get { return m_EndTakeUID ; }
		set { m_EndTakeUID = value ; }
	}
	private int m_EndTakeUID = 0 ;
}
