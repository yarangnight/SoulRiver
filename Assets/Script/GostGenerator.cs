using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GostGenerator : MonoBehaviour
{
    GameObject Player; //플레이어
 
    GameObject GostPrefab; // 유령 프리팹을 가져옴
    GameObject Arrange_Gost; 

    int ScaleKey = 0;

    private void Awake()
    {
    }

    void Start()
    {
        Player = GameObject.Find("Player");
       
        GostPrefab = Resources.Load("GostPrefab") as GameObject;
        Arrange_Gost = Instantiate(GostPrefab) as GameObject; //유령 생성
        //유령 초기 position
        Arrange_Gost.transform.position = new Vector3(Player.transform.position.x + 0.5f, Player.transform.position.y + 0.33f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //플레이어보다 왼쪽에 있으면 플레이어를 바라볼 수 있도록 좌우 반전 key값 설정
        if (Player.transform.position.x > Arrange_Gost.transform.position.x) 
            ScaleKey = -1;
        else 
            ScaleKey = 1;

        Arrange_Gost.transform.localScale = new Vector3(ScaleKey, 1.0f, 1.0f); //좌우 반전

        //플레이어를 따라 이동
        Arrange_Gost.transform.position = Vector3.Lerp(Arrange_Gost.transform.position, 
            new Vector3(Player.transform.position.x + 0.5f, Player.transform.position.y + 0.33f, 0), 0.05f);        
    } 
}