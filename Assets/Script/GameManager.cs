using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject m_GameEndPanel = null;

    // Start is called before the first frame update
    void Start()
    {
        m_GameEndPanel.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
