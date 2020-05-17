using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public string currentMapName;// transferMap 스크립트에 있는 transferMapName 변수의 값을 저장
    public Tile m_initTile;//플레이어가 최초로 서 있는 타일

    //이동속도 설정
    float PlayerShiftx = 2.0f;
    float PlayerShifty = 1.0f;
    public  float Key = 0f;

    private Coroutine m_MoveCoroutine = null;
    

    



    void Start()
    {
        //DontDestroyOnLoad(this.gameObject); // 씬이 넘어갔을 때 오브젝트가 사라지는 것을 방지
    }

    void Update()
    {
        //// # 캐릭터가 블록 바깥으로 나가지 않도록 타일들과 트리거가 맞닿아있는지 상태를 확인해주면 
        ///*float x = Input.GetAxisRaw("Horizontal"); // 좌우이동
        //float y = Input.GetAxisRaw("Vertical"); // 위아래이동

        //moveDirection = new Vector3(x, y, 0);

        //transform.position += moveDirection* moveSpeed * Time.deltaTime;*/

        //    //캐릭터 이동
        // if (Input.GetKey(KeyCode.RightArrow))
        // {
        //     Key = -1.0f;
        //     transform.Translate(PlayerShiftx * Time.deltaTime , PlayerShifty * Time.deltaTime, 0);
        // }

        // if (Input.GetKey(KeyCode.LeftArrow))
        // {
        //     Key = 1.0f;
        //     transform.Translate(-PlayerShiftx * Time.deltaTime , -PlayerShifty * Time.deltaTime, 0);
        // }

        // if (Input.GetKey(KeyCode.UpArrow))
        //     transform.Translate(-PlayerShiftx * Time.deltaTime , PlayerShifty * Time.deltaTime , 0);

        // if (Input.GetKey(KeyCode.DownArrow))
        //     transform.Translate(PlayerShiftx * Time.deltaTime , -PlayerShifty * Time.deltaTime, 0);

        // if (Key != 0) //캐릭터 좌우 반전
        //     transform.localScale = new Vector3(Key, transform.localScale.y, transform.localScale.z);

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray2NowTile = Camera.main.ScreenPointToRay(Camera.main.WorldToScreenPoint(transform.position));//현재 서 있는 타일을 검출
            RaycastHit2D hit2NowTile = Physics2D.Raycast(ray2NowTile.origin, ray2NowTile.direction, 100, 1 << 8);//8번 레이어, 타일만 들어있는 레이어

            if(hit2NowTile.collider == null)
            {
                Debug.LogError("Now Tile Not Detected");
                return;
            }

            Ray ray2ClickedTile = Camera.main.ScreenPointToRay(Input.mousePosition);//클릭한 타일을 검출
            RaycastHit2D hit2ClickedTile = Physics2D.Raycast(ray2ClickedTile.origin, ray2ClickedTile.direction, 100,1 << 8);//레이어 마스크 8번 Tile만 들어있는 레이어

            if (hit2ClickedTile.collider != null)
            {
                Debug.Log(hit2ClickedTile.collider.gameObject.name);
                Move(hit2NowTile.collider.gameObject.GetComponent<Tile>(), hit2ClickedTile.collider.gameObject.GetComponent<Tile>());
            }
            else
            {
                Debug.LogError("not Detected");
            }
        }
    }

    private void Move(Tile start,Tile end)
    {
        Tile[] path = TileNavigator.Instance.GetPath(start, end);
        if(path == null)
        {
            return;
        }      
        //transform.position = start.transform.position;

        if(m_MoveCoroutine != null)
        {
            StopCoroutine(m_MoveCoroutine);
        }

        foreach(var v in path)
        {
            Debug.Log(v);
        }

        m_MoveCoroutine = StartCoroutine(MoveCorouitne(path));
    }

    IEnumerator MoveCorouitne(Tile[] path)
    {
        int i = 0;
        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, path[i].transform.position, 0.01f);
            if (Vector3.Distance(transform.position, path[i].transform.position) <= Vector3.kEpsilon)
            {
                ++i;
                if(i >= path.Length)
                {
                    break;
                }
            }
            yield return null;
        }
    }
}
