using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontStepTileCondition : ClearCondition
{
    [SerializeField] Tile[] m_Tiles;
    private bool m_NotStep = true;


    private void Awake()
    {
        foreach(var v in m_Tiles)
        {
            v.m_OnPlayerStepOn.AddListener(SetIsStep);
        }
    }

    public override bool GetIsSuccess()
    {
        return false;
    }

    private void SetIsStep()
    {
        m_NotStep = false;
    }
}
