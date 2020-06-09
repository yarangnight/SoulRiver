using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostCountClearCondition : ClearCondition
{
    private int m_GhostCnt = 0;

    [SerializeField]
    private Ghost[] m_Ghosts = null;

    private void Awake()
    {
        foreach (var v in m_Ghosts)
        {
            v.m_OnGainEvent.AddListener(OnGhostAcquired);
        }
    }

    public override bool GetIsClear()
    {
        base.GetIsClear();

        return m_GhostCnt >= m_Ghosts.Length;
    }

    private void OnGhostAcquired()
    {
        ++m_GhostCnt;
    }
}
