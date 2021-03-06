/*
@file Selection01.cs
@author NDark

Attention!!!!
Script must be placed in the folder called "Editor" in Assets

@date 20130820 file started.
*/
using UnityEngine;
using UnityEditor ; // add this for editor
	
// You don't have to put script on GameObject
public class Selection01 : EditorWindow 
{
	[MenuItem ("Window/SelectionDemonstration")]
    static void ShowWindow () 
	{
        EditorWindow.GetWindow<Selection01>() ;
    }		
	
	
	static public bool m_Toggle1 = true ;
	static public bool m_Toggle2 = true ;
	static public bool m_Toggle3 = true ;
	static public bool m_Toggle4 = true ;
	static public SelectionMode m_Selection = SelectionMode.Unfiltered ;
	// the content of your window draw here.
	void OnGUI()
	{
		string str = "" ;
		m_Toggle1 = GUILayout.Toggle( m_Toggle1 , "Selection.activeObject"  ) ;
		if( true == m_Toggle1 )
		{
			str = Selection.activeObject.ToString() ;
			GUILayout.TextArea( str ) ;
		}
		
		GUILayout.Label( "Selection.activeGameObject" ) ;
		str = Selection.activeGameObject.ToString() ;
		GUILayout.TextArea( str ) ;
		
		GUILayout.Label( "Selection.activeTransform" ) ;
		str = Selection.activeTransform.ToString() ;
		GUILayout.TextArea( str ) ;
		
		GUILayout.Label( "Selection.activeInstanceID" ) ;
		str = Selection.activeInstanceID.ToString() ;
		GUILayout.TextArea( str ) ;		
		
		m_Toggle2 = GUILayout.Toggle( m_Toggle2 , 
			"Selection.gameObjects"  ) ;
		if( true == m_Toggle2 )
		{
			str = "" ;
			foreach( GameObject obj in Selection.gameObjects )
			{
				str += obj.name + "\n" ;
			}
			GUILayout.TextArea( str ) ;
		}
		
		m_Toggle3 = GUILayout.Toggle( m_Toggle3 , 
			"Selection.objects"  ) ;
		if( true == m_Toggle3 )
		{
			str = "" ;
			foreach( Object obj in Selection.objects )
			{
				str += obj.name + "\n" ;
			}
			GUILayout.TextArea( str ) ;
		}
		
		GUILayout.Label( "Selection.transforms" ) ;
		str = "" ;
		foreach( Transform trans in Selection.transforms )
		{
			str += trans.name + "\n" ;
		}
		GUILayout.TextArea( str ) ;
		
		

		GUILayout.Label( "typeof( Texture )" ) ;
		str = "" ;
		foreach( Object obj in 
			Selection.GetFiltered( typeof( Texture ) , 
								   SelectionMode.Unfiltered ) )
		{
			str += obj.name + "\n" ;
		}
		GUILayout.TextArea( str ) ;	
		
		m_Toggle4 = GUILayout.Toggle( m_Toggle4 , 
			"SelectionMode"  ) ;
		
		if( true == m_Toggle4 )
		{
			/*
			Unfiltered 	Return the whole selection.
			TopLevel 	Only return the topmost selected transform. A selected child of another selected transform will be filtered out.
					  (選複數,只傳回最上層)
			Deep 	Return the selection and all child transforms of the selection.
					  (選單數,會連下面一起傳回)
					  
			ExcludePrefab 	Excludes any prefabs from the selection.
			Editable 	Excludes any objects which shall not be modified.
			Assets 	Only return objects that are assets in the Asset directory.
			DeepAssets 	If the selection contains folders, also include all assets and subfolders within that folder in the file hierarchy.
					  (選單數,會連下面一起傳回)					
			*/					
			m_Selection = (SelectionMode)EditorGUILayout.EnumPopup( m_Selection ) ;
			
			str = "" ;
			foreach( Object obj in 
				Selection.GetFiltered( typeof( Object ) , 
									   m_Selection ) )
			{
				str += obj.name + "\n" ;
			}
			GUILayout.TextArea( str ) ;
		}
	}
	
    public void OnInspectorUpdate()
    {
		// http://answers.unity3d.com/questions/38783/repainting-an-editorwindow-when-its-not-the-curren.html
		// This will only get called 10 times per second.
		Repaint();
    }	
}
