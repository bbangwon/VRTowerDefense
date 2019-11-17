using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TDCrossHair : MonoBehaviourPun
{
    public GameObject bulletImpactPrefab;
    Transform bulletImpact;

    // Start is called before the first frame update
    void Start()
    {
        //파티클 생성
        bulletImpact = Instantiate(bulletImpactPrefab).transform;
    }

    [PunRPC]
    void RpcFire()
    {
        //총알 파티클 재생
        bulletImpact.position = transform.position;
        bulletImpact.GetComponent<ParticleSystem>().Stop();
        bulletImpact.GetComponent<ParticleSystem>().Play();

        //사운드 재생
        bulletImpact.GetComponent<AudioSource>().Stop();
        bulletImpact.GetComponent<AudioSource>().Play();
    }

    public void Fire()
    {
        photonView.RPC("RpcFire", RpcTarget.All);
    }


}
