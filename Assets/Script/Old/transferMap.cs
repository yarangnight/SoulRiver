using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class transferMap : MonoBehaviour
{
    public string transferMapName; // 씬 stage2를 얻어옴
    TheEnd StageEnd; 
    private PlayerController thePlayer; 
    
    // Start is called before the first frame update
    void Start()
    {
        StageEnd = GameObject.Find("Player").GetComponent<TheEnd>();
        thePlayer = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (StageEnd.Stage_1)// TheEnd 클래스에서 클리어 확인용 bool 값을 가져옴
        //{
        //    thePlayer.currentMapName = transferMapName; // 플레이어 클래스에 있는 currrentMapName에 Stage2 씬을 넣어줌
        //    SceneManager.LoadScene(transferMapName); //씬 전환
        //}
    }
}
