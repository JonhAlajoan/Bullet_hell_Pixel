using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Selectable))]

public class Selected : MonoBehaviour, ISelectHandler,
    IDeselectHandler
{
    [SerializeField]
    ButtonsMainMenu menuManager;

    [SerializeField]
    public Text m_tracos, m_texts;
    public Canvas m_canvasEarly, m_canvasNext;

    public void OnSelect(BaseEventData eventData)
    {
        m_tracos.gameObject.SetActive(true);
        menuManager.m_markers = m_tracos;
        menuManager.m_text = m_texts;
    }
    public void OnDeselect(BaseEventData eventData)
    {
        m_tracos.gameObject.SetActive(false);
    }

    public void CanvasNames()
    {
        if(gameObject.name =)
        m_canvasNext = gameObject.GetComponentInParent<Canvas>();
    }
   
}
