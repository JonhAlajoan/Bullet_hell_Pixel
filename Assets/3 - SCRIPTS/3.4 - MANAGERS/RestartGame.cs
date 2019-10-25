using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class RestartGame : MonoBehaviour, ISelectHandler,
    IDeselectHandler
{
    [SerializeField]
    protected Text m_tracos, m_texts;

    protected bool m_colourChange, m_changeCanvas;

    protected float m_currentDelay, m_delayBetweenColorChange;

    public EventSystem m_eventSystem;

    public GameObject m_defaultButton;

    GameObject m_nextCanvasSelectable;

    public GameObject canvas;
    private void OnEnable()
    {
       
    }

    void Start()
    {

        if(!m_eventSystem)
            m_eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();

        if (!m_nextCanvasSelectable)
        {
            GameObject m_nextCanvasSelectable = canvas.GetComponentInChildren<Selectable>().gameObject;
            m_eventSystem.SetSelectedGameObject(m_nextCanvasSelectable);
        }
        

        m_currentDelay = 0;
        m_delayBetweenColorChange = 0.7f;
    }

    // Update is called once per frame
    void Update()
    {
        changeColor();

        if (m_eventSystem.currentSelectedGameObject != null)
            m_defaultButton = m_eventSystem.currentSelectedGameObject.gameObject;

        if ((Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0 || Input.GetMouseButtonDown(0))
           && m_defaultButton != null
           && m_eventSystem.currentSelectedGameObject == null)
        {
            m_eventSystem.SetSelectedGameObject(m_defaultButton);
        }

    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Scene_menu");
    }

    public void changeColor()
    {
        //Duration of the lerp
        float _duration = 0.04f;
        float _lerp = Mathf.PingPong(Time.time, _duration) / _duration;

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

    }

    public void OnSelect(BaseEventData eventData)
    {
        m_tracos.gameObject.SetActive(true);
    }
    public void OnDeselect(BaseEventData eventData)
    {
        m_tracos.gameObject.SetActive(false);
    }

}
