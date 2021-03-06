/**
@file Handle01Editor.cs
@author NDark
@date 20130819 . file started
*/
using UnityEngine;
using UnityEditor;

[CustomEditor ( typeof(Handle01) ) ]
public class Handle01Editor : Editor 
{
	public enum HandleDemonstration
	{
		PositionHandle ,
		RotationHandle ,
		ScaleHandle ,
		RadiusHandle ,
		FreeMoveHandle ,
		FreeRotateHandle ,
		ScaleValueHandle ,
		ScaleSlider ,
		Slider ,
		
		ArrowCap ,
		CircleCap ,
		ConeCap ,	
		CubeCap ,
		CylinderCap ,
		SphereCap ,
		DotCap ,
		Disc ,
		DrawSolidArc ,
		DrawWireArc ,
		
		DrawSolidDisc ,
		DrawWireDisc ,
		
		BeginGUI ,
		Label ,
		MakeA3DButton ,
		
		SelectionFrame ,
		ClearCamera ,
		
	}
	
	
	static public HandleDemonstration m_Switch = HandleDemonstration.PositionHandle ;
	
	static public Color m_HandlesColor = Color.white ;
	static public float m_ScaleSize = 1.0f ;
	
	static public Vector3 m_FreeMoveVec = Vector3.zero ;
	static public float m_RectangleSize = 1.0f ;
	
	static public Quaternion m_FreeMoveRotation = Quaternion.identity ;
	
	static public Rect m_BeginGUIAtScene = new Rect( 50 , 50 , 100 , 200 ) ;
	static public GUIStyle m_Style1 = new GUIStyle() ;
	static public Texture2D m_BackgroundTexture = null ;
	
	static public Quaternion m_DiscRotation = Quaternion.identity ;
	static public bool m_DiscCutOff = false ;
	
	static string m_LabelString = "XD" ;
	
	// change layout of Inspector
    public override void OnInspectorGUI() 
	{
		// target is the class of ExecuteInEditMode01
		Handle01 targetWithType = (Handle01)target ;
		
		m_Switch = (HandleDemonstration) EditorGUILayout.EnumPopup( "Choose Enum" , m_Switch ) ;
		
		m_HandlesColor = EditorGUILayout.ColorField( "Handles.color" , m_HandlesColor ) ;
		
		
		switch( m_Switch )
		{
			
		case HandleDemonstration.FreeMoveHandle :
			m_RectangleSize = EditorGUILayout.FloatField( "RectangleSize" , m_RectangleSize ) ;
			break;		
			
			
		case HandleDemonstration.ScaleSlider :
			EditorGUILayout.FloatField( "ScaleSize" , m_ScaleSize ) ;
			break ;
			
		case HandleDemonstration.ScaleHandle :
		case HandleDemonstration.ArrowCap :
		case HandleDemonstration.CircleCap :
		case HandleDemonstration.ConeCap :
		case HandleDemonstration.CubeCap :
		case HandleDemonstration.CylinderCap :
		case HandleDemonstration.SphereCap :
		case HandleDemonstration.DotCap :
		case HandleDemonstration.DrawSolidArc :
		case HandleDemonstration.DrawWireArc :
		case HandleDemonstration.DrawSolidDisc :
		case HandleDemonstration.DrawWireDisc :
			
			
			m_ScaleSize = EditorGUILayout.FloatField( "ScaleSize" , m_ScaleSize ) ;
			break;		
			
		case HandleDemonstration.Disc :
			m_DiscCutOff = EditorGUILayout.Toggle( "DiscCutOff" , m_DiscCutOff ) ;
			break ;
			
		case HandleDemonstration.BeginGUI :
		
			m_BeginGUIAtScene = EditorGUILayout.RectField( "BeginGUIAtScene" , m_BeginGUIAtScene ) ;
			m_BackgroundTexture = (Texture2D)
				EditorGUILayout.ObjectField( "Background Texture " , 
					m_BackgroundTexture , 
					typeof( Texture2D ) , false ) ;
			m_Style1.fontSize = EditorGUILayout.IntField( "FontSize" , m_Style1.fontSize ) ;
			m_Style1.normal.textColor = m_HandlesColor ;
			m_Style1.normal.background = m_BackgroundTexture ;
				
			break;		
			
		case HandleDemonstration.Label :
			m_LabelString = EditorGUILayout.TextField( "LabelString" , m_LabelString ) ;
			m_Style1.fontSize = EditorGUILayout.IntField( "FontSize" , m_Style1.fontSize ) ;
			m_Style1.normal.textColor = m_HandlesColor ;
			
			
			break ;

		case HandleDemonstration.ClearCamera :

			m_BeginGUIAtScene = 
				EditorGUILayout.RectField( "BeginGUIAtScene" , 
					m_BeginGUIAtScene ) ;			
			
			break ;			

		}
		
		
        if( GUI.changed )
		{
			EditorUtility.SetDirty( targetWithType );
		}
    }	
	
    public void OnSceneGUI () 
	{
		Handle01 targetWithType = (Handle01)target ;
		
		Handles.color = m_HandlesColor ;
		switch( m_Switch )
		{
		case HandleDemonstration.PositionHandle :
			
			Vector3 tmpPos = 
				Handles.PositionHandle( 
					targetWithType.gameObject.transform.position + Vector3.up , 
					targetWithType.gameObject.transform.rotation ) ;
			
			targetWithType.gameObject.transform.position = tmpPos - Vector3.up ;
			break;
		case HandleDemonstration.RotationHandle :
			targetWithType.gameObject.transform.rotation = 
				Handles.RotationHandle( 
					targetWithType.gameObject.transform.rotation ,
					targetWithType.gameObject.transform.position + Vector3.forward ) ;
			break;
			
		case HandleDemonstration.ScaleHandle :
			targetWithType.gameObject.transform.localScale = 
				Handles.ScaleHandle( 
					targetWithType.gameObject.transform.localScale ,
					targetWithType.gameObject.transform.position + Vector3.right , 
					targetWithType.gameObject.transform.rotation , 
					m_ScaleSize ) ;
			
			break;
			
		case HandleDemonstration.RadiusHandle :
			m_ScaleSize = 
				Handles.RadiusHandle( 
					targetWithType.gameObject.transform.rotation , 
					targetWithType.gameObject.transform.position + Vector3.up , 
					m_ScaleSize ) ;
			break;		
			
		case HandleDemonstration.FreeMoveHandle :
			m_FreeMoveVec = Handles.FreeMoveHandle( m_FreeMoveVec , 
													Quaternion.identity ,
												 	m_RectangleSize , 
													Vector3.zero , 
													Handles.RectangleCap ) ;
			targetWithType.gameObject.transform.position = m_FreeMoveVec ;
			break;
			
		case HandleDemonstration.FreeRotateHandle :
			m_FreeMoveRotation = Handles.FreeRotateHandle( m_FreeMoveRotation , 
													Vector3.zero ,
												 	m_RectangleSize  ) ;
			targetWithType.gameObject.transform.rotation = m_FreeMoveRotation ;
			break;
			
		case HandleDemonstration.ScaleValueHandle :
			m_ScaleSize = Handles.ScaleValueHandle( m_ScaleSize ,
													targetWithType.gameObject.transform.position , 
													targetWithType.gameObject.transform.rotation ,
													30 , 
													Handles.RectangleCap ,
													1.0f ) ;
			targetWithType.gameObject.transform.localScale = 
				new Vector3( m_ScaleSize , m_ScaleSize , m_ScaleSize ) ;
			
			break;		
			
		case HandleDemonstration.ScaleSlider :
			m_ScaleSize = Handles.ScaleSlider( m_ScaleSize ,
												targetWithType.gameObject.transform.position , 
												targetWithType.gameObject.transform.up ,
											 	targetWithType.gameObject.transform.rotation ,
												5 , 
												1.0f ) ;
			
			break;			
			
		case HandleDemonstration.Slider :
			targetWithType.gameObject.transform.position = 
				Handles.Slider( targetWithType.gameObject.transform.position  , 
							targetWithType.gameObject.transform.forward + 
							targetWithType.gameObject.transform.up + 
							targetWithType.gameObject.transform.right ) ;
			
			break;			
		
			
			
		case HandleDemonstration.ArrowCap :
			Handles.ArrowCap( 0 , 
				targetWithType.gameObject.transform.position + 
					targetWithType.gameObject.transform.forward * 3 , 
				targetWithType.gameObject.transform.rotation ,
				m_ScaleSize ) ;
			
			break;	
			
		case HandleDemonstration.CircleCap :
			Handles.CircleCap( 0 , 
							   targetWithType.gameObject.transform.position + Vector3.right * 7.0f ,
							   targetWithType.gameObject.transform.rotation ,
							   m_ScaleSize ) ;
				
			break;
			
		case HandleDemonstration.ConeCap :
			Handles.ConeCap( 0 , 
							 targetWithType.gameObject.transform.position + Vector3.right * 7.0f ,
							 targetWithType.gameObject.transform.rotation ,
							 m_ScaleSize ) ;
				
			break;			
			
		case HandleDemonstration.CubeCap :
			Handles.CubeCap( 0 , 
							 targetWithType.gameObject.transform.position + Vector3.right * 7.0f ,
							 targetWithType.gameObject.transform.rotation ,
							 m_ScaleSize ) ;
			break;
			
		case HandleDemonstration.CylinderCap :
			Handles.CylinderCap( 0 , 
							 targetWithType.gameObject.transform.position + Vector3.right * 7.0f ,
							 targetWithType.gameObject.transform.rotation ,
							 m_ScaleSize ) ;
			break;
			
		case HandleDemonstration.SphereCap :
			Handles.SphereCap( 0 , 
							 targetWithType.gameObject.transform.position + Vector3.right * 7.0f ,
							 targetWithType.gameObject.transform.rotation ,
							 m_ScaleSize ) ;
			break;
				
		case HandleDemonstration.DotCap :
			Handles.DotCap( 0 , 
							targetWithType.gameObject.transform.position + Vector3.right * 7.0f ,
							targetWithType.gameObject.transform.rotation ,
							m_ScaleSize ) ;
			break;			
			
		case HandleDemonstration.Disc :
			m_DiscRotation = Handles.Disc( m_DiscRotation , 
						  targetWithType.gameObject.transform.position ,
						  Vector3.up , 
						  10.0f , 
						  m_DiscCutOff , 
						  1.0f ) ;
			break;			
			
		case HandleDemonstration.DrawSolidArc :
			Handles.DrawSolidArc( 
						  targetWithType.gameObject.transform.position , 
						  targetWithType.gameObject.transform.up ,
						  targetWithType.gameObject.transform.right ,
						  m_ScaleSize , 
						  10.0f  ) ;
			break;		

		case HandleDemonstration.DrawWireArc :
			Handles.DrawWireArc( targetWithType.gameObject.transform.position , 
						  targetWithType.gameObject.transform.up ,
						  targetWithType.gameObject.transform.right ,
						  m_ScaleSize , 10.0f  ) ;
			break;					
			 
			
		case HandleDemonstration.DrawSolidDisc :
			Handles.DrawSolidDisc( targetWithType.gameObject.transform.position , 
								   targetWithType.gameObject.transform.up ,
								   m_ScaleSize  ) ;
			break;

		case HandleDemonstration.DrawWireDisc :
			Handles.DrawWireDisc( targetWithType.gameObject.transform.position , 
								   targetWithType.gameObject.transform.up ,
								   m_ScaleSize  ) ;
			break;			
			
			
		case HandleDemonstration.BeginGUI :
			{
				Handles.BeginGUI() ;
					GUI.Box( m_BeginGUIAtScene , 
							"This is " + targetWithType.gameObject.name , 
							m_Style1 ) ;
				Handles.EndGUI() ;
			}
			break;			
			
		case HandleDemonstration.Label :
			Handles.Label( targetWithType.gameObject.transform.position , 
						   m_LabelString , 
							m_Style1 ) ;
			break;		
			
			
		case HandleDemonstration.MakeA3DButton :
			if( true == Handles.Button( targetWithType.gameObject.transform.position + Vector3.right * 7.0f ,
										targetWithType.gameObject.transform.rotation ,
										m_ScaleSize , 2 ,
										Handles.RectangleCap ) )
			{
				Debug.Log( "Button pressed" ) ;
			}
				
			break;
				
		case HandleDemonstration.SelectionFrame :
			
			// just draw a frame , it's boring.....
			Handles.SelectionFrame( 0 , 
									targetWithType.gameObject.transform.position , 
									targetWithType.gameObject.transform.rotation , 
									m_ScaleSize ) ;
			break;			
									
		case HandleDemonstration.ClearCamera :
			
			// Clear camera and re-draw camera view at scene , try enlarge the rectanble
			Handles.ClearCamera( m_BeginGUIAtScene , 
								 Camera.mainCamera ) ;
			break;	

		}
		
        if (GUI.changed)
		{
            EditorUtility.SetDirty (targetWithType);
		}
    }		
}

