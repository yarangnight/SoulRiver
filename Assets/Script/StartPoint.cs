using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{
    public string startPoint; // 맵 이동시 캐릭터가 시작할 위치
    private PlayerController thePlayer; 
    // Start is called before the first frame update
    void Start()
    {
        thePlayer = GameObject.Find("Player").GetComponent<PlayerController>();// 플레이어의 스크립트을 얻어옴
        if (startPoint == thePlayer.currentMapName) 
        {
            thePlayer.transform.position = this.transform.position; // stage2에 생성한 박스 콜라이더 위치에 캐릭터를 이동
        }
    }
}
