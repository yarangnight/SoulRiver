using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VineTile : Tile
{
    [SerializeField] private Sprite m_ChangeTargetSprite;

    public override bool IsWalkable
    {
        get => base.IsWalkable;
        set
        {
            base.IsWalkable = value;
            GetComponent<SpriteRenderer>().sprite = m_ChangeTargetSprite;
        }
    }
}
