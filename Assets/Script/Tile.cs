using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField]
    private bool isWalkable = true;

    private Tile[] m_closeTiles = null;

    public Tile[] m_CloseTiles { get => m_closeTiles; }


    [HideInInspector]
    public int m_Id;

    private void Awake()
    {
        if (isWalkable)
        {
            TileNavigator.Instance.RegisterTile(this);
        }
        //타일을 네이게이션에 등록
    }

    // Start is called before the first frame update
    void Start()
    {
        if (isWalkable)
        {
            DetectCloseTile();//주변 타일을 인식해 m_CloseTiles에 저장

            //주변 타일 인식이 끝난 후
            foreach (var v in m_closeTiles)
            {
                if (v == null)
                {
                    Debug.LogError("Tlqkf");
                }


                if (!v.HaveTile(this))//주변 타일이 자신을 가지고 있는지 체크
                {
                    v.DetectCloseTile();//자신을 가지고 있지 않다면 다시 인식하도록 함
                }
            }
        }
    }

    public void DetectCloseTile()
    {
        Vector2 position2D = new Vector2(transform.position.x, transform.position.y);
        RaycastHit2D hitRightUp = Physics2D.Raycast(position2D + new Vector2(0.5f, 0.25f), Vector2.zero, 10.0f, 1 << 8);
        RaycastHit2D hitRightDown = Physics2D.Raycast(position2D + new Vector2(0.5f, -0.25f), Vector2.zero, 10.0f, 1 << 8);
        RaycastHit2D hitLeftUp = Physics2D.Raycast(position2D + new Vector2(-0.5f, 0.25f), Vector2.zero, 10.0f, 1 << 8);
        RaycastHit2D hitLeftDown = Physics2D.Raycast(position2D + new Vector2(-0.5f, -0.25f), Vector2.zero, 10.0f, 1 << 8);

        List<Tile> closeTileList = new List<Tile>();
        if (hitRightUp.collider != null)
        {
            if (hitRightUp.collider.gameObject.GetComponent<Tile>() != null)
            {
                if(hitRightUp.collider.gameObject.GetComponent<Tile>().isWalkable)
                {
                    closeTileList.Add(hitRightUp.collider.gameObject.GetComponent<Tile>());
                }

            }
        }

        if (hitRightDown.collider != null)
        {
            if (hitRightDown.collider.gameObject.GetComponent<Tile>() != null)
            {
                if (hitRightDown.collider.gameObject.GetComponent<Tile>().isWalkable)
                {
                    closeTileList.Add(hitRightDown.collider.gameObject.GetComponent<Tile>());
                }
            }
        }

        if (hitLeftUp.collider != null)
        {
            if (hitLeftUp.collider.gameObject.GetComponent<Tile>() != null)
            {
                if (hitLeftUp.collider.gameObject.GetComponent<Tile>().isWalkable)
                {
                    closeTileList.Add(hitLeftUp.collider.gameObject.GetComponent<Tile>());
                }
            }

        }

        if (hitLeftDown.collider != null)
        {
            if (hitLeftDown.collider.gameObject.GetComponent<Tile>() != null)
            {
                if (hitLeftDown.collider.gameObject.GetComponent<Tile>() != null)
                {
                    closeTileList.Add(hitLeftDown.collider.gameObject.GetComponent<Tile>());
                }
            }
        }

        m_closeTiles = closeTileList.ToArray();
    }

    public bool HaveTile(Tile other)
    {
        if (m_closeTiles != null)
        {

            foreach (var v in m_closeTiles)
            {
                if (v == other)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public virtual void OnPlayerStepOn(PlayerController pc)
    {
        Debug.Log("Call");
    }
}
