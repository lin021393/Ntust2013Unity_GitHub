using UnityEngine;
using System.Collections;

public class ChessUnitData01 : MonoBehaviour 
{
	public string m_ChessType = "Pawn" ;
	public string m_Role = "Player" ;
	// Use this for initialization
	void Start () 
	{
		if( this.gameObject.transform.position.z > 5 )
			m_Role = "Enemy" ;
		else
			m_Role = "Player" ;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
