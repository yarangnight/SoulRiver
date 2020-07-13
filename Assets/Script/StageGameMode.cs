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

    [SerializeField]
    private GameObject m_PausePanel = null;

    private bool m_IsCharCamera = true;

    private void Awake()
    {
        m_EndTile.m_OnPlayerStepOn.AddListener(OnGameClear);
        Time.timeScale = 0.0f;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SetGamePause(true);
        }
    }

    public void SetTimeScaleTo1()
    {
        Time.timeScale = 1.0f;
    }

    public void SetGamePause(bool haveToPause)
    {
        if(haveToPause)
        {
            Time.timeScale = 0.0f;
            m_PausePanel.SetActive(true);
        }
        else
        {
            Time.timeScale = 1.0f;
            m_PausePanel.SetActive(false);
        }
    }

    private void OnGameClear()
    {
        Time.timeScale = 0.0f;
        ClearCondition[] conditions = GetComponents<ClearCondition>();
        bool[] isConditionsSuccess = new bool[2];


        if(conditions.Length != 2)
        {
            Debug.LogError("Clear Condition is not two");
        }

        int cnt = 1;
        GameSaveManager.SetHaveStageStar(SceneManager.GetActiveScene().name, 0, true);//0번 스타는 클리어시 무조건 획득
        for (int i = 0; i < conditions.Length; ++i)
        {
            if(isConditionsSuccess[i] = conditions[i].GetIsSuccess())
            {
                GameSaveManager.SetHaveStageStar(SceneManager.GetActiveScene().name, i + 1, true);
                ++cnt;
            }
        }
        m_GameEndInfo.ShowGameEndInfo(isConditionsSuccess[0],isConditionsSuccess[1]);
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

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }

    public void Return2StageSelect()
    {
        SceneManager.LoadScene("StageSelectScene");
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
