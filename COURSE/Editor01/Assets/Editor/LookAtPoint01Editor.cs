/**
@file LookAtPoint01Editor.cs
@author NDark
@date 20130819 . file started
*/
#define SHOW_EDITOR
using UnityEngine;
using UnityEditor;

#if SHOW_EDITOR
// Attention!!! un-mark this line to see the change of m_BallTransform
[CustomEditor ( typeof(LookAtPoint01) ) ]
#endif
public class LookAtPoint01Editor : Editor 
{
	
	// change layout of Inspector
    public override void OnInspectorGUI() 
	{
		// target is the class of ExecuteInEditMode01
		LookAtPoint01 targetWithType = (LookAtPoint01)target ;
		
        targetWithType.m_TargetPosition = 
			EditorGUILayout.Vector3Field( "Look At Point" , 
										  targetWithType.m_TargetPosition );
		
        if( GUI.changed )
		{
			/*
			Unity internally uses the dirty flag to find out when assets have changed and need to be saved to disk.
			E.g. if you modify a prefab's MonoBehaviour or ScriptableObject variables, 
			you must tell Unity that the value has changed. Unity builtin components 
			internally call SetDirty whenever a property changes. 
			MonoBehaviour or ScriptableObject don't do this automatically 
			so if you want your value to be saved you need to call SetDirty.
			*/
            EditorUtility.SetDirty( targetWithType );
		}
		//*/
    }		
}

