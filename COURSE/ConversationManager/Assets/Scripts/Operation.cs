/**
 * @file Operation.cs
 * @author NDark
 * @date 20140405 file started.
 */
using UnityEngine;
using System.Collections;

public class Operation 
{
	public string CommandString
	{
		get { return m_CommandString ; }
		set { m_CommandString = value ; }
	}
	private string m_CommandString = "" ;

	public string DisplayString
	{
		get { return m_DisplayString ; }
		set { m_DisplayString = value ; }
	}
	private string m_DisplayString = "" ;
}
