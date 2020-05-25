using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageGameMode : MonoBehaviour
{
    [SerializeField]
    private GameEndInfo m_GameEndInfo = null;

    [SerializeField]
    private Text m_TimeText = null;

    [SerializeField]
    private float m_TimeLimit = 0.0f;

    private int m_GhostCnt = 0;

    [SerializeField]
    private Ghost[] m_Ghosts = null;

    [SerializeField]
    private GameEndTile m_EndTile = null;

    private void Awake()
    {
        foreach(var v in m_Ghosts)
        {
            v.m_OnGainEvent.AddListener(OnGhostAcquired);
        }
        m_EndTile.m_OnPlayerEnter.AddListener(OnGameEnd);
    }

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    public void SetGamePause(bool haveToPause)
    {
        if(haveToPause)
        {
            Time.timeScale = 0.0f;
        }
        else
        {
            Time.timeScale = 1.0f;
        }
    }

    private void OnGameEnd()
    {
        int cnt = 1;
        if(m_GhostCnt >= m_Ghosts.Length)
        {
            ++cnt;
        }
        if(m_TimeLimit >= 0.0f)
        {
            ++cnt;
        }

        m_GameEndInfo.ShowGameEndInfo(cnt);
    }

    private void OnGhostAcquired()
    {
        ++m_GhostCnt;
    }
}
