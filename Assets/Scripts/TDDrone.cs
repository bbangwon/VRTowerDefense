using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Photon.Pun;

public class TDDrone : MonoBehaviourPun
{
    NavMeshAgent agent;

    public GameObject explosion;

    public int maxHp = 1;
    int currentHp = 0;

    public Slider hpSlider;

    public float attackTime = 5f;   //공격후 이 시간(초)이 지나야 다시 공격할수 있다. 
    float calcAttackTime = 0f;  //공격시간을 계산하기 위한 변수

    public float attackRange = 2f;  //공격범위
    public int attackPower = 1; //공격력

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //agent.destination = TDTower.Instance.transform.position;
        if (photonView.IsMine)
            agent.destination = TDTower.Instance.transform.position;    //MasterClient가 조종
        else
            agent.enabled = false;

        currentHp = maxHp;  //체력 세팅

        calcAttackTime = attackTime;    //적 총알 장전

        hpSlider.maxValue = maxHp;
        hpSlider.value = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        if (TDTower.Instance.gameOver)  //게임 오버가되면 공격하지 않음
            return;

        //Tower를 Detection
        if (Vector3.Distance(TDTower.Instance.transform.position, transform.position) <= attackRange)
        {
            calcAttackTime += Time.deltaTime;
            //공격시작
            if (calcAttackTime >= attackTime)
            {
                calcAttackTime = 0f;
                TDTower.Instance.Damage(attackPower);
            }
        }
    }

    [PunRPC]    //원격호출
    void RpcDamage(int damage)
    {
        currentHp -= damage;
        hpSlider.value = currentHp;
        if (currentHp <= 0)  //사망
        {
            //Explosion Effect 처리
            GameObject explosionGO = Instantiate(explosion, transform.position, Quaternion.identity);

            explosionGO.GetComponent<ParticleSystem>().Stop();
            explosionGO.GetComponent<ParticleSystem>().Play();

            explosionGO.GetComponent<AudioSource>().Stop();
            explosionGO.GetComponent<AudioSource>().Play();

            if (PhotonNetwork.IsMasterClient)
                PhotonNetwork.Destroy(gameObject);
        }
    }

    public void Damage(int damage)
    {
        photonView.RPC("RpcDamage", RpcTarget.All, damage);
    }
}
