using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonsMainMenu : MonoBehaviour, ISelectHandler,
    IDeselectHandler
{
    [SerializeField]
    protected Canvas m_actualCanvas, m_nextCanvas, m_backCanvas;

    [SerializeField]
    protected Animator m_canvasAnimator;

    [SerializeField]
    public Text m_markers, m_text;

    protected bool m_colourChange, m_changeCanvas;

    protected float m_currentDelay, m_delayBetweenColorChange;

    [SerializeField]
    protected EventSystem m_eventSystem;
    [SerializeField]
    protected ManagerMenu m_managerMenu;
 
   
    private bool loadScene = false;


    void Start()
    {
        
        if(!m_eventSystem)
            m_eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        if (!m_managerMenu)
            m_managerMenu = FindObjectOfType<ManagerMenu>().GetComponent<ManagerMenu>();
        if(!m_canvasAnimator)
            m_canvasAnimator = GameObject.Find("CANVAS_MAIN").GetComponent<Animator>();

        m_changeCanvas = false;
        m_currentDelay = 0;
        m_delayBetweenColorChange = 0.7f;
    }

    private void OnEnable()
    {
        if (!m_eventSystem)
            m_eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        if (!m_managerMenu)
            m_managerMenu = FindObjectOfType<ManagerMenu>().GetComponent<ManagerMenu>();
        if (!m_canvasAnimator)
            m_canvasAnimator = GameObject.Find("CANVAS_MAIN").GetComponent<Animator>();

        m_changeCanvas = false;
        m_currentDelay = 0;
        m_delayBetweenColorChange = 0.7f;
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

    #region Select_or_deselect

    //Where the GameObject is selected
    public void OnSelect(BaseEventData eventData)
    {
        Debug.Log("button: " + gameObject.name + "Selected");
        m_markers.gameObject.SetActive(true);
    }

    //This triggers on event of deselecting the Selectable Object
    public void OnDeselect(BaseEventData eventData)
    {
        Debug.Log("button: " + gameObject.name + "Deselected");
        m_markers.gameObject.SetActive(false);
    }
    
    #endregion
    
    void Update()
    {
        changeColor();
    }


    #region Canvas_Changing

    private IEnumerator NextCanvas()
    {   
        

        //this bool is what tells 
        m_colourChange = true;
        m_currentDelay = Time.time + m_delayBetweenColorChange;        
        yield return new WaitForSeconds(0.9f);
        m_canvasAnimator.SetBool("FADE_OUT", true);
        yield return new WaitForSeconds(1f);
        m_colourChange = false;
        m_canvasAnimator.SetBool("FADE_OUT", false);


        m_actualCanvas.gameObject.SetActive(false);
        m_nextCanvas.gameObject.SetActive(true);
        GameObject m_nextCanvasSelectable = m_nextCanvas.gameObject.GetComponentInChildren<Selectable>().gameObject;
        m_eventSystem.SetSelectedGameObject(m_nextCanvasSelectable);
    }

    private IEnumerator ReturnCanvas()
    {
        for (int i = 0; i < m_managerMenu.selectables.Length; i++)
        {
            m_managerMenu.selectables[i].enabled = false;
        }

        m_canvasAnimator.SetBool("FADE_OUT", true);
        yield return new WaitForSeconds(1f);
        m_canvasAnimator.SetBool("FADE_OUT", false);

        if (m_backCanvas)
        {
            m_actualCanvas.gameObject.SetActive(false);
            m_backCanvas.gameObject.SetActive(true);

            for (int i = 0; i < m_managerMenu.selectables.Length; i++)
            {
                m_managerMenu.selectables[i].enabled = true;
            }

            GameObject m_backCanvasSelectable = m_backCanvas.gameObject.GetComponentInChildren<Selectable>().gameObject;
            m_eventSystem.SetSelectedGameObject(m_backCanvasSelectable);
        }

    }

    private IEnumerator StartGame()
    {
        yield return new WaitForSeconds(3);

        // Start an asynchronous operation to load the scene that was passed to the LoadNewScene coroutine.
        AsyncOperation async = SceneManager.LoadSceneAsync("Scene_demo");

        // While the asynchronous operation to load the new scene is not yet complete, continue waiting until it's done.
        while (!async.isDone)
        {
            yield return null;
        }
    }

    #endregion

    #region Wrappers
    public void WrapperNextCanvas()
    {
        StartCoroutine(NextCanvas());
    }

    public void WrapperReturnCanvas()
    {
        StartCoroutine(ReturnCanvas());
    }

    public void WrapperStartGame()
    {
        StartCoroutine(StartGame());
    }
    
    #endregion
    

   

}
