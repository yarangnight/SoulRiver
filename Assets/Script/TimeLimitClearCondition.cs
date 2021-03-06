﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeLimitClearCondition : ClearCondition
{
    [SerializeField]
    private Text m_TimeText = null;

    [SerializeField]
    private float m_TimeLimit = 0.0f;

    private bool m_isUpdateEnabled = false;

    private void Start()
    {
        StartCoroutine(UpdateOn());
        m_TimeText.text = "" + Mathf.Round(m_TimeLimit);
    }

    private void Update()
    {
        if (m_isUpdateEnabled)
        {
            if (m_TimeLimit > 0)
            {
                m_TimeLimit -= Time.deltaTime;
                if (m_TimeLimit < 0)
                {
                    m_TimeLimit = 0;
                }

                m_TimeText.text = "" + Mathf.Round(m_TimeLimit);
            }
        }
    }


    public override bool GetIsSuccess()
    {
        base.GetIsSuccess();


        return m_TimeLimit > 0; 
    }

    IEnumerator UpdateOn()
    {
        yield return new WaitForSeconds(2.0f);
        m_isUpdateEnabled = true;
    }
}
