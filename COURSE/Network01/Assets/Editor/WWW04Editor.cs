/**
@file WWW04Editor.cs
@author NDark
@date 20130822 . file started
*/
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof(WWW04)), CanEditMultipleObjects ]
public class WWW04Editor : Editor 
{
	
	
	
	// change layout of Inspector
    public override void OnInspectorGUI() 
	{
		WWW04 finalTarget = (WWW04)target ;

		if( null != finalTarget.m_WWWObj )
		{
			/*
			 ProgressBar has to be assign by rect, 
			 or it will start from the top of whole inspector.
			 http://answers.unity3d.com/questions/133873/editorguiprogressbar.html
			 */
			Rect r = EditorGUILayout.BeginVertical() ;
			EditorGUI.ProgressBar( r , 
								   finalTarget.m_WWWObj.progress , 
								   "Progress" ) ;
			GUILayout.Space( 16 ) ;
			EditorGUILayout.EndVertical() ;
						
		}
		
		/* 
		 強制每個畫格都重畫
		 http://answers.unity3d.com/questions/333181/how-do-you-force-a-custom-inspector-to-redraw.html
		*/
		EditorUtility.SetDirty( target );
		
    }		
}
