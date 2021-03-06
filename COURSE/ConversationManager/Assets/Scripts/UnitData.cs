/*
@file UnitData.cs
@author NDark
@date 20140329 file started.
@date 20140412 by NDark . add class method Import()
@date 20140413 by NDark
. add class method AssignSkill()
. add class method ImportSkills()
@date 20140420 by NDark 
. 將UnitDataParam整合進來

*/
#define TESTPLAYTYPE
// #define TESTSKILL
// #define DEBUG_SHOW_STANDARD_PARAMETER

using UnityEngine;
using System.Collections.Generic;

public class UnitData : MonoBehaviour 
{
	public UnitDataStruct m_UnitDataStruct = new UnitDataStruct() ;

	public void Import( UnitData _Src ) 
	{
		m_UnitDataStruct.Import( _Src.m_UnitDataStruct ) ;
	}

	void Awake () 
	{
		// m_UnitDataStruct = new UnitDataStruct() ;
		m_UnitDataStruct.m_ThisGameObject = this.gameObject ;
	}
	// Use this for initialization
	void Start () 
	{

		#if TESTPLAYTYPE
		this.m_UnitDataStruct.SetUnitType( UnitType.Player ) ;
		#endif
		
		#if TESTSKILL
		Skill addSkill = new Skill() ;
		addSkill.DisplayString = "SaveSP" ;
		// Debug.Log( "SaveSP" ) ;
		this.m_UnitDataStruct.m_Skills.Add( "SaveSP" , addSkill ) ; 
		
		addSkill = new Skill() ;
		addSkill.DisplayString = "Hadoken" ;
		// Debug.Log( "SaveSP" ) ;
		this.m_UnitDataStruct.m_Skills.Add( "Hadoken" , addSkill ) ; 
		#endif	
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		#if DEBUG_SHOW_STANDARD_PARAMETER
		Debug_standardParameters = new StandardParameter[ this.standardParameters.Count ] ;
		this.m_UnitDataStruct.standardParameters.Values.CopyTo( Debug_standardParameters , 0 ) ;
		#endif // #if DEBUG_SHOW_STANDARD_PARAMETER	
	}
}
