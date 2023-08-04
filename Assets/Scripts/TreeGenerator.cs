using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeGenerator : MonoBehaviour
{
    public GameObject[] treePrefabs;
    public float minX, maxX, minZ, maxZ;
    public float minHeight, maxHeight;
    public int numTrees;
    public Transform Parent;

    void Start()
    {
        
        for (int i = 0; i < numTrees; i++)
        {
            GameObject treePrefab = treePrefabs[Random.Range(0, treePrefabs.Length)];
            GameObject tree = Instantiate(treePrefab, Parent);
            tree.transform.localPosition = GetRandomPosition();
        }
    }
    private Vector3 GetRandomPosition()
    {
        return new Vector3(Random.Range(minX, maxX), Random.Range(minHeight, maxHeight), Random.Range(minZ, maxZ));
    }
}
