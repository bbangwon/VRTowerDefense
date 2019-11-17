using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            //일정시간 이후 드론을 생성 한다.
            yield return new WaitForSeconds(Random.Range(minTime, maxTime));
            Instantiate(dronePrefab, transform.position, Quaternion.identity);
        }
    }
}
