/*
@file EditorWindow04.cs
@author NDark
 
http://docs.unity3d.com/Documentation/ScriptReference/EditorUtility.html

Attention!!!!
Script must be placed in the folder called "Editor" in Assets

@date 20130903 file started.
*/
#define BE_CAREFUL_THIS_BAR
using UnityEngine;
using UnityEditor ; // add this for editor
	
// You don't have to put script on GameObject
public class EditorWindow04 : EditorWindow 
{
	[MenuItem ("Window/EditorWindow04")]
    static void ShowWindow () 
	{
        EditorWindow.GetWindow<EditorWindow04>() ;
    }		
	
	public bool m_ShowProgressBar = false ;
	public float m_ProgressValue = 0.0f ;
	public string m_CollectDeepHierachyStr = "" ;
	public string m_CollectDependenciesStr = "" ;
	// the content of your window draw here.
	void OnGUI()
	{
		if( true == GUILayout.Button( "CollectDeepHierachy" ) && 
			Selection.objects.Length > 0 )
		{
			// Calculates and returns a list of all assets the assets listed in Selection.objects depend on.
			Object []objects = EditorUtility.CollectDeepHierarchy( Selection.objects );
			
			m_CollectDeepHierachyStr = "Length=" + objects.Length + "\n" ;
			foreach( Object obj in objects )
			{
				m_CollectDeepHierachyStr += ( obj.name + " " + obj.GetType() + "\n"  ) ;
			}
		}
		
		EditorGUILayout.TextArea( m_CollectDeepHierachyStr ) ;
		
		if( true == GUILayout.Button( "CollectDependencies" ) && 
			Selection.objects.Length > 0 )
		{
			// Calculates and returns a list of all assets the assets listed in Selection.objects depend on.
			Object []objects = EditorUtility.CollectDependencies( Selection.objects );
			
			m_CollectDependenciesStr = "Length=" + objects.Length + "\n" ;
			foreach( Object obj in objects )
			{
				m_CollectDependenciesStr += ( obj.name + " " + obj.GetType() + "\n"  ) ;
			}
		}
		
		EditorGUILayout.TextArea( m_CollectDependenciesStr ) ;
		
		m_ShowProgressBar = 
			EditorGUILayout.Toggle( "ShowProgressBar" , 
									m_ShowProgressBar ) ;
		if( true == m_ShowProgressBar )
		{
			
			m_ProgressValue += 0.01f ;
			if( m_ProgressValue > 1.0f ) 
				m_ProgressValue = 0.0f ;
			EditorGUILayout.FloatField( m_ProgressValue ) ;
			
			/*
			Attention!!!! 
			警告!!! 
			ご注意ください!!!
			Achtung!
			
			This bar MUST be used the same with OnInspectorUpdate() { Repaint() }.
			Because OnGUI() will not update itself. 
			Therefore, 
			EditorUtility.DisplayCancelableProgressBar() will NOT have chance 
			to excute the code : EditorUtility.ClearProgressBar() ,
			after user click the cancel button.
			
			And unfortunately,
			THE progress bar will block all actions of Unity interface.
			Which means you cannot deactive that bar except close unity.
			
			And sadly,
			The progress bar will show up again, block everything, 
			after you turn on unity again,
			because the variable m_ShowProgressBar is true.
			
			The only thing you have to do is mark the code ( or make it un-compilable ).
			*/
#if BE_CAREFUL_THIS_BAR
			if( true == EditorUtility.DisplayCancelableProgressBar( 
							"Title" , 
							"Progress" , 
							m_ProgressValue ) )
			{
				Debug.Log( "false == EditorUtility.DisplayCancelableProgressBar" ) ;
				EditorUtility.ClearProgressBar() ;
				m_ShowProgressBar = false ;
			}
#endif			
		}
		else
		{
			EditorUtility.ClearProgressBar() ;
		}
	}
	
#if BE_CAREFUL_THIS_BAR
	void OnInspectorUpdate() 
	{
		Repaint();
	}	
#endif
	
}
