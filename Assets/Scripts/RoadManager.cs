// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class RoadManger : MonoBehaviour
// {
//   public GameObject roadPrefab; 
//     public int numRoadsToSpawn = 5; 
//     public float roadLength = 100f; 
//     public float despawnDistance = 50f; 
//     public Transform playerTransform; 

//     private List<GameObject> roadPool; 
//     private int currentRoadIndex = 0; 
//     private float lastSpawnedPosition = 0f; 

//     void Start()
//     {
        
//         roadPool = new List<GameObject>();
//         for (int i = 0; i < numRoadsToSpawn; i++)
//         {
//             GameObject road = Instantiate(roadPrefab);
//             road.SetActive(false);
//             roadPool.Add(road);
//         }
//         lastSpawnedPosition = playerTransform.position.z;
//         for (int i = 0; i < numRoadsToSpawn; i++)
//         {
//             SpawnNextRoad();
//         }
//     }

//     void Update()
//     { 
//         if (playerTransform.position.z > lastSpawnedPosition - roadLength)
//         {
//             SpawnNextRoad();
//         }

//         for (int i = 0; i < roadPool.Count; i++)
//         {
//             GameObject road = roadPool[i];
//             if (road.activeSelf && road.transform.position.z < playerTransform.position.z - despawnDistance)
//             {
//                 road.SetActive(false);
//             }
//         }
//     }

//     void SpawnNextRoad()
//     {
       
//         GameObject road = roadPool[currentRoadIndex];
//         currentRoadIndex = (currentRoadIndex + 1) % roadPool.Count;
//         road.transform.position = new Vector3(0f, 0f, lastSpawnedPosition);
//         road.SetActive(true);
//         lastSpawnedPosition += roadLength;
//     }
// }
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadManager : MonoBehaviour
{
    [SerializeField] private GameObject roadPrefab;
    [SerializeField] private int numRoadsToSpawn = 5;
    [SerializeField] private float roadLength = 100f;
    [SerializeField] private float despawnDistance = 50f;
    [SerializeField] private Transform playerTransform;

    private List<GameObject> roadPool;
    private int currentRoadIndex = 0;
    private float lastSpawnedPosition = 0f;

    private void Start()
    {
        InitializeRoadPool();
        SpawnInitialRoads();
    }

    private void Update()
    {
        CheckForRoadSpawn();
        CheckForRoadDespawn();
    }

    private void InitializeRoadPool()
    {
        roadPool = new List<GameObject>();
        for (int i = 0; i < numRoadsToSpawn; i++)
        {
            GameObject road = Instantiate(roadPrefab);
            road.SetActive(false);
            roadPool.Add(road);
        }
    }

    private void SpawnInitialRoads()
    {
        lastSpawnedPosition = playerTransform.position.z;
        for (int i = 0; i < numRoadsToSpawn; i++)
        {
            SpawnNextRoad();
        }
    }

    private void CheckForRoadSpawn()
    {
        if (playerTransform.position.z > lastSpawnedPosition - roadLength)
        {
            SpawnNextRoad();
        }
    }

    private void CheckForRoadDespawn()
    {
        for (int i = 0; i < roadPool.Count; i++)
        {
            GameObject road = roadPool[i];
            if (road.activeSelf && road.transform.position.z < playerTransform.position.z - despawnDistance)
            {
                road.SetActive(false);
            }
        }
    }

    private void SpawnNextRoad()
    {
        GameObject road = roadPool[currentRoadIndex];
        currentRoadIndex = (currentRoadIndex + 1) % roadPool.Count;
        road.transform.position = new Vector3(0f, 0f, lastSpawnedPosition);
        road.SetActive(true);
        lastSpawnedPosition += roadLength;
    }
}