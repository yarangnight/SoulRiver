using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSetChild : MonoBehaviour//MouseDrag이벤트를 TileSet으로 바인딩하는 역할
{
    [SerializeField]
    private TileSet m_Parents = null;

    [SerializeField]
    private GameObject m_TilePrefab = null;

    [SerializeField]
    private Vector2[] m_TilesPosition = null;

    private bool m_isOverlaped = false;

    private bool m_isDraged = false;

    private void Update()
    {
        if(Input.GetMouseButtonUp(0))
        {
            OnMouseUp();
        }

        Debug.Log(m_Parents.transform.position);

        m_isOverlaped = false;
        foreach(var v in m_TilesPosition)
        {
            Vector2 temp = new Vector2(m_Parents.transform.position.x, m_Parents.transform.position.y);
            RaycastHit2D hit = Physics2D.Raycast(temp + v, Vector2.zero, 10.0f, 1 << 8);
            if(hit.collider != null)
            {
                m_isOverlaped = true;
            }
        }
        if(m_isOverlaped)
        {
            GetComponent<SpriteRenderer>().color = new Color(1.0f,0,0,0.5f);
        }
        else
        {
            GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
        }

        if (m_isDraged)
        {
            Vector3 temp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            m_Parents.transform.position = new Vector3(temp.x, temp.y, 0);
        }

    }

    public void OnMouseDrag()
    {
        Vector3 temp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        m_Parents.transform.position = new Vector3(temp.x, temp.y, 0);
    }

    public void OnMouseDown()
    {
        m_isDraged = true;
    }

    public void OnMouseUp()
    {
        m_isDraged = false;
    }

    public bool InstallTiles()
    {
        if(m_isOverlaped)
        {
            return false;
        }

        foreach (var v in m_TilesPosition)
        {
            Instantiate(m_TilePrefab, m_Parents.transform.position + new Vector3(v.x, v.y, 0), Quaternion.Euler(0, 0, 0));
        }
        return true;
    }
}