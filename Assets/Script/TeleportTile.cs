using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportTile : Tile
{
    [SerializeField] Transform m_Dest;

    public override void OnPlayerStepOn(PlayerController pc)
    {
        base.OnPlayerStepOn(pc);

        pc.transform.position = m_Dest.position;
    }
}
