/**
 * @file InfoProperty.cs
 * @author NDark
 * @date20140322 . file started.
 */
using UnityEngine;

public class InfoProperty 
{
	public InfoProperty( string _Label , string _Value )
	{
		Label = _Label ;
		Value = _Value ;
	}

	public string Label
	{
		get { return m_Label ; }
		set { m_Label = value ; }
	}
	private string m_Label = "" ;

	public string Value
	{
		get { return m_Value ; }
		set { m_Value = value ; }
	}
	private string m_Value = "" ;
}
