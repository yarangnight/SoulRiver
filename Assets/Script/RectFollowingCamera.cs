﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(BoxCollider2D))]
public class RectFollowingCamera : MonoBehaviour
{
    [SerializeField]
    private GameObject m_Player;

    private BoxCollider2D m_Collider;

    private bool m_isUpdateEnabled = false;

    private bool m_toggleEnabled = false;

    private void Awake()
    {
        m_Collider = gameObject.GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        StartCoroutine(UpdateOn());
    }

    private void Update()
    {
        if (m_isUpdateEnabled)
        {
            if (!(m_Collider.bounds.min.x < m_Player.transform.position.x &&//바운드 내에 플레이어가 존재하지 않으면
                m_Player.transform.position.x < m_Collider.bounds.max.x &&
                m_Collider.bounds.min.y < m_Player.transform.position.y &&
                m_Player.transform.position.y < m_Collider.bounds.max.y))
            {
                transform.position = Vector3.Lerp(
                    new Vector3(transform.position.x, transform.position.y, transform.position.z),
                    new Vector3(m_Player.transform.position.x, m_Player.transform.position.y, transform.position.z), Time.deltaTime * 0.4f);
            }
        }
    }

    IEnumerator UpdateOn()
    {
        yield return new WaitForSeconds(2.0f);
        m_isUpdateEnabled = true;
        m_toggleEnabled = true;
    }

    public void Toggle()
    {
        if(m_toggleEnabled)
        {
            m_isUpdateEnabled = !m_isUpdateEnabled;
            Debug.Log("RectFollowingCamera ToggleEnabled");
        }
    }
}
