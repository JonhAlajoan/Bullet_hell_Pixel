using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonsMainMenu : MonoBehaviour, ISelectHandler, 
    IDeselectHandler
{
    [SerializeField]
    protected Text m_tracos, m_texts;

    [SerializeField]
    protected Canvas m_actualCanvas, m_nextCanvas, m_backCanvas;

    [SerializeField]
    

    protected bool m_colourChange, m_fadeOut;

    protected float m_currentDelay, m_delayBetweenColorChange, m_CurrentDelayFade;

    protected EventSystem m_eventSystem;

    public Image imageFadeOut;

    void Start()
    {
        imageFadeOut.color = new Color32(34, 32, 32, 0);
        m_eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        m_currentDelay = 0;
        m_CurrentDelayFade = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        changeColor();
    }

    public void OnSelect(BaseEventData eventData)
    {
        m_tracos.gameObject.SetActive(true);
    }
    public void OnDeselect(BaseEventData eventData)
    {
        m_tracos.gameObject.SetActive(false);
    }

    public IEnumerator NextCanvasColorChange()
    {
        m_colourChange = true;
        m_currentDelay = Time.time + m_CurrentDelayFade;

        yield return new WaitForSeconds(0.5f);
        m_colourChange = false;
        m_fadeOut = true;
        m_currentDelay = Time.time + m_CurrentDelayFade;
        yield return new WaitForSeconds(0.5f);
        m_fadeOut = false;
        NextCanvas();

    }

    public IEnumerator ReturnCanvasColorChange()
    {
        m_colourChange = true;
        yield return new WaitForSeconds(0.5f);
        m_colourChange = false;
        m_texts.color = new Color32(255, 255, 255, 255);
        m_tracos.color = new Color32(255, 255, 255, 255);
        ReturnCanvas();
    }

    public void NextCanvas()
    {
            m_actualCanvas.gameObject.SetActive(false);
            m_nextCanvas.gameObject.SetActive(true);
            GameObject m_nextCanvasSelectable = m_nextCanvas.gameObject.GetComponentInChildren<Selectable>().gameObject;
            m_eventSystem.SetSelectedGameObject(m_nextCanvasSelectable);
    }

    public void ReturnCanvas()
    {

            m_actualCanvas.gameObject.SetActive(false);
            m_backCanvas.gameObject.SetActive(true);
            GameObject m_backCanvasSelectable = m_backCanvas.gameObject.GetComponentInChildren<Selectable>().gameObject;
            m_eventSystem.SetSelectedGameObject(m_backCanvasSelectable);
           
    }

    public void WrapperNextCanvas()
    {
        StartCoroutine(NextCanvasColorChange());
    }

    public void WrapperReturnCanvas()
    {
        StartCoroutine(ReturnCanvasColorChange());
    }

    public void changeColor()
    {
        //Duration of the lerp
        float _duration = 0.04f;
        float _lerp = Mathf.PingPong(Time.time, _duration) / _duration;
        float _lerpFadeOut = Mathf.PingPong(Time.time, _duration) / 0.5f;

        if (m_colourChange)
        {
            m_texts.color = Color32.Lerp(new Color32(255, 255, 255, 255), new Color32(50, 50, 50, 255), _lerp);
            m_tracos.color = Color32.Lerp(new Color32(255, 255, 255, 255), new Color32(50, 50, 50, 255), _lerp);

            if (Time.time > m_currentDelay)
            {
                m_texts.color = new Color32(255, 255, 255, 255);
                m_tracos.color = new Color32(255, 255, 255, 255);
                m_colourChange = false;
            }
        }

        if (m_fadeOut)
        {
            imageFadeOut.color = Color32.Lerp(new Color32(34, 32, 32, 255), new Color32(34, 32, 32, 0), _lerp);          

            if (Time.time > m_currentDelay)
            {
                m_colourChange = false;
            }
        }

    }
}
