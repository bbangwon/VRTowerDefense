using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDGun : MonoBehaviour
{
    public GameObject crossHairPrefab;
    Transform crossHair = null;
    Vector3 originScale;

    public float autoFireTime = 0.5f;   //자동 공격 시간
    float calcAutoFireTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        originScale = crossHairPrefab.transform.localScale;

        //Aim 생성
        if (crossHair == null)
        {
            crossHair = Instantiate(crossHairPrefab, Vector3.zero, Quaternion.identity).transform;
            crossHair.localScale = originScale;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //카메라에서 앞 방향으로 쏘는 레이 생성
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

        //레이를 쏘아 맞는 오브젝트가 있을경우
        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            //Aim 을 위치 한다. 
            crossHair.position = hitInfo.point;
            crossHair.localScale = originScale * hitInfo.distance;
            crossHair.forward = ray.direction * -1; //Aim이 카메라를 향하게 한다.

            calcAutoFireTime += Time.deltaTime;

            //총알을 쏘면
            if (calcAutoFireTime >= autoFireTime)
            {
                calcAutoFireTime = 0f;
                crossHair.GetComponent<TDCrossHair>().Fire();
            }
        }
    }
}
