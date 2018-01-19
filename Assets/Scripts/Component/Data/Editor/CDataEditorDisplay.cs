using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CDataComponent))]
public class CDataEditorDisplay : Editor {

	public override void OnInspectorGUI ()
	{
		base.OnInspectorGUI ();
		var dataComponent = target as CDataComponent;
		if (dataComponent.cloneData != null) {
			GUILayout.Label ("Data Sample");
			// GET PROPERTIES ATTRIBUTE
			var fields = dataComponent.cloneData.GetType ().GetProperties ();
			foreach (var fld in fields) {
				GUILayout.Label (string.Format("{0} - {1}", fld.Name, fld.GetValue (dataComponent.cloneData, null)));
			}
		}
	}

}
