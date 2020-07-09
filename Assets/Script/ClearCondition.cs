using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCondition : MonoBehaviour
{
    [SerializeField] private string m_QuestText;

    public virtual bool GetIsSuccess()
    {
        return true;
    }
}
