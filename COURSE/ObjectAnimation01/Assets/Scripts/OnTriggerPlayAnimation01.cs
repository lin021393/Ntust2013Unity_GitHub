/*
@file OnTriggerPlayAnimation01.cs
@author NDark
@date 20130807 by NDark
*/
using UnityEngine;

public class OnTriggerPlayAnimation01 : MonoBehaviour 
{
	public GameObject m_AnimationTarget = null ;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter( Collider _Other )
	{
		if( null != m_AnimationTarget )
		{
			m_AnimationTarget.animation.Play("WallDown01") ;
		}
	}
	
	void OnTriggerExit( Collider _Other )
	{
		if( null != m_AnimationTarget )
		{
			m_AnimationTarget.animation.Play("WallUp01") ;
		}
	}	
}
