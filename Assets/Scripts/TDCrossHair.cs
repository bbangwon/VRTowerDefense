using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDCrossHair : MonoBehaviour
{
    public GameObject bulletImpactPrefab;
    Transform bulletImpact;

    // Start is called before the first frame update
    void Start()
    {
        //파티클 생성
        bulletImpact = Instantiate(bulletImpactPrefab).transform;
    }

    public void Fire()
    {
        //총알 파티클 재생
        bulletImpact.position = transform.position;
        bulletImpact.GetComponent<ParticleSystem>().Stop();
        bulletImpact.GetComponent<ParticleSystem>().Play();

        //사운드 재생
        bulletImpact.GetComponent<AudioSource>().Stop();
        bulletImpact.GetComponent<AudioSource>().Play();
    }


}
