using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileSetSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] m_TileSetArr = null;//인스펙터에서 사용하기 위한 2차원 배열

    [SerializeField] private int[] m_TileLimit = null;

    [SerializeField] private Text[] m_TileLimitText = null;

    [SerializeField] private RectTransform m_ButtonPanel = null;

    [SerializeField] private CustomButton[] m_Buttons = null;

    [SerializeField] private Button m_RotateButton_RCW = null;

    [SerializeField] private Button m_RotateButton_CW = null;

    [SerializeField] private Button m_SelectButton = null;

    [SerializeField] private PlayerController m_PlayerController = null;

    private GameObject m_nowTileSet = null;

    private int m_LatestTileSetNum = -1;

    public GameObject m_NowTileSet { get => m_nowTileSet; }

    private void Awake()
    {
        for (int i = 0; i < m_TileLimit.Length; ++i)
        {
            m_TileLimitText[i].text = m_TileLimit[i].ToString();
            if (m_TileLimit[i] <= 0)
            {
                m_Buttons[i].Interactable = false;
                m_Buttons[i].gameObject.GetComponent<Image>().color = new Color(100 / 255.0f, 100 / 255.0f, 100 / 255.0f, 255 / 255.0f);
            }
        }
    }

    public void SpawnTileSet(int num)
    {
        if (m_nowTileSet != null)
        {
            m_RotateButton_RCW.onClick.Invoke();
        }

        m_RotateButton_RCW.onClick.RemoveAllListeners();
        m_RotateButton_CW.onClick.RemoveAllListeners();
        m_SelectButton.onClick.RemoveAllListeners();
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        m_nowTileSet = Instantiate(m_TileSetArr[num], new Vector3(mouseWorldPosition.x, mouseWorldPosition.y, 0), Quaternion.Euler(0, 0, 0));
        m_nowTileSet.GetComponentInChildren<TileSetChild>().OnMouseDown();

        m_ButtonPanel.gameObject.SetActive(true);

        m_RotateButton_RCW.onClick.AddListener(m_nowTileSet.GetComponent<TileSet>().CancelTile);
        m_RotateButton_RCW.onClick.AddListener(m_PlayerController.MoveEnable);

        m_RotateButton_CW.onClick.AddListener(m_nowTileSet.GetComponent<TileSet>().RotateTileSet_CW);

        m_SelectButton.onClick.AddListener(m_nowTileSet.GetComponent<TileSet>().InstallTile);
        m_nowTileSet.GetComponent<TileSet>().m_spawner = this;
        m_nowTileSet.GetComponent<TileSet>().m_playerController = m_PlayerController;


        m_LatestTileSetNum = num;
        m_PlayerController.MoveDisable();
    }

    private void LateUpdate()
    {
        if (m_nowTileSet != null)
        {
            //Debug.Log(Camera.main.WorldToScreenPoint(m_NowTileSet.transform.position));
            //m_ButtonPanel.position = Camera.main.WorldToScreenPoint(m_NowTileSet.transform.position);
        }
        else
        {
            m_ButtonPanel.gameObject.SetActive(false);
        }
    }

    public void ReduceTileLimit()
    {
        m_TileLimitText[m_LatestTileSetNum].text = (--m_TileLimit[m_LatestTileSetNum]).ToString();
        if (m_TileLimit[m_LatestTileSetNum] <= 0)
        {
            m_Buttons[m_LatestTileSetNum].Interactable = false;
            m_Buttons[m_LatestTileSetNum].gameObject.GetComponent<Image>().color = new Color(100 / 255.0f, 100 / 255.0f, 100 / 255.0f, 255 / 255.0f);
        }
    }

    public int[] GetTileLimits()
    {
        return m_TileLimit;
    }
}
