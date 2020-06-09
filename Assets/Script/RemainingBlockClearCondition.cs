using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemainingBlockClearCondition : ClearCondition
{
    [SerializeField] private TileSetSpawner m_tileSetSpawner;
    [SerializeField] int m_RemainingTileLimits;
    public override bool GetIsClear()
    {
        base.GetIsClear();

        int[] remainingTiles = m_tileSetSpawner.GetTileLimits();
        int sum = 0;
        foreach(var v in remainingTiles)
        {
            sum += v;
        }

        return sum >= m_RemainingTileLimits;
    }
}
