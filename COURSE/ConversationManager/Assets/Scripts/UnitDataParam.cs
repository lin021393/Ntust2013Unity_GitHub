/*
@file UnitDataParam.cs
@author NDark
@date 20140329 file started.
*/
#define TESTSKILL
#define DEBUG_SHOW_STANDARD_PARAMETER
using UnityEngine;
using System.Collections.Generic;

public class UnitDataParam : UnitData 
{
	public Dictionary< string , StandardParameter > standardParameters = new Dictionary< string , StandardParameter >() ;
	public StandardParameter [] Debug_standardParameters = null ;

	public void ImportStandardParameter( Dictionary< string , StandardParameter > _ParameterMap )
	{
		Dictionary< string , StandardParameter >.Enumerator i = _ParameterMap.GetEnumerator() ;
		while( i.MoveNext() )
		{
			AssignStandardParameter( i.Current.Key , i.Current.Value ) ;
		}

	}

	public void AssignStandardParameter( string _Key , 
	                                     StandardParameter _Parameter )
	{
		standardParameters[ _Key ] = new StandardParameter( _Parameter ) ;
	}

	// Use this for initialization
	void Start () 
	{
		#if TESTSKILL
		this.SetUnitType( UnitType.Player ) ;
		Skill addSkill = new Skill() ;
		addSkill.DisplayString = "SaveSP" ;
		// Debug.Log( "SaveSP" ) ;
		m_Skills.Add( "SaveSP" , addSkill ) ; 

		addSkill = new Skill() ;
		addSkill.DisplayString = "Hadoken" ;
		// Debug.Log( "SaveSP" ) ;
		m_Skills.Add( "Hadoken" , addSkill ) ; 
		#endif	
	}
	
	// Update is called once per frame
	void Update () 
	{
#if DEBUG_SHOW_STANDARD_PARAMETER
		Debug_standardParameters = new StandardParameter[ this.standardParameters.Count ] ;
		this.standardParameters.Values.CopyTo( Debug_standardParameters , 0 ) ;
#endif // #if DEBUG_SHOW_STANDARD_PARAMETER

	
	}
}
