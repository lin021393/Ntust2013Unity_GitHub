/*
@file StandardParameter.cs
@author NDark
@date 20140329 file started and copy from Kobayashi Maru Commander Open Source.
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic ;

/**
 * @brief 標準資料
 * 
 * 內含
 * -# now 
 * -# max maximum
 * -# min minimum , default is 0.
 * -# std standard , default is maximum.
 * -# init initialization , default is maximum.
 */
[System.Serializable]
public class StandardParameter
{
	public StandardParameter()
	{}
	public StandardParameter( StandardParameter _src )
	{
		Setup( _src.m_Now , _src.m_Max , _src.m_Min , _src.m_Std , _src.m_Init ) ;
	}	
	
	public StandardParameter( float _Now , float _Maximum )
	{
		Setup( _Now , _Maximum ) ;
	}	
	public StandardParameter( float _Now , float _Maximum , float _Initialization )
	{
		Setup( _Now , _Maximum , _Initialization ) ;
	}
	public StandardParameter( float _Now , float _Maximum , float _Minimum , float _Initialization )
	{
		Setup( _Now , _Maximum , _Minimum , _Initialization ) ;
	}	
	public StandardParameter( float _Now , float _Maximum , float _Minimum , float _Std , float _Initialization )
	{
		Setup( _Now , _Maximum , _Minimum , _Std , _Initialization ) ;
	}
	
	public void Reset()
	{
		m_Now = m_Init ;
	}
	public void Clear()
	{
		m_Now = m_Min ;
	}
	public void ToMax()
	{
		m_Now = m_Max ;
	}
	public void Setup( float _Now , 
					   float _Maximum ) 
	{
		Setup( _Now , _Maximum , 0.0f , _Maximum , _Maximum ) ;
	}			
	
	public void Setup( float _Now , 
					   float _Maximum , 
					   float _Initialization ) 
	{
		Setup( _Now , _Maximum , 0.0f , _Maximum , _Initialization ) ;
	}		
	public void Setup( float _Now , 
					   float _Maximum , 
					   float _Minimum , 
					   float _Initialization ) 
	{
		Setup( _Now , _Maximum , _Minimum , _Maximum , _Initialization ) ;
	}	
	
	public void Setup( float _Now , 
					   float _Maximum , 
					   float _Minimum , 
					   float _Std , 
					   float _Initialization ) 
	{
		m_Now = _Now ;
		m_Max = _Maximum ;
		m_Min = _Minimum ;
		m_Std = _Std ;
		m_Init = _Initialization ;
	}
	
	/// <summary>
	/// Checks the range of now from min to max
	/// </summary> 
	public void CheckRange()
	{
		if( m_Now > m_Max )
			ToMax() ;
		else if( m_Now < m_Min )
			Clear() ;				
	}
	
	/// <summary>
	/// Ratio of m_Now / m_Std
	/// </summary>
	public float Ratio()
	{
		if( 0.0f == m_Std )
			return 0.0f ;
		return m_Now / m_Std ;
	}
	
	// property
	public float now
	{
		get
		{
			return m_Now ;
		}
		set
		{
			m_Now = value ;
			CheckRange() ;
		}
	}

	public float max
	{
		get
		{
			return m_Max ;
		}
		set
		{
			m_Max = value ;
			CheckRange() ;
		}
	}
	
	public float min
	{
		get
		{
			return m_Min ;
		}
		set
		{
			m_Min = value ;
			CheckRange() ;
		}
	}	
	
	public float m_Now = 0.0f ;
	public float m_Max = 0.0f ;
	public float m_Min = 0.0f ;
	public float m_Std = 0.0f ;
	public float m_Init = 0.0f ;
}

