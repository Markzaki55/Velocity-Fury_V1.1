// using System;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEditor;
// using UnityEngine;

// public class WayPointManget : EditorWindow {

//     [MenuItem("Project_Wheel_Master/WayPointManget")]
//     private static void ShowWindow() {
//         var window = GetWindow<WayPointManget>();
//         window.titleContent = new GUIContent("WayPointManget");
//         window.Show();
//     }

//     public Transform WaypointRoot;
//     private void OnGUI() {
//         SerializedObject obj = new SerializedObject(this);
//         EditorGUILayout.PropertyField(obj.FindProperty("WaypointRoot"));
//         if(WaypointRoot == null){
//             EditorGUILayout.HelpBox("assin a waypointroot",MessageType.Warning);

//         }
//         else{
//             EditorGUILayout.BeginVertical("Box");
//             DrawButtons();
//             EditorGUILayout.EndVertical();

//         }
//         obj.ApplyModifiedProperties();
        
//     }

//     private void DrawButtons()
//     {
//        if(GUILayout.Button("Create a waypoint")){
//         CreateWaypoint();
//        }
       
//     }

//     private void CreateWaypoint()
//     {
//         GameObject waypointobj = new GameObject("Waypoint"+ WaypointRoot.childCount,typeof(WayPoint));
//         waypointobj.transform.SetParent(WaypointRoot,false);
//         WayPoint waypoint = waypointobj.GetComponent<WayPoint>();
//         if(WaypointRoot.childCount >1){
//             waypoint.PREVIOUSwaypoint = WaypointRoot.GetChild(WaypointRoot.childCount - 2 ).GetComponent<WayPoint>();
//             waypoint.PREVIOUSwaypoint.NEXTwaypoint = waypoint;

//             waypoint.transform.position = waypoint.PREVIOUSwaypoint.transform.position;
//             waypoint.transform.forward = waypoint.PREVIOUSwaypoint.transform.forward;
//         }
//         Selection.activeGameObject = waypoint.gameObject;
//     }
// }
