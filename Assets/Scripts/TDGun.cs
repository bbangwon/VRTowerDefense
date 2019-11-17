using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TDGun : MonoBehaviour
{
    public GameObject crossHairPrefab;
    Transform crossHair = null;
    Vector3 originScale;

    public float autoFireTime = 0.5f;   //자동 공격 시간
    float calcAutoFireTime = 0f;

    public int gunPower = 1;    //공격력

    public Sprite aimSprite = null; //나의 aim Sprite

    // Start is called before the first frame update
    void Start()
    {
        originScale = crossHairPrefab.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (TDTower.Instance.gameOver)
            return;

        //Aim 생성
        if (crossHair == null)
        {
            //crossHair = Instantiate(crossHairPrefab, Vector3.zero, Quaternion.identity).transform;
            crossHair = PhotonNetwork.Instantiate("Prefabs/Crosshair", Vector3.zero, Quaternion.identity).transform;
            crossHair.localScale = originScale;
            crossHair.GetComponent<SpriteRenderer>().sprite = aimSprite;
        }

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

                if (hitInfo.collider.tag == "Enemy")
                {
                    //적에게 데미지를 준다.
                    hitInfo.collider.GetComponent<TDDrone>().Damage(gunPower);
                }
                else
                {
                    crossHair.GetComponent<TDCrossHair>().Fire();
                }
            }
        }
    }
}
