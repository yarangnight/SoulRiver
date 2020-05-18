using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Coroutine m_MoveCoroutine = null;

    [SerializeField]
    private float m_PlayerSpeed = 0.01f;

    void Start()
    {
        //DontDestroyOnLoad(this.gameObject); // 씬이 넘어갔을 때 오브젝트가 사라지는 것을 방지
    }

    void Update()
    {
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
            transform.position = Vector3.MoveTowards(transform.position, path[i].transform.position, m_PlayerSpeed);
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
