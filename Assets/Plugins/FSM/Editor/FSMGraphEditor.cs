﻿using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace FSM.GraphNode {
	public class FSMGraphEditor : EditorWindow {

		#region Fields

		public static List<FSMNodeEditor> nodeList;
		public static List<FSMConditionLineEditor> lineList;

		private static FSMNodeEditor rootNode;
		private static FSMNodeEditor anyState;
		private static FSMGraphEditor root;

		private static string graphName = "FSMGraph";
		private static int nodeTotalCount;
		private static Dictionary<string, FSMNodeEditor> m_Map;

		#endregion

		#region Implementation EditorWindow

		[MenuItem("FSM/Grapth Editor Window")]
		static void Init() {
			root = EditorWindow.GetWindow (typeof (FSMGraphEditor)) as FSMGraphEditor;
			nodeList = new List<FSMNodeEditor>();
			lineList = new List<FSMConditionLineEditor> ();
			rootNode = new FSMNodeEditor ("Root", 50f, 50f, root, FSMNodeEditor.ENodeType.Root);
			anyState = new FSMNodeEditor ("AnyState", 50f, 300f, root, FSMNodeEditor.ENodeType.AnyNode);
			m_Map = new Dictionary<string, FSMNodeEditor> ();
			nodeTotalCount = 0;
		}

		private void OnGUI() {
			var currentEvent = Event.current;
			BeginWindows ();
			rootNode.OnDraw (0, currentEvent);
			anyState.OnDraw (1, currentEvent);
			for (int i = 0; i < nodeList.Count; i++) {
				nodeList [i].OnDraw (i + 2, currentEvent);
			}
			var size = this.position;
			graphName = GUI.TextField (new Rect (size.width - 320f, size.height - 50f, 200f, 30f), graphName);
			if (GUI.Button (new Rect (size.width - 110f, size.height - 60f, 100f, 50f), "Generate JSON")) {
				GenerateJson ("GenerateJson");
			}
			EndWindows();
			if (currentEvent.type == EventType.ContextClick) {
				GenericMenu menu = new GenericMenu ();
				menu.AddItem (new GUIContent ("Create New State"), false, CreateNewNode, "CreateNewState");
				menu.AddSeparator("");
				menu.AddItem (new GUIContent ("Generate JSON"), false, GenerateJson, "GenerateJson");
				menu.ShowAsContext ();
				currentEvent.Use ();
			}
		}

		#endregion

		#region Main methods

		private void CreateNewNode(object obj) {
			nodeTotalCount++;
			var node = new FSMNodeEditor ("FSMState" + nodeTotalCount, 
				300f + (20f * nodeList.Count), 
				200f + (20f * nodeList.Count), root);
			nodeList.Add (node);
			if (nodeList.Count == 1) {
				var isRootCondition = new FSMConditionLineEditor ("IsRoot", rootNode, node);
				rootNode.conditionLines.Add (isRootCondition);
				rootNode.OnDrawCondition = false;
				rootNode.conditionLines[0].haveDraw = true;
			}
		}

		public virtual void GenerateJson(object obj) {
			var path = Application.dataPath + "/Resources/FSMGrapth/" + graphName + ".txt";
			EditorUtility.DisplayProgressBar ("Generate FSM", path, 1f);
			var json = "{\"fsm\":[";
			json += GenerateJson (string.Empty, rootNode);
			json += ",";
			json = json.Replace ("<<0>>", string.Empty);
			if (anyState.conditionLines.Count > 0) {
				json += GetJsonText ("IsAnyState", "AnyState");
				json = json.Replace ("<<0>>", GenerateJson (string.Empty, anyState));
			} else {
				json += GetJsonText ("IsAnyState", "AnyState");
			}
			json = json.Replace ("<<0>>", string.Empty);
			json += "]}";
			// CLASS
			var classDirectory = Application.dataPath + "/Scripts/FSMState";
			if (Directory.Exists(classDirectory) == false) {
				Directory.CreateDirectory (classDirectory);
			}
			var curreProgress = 0f;
			var maxProgress = m_Map.Count;
			foreach (var item in m_Map) {
				var classText = this.GenerateStateFile (item.Key);
				var classPath = classDirectory + "/" + item.Key + ".cs";
				if (File.Exists (classPath) == false) {
					File.WriteAllText (classPath, classText);
				} else {
					Debug.Log ("[DUPLICATE CLASS] " + classPath);
				}
				curreProgress++;
				EditorUtility.DisplayProgressBar ("Generate FSM State", classPath, curreProgress / maxProgress);
			}
			m_Map.Clear ();
			Debug.Log (json);
			File.WriteAllText(path, json);
			AssetDatabase.Refresh ();
			EditorUtility.ClearProgressBar ();
		}

		public virtual string GenerateJson(string json, FSMNodeEditor node) {
			for (int i = 0; i < node.conditionLines.Count; i++) {
				var condition = node.conditionLines [i];
				json += GetJsonText (condition.GetName (), condition.targetNode.GetName ());
				json += (i == node.conditionLines.Count - 1) ? "" : ",";
				if (m_Map.ContainsKey (condition.targetNode.GetName()))
					continue;
				m_Map.Add (condition.targetNode.GetName (), condition.targetNode);
				json = json.Replace ("<<0>>", GenerateJson (string.Empty, condition.targetNode));
			}
			return json;
		}

		public virtual string GenerateStateFile(string name) {
			return 
				"using UnityEngine;\n" +
				"using System.Collections;\n" +
				"using FSM;\n\n" +
				"public class "+name+" : FSMBaseState\n{" +
				"\n\tpublic "+name+"(IContext context) : base (context)\n\t" +
				"{\n\t\tthis.m_FSMStateName = \""+name+"\";" +
				"\n\t}\n\t\n\t" +
				"public override void StartState()\n\t" +
				"{\n\t\tbase.StartState ();\n\t}\n\t\n\t" +
				"public override void UpdateState(float dt)\n\t" +
				"{\n\t\tbase.UpdateState (dt);\n\t}\n\t\n\t" +
				"public override void ExitState()\n\t" +
				"{\n\t\tbase.ExitState ();\n\t}\n}";
		}

		public virtual void DeleteNode(FSMNodeEditor node) {
			for (int i = 0; i < nodeList.Count; i++) {
				for (int x = 0; x < nodeList[i].conditionLines.Count; x++) {
					var line = nodeList [i].conditionLines [x];
					var source = line.sourceNode;
					var target = line.targetNode;
					if (node == source || node == target) {
						nodeList [i].conditionLines.Remove (line);
					}
				}
			}
			if (node.IsNodeType == FSMNodeEditor.ENodeType.Node && nodeList.Contains (node)) {
				nodeList.Remove (node);
			}
		}

		public virtual void DeleteLine(FSMConditionLineEditor line) {
			lineList.Remove (line);
		}

		public virtual string GetJsonText(string condition, string name) {
			return "{\"condition_name\":\"" 
				+ condition + "\",\"state_name\":\"" 
				+ name + "\",\"states\": [<<0>>]}";
		}

		#endregion

	}
}
