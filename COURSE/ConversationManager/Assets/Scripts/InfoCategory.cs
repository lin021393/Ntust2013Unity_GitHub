/**
 * @file InfoCategory.cs
 * @author NDark
 * @date20140322 . file started.
 */
using UnityEngine;
using System.Collections.Generic ;

public class InfoCategory 
{
	public InfoCategory( string _CategoryLabel )
	{
		Label = _CategoryLabel ;
	}

	public string Label
	{
		get { return m_CategoryLabel ; }
		set { m_CategoryLabel = value ; }
	}
	private string m_CategoryLabel = "" ;

	public bool HasProperty( string _Label ) 
	{
		return m_Properties.ContainsKey( _Label ) ;
	}

	public InfoProperty GetProperty( string _Label ) 
	{
		InfoProperty ret = null ;
		if( true == m_Properties.ContainsKey( _Label ) )
		{
			ret = m_Properties[ _Label ] ;
		}
		return ret ;
	}

	public string Read( string _Label ) 
	{
		string ret = "" ;
		if( true == m_Properties.ContainsKey( _Label ) )
		{
			ret = m_Properties[ _Label ].Value ;
		}
		return ret ;
	}

	public void Write( string _Label , string _Value ) 
	{
		if( false == m_Properties.ContainsKey( _Label ) )
		{
			m_Properties.Add( _Label , new InfoProperty( _Label , _Value ) ) ;
		}
		else
		{
			m_Properties[ _Label ].Value = _Value ;
		}
	}

	public void Remove( string _Label )
	{
		if( true == m_Properties.ContainsKey( _Label ) )
		{
			m_Properties.Remove( _Label ) ;
		}
	}

	public void Clear()
	{
		m_Properties.Clear();
	}

	private Dictionary< string , InfoProperty > m_Properties = new Dictionary<string, InfoProperty>() ;
}
