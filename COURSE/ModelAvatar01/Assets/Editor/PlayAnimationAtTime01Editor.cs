/*
@file PlayAnimationAtTime01Editor.cs
@author NDark
@date 20130908 file started.
*/
using UnityEngine;
using UnityEditor;
using System.Collections.Generic ;

[CustomEditor ( typeof(PlayAnimationAtTime01) ) ]
public class PlayAnimationAtTime01Editor : Editor 
{
	public int m_Index = 0 ;
	// change layout of Inspector
    public override void OnInspectorGUI() 
	{
		// target is the class of ExecuteInEditMode01
		PlayAnimationAtTime01 targetWithType = (PlayAnimationAtTime01)target ;
		
		List<string> stateName = new List<string>() ;
		foreach( AnimationState state in targetWithType.animation )
		{
			stateName.Add( state.name ) ;
		}
		
		string [] options = stateName.ToArray() ;
		if( options.Length > 0 )
		{
			m_Index  = EditorGUILayout.Popup( m_Index , options ) ;
			
			string selectStr = options[ m_Index ] ;
			
			/*
			http://answers.unity3d.com/questions/181903/jump-to-a-specific-frame-in-an-animation.html
			*/
			targetWithType.m_AnimationStr = EditorGUILayout.TextField( selectStr ) ;
			
			if( null != targetWithType.animation[ selectStr ] )
			{
				AnimationState animState = targetWithType.animation[ selectStr ] ;
				
				targetWithType.m_AnimationTime = 
					EditorGUILayout.Slider( "Animation Time" , 
						targetWithType.m_AnimationTime , 
						0 , 
						animState.length ) ;
			}
		}
		
        if( GUI.changed )
		{
			targetWithType.PlayAnimationAtSpecifiedTime() ;
            EditorUtility.SetDirty( targetWithType );
		}
    }		
}
