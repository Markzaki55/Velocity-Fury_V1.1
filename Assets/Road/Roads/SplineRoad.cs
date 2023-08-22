using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.Splines;
using Random = UnityEngine.Random;

public struct VertexPair
{
    public float3 P1;
    public float3 P2;
}

[ExecuteInEditMode]
public class SplineRoad : MonoBehaviour
{
    [SerializeField] SplineContainer _splineContainer;
    [SerializeField] MeshFilter _meshFilter;

    [SerializeField] int _splineIndex = 0;
    [SerializeField] float currentX;
    [SerializeField] float currentZ;
    [SerializeField]
    [Range(0f, 1f)]
    float _time;

    [SerializeField] float _width;
    [SerializeField] int _resolution;
    [SerializeField] int _proceduralChunks;
    [SerializeField] int _smoothSteps;

    List<float3> _vertices = new List<float3>();

    float3 _tangent;
    float3 _position;
    float3 _upVector;

    float3 p1;
    float3 p2;

    private void Update()
    {
        
    }

    // Calculate ROAD vertices
    private void CalculateVertices(int increment)
    {
        _vertices = new List<float3>();

        float step = 1f / (float)increment;
        float stepAccumulator = 0;
        for (int i = 0; i <= increment; i++)
        {
            float3[] roadVertsPerKnot = new float3[4];
            CalculatePoints(ref roadVertsPerKnot, stepAccumulator);
            stepAccumulator += step;
            _vertices.AddRange(roadVertsPerKnot);
        }
    }
    private void CalculatePoints(ref float3[] points, float stepAccumulator)
    {
        _splineContainer.Evaluate(_splineIndex, _time + stepAccumulator, out _position, out _tangent, out _upVector);
        float3 right = Vector3.Cross(_tangent, _upVector).normalized;
        for (int i = points.Length - 1; i >= 0; i--)
        {
            if (i < points.Length/ 2)
            {
                points[i] = _position + (-right * _width) * (i % (points.Length / 2) + 1);
            }
            else
            {
                points[i] = _position + (right * _width) * (i % (points.Length / 2) + 1);
            }
        }
    }

    public void GenerateRoad()
    {
        _splineContainer.Spline = new Spline();
        for (int i = 0; i <= _proceduralChunks; i++)
        {
            _splineContainer.Spline.Add(new BezierKnot(new float3(currentX, 0.0f, currentZ)));
            CalculateVertices(i);
            currentX += Random.Range(10.0f, 500.0f);
            currentZ += Random.Range(1.0f, 100.0f);
        }
        MeshData meshData = MeshGenerator.GenerateTerrainMeshPlane(_vertices);
        _meshFilter.sharedMesh = meshData.CreateMesh();
        currentX = 0;
        currentZ = 0;
    }
}
