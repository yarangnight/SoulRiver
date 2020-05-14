using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureGenerator : MonoBehaviour
{
    GameObject CreaturePrefab = new GameObject(); // 크리쳐
    GameObject TP = new GameObject(); // 맵에 뿌려지는 타일들의 부모를 데려옴
    // Start is called before the first frame update
    void Start()
    {
        GameObject CreaturePrefab = Resources.Load("CreaturePrefab") as GameObject; // 크리쳐 프리팹을 가져옴
        GameObject TP = GameObject.Find("TileParent"); 

        int CreaturePoS = Random.Range(2, 6); //크리쳐를 배치할 타일번호를 중간 쯤으로 랜덤으로 부여
        for (int i = 0; i < TP.transform.childCount; i++)
        {           
            if (i == CreaturePoS)
            {               
                GameObject Arrangement_CT = Instantiate(CreaturePrefab) as GameObject;
                Arrangement_CT.transform.position = TP.transform.GetChild(i).transform.position;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
