using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEndTile : MonoBehaviour
{
    private UnityEvent m_onPlayerEnter = new UnityEvent();

    public UnityEvent m_OnPlayerEnter { get => m_onPlayerEnter;}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<PlayerController>() != null)
        {
            m_onPlayerEnter.Invoke();
        }
    }
}
