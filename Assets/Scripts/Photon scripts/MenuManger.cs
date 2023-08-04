using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;
using TMPro;

public class MenuManger : MonoBehaviourPunCallbacks
{
    public TMP_InputField createlnput;
    public TMP_InputField joinlnput;


    public void CreateRoom(){
        RoomOptions RM = new RoomOptions();
        RM.MaxPlayers = 2;
        PhotonNetwork.CreateRoom(createlnput.text);
    }
    public void JoinRoom(){
        PhotonNetwork.JoinRoom(joinlnput.text);
    }
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Main");
    }
   
}
