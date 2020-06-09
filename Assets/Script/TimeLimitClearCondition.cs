using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeLimitClearCondition : ClearCondition
{
    [SerializeField]
    private Text m_TimeText = null;

    [SerializeField]
    private float m_TimeLimit = 0.0f;

    private void Update()
    {
        m_TimeLimit -= Time.deltaTime;
        m_TimeText.text = "" + Mathf.Round(m_TimeLimit);
    }


    public override bool GetIsClear()
    {
        base.GetIsClear();


        return m_TimeLimit > 0; 
    }
}
