using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameEndInfo : MonoBehaviour
{
    [SerializeField]//0번은 반드시 열리기 때문에 따로 신경쓰지 않는다
    private Image m_Star1 = null;
    [SerializeField]
    private Image m_Star2 = null;
    [SerializeField]
    private Text[] m_QuestText = null;

    public void ShowGameEndInfo(bool haveStar1, bool haveStar2)
    {
        string[,] data = CsvLoader.LoadCsvBy2DimensionArray("Csv/SoulRiver_Mission");

        for (int i = 1; i < data.GetLength(0); ++i)
        {
            if (data[i, 1] == SceneManager.GetActiveScene().name)
            {
                m_QuestText[0].text = data[i, 3];
                m_QuestText[1].text = data[i, 4];
            }
        }



        gameObject.SetActive(true);
        if (haveStar1)
        {
            m_Star1.gameObject.SetActive(true);
        }

        if (haveStar2)
        {
            m_Star2.gameObject.SetActive(true);
        }


    }
}
