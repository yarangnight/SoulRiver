using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSet : MonoBehaviour
{
    [SerializeField]
    private GameObject[] m_Children;

    private int m_ChildrenSel;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            InstallTile();
        }
        else if(Input.GetKeyDown(KeyCode.R))
        {
            RotateTileSet();
        }
    }

    private void OnMouseDown()
    {
        Debug.Log("down");
    }

    public void ChildTileOnDrag()
    {
        Vector3 temp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(temp.x, temp.y, 0);
    }

    public void RotateTileSet()
    {
        m_Children[m_ChildrenSel++].SetActive(false);

        m_ChildrenSel %= m_Children.Length;

        m_Children[m_ChildrenSel].SetActive(true);
    }

    public void InstallTile()
    {
        m_Children[m_ChildrenSel].GetComponent<TileSetChild>().InstallTiles();
        Destroy(gameObject);
    }
}