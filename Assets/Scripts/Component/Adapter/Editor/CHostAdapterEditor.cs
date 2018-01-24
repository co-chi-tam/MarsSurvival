using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Ludiq.Reflection;

[CustomEditor(typeof(CHostAdapterComponent))]
public class CHostAdapterEditor : Editor {

	protected bool m_IsFoldout;
	protected SerializedProperty m_ListData;

	protected virtual void OnEnable() {
//		var targetComponent = target as CHostAdapterComponent;
		this.m_IsFoldout = false;
		this.m_ListData = serializedObject.FindProperty ("m_ListDataResponses").Copy();
	}

	public override void OnInspectorGUI ()
	{
		DrawDefaultInspector ();
		this.m_IsFoldout = EditorGUILayout.Foldout (this.m_IsFoldout, "List events editor", true);
		if (this.m_IsFoldout) {
			var targetComponent = target as CHostAdapterComponent;
			if (GUILayout.Button ("Add Events")) {
				targetComponent.listDataResponse.Add (new CInOutTriggerData ());
			}
			GUILayout.Space (20f);
			EditorGUILayout.BeginVertical ();
			if (this.m_ListData.isArray) {
				for (int i = 0; i < targetComponent.listDataResponse.Count; i++) {
					var prop = this.m_ListData.GetArrayElementAtIndex (i);
					EditorGUILayout.PropertyField (prop, true);
					EditorGUILayout.BeginHorizontal ();
					if (GUILayout.Button ("Remove", GUILayout.Width (100f))) {
						targetComponent.listDataResponse.RemoveAt (i);
						i--;
					}
					EditorGUILayout.EndHorizontal ();
				}
			}
			serializedObject.ApplyModifiedProperties ();
			EditorGUILayout.EndVertical ();
		}
	}

}
