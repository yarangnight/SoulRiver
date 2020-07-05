using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.Events;
using System;

public class CustomButton : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private bool m_interactable;
    [SerializeField] private UnityEvent m_OnDown;
    [SerializeField] private Color m_Normal_Color;
    [SerializeField] private Color m_Disabled_Color;


    public bool Interactable { get => m_interactable; set => m_interactable = value; }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(Interactable)
        {
            m_OnDown.Invoke();
        }
    }
}