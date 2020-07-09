using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageInfoLoader : MonoBehaviour
{
    [SerializeField] private Image[] starImages = null;
    [SerializeField] private Image[] starImages2 = null;

    [SerializeField] private int stageNum;
    [SerializeField] private Text[] MissionText = null;

    [SerializeField] private Button m_StageButton;

    private void Awake()
    {
        string[,] data = CsvLoader.LoadCsvBy2DimensionArray("Csv/SoulRiver_Mission");
        string stageName = data[stageNum, 1];
        MissionText[0].text = data[stageNum, 3];
        MissionText[1].text = data[stageNum, 4];

        for (int i = 0; i < 3; ++i)
        {
            starImages[i].gameObject.SetActive(GameSaveManager.GetHaveStageStar(stageName, i));
            starImages2[i].gameObject.SetActive(GameSaveManager.GetHaveStageStar(stageName, i));
        }

        if (stageNum < 2)
        {
            m_StageButton.interactable = true;
        }
        else
        {
            m_StageButton.interactable = GameSaveManager.GetHaveStageStar(data[stageNum - 1, 1], 0);
        }


        gameObject.SetActive(false);
    }
}
