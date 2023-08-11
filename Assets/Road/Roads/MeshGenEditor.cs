using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;


[CustomEditor(typeof(SplineRoad))]
public class MeshGenEditor : Editor
{
    public override void OnInspectorGUI()
    {
        SplineRoad mapGen = (SplineRoad)target;
        if (DrawDefaultInspector())
        {
            mapGen.GenerateRoad();
        }
        if (GUILayout.Button("Generate"))
        {
            mapGen.GenerateRoad();
        }
    }
}
