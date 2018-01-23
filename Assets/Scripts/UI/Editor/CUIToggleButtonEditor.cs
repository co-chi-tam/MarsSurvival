using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof (CUIToggleButton))]
public class CUIToggleButtonEditor : Editor {

	public override void OnInspectorGUI ()
	{
		DrawDefaultInspector ();
	}

}
