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
        m_EndTile.m_OnPlayerStepOn.AddListener(OnGameEnd);
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

        ClearCondition[] conditions = GetComponents<ClearCondition>();

        if(conditions.Length != 2)
        {
            Debug.LogError("Clear Condition is not two");
        }

        int cnt = 1;

        foreach( var v in conditions)
        {
            if(v.GetIsClear())
            {
                ++cnt;
            }
        }

        m_GameEndInfo.ShowGameEndInfo(cnt);
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
