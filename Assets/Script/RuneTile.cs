using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneTile : Tile
{
    [SerializeField] private Tile m_TargetTile;

    public override void OnPlayerStepOn(PlayerController pc)
    {
        base.OnPlayerStepOn(pc);
        m_TargetTile.IsWalkable = true;
    }
}
