using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEndTile : Tile
{
    private UnityEvent m_onPlayerStepOn = new UnityEvent();

    public UnityEvent m_OnPlayerStepOn { get => m_onPlayerStepOn;}

    public override void OnPlayerStepOn(PlayerController pc)
    {
        base.OnPlayerStepOn(pc);
        m_onPlayerStepOn.Invoke();
    }
}
