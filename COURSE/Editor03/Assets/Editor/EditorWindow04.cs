/*
@file EditorWindow04.cs
@author NDark

Attention!!!!
Script must be placed in the folder called "Editor" in Assets

# 收集 AlienUnit 集體掛上適當的script
# 收集 Waypoint 集體掛上適當的script 
# 依據目前顯示的 Waypoint 顯示 Scene 畫面中 不同的顏色線條

@date 20130824 file started.
*/
using UnityEngine;
using UnityEditor ; // add this for editor
	
// You don't have to put script on GameObject
public class EditorWindow04 : EditorWindow 
{
	/*
	[MenuItem ("Window/My Window 3")]
    static void ShowWindow () 
	{
        EditorWindow.GetWindow<EditorWindow04>() ;
    }		
	 */
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	}
	
	// the content of your window draw here.
	void OnGUI()
	{
	}

	void OnSceneGUI()
	{
	}
}
