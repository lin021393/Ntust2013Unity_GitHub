/*
@file GameSystemData.cs
@author NDark
@date 20130814 file started.
*/
using UnityEngine;

public class GameSystemData : MonoBehaviour 
{
	public string m_PickUpCharacterName = "" ;
	
	// Use this for initialization
	void Start () 
	{
		Object.DontDestroyOnLoad( this.gameObject ) ;	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
