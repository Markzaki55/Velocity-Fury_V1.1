// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using Photon.Pun;
// using Photon.Realtime;

// public class SpawnPlayers : MonoBehaviourPunCallbacks
// {
//     [SerializeField] GameObject playerPrefab;

//     [SerializeField] float minX, minY, minZ, maxX, maxY, maxZ;

//     void Start()
//     {
//         if (PhotonNetwork.IsConnectedAndReady)
//         {

//             Vector3 randomPos = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), Random.Range(minZ, maxZ));
//             GameObject playerObj = PhotonNetwork.Instantiate(playerPrefab.name, randomPos, Quaternion.identity);



//         }
//     }

// }
// using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.Rendering.Universal;

public class SpawnPlayers : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject[] carPrefabs;
    [SerializeField] float minX, minY, minZ, maxX, maxY, maxZ;

    void Start()
    {
        if (PhotonNetwork.IsConnectedAndReady)
        {

            int randomCarIndex = Random.Range(0, carPrefabs.Length);
            GameObject carPrefab = carPrefabs[randomCarIndex];


            Vector3 randomPos = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), Random.Range(minZ, maxZ));


            GameObject playerObj = PhotonNetwork.Instantiate(carPrefab.name, randomPos, Quaternion.identity);
        }
    }
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.L))
        {
            QualitySettings.SetQualityLevel(0, true);


            UniversalRenderPipelineAsset urpAsset = UnityEngine.Rendering.GraphicsSettings.renderPipelineAsset as UniversalRenderPipelineAsset;


            urpAsset.shadowCascadeOption = ShadowCascadesOption.NoCascades;


            urpAsset.msaaSampleCount = 1;

            UnityEngine.Rendering.GraphicsSettings.renderPipelineAsset = urpAsset;
        }

    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (PhotonNetwork.LocalPlayer.ActorNumber != PhotonNetwork.MasterClient.ActorNumber)
        {
            int randomCarIndex = Random.Range(0, carPrefabs.Length);
            GameObject carPrefab = carPrefabs[randomCarIndex];

            Vector3 randomPos = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), Random.Range(minZ, maxZ));

            GameObject playerObj = PhotonNetwork.Instantiate(carPrefab.name, randomPos, Quaternion.identity);
        }
    }
}

// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using Photon.Pun;
// using Photon.Realtime;
// using UnityEngine.Rendering.Universal;

// public class SpawnPlayers : MonoBehaviourPunCallbacks
// {
//     [SerializeField] GameObject[] carPrefabs;
//     [SerializeField] float minX, minY, minZ, maxX, maxY, maxZ;
//     bool Executed;

//     void Start()
//     {
//         if (PhotonNetwork.IsConnectedAndReady && !PhotonNetwork.IsMasterClient)
//         {
//             int randomCarIndex = Random.Range(0, carPrefabs.Length);
//             GameObject carPrefab = carPrefabs[randomCarIndex];

//             Vector3 randomPos = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), Random.Range(minZ, maxZ));

//             GameObject playerObj = PhotonNetwork.Instantiate(carPrefab.name, randomPos, Quaternion.identity);
//         }
//     }

// 

//     public override void OnPlayerEnteredRoom(Player newPlayer)
//     {
//         if (PhotonNetwork.IsMasterClient)
//         {
//             int randomCarIndex = Random.Range(0, carPrefabs.Length);
//             GameObject carPrefab = carPrefabs[randomCarIndex];

//             Vector3 randomPos = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), Random.Range(minZ, maxZ));

//             GameObject playerObj = PhotonNetwork.Instantiate(carPrefab.name, randomPos, Quaternion.identity);
//         }
//     }
// }