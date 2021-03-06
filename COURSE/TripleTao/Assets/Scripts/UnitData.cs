/*
@file UnitData.cs
@author NDark
@date 20130827 file started.
*/
using UnityEngine;
using System.Collections.Generic ;

public enum UnitType
{
	grass = 0 ,
	bush ,
	tree ,
	smallhouse,
	bighouse ,
	castle ,
	flyingcastle ,
}

public class UnitData : MonoBehaviour 
{
	static public string[] s_MaterialNames = null ;
	static public Dictionary<string,Material> s_AllMaterial = null ;
	
	public int m_IndexI = 0 ;
	public int m_IndexJ = 0 ;
	public UnitType m_UnitType = UnitType.grass ;
	
	public void Upgrade( UnitType _UpgradeTo ) 
	{
#if DEBUG				
		Debug.Log( "UnitData::Upgrade()" ) ;
#endif		
		if( UnitType.flyingcastle <= _UpgradeTo )
			return ;
			
		m_UnitType = _UpgradeTo ;
		SetMaterial( this.gameObject.renderer ) ;
	}
	
	public void SetMaterial( Renderer _Render )
	{
		if( false == s_AllMaterial.ContainsKey( m_UnitType.ToString() ) )
		{
			Debug.LogError( "UnitData::SetMaterial() no material" ) ;
		}
		_Render.material = s_AllMaterial[ m_UnitType.ToString() ] ;
	}	
	
	// Use this for initialization
	void Start () 
	{
		if( null == s_AllMaterial )
		{
			InitializeMaterials() ;
			
		}
		
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	

	
	private void InitializeMaterials()
	{
		s_AllMaterial = new Dictionary<string, Material>() ;
		TextAsset textAsset = (TextAsset) Resources.Load( "MaterialDoc" ) ;
		if( null != textAsset )
		{
			Debug.Log( "null != textAsset[ i ]" ) ;
			char [] splitor = {','} ;
			s_MaterialNames = textAsset.text.Split( splitor ) ;
			
			for( int i = 0 ; i < s_MaterialNames.Length ; ++i )
			{
				Material material = 
					(Material) Resources.Load( s_MaterialNames[ i ] ) ;
				if( null != material )
				{
					Debug.Log( "s_MaterialNames[ i ]=" + s_MaterialNames[ i ] ) ;
					s_AllMaterial.Add( s_MaterialNames[ i ] , material ) ;
				}
			}
		}
	
		Debug.Log( "s_AllMaterial.Count" + s_AllMaterial.Count ) ;
	}
}
