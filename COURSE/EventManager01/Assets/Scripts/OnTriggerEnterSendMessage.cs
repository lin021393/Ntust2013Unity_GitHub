/*
@file OnTriggerEnterSendMessage.cs
@author NDark
@date 20130816 file started.
*/
using UnityEngine;

public class OnTriggerEnterSendMessage : MonoBehaviour 
{
	
	public void Setup( Condition_Collision _Collision )
	{
		m_ConditionCollisionPtr = _Collision ;
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter( Collider other )
	{
		if( null != m_ConditionCollisionPtr )
		{
			m_ConditionCollisionPtr.SetCollide( true ) ;
		}
	}
	
	Condition_Collision m_ConditionCollisionPtr = null ;
	
}
