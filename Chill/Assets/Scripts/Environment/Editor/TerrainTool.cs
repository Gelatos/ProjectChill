using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;

public class TerrainTool : EditorWindow {

	//_______________________________________________ [PROTECTED VARIABLES]

	protected enum EditType
	{
		None,
		Pencil,
		Brush,
		Erase
	}

	protected class EditHistory
	{
		TerrainObject newTerrainObject;
		TerrainObject oldTerrainObject;
		Vector3 location;
	}

	// Determine editing capabilities
	protected EditType editMode = EditType.None;
	protected bool isVisible = false;

	// Editing
	protected TerrainObject selectedTerrain;


	//_______________________________________________ [GUI FUNCTIONS]
	
	protected void OnGUI () {

		// set this script to visible
		isVisible = true;

		// --------------- Edit Options -----------------

		// show the edit options
		GUIStyle editTypeStyle = new GUIStyle ();
		editTypeStyle.padding = new RectOffset (5, 5, 9, 9);
		EditorGUILayout.BeginVertical (editTypeStyle);
		GUIStyle editTypeButtonStyle = GUI.skin.button; 
		editTypeButtonStyle.margin = new RectOffset (0, 0, 0, 0);
		editMode = (EditType) GUILayout.SelectionGrid ((int)editMode, 
		                                               Enum.GetNames (typeof(EditType)), 
		                                               Enum.GetValues (typeof(EditType)).Length, 
		                                               editTypeButtonStyle, 
		                                               GUILayout.Height (22));
		EditorGUILayout.EndHorizontal ();

	}
	
	protected void SceneGUI (SceneView sceneView) {

		// This will have scene events including mouse down on scenes objects
		if (editMode != EditType.None && isVisible) {
			Event cur = Event.current;

			if ((cur.type == EventType.mouseDown) || (editMode == EditType.Brush && cur.type == EventType.mouseDrag)) {
				// TODO add painting code
			}
		}
	}
	
	[MenuItem ("Window/Terrain Tool")]
	public static void  Init () {

		TerrainTool window = (TerrainTool)EditorWindow.GetWindow (typeof (TerrainTool));
		window.Show();
	}
	
	//_______________________________________________ [MONO FUNCTIONS]
	
	protected void OnEnable () {

		SceneView.onSceneGUIDelegate += SceneGUI;
	}
	
	protected void OnDisable () {

		SceneView.onSceneGUIDelegate -= SceneGUI;
	}

	protected void OnLostFocus () {

		isVisible = false;
	}
}
