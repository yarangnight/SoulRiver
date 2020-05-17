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
        Vector2 position2D = new Vector2(transform.position.x, transform.position.y);
        TileNavigator.Instance.RegisterTile(this);

        Physics2D.Raycast(position2D, new Vector2(0.5f, 0.25f),10);
        Physics2D.Raycast(position2D, new Vector2(0.5f, -0.25f), 10);
        Physics2D.Raycast(position2D, new Vector2(-0.5f, 0.25f), 10);
        Physics2D.Raycast(position2D, new Vector2(-0.5f, -0.25f), 10);


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
