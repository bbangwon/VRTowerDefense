using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDCamRotate : MonoBehaviour
{
    float RX = 0f;
    float RY = 0f;
    public int speed = 300;

    // Start is called before the first frame update
    void Start()
    {
        //처음 오브젝트가 생성될때 호출되는 함수    
    }

    // Update is called once per frame
    void Update()
    {

#if UNITY_EDITOR //안드로이드에서는 실행되지 않도록
        //매 프레임 호출되는 함수
        float mx = Input.GetAxis("Mouse X");
        float my = Input.GetAxis("Mouse Y");
        
        //마우스의 X가 움직일땐 Y축 회전, 마우스의 Y가 움직일땐 X축 회전이 일어난다.
        RY += mx * Time.deltaTime * speed;
        RX += -my * Time.deltaTime * speed; 

        RX = Mathf.Clamp(RX, -60f, 60f);

        transform.eulerAngles = new Vector3(RX, RY, 0f);
#endif
    }
}
