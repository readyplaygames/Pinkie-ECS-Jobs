using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SystemSwitcher))]
public class SystemSwitcherEditor : Editor {

	SystemSwitcher obj;

	void OnEnable() {
		obj = (SystemSwitcher)target;
	}

	public override void OnInspectorGUI() {

		GUIStyle selectedStyle = new GUIStyle(GUI.skin.button);
		GUIStyle unselectedStyle = new GUIStyle(GUI.skin.button);
		selectedStyle.normal.textColor = Color.blue;
		unselectedStyle.normal.textColor = Color.black;

		for(int i = 0; i < obj.systems.Length; i++) {

			GUIStyle appliedStyle = obj.systems[i].activeInHierarchy ? selectedStyle : unselectedStyle;

			if(GUILayout.Button(obj.systems[i].name, appliedStyle)) {
				int t = i;
				obj.Activate(t);
			}
		}
		DrawDefaultInspector();
	}
}
