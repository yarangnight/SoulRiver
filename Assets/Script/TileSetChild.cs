using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSetChild : MonoBehaviour//MouseDrag이벤트를 TileSet으로 바인딩하는 역할
{
    [SerializeField]
    private TileSet m_Parents;

    [SerializeField]
    private GameObject m_TilePrefab;

    [SerializeField]
    private Vector2[] m_TilesPosition;

    private void OnMouseDrag()
    {
        m_Parents.ChildTileOnDrag();
    }

    public void InstallTiles()
    {
        foreach(var v in m_TilesPosition)
        {
            Instantiate(m_TilePrefab,transform.position + new Vector3(v.x, v.y, 0),Quaternion.Euler(0,0,0));
        }
    }
}