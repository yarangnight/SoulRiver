using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TileSet : MonoBehaviour
{
    [SerializeField]
    private GameObject[] m_Children = null;

    private int m_ChildrenSel;

    private void OnMouseDown()
    {
        Debug.Log("down");
    }

    public void RotateTileSet_CW()
    {
        m_Children[m_ChildrenSel++].SetActive(false);

        m_ChildrenSel %= m_Children.Length;

        m_Children[m_ChildrenSel].SetActive(true);
    }

    public void CancelTile()
    {
        Destroy(gameObject);
    }

    public void InstallTile()
    {
        if (m_Children[m_ChildrenSel].GetComponent<TileSetChild>().InstallTiles())
        {
            Destroy(gameObject);
        }
    }
}