using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TDTower : MonoBehaviour
{
    //싱글턴
    static TDTower _instance = null;
    public static TDTower Instance => _instance;

    public int maxHp = 10;
    int currentHp;

    public Slider hpSlider;

    internal bool gameOver = false;
    public GameObject GameOverUi;


    private void Awake()
    {
        _instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        currentHp = maxHp;
        hpSlider.maxValue = maxHp;
        hpSlider.value = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage(int enemyPower)
    {
        currentHp--;
        hpSlider.value = currentHp;
        if (currentHp <= 0)
        {
            gameOver = true;
            GameOverUi.SetActive(true);
            StartCoroutine(RestartCountdown());
        }
    }

    //게임 오버후 자동 카운트 다운후 게임 다시 시작
    IEnumerator RestartCountdown()
    {
        int count = 10;
        while (count > 0)
        {
            //5초가 남았을때 부터 카운트 다운을 보여준다.
            if (count <= 5)
                GameOverUi.GetComponent<Text>().text = "ReGame " + count;

            yield return new WaitForSeconds(1);
            count--;
        }

        SceneManager.LoadScene(0);
    }
}
