using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TDDroneSpawner : MonoBehaviour
{
    public float minTime = 1f;
    public float maxTime = 5f;
    public GameObject dronePrefab;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Spawn()
    {
        while(true)
        {
            if(!TDTower.Instance.gameOver && PhotonNetwork.IsMasterClient)  //게임 오버후에는 생성하지 않도록
            {
                //일정시간 이후 드론을 생성 한다.
                yield return new WaitForSeconds(Random.Range(minTime, maxTime));

                //Wait For Second이후 gameOver가 발생하면 Drone을 생성하게 되고
                //이후 씬 이동시 동기화가 엉킬수도 있음.
                if (TDTower.Instance.gameOver)  
                    break;

                //Instantiate(dronePrefab, transform.position, Quaternion.identity);
                PhotonNetwork.InstantiateSceneObject("Prefabs/Drone", transform.position, Quaternion.identity);
            }
            else
            {
                yield return null;
            }

        }
    }
}
