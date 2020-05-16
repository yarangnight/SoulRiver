using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Tile[] m_CloseTiles;
    [HideInInspector]
    public int m_Id;

    private void Awake()
    {
        TileNavigator.Instance.RegisterTile(this);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
