/*
@file MouseRayTest01.cs
@author NDark
@date 20130802 . file started.
*/
using UnityEngine;

public class MouseRayTest01 : MonoBehaviour 
{
	int m_Iterator = 0 ;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		if( true == Input.GetMouseButtonDown( 0 ) )
		{
			Ray ray = 
				Camera.mainCamera.ScreenPointToRay( Input.mousePosition ) ;
			
			RaycastHit hitInfo ;
			if( true == Physics.Raycast( ray , out hitInfo ) ) 
			{
				Object prefab = Resources.Load( "Prefabs/Sphere" ) ;
				if( null != prefab )
				{
					Object obj = Instantiate( prefab , 
						hitInfo.point , 
						Quaternion.identity ) ;
					obj.name = "Touch" + hitInfo.collider.name + m_Iterator.ToString() ;
					++m_Iterator ;
				}
			}
		}
	}
}
