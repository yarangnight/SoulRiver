using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGenerator : MonoBehaviour
{
    //배치할 타일의 갯수
    public int TileSP = 10;
    //타일 배치 초기값
    float Tile_X = 7.04f;
    float Tile_Y = 3.52f;
    bool CheckArrange = true; // 배치가 제대로 되었다면 false
    bool Once = false; // 첫 번째 타일이 배치가 되엇다면 true
    bool Once_2 = true;

    //타일 범위 제한
    // float Max_X = 8;
    //float Max_Y = 4;


    // Start is called before the first frame update

    void Awake()
    {
        GameObject TilePrefab = Resources.Load("TilePrefab") as GameObject;
        GameObject TP = GameObject.Find("TileParent");
        GetSize Background = GameObject.Find("Background").GetComponent<GetSize>(); //Background 사이즈 확인용       


        for (int Tile_Supply = 0; Tile_Supply < 10; Tile_Supply++)
        {

            //타일 대각선 첫 배치
            GameObject Arrangement_Tile = Instantiate(TilePrefab);
            Arrangement_Tile.transform.SetParent(TP.transform);
            Arrangement_Tile.transform.position = new Vector3(Tile_X, Tile_Y, 0);



            //맵 타일 배치
            //2의 배수가 되어야 유격이 안생김 
            int RandomX = (Random.Range(1, 10) * 2);
            int RandomY = (Random.Range(1, 10) * 2); 
            int RandomY_PM = (Random.Range(0, 1)); // Y축 음수 양수 랜덤
            int RandomX_PM = (Random.Range(0, 1)); // X축 음수 양수 랜덤
            

            //X축이 배경 밖으로 안나가게 방지
            if (RandomX_PM == 0)
            {
                if (-(Background.GetSpriteSize().x / 2.0f) + 0.44f < Tile_X - (0.44f * RandomX))
                    Tile_X -= 0.44f * RandomX;
                else if (-(Background.GetSpriteSize().x / 2.0f) + 0.44f > Tile_X - (0.44f * RandomX))
                    Tile_X += 0.44f * RandomX;
            }

            if (RandomX_PM == 1)
            {
                if (7.04f - 0.44f > Tile_X + (0.44f * RandomX))
                    Tile_X += 0.44f * RandomX;
                else if (7.04f - 0.44f < Tile_X + (0.44f * RandomX))
                    Tile_X -= 0.44f * RandomX;
            }

            //Y축이 배경 밖으로 안나가게 방지
            if (RandomY_PM == 0)
            {
                if (-(Background.GetSpriteSize().y / 2.0f) + 0.44 < Tile_Y - (0.22f * RandomY))
                    Tile_Y -= 0.22f * RandomY;
                else if (-(Background.GetSpriteSize().y / 2.0f) + 0.44 > Tile_Y - (0.22f * RandomY))
                    Tile_Y += 0.22f * RandomY;
            }

            if (RandomY_PM == 1)
            {
                if ((Background.GetSpriteSize().y / 2.0f) - 0.44 > Tile_Y + (0.22f * RandomY))
                    Tile_Y += 0.22f * RandomY;
                else if ((Background.GetSpriteSize().y / 2.0f) - 0.44 < Tile_Y + (0.22f * RandomY))
                    Tile_Y -= 0.22f * RandomY;
            }
            

           
                
            /*
            if (Once) //최초 배치가 끝났다면
            {
                //x 좌표 거리 차
                if (Mathf.Abs(FirstTile.x) > Mathf.Abs(Arrangement_Tile.transform.position.x))
                    CalculatePos.x = Mathf.Abs(FirstTile.x) - Mathf.Abs(Arrangement_Tile.transform.position.x);
                else if(Mathf.Abs(FirstTile.x) < Mathf.Abs(Arrangement_Tile.transform.position.x))
                    CalculatePos.x =  Mathf.Abs(Arrangement_Tile.transform.position.x) - Mathf.Abs(FirstTile.x) ;

                //y 좌표 거리 차
                if (Mathf.Abs(FirstTile.y) > Mathf.Abs(Arrangement_Tile.transform.position.y))
                    CalculatePos.y = Mathf.Abs(FirstTile.y) - Mathf.Abs(Arrangement_Tile.transform.position.y);
                else if (Mathf.Abs(FirstTile.y) < Mathf.Abs(Arrangement_Tile.transform.position.y))
                    CalculatePos.y = Mathf.Abs(Arrangement_Tile.transform.position.y) - Mathf.Abs(FirstTile.y);

                //거리 차를 타일 하나 거리만큼 나눔
                CalculatePos_2.x = Mathf.Round(CalculatePos.x / 0.44f);
                CalculatePos_2.y = Mathf.Round(CalculatePos.y / 0.22f);

                //
                if(CalculatePos_2.x > CalculatePos_2.y)
                {
                    if ((CalculatePos_2.x - 1.0f) % (CalculatePos_2.y - 1.0f) == 0f)
                        Debug.Log("영");

                    else if ((CalculatePos_2.x - 1.0f) % (CalculatePos_2.y - 1.0f) != 0f)
                    {                      
                        if (CalculatePos_2.y != 0f)
                        {
                           // while (Mathf.Round(Tile_X * 10) * 0.1f % Mathf.Round(Tile_Y * 10) * 0.1f != 0)
                            //{
                                tempCalculate = (int)((CalculatePos_2.x - 1.0f) % (CalculatePos_2.y - 1.0f));
                         
                            Tile_Y -= tempCalculate * 0.22f;
                                //Tile_Y +=  0.22f;
                           // }
                        }


                        Debug.Log(CalculatePos_2.x - 1.0f);
                        Debug.Log(CalculatePos_2.y - 1.0f);

                        Debug.Log((CalculatePos_2.x - 1.0f) % (CalculatePos_2.y - 1.0f));
                        Debug.Log(TP.transform.childCount + " 번 쨰 타일");
                        Debug.Log("고쳐봐야한다.");
                        //나머지 * 0.44 x축에 빼거나 더하고 또는 나머지 *0.22를 y축에 뺴거나 더한다. 
                    }
                }
                else if (CalculatePos_2.x < CalculatePos_2.y)
                {
                    if ((CalculatePos_2.y - 1.0f) % (CalculatePos_2.x - 1.0f) == 0f)
                        Debug.Log("영");

                    else if ((CalculatePos_2.y - 1.0f) % (CalculatePos_2.x - 1.0f) != 0f)
                    {
                       
                        if (CalculatePos_2.x != 0f)
                        {
                           // while (Mathf.Round(Tile_Y * 10) * 0.1f % Mathf.Round(Tile_X * 10) * 0.1f != 0f)
                            //{
                                tempCalculate = (int)((CalculatePos_2.y - 1.0f) % (CalculatePos_2.x - 1.0f));                          
                                Tile_Y -= tempCalculate * 0.22f;
                                //Tile_Y += 0.22f;
                          //  }
                        }

                        Debug.Log(CalculatePos_2.x -1.0f);
                        Debug.Log(CalculatePos_2.y -1.0f);

                        Debug.Log((CalculatePos_2.y - 1.0f) % (CalculatePos_2.x - 1.0f));
                        Debug.Log(TP.transform.childCount + " 번 쨰 타일");
                        Debug.Log("고쳐봐야한다.");
                    }
                }
                else if((CalculatePos_2.x - 1.0f) % (CalculatePos_2.y - 1.0f) == 0f)
                    Debug.Log("perfect");
                //0으로 나누는 경우 x축 또는 y축 한 쪽만 빼고 더해야하는 경우
            }

           
            Once = true; // 첫 번째 배치 끝남
            */



            //범위를 초과하는 오브젝트는 지움
            //if (Tile_X >= Max_X || Tile_Y >= Max_Y || Tile_X <= -(Max_X) || Tile_Y <= -(Max_Y))
            //   Destroy(gameObject);

            //생성되는 타일의 갯 수는 약 10개 정도로 제한
            //1~ 20 사이 랜덤 수 * Tile_X  || -(랜덤 수 * Tile_Y)  && 랜덤 수 * Tile_Y
            //X는 초기값보다 큰수 즉 양수 6보다 큰 수는 안된다.

        }

    }
}