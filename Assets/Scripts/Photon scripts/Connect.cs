using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class Connect : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.SendRate = 30;
        PhotonNetwork.SerializationRate = 12;
        //PhotonNetwork.AutomaticallySyncScene = true;

        
    }

    public override void OnConnectedToMaster()
    {SceneManager.LoadScene("MultiMenu");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
