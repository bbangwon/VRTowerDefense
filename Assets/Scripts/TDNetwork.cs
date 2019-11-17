using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TDNetwork : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {       
        if (PhotonNetwork.IsConnected)
        {
            //이미 연결되어 있을경우 룸에 접속 시도
            if (PhotonNetwork.InRoom)
                GameStart();
            else
                PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            //포톤 클라우드에 접속
            PhotonNetwork.ConnectUsingSettings();
        }                
    }


    public override void OnConnectedToMaster()
    {
        Debug.Log("새로 접속하여 룸 입장을 시도합니다.");
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Room이 없어 새로 생성합니다.");
        PhotonNetwork.CreateRoom(null);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("룸에 입장했습니다.");
        GameStart();
    }

    public void GameStart()
    {
        Debug.Log("게임을 시작합니다.");
        TDTower.Instance.gameOver = false;
    }





}
