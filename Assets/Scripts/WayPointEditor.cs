// using UnityEngine;
// using UnityEditor;

// [CustomEditor(typeof(WayPoint))]
// public class WayPointEditor : Editor
// {
//     private void OnSceneGUI()
//     {
//         // Loop through all WayPoint objects in the scene
//         foreach (WayPoint waypoint in FindObjectsOfType<WayPoint>())
//         {
//             // Draw line to next waypoint
//             if (waypoint.NEXTwaypoint != null)
//             {
//                 Handles.color = Color.green;
//                 Handles.DrawLine(waypoint.transform.position, waypoint.NEXTwaypoint.transform.position);
//             }

//             // Draw line to previous waypoint
//             if (waypoint.PREVIOUSwaypoint != null)
//             {
//                 Handles.color = Color.yellow;
//                 Handles.DrawLine(waypoint.transform.position, waypoint.PREVIOUSwaypoint.transform.position);
//             }

//             // Draw waypoint sphere
//             Handles.color = Color.white;
//             Handles.DrawSolidDisc(waypoint.transform.position, Vector3.up, waypoint.width / 2);
//             Handles.SphereHandleCap(0, waypoint.transform.position, Quaternion.identity, HandleUtility.GetHandleSize(waypoint.transform.position) * 0.2f, EventType.Repaint);

//             // Allow user to select the waypoint in the scene view
//             EditorGUI.BeginChangeCheck();
//             Vector3 newPosition = Handles.PositionHandle(waypoint.transform.position, Quaternion.identity);
//             if (EditorGUI.EndChangeCheck())
//             {
//                 Undo.RecordObject(waypoint.transform, "Move Waypoint");
//                 waypoint.transform.position = newPosition;
//             }
//         }
//     }
// }