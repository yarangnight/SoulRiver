using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Tile : MonoBehaviour
{
    private Tile[] m_closeTiles;

    public Tile[] m_CloseTiles { get => m_closeTiles; }

    [HideInInspector]
    public int m_Id;


    private void Awake()
    {
        TileNavigator.Instance.RegisterTile(this);//타일을 네이게이션에 등록
        DetectCloseTile();//주변 타일을 인식해 m_CloseTiles에 저장
    }

    // Start is called before the first frame update
    void Start()
    {
        //주변 타일 인식이 끝난 후(Awake 이후)
        foreach (var v in m_closeTiles)
        {
            if (!v.HaveTile(this))//주변 타일이 자신을 가지고 있는지 체크
            {
                v.DetectCloseTile();//자신을 가지고 있지 않다면 다시 인식하도록 함
            }
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float x = transform.position.x * 2;
        float y = transform.position.y * 4;

        if (Mathf.Ceil(x) == 1 && Mathf.Floor(y) == 4)
        {
            Debug.Log("eee");
        }

        if ((int)(Mathf.Floor(x) + Mathf.Floor(y)) % 2 == 0)//xy의 합이 짝수
        {
            if(((x - Mathf.Floor(x)) + (y - Mathf.Floor(y))) >= 1)
            {
                x = Mathf.Ceil(x);
                y = Mathf.Ceil(y);
                if (x == 1 && y == 4)
                {
                    Debug.Log("eee");
                }
            }
            else
            {
                x = Mathf.Floor(x);
                y = Mathf.Floor(y);
                if (x == 1 && y == 4)
                {
                    Debug.Log("eee");
                }
            }
        }
        else//xy의 합이 홀수
        {
            if( ((Mathf.Ceil(x) - x)+ (y - Mathf.Floor(y))) >= 1)
            {
                x = Mathf.Floor(x);
                y = Mathf.Ceil(y);
                if (x == 1 && y == 4)
                {
                    Debug.Log("eee");
                }
            }
            else
            {
                x = Mathf.Ceil(x);
                y = Mathf.Floor(y);
                if((int)(x+y)% 2 != 0)
                {
                    x -= 1;
                }

            }
        }




        transform.position = new Vector3(x / 2,y / 4,transform.position.z);
        GetComponent<SpriteRenderer>().sortingOrder = -(int)y;
    }

    public void DetectCloseTile()
    {
        Vector2 position2D = new Vector2(transform.position.x, transform.position.y);
        RaycastHit2D hitRightUp = Physics2D.Raycast(position2D + new Vector2(0.5f, 0.25f), Vector2.zero, 10, 1 << 8);
        RaycastHit2D hitRightDown = Physics2D.Raycast(position2D + new Vector2(0.5f, -0.25f), Vector2.zero, 10, 1 << 8);
        RaycastHit2D hitLeftUp = Physics2D.Raycast(position2D + new Vector2(-0.5f, 0.25f), Vector2.zero, 10, 1 << 8);
        RaycastHit2D hitLeftDown = Physics2D.Raycast(position2D + new Vector2(-0.5f, -0.25f), Vector2.zero, 10, 1 << 8);


        List<Tile> closeTileList = new List<Tile>();
        if (hitRightUp.collider != null)
        {
            Debug.Log(hitRightUp.collider.gameObject.name);
            closeTileList.Add(hitRightUp.collider.gameObject.GetComponent<Tile>());
        }

        if (hitRightDown.collider != null)
        {
            Debug.Log(hitRightDown.collider.gameObject.name);
            closeTileList.Add(hitRightDown.collider.gameObject.GetComponent<Tile>());
        }

        if (hitLeftUp.collider != null)
        {
            Debug.Log(hitLeftUp.collider.gameObject.name);
            closeTileList.Add(hitLeftUp.collider.gameObject.GetComponent<Tile>());
        }

        if (hitLeftDown.collider != null)
        {
            Debug.Log(hitLeftDown.collider.gameObject.name);
            closeTileList.Add(hitLeftDown.collider.gameObject.GetComponent<Tile>());
        }

        m_closeTiles = closeTileList.ToArray();

    }

    public bool HaveTile(Tile other)
    {
        foreach (var v in m_closeTiles)
        {
            if (v == other)
            {
                return true;
            }
        }
        return false;
    }

}
