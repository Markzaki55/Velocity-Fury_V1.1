using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using System.Linq;

public static class MeshGenerator
{
    public static MeshData GenerateTerrainMeshPlane(List<float3> vertices)
    {
        int width = vertices.Count;
        MeshData meshData = new MeshData(width);
        int vertIndex = 0;
        int triIndex = 0;
        for (int y = 0; y < width; y++)
        {
            meshData.Vertices[vertIndex] = vertices[vertIndex];
            meshData.UVs[vertIndex] = new Vector2(y / (float)width, y / (float)width);
            if (y < (width - 2) / 2)
            {
                meshData.AddTriangle(triIndex + 1, triIndex + 3, triIndex + 2);
                meshData.AddTriangle(triIndex, triIndex + 1, triIndex + 2);
                triIndex += 2;
            }
            vertIndex++;
        }
        return meshData;
    }
}

public class MeshData
{
    public Vector3[] Vertices;
    public int[] Triangles;
    public Vector2[] UVs;
    int _triIndex;
    public MeshData(int numberOfVertices)
    {
        Vertices = new Vector3[numberOfVertices];
        UVs = new Vector2[numberOfVertices];
        Triangles = new int[(numberOfVertices - 2) * 3];
    }

    public void AddTriangle(int a, int b, int c)
    {
        Triangles[_triIndex] = a;
        Triangles[_triIndex + 1] = b;
        Triangles[_triIndex + 2] = c;
        _triIndex += 3;
    }

    public Mesh CreateMesh()
    {
        Mesh mesh = new Mesh();
        mesh.Clear();
        mesh.vertices = Vertices;
        mesh.uv = UVs;
        mesh.triangles = Triangles.Reverse().ToArray();
        
        mesh.RecalculateNormals();
        return mesh;
    }
}