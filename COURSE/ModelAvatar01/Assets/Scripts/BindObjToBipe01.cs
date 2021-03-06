/*
@file BindObjToBipe01.cs
@author NDark
@date 20130927 file started.
*/
using UnityEngine;

public class BindObjToBipe01 : MonoBehaviour 
{
	public GameObject m_ModelObj = null ;
	public string m_BipeTargetName = "" ;
	public GameObject m_BipeObj = null ;
	
	public string m_EquipmentPrefabFullName = "" ;
	public GameObject m_EquipmentObj = null ;
	public string m_EquipmentGameObjectName = "" ;
	
		
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if( null != m_ModelObj )
		{
			if( 0 != m_BipeTargetName.Length )
			{
				if( null == m_BipeObj ||
					m_BipeTargetName != m_BipeObj.name )
				{
					
					Transform[] transVec = m_ModelObj.GetComponentsInChildren<Transform>() ;
					foreach( Transform trans in transVec )
					{
						if( trans.gameObject.name == m_BipeTargetName )
						{
							m_BipeObj = trans.gameObject ;
							// Debug.Log( m_BipeObj ) ;
							return ;
						}
					}
				}
			}
		}
	
		CheckEquipment() ;
	}
	
	private void CheckEquipment() 
	{
		if( 0 != m_EquipmentPrefabFullName.Length )
		{
			if( null == m_EquipmentObj ||
				-1 == m_EquipmentObj.name.IndexOf( m_EquipmentPrefabFullName ) )
			{
				GameObject.Destroy( m_EquipmentObj ) ;
				BindObjOntoBipeObj( m_EquipmentPrefabFullName , m_BipeObj ) ;
			}
		}
	}
	
	private void BindObjOntoBipeObj( string _EquipmentPrefabName , GameObject _BipeParent ) 
	{
		
		Object prefab = Resources.Load( _EquipmentPrefabName ) ;
		if( null == prefab )
		{
			
		}
		
		GameObject equipment = (GameObject) GameObject.Instantiate( prefab ) ;
		if( null == equipment )
		{
			return ;
		}
		m_EquipmentGameObjectName = equipment.name = this.gameObject.name + ":" + _EquipmentPrefabName ;
		m_EquipmentObj = equipment ;
		
		Vector3 localPos = equipment.transform.localPosition ;
		Quaternion localRotation = equipment.transform.localRotation ;
		
		equipment.transform.parent = _BipeParent.transform ;
		equipment.transform.localPosition = localPos ;
		equipment.transform.localRotation = localRotation ;
		
	}
}
