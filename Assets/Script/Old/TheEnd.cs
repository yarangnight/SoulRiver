using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheEnd : MonoBehaviour
{ //임의 종료
    GameObject TP; // 타일 부모
    public bool Stage_1 = false; //임의 클리어 판정
    List<float> TilePos_X; //x 축 값을 정렬
    // Start is called before the first frame update

    private void Awake()
    {
        
    }


    void Start()
    {
        TP = GameObject.Find("TileParent"); //타일 부모
        TilePos_X = new List<float>(); //타일의 x축 값을 담을 리스트
        for (int i = 0; i < TP.transform.childCount; i++)
            TilePos_X.Add(TP.transform.GetChild(i).transform.position.x); //맵에 있는 타일들의 x축 값을 받아옴

        TilePos_X.Sort();       // 정렬
    }

    private void OnTriggerEnter2D(Collider2D other) // 트리거 충돌이벤트
    {
        for (int i = 0; i < TP.transform.childCount; i++)
        {// 플레이어와 충돌한 타일의 x출 값을 비교 오름차순으로 정렬된 리스트의 첫 번쨰(가장 작은 값의 x축과 일치하면)
            //stage1은 임의로 클리어 => transferMap에서 씬 전환
            // # 씬전환시 카메라 bound 영역설정이 캐릭터를 못쫒아가게 막게되어 고칠 예ㅔ정 
            if (other.gameObject.tag == "TilePrefab" && other.transform.position.x == TilePos_X[0])                
                Stage_1 = true;
        }
    }
    // Update is called once per frame
}
