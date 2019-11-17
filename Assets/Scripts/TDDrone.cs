using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class TDDrone : MonoBehaviour
{
    NavMeshAgent agent;

    public GameObject explosion;

    public int maxHp = 1;
    int currentHp = 0;

    public Slider hpSlider;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = TDTower.Instance.transform.position;

        currentHp = maxHp;  //체력 세팅
        hpSlider.maxValue = maxHp;
        hpSlider.value = maxHp;
    }

    // Update is called once per frame
    void Update()
    {


    }

    public void Damage(int damage)
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

            //적 제거
            Destroy(gameObject);
        }
    }
}
