/*
@file UnitData.cs
@author NDark
@date 20140329 file started.
@date 20140412 by NDark . add class method Import()
@date 20140413 by NDark
. add class method AssignSkill()
. add class method ImportSkills()

*/
// #define TESTSKILL
using UnityEngine;
using System.Collections.Generic;

public enum UnitState
{
	UnActive = 0 ,
	Initialized ,
	Borning ,
	Active ,
	Idle ,
	Dying ,
	Dead ,
} ;

public enum UnitType
{
	UnDefine = 0 ,
	StaticObject ,
	Unit ,
	Player ,
} ;

public class UnitData : MonoBehaviour 
{
	public Vector3 GetCurrentPos() 
	{
		return this.gameObject.transform.position ;
	}

	public bool IsVisible()
	{
		return this.gameObject.renderer.enabled ;
	}

	public UnitState GetUnitState()
	{
		return m_UnitState ;
	}
	private UnitState m_UnitState = UnitState.UnActive ;

	public void SetUnitType( UnitType _Set )
	{
		m_UnitType = _Set ;
	}
	public UnitType GetUnitType()
	{
		return m_UnitType ;
	}
	private UnitType m_UnitType = UnitType.UnDefine ;

	public string ProfileTextureName
	{
		get { return m_ProfileTextureName ; }
		set 
		{ 
			string set = value ;
			if( set != m_ProfileTextureName )
			{
				//reload?
				m_ProfileTextureName = set ;
			}
		}
	}
	private string m_ProfileTextureName = "" ;

	public Texture ProfileTexture
	{
		get { return m_ProfileTexture ; }
		set 
		{
			if( null != m_ProfileTexture )
			{

			}
			m_ProfileTexture = value ;
		}
	}
	private Texture m_ProfileTexture = null ;


	public void AssignSkill( string _SkillName, Skill _Skill )
	{
		if( true == m_Skills.ContainsKey( _SkillName )  )
		{
			m_Skills[ _SkillName ] = _Skill ;
		}
		else
		{
			m_Skills.Add( _SkillName , _Skill ) ;
		}
	}

	public void ImportSkills( Dictionary< string , Skill > _Skills )
	{
		Dictionary< string , Skill >.Enumerator i = _Skills.GetEnumerator() ;
		while( i.MoveNext() )
		{
			AssignSkill( i.Current.Key , i.Current.Value ) ;
		}
	}
	public Dictionary< string , Skill > m_Skills = new Dictionary<string, Skill>() ;


	public void Import( UnitData _Src ) 
	{
		Dictionary< string , Skill >.Enumerator skill = _Src.m_Skills.GetEnumerator() ;
		while( skill.MoveNext() )
		{
			/* @todo skill new ? */
			m_Skills.Add( skill.Current.Key , skill.Current.Value ) ;
		}
		m_ProfileTexture = _Src.m_ProfileTexture ;
		m_ProfileTextureName = _Src.m_ProfileTextureName ;
		m_UnitType = _Src.m_UnitType ;
		m_UnitState = _Src.m_UnitState ;

	}

	// Use this for initialization
	void Start () 
	{
#if TESTSKILL
		Skill addSkill = new Skill() ;
		addSkill.DisplayString = "SaveSP" ;
		Debug.Log( "SaveSP" ) ;
		m_Skills.Add( "SaveSP" , addSkill ) ; 
#endif
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
