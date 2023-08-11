using System.Collections;
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
    [SerializeField] int _proceduralIterations;

    List<float3> _vertices = new List<float3>();

    float3 _tangent;
    float3 _position;
    float3 _upVector;

    float3 p1;
    float3 p2;

    private void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        if (_vertices?.Count <= 0)
            return;
        Handles.matrix = transform.localToWorldMatrix;
        int count = 0;
        //for (int i = 0; i < _vertices.Count; i += 2)
        //{
        //    Handles.SphereHandleCap(count, _vertices[i], quaternion.identity, 1f, EventType.Repaint);
        //    Handles.SphereHandleCap(count, _vertices[i + 1], quaternion.identity, 1f, EventType.Repaint);

        //}

    }

    private void CalculateVertices(int increment)
    {
        _vertices = new List<float3>();

        float step = 1f / (float)increment;
        float stepAccumulator = 0;
        for (int i = 0; i <= increment; i++)
        {
            CalculatePoints(out float3 p1, out float3 p2, stepAccumulator);
            stepAccumulator += step;
            _vertices.Add(p1);
            _vertices.Add(p2);
        }
    }
    private void CalculatePoints(out float3 p1, out float3 p2, float stepAccumulator)
    {
        _splineContainer.Evaluate(_splineIndex, _time + stepAccumulator, out _position, out _tangent, out _upVector);
        float3 right = Vector3.Cross(_tangent, _upVector).normalized;
        p1 = _position + (right * _width);
        p2 = _position + (-right * _width);
    }

    public void GenerateRoad()
    {
        _splineContainer.Spline = new Spline();

        for (int i = 0; i <= _proceduralIterations; i++)
        {
            _splineContainer.Spline.Add(new BezierKnot(new float3(currentX, 0.0f, currentZ)));
            CalculateVertices(i);
            currentX += Random.Range(100.0f, 3000.0f);
            currentZ += Random.Range(10.0f, 30.0f);
        }
        MeshData meshData = MeshGenerator.GenerateTerrainMesh(_vertices);
        _meshFilter.sharedMesh = meshData.CreateMesh();
        currentX = 0;
        currentZ = 0;
    }
}
