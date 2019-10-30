using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ButtonsMainMenu : MonoBehaviour
{
    Canvas


        //tava tentando fazer uma list com o canvas
    [SerializeField]
    protected Canvas m_actualCanvas, m_nextCanvas, m_backCanvas;

    [SerializeField]
    protected Animator m_canvasAnimator;

    [SerializeField]
    public Text m_markers, m_text;

    protected bool m_colourChange, m_changeCanvas;

    protected float m_currentDelay, m_delayBetweenColorChange;

    protected EventSystem m_eventSystem;

    public Selectable[] selectables;
   
    private bool loadScene = false;


    private void OnEnable()
    {
        for (int i = 0; i < selectables.Length; i++)
        {
            selectables[i].enabled = true;
        }
    }


    public void startGame()
    {
        StartCoroutine("LoadNewScene");
    }

    void Start()
    {
        m_changeCanvas = false;
        m_eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        m_currentDelay = 0;
        m_delayBetweenColorChange = 0.7f;        
    }

    // Update is called once per frame
    void Update()
    {
        changeColor();
    }

    public IEnumerator NextCanvasColorChange()
    {   
        for (int i =0; i < selectables.Length; i++)
        {
            selectables[i].enabled = false;
        }
        m_colourChange = true;
        m_currentDelay = Time.time + m_delayBetweenColorChange;        
        yield return new WaitForSeconds(0.9f);
        m_canvasAnimator.SetBool("FADE_OUT", true);
        yield return new WaitForSeconds(1f);
        m_colourChange = false;
        m_canvasAnimator.SetBool("FADE_OUT", false);

        NextCanvas();
    }

    public IEnumerator ReturnCanvasColorChange()
    {
        for (int i = 0; i < selectables.Length; i++)
        {
            selectables[i].enabled = false;
        }
        m_canvasAnimator.SetBool("FADE_OUT", true);
        yield return new WaitForSeconds(1f);
        m_canvasAnimator.SetBool("FADE_OUT", false);

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
        if(m_backCanvas)
        {
            m_actualCanvas.gameObject.SetActive(false);
            m_backCanvas.gameObject.SetActive(true);
            GameObject m_backCanvasSelectable = m_backCanvas.gameObject.GetComponentInChildren<Selectable>().gameObject;
            m_eventSystem.SetSelectedGameObject(m_backCanvasSelectable);
        }
      
    }

    public void WrapperNextCanvas(string _actualCanvas, string _nextCanvas, string _previousCanvas)
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

        if (m_colourChange)
        {
            m_text.color = Color32.Lerp(new Color32(255, 255, 255, 255), new Color32(50, 50, 50, 255), _lerp);
            m_markers.color = Color32.Lerp(new Color32(255, 255, 255, 255), new Color32(50, 50, 50, 255), _lerp);

            if (Time.time > m_currentDelay)
            {
                m_text.color = new Color32(255, 255, 255, 255);
                m_markers.color = new Color32(255, 255, 255, 255);
                m_colourChange = false;
            }
        }

    }

   

}
