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
			var valueObj = dataComponent.cloneData;
			this.DisplayProperty (valueObj);
		}
	}

	protected virtual void DisplayProperty(object valueObj) {
		var fields = valueObj.GetType ().GetProperties ();
		foreach (var fld in fields) {
			var sampleValue = fld.GetValue (valueObj, null);
			if (sampleValue is int
				|| sampleValue is float
				|| sampleValue is string) {
				GUILayout.Label (string.Format ("{0} - {1}", fld.Name, fld.GetValue (valueObj, null)));
			} else {
				var fieldConstinues = fld.GetCustomAttributes (typeof(UpdateContinueAttribute), false);
				if (fieldConstinues.Length > 0) {
					this.DisplayProperty (fld.GetValue (valueObj, null));
				}
			}
		}
	}

}
