using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    [SerializeField]
    private string m_NextSceneName = null;

    [SerializeField]
    private Camera m_CharacterCamera = null;
    [SerializeField]
    private Camera m_FullScreenCamera = null;

    private bool m_IsCharCamera = true;

    private void Awake()
    {
        foreach(var v in m_Ghosts)
        {
            v.m_OnGainEvent.AddListener(OnGhostAcquired);
        }
        m_EndTile.m_OnPlayerEnter.AddListener(OnGameEnd);
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

    public void NextStage()
    {
        SceneManager.LoadScene(m_NextSceneName);
    }

    public void SwapCamera()
    {
        if(m_IsCharCamera)
        {
            m_IsCharCamera =! m_IsCharCamera;
            m_CharacterCamera.enabled = false;
            m_FullScreenCamera.enabled = true;
        }
        else
        {
            m_IsCharCamera = !m_IsCharCamera;
            m_CharacterCamera.enabled = true;
            m_FullScreenCamera.enabled = false;
        }
    }
}
