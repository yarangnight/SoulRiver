using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameEndInfo : MonoBehaviour
{
    [SerializeField]
    private Image[] m_Stars1;
    [SerializeField]
    private Image[] m_Stars2;
    [SerializeField]
    private Text[] m_QuestText;

    public void ShowGameEndInfo(int num)
    {
        gameObject.SetActive(true);
        for(int i = 0; i < num; ++i)
        {
            m_Stars1[i].gameObject.SetActive(true);
            m_Stars2[i].gameObject.SetActive(true);
        }
    }
}
