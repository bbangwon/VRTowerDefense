using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class TDTower : MonoBehaviourPun, IPunObservable
{
    //싱글턴
    static TDTower _instance = null;
    public static TDTower Instance => _instance;

    public int maxHp = 10;
    int currentHp;

    public Slider hpSlider;

    internal bool gameOver = true;
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

    [PunRPC]
    public void RpcDamage(int damage)
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

    public void Damage(int enemyPower)
    {
        // 데미지는 MasterClient 만 계산한다. 타워 HP는 공유 하기 때문에 서버에서만 계산하여
        // HP를 공유하고 게임오버를 공유한다.
        if (PhotonNetwork.IsMasterClient)
            photonView.RPC("RpcDamage", RpcTarget.All, enemyPower);
    }

    //게임 오버후 자동 카운트 다운후 게임 다시 시작
    IEnumerator RestartCountdown()
    {
        int count = 10;
        while (count > 0)
        {
            //씬 이동 전 모든 오브젝트들을 삭제하여 더이상 동기화 할 오브젝트가 없도록 한다
            if (count == 5 && PhotonNetwork.IsMasterClient)
                PhotonNetwork.DestroyAll();     

            //5초가 남았을때 부터 카운트 다운을 보여준다.
            if (count <= 5)
                GameOverUi.GetComponent<Text>().text = "ReGame " + count;

            yield return new WaitForSeconds(1);
            count--;
        }

        SceneManager.LoadScene(0);
    }

    //타워 HP 동기화
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(currentHp);
        }
        else
        {
            currentHp = (int)stream.ReceiveNext();
        }
    }
}
