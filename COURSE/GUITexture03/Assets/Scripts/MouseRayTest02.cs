/*
@file MouseRayTest02.cs
@author NDark
@date 20130808 . file started.
*/
using UnityEngine;

public class MouseRayTest02 : MonoBehaviour 
{
	DestinationController01 m_DestinationController = null ;

	// Use this for initialization
	void Start () 
	{
		if( null == m_DestinationController )
			InitDestinationController() ;
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if( null == m_DestinationController )
			return ;
		
		if( true == Input.GetMouseButtonDown( 0 ) )
		{
			Ray ray = 
				Camera.mainCamera.ScreenPointToRay( Input.mousePosition ) ;
			RaycastHit hitInfo ;
			if( true == Physics.Raycast( ray , out hitInfo ) ) 
			{
				m_DestinationController.ShowDestination( true , 
														 hitInfo.point ) ;
			}
		}
	}
	
	private void InitDestinationController()
	{
		GameObject globalSingleton = GameObject.Find( "GlobalSingleton" ) ;
		
		if( null != globalSingleton )
		{
			m_DestinationController = 
				globalSingleton.GetComponent<DestinationController01>() ;
		}
	}
}
