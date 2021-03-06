/**
 * @file InfoDataCenter.cs
 * @author NDark
 * @date20140322 . file started.
 */
// #define DEBUG
using UnityEngine;
using System.Collections.Generic ;

public class InfoDataCenter 
{

	public bool HasProperty( string _CategoryLabel , string _Label ) 
	{
		bool ret = false ;
		if( true == m_Categories.ContainsKey( _CategoryLabel ) )
		{
			ret = m_Categories[ _CategoryLabel ].HasProperty( _Label ) ;
		}
		return ret ;
	}

	public InfoProperty PopProperty( string _CategoryLabel , string _Label ) 
	{
		InfoProperty ret = null ;
		if( true == m_Categories.ContainsKey( _CategoryLabel ) )
		{
			ret = m_Categories[ _CategoryLabel ].GetProperty( _Label ) ;
			if( null != ret )
			{
				m_Categories[ _CategoryLabel ].Remove( _Label ) ;
			}
		}
		return ret ;
	}

	public InfoProperty GetProperty( string _CategoryLabel , string _Label ) 
	{
		InfoProperty ret = null ;
		if( true == m_Categories.ContainsKey( _CategoryLabel ) )
		{
			ret = m_Categories[ _CategoryLabel ].GetProperty( _Label ) ;
		}
		return ret ;
	}
	
	public string ReadProperty( string _CategoryLabel , string _Label ) 
	{
		string ret = "" ;
		if( true == m_Categories.ContainsKey( _CategoryLabel ) )
		{
			ret = m_Categories[ _CategoryLabel ].Read( _Label ) ;
		}
		return ret ;
	}
	
	public void WriteProperty( string _CategoryLabel , string _Label , string _Value ) 
	{
		#if DEBUG
		Debug.Log( "WriteProperty=" + _CategoryLabel + "," + _Label + "," + _Value ) ;
		#endif
		if( false == m_Categories.ContainsKey( _CategoryLabel ) )
		{
			m_Categories.Add( _CategoryLabel , new InfoCategory( _CategoryLabel ) ) ;
		}
		m_Categories[ _CategoryLabel ].Write( _Label , _Value ) ;
	}
	
	public void RemoveCategory( string _CategoryLabel )
	{
		if( true == m_Categories.ContainsKey( _CategoryLabel ) )
		{
			m_Categories.Remove( _CategoryLabel ) ;
		}
	}

	public void InsertCategory( string _CategoryLabel ) 
	{
		if( false == m_Categories.ContainsKey( _CategoryLabel ) )
		{
			m_Categories.Add( _CategoryLabel , new InfoCategory( _CategoryLabel ) ) ;
		}
	}
	
	public void Clear()
	{
		m_Categories.Clear();
	}
	
	private Dictionary< string , InfoCategory > m_Categories = new Dictionary<string, InfoCategory>() ;
}
